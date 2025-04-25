using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using HRM.DTO;

namespace HRM.DAL
{
    public class TangLuongDAL
    {
        private static readonly string connectionString = ConfigurationManager.ConnectionStrings["HRMConnectionString"].ConnectionString;

        public static DataTable GetNhanVienBienChe()
        {
            string query = @"SELECT DISTINCT nv.MaNV, nv.HoTen, hd.LuongTheoHopDong
                           FROM NhanVien nv
                           INNER JOIN HopDong hd ON nv.MaNV = hd.MaNV
                           WHERE hd.LoaiHopDong = N'Biên chế'
                           AND hd.TrangThai = N'Đang hiệu lực'
                           ORDER BY nv.HoTen";
            return DBConnection.ExecuteQuery(query);
        }

        public static DataTable GetList()
        {
            string query = @"SELECT tl.MaTangLuong, tl.MaNV, nv.HoTen, 
                           tl.MaBacLuongCu, bl1.TenBac AS TenBacCu, bl1.HeSo AS HeSoCu, bl1.LuongCoBan AS LuongCu,
                           tl.MaBacLuongMoi, bl2.TenBac AS TenBacMoi, bl2.HeSo AS HeSoMoi, bl2.LuongCoBan AS LuongMoi,
                           tl.NgayTangLuong, tl.LyDo, tl.GhiChu
                           FROM TangLuong tl
                           INNER JOIN NhanVien nv ON tl.MaNV = nv.MaNV
                           INNER JOIN BacLuong bl1 ON tl.MaBacLuongCu = bl1.MaBacLuong
                           INNER JOIN BacLuong bl2 ON tl.MaBacLuongMoi = bl2.MaBacLuong
                           ORDER BY tl.NgayTangLuong DESC";

            return DBConnection.ExecuteQuery(query);
        }

        public static TangLuongDTO GetByID(int maTangLuong)
        {
            TangLuongDTO tl = null;
            string query = @"SELECT tl.MaTangLuong, tl.MaNV, nv.HoTen, 
                           tl.MaBacLuongCu, bl1.TenBac AS TenBacCu, bl1.HeSo AS HeSoCu, bl1.LuongCoBan AS LuongCu,
                           tl.MaBacLuongMoi, bl2.TenBac AS TenBacMoi, bl2.HeSo AS HeSoMoi, bl2.LuongCoBan AS LuongMoi,
                           tl.NgayTangLuong, tl.LyDo, tl.GhiChu
                           FROM TangLuong tl
                           INNER JOIN NhanVien nv ON tl.MaNV = nv.MaNV
                           INNER JOIN BacLuong bl1 ON tl.MaBacLuongCu = bl1.MaBacLuong
                           INNER JOIN BacLuong bl2 ON tl.MaBacLuongMoi = bl2.MaBacLuong
                           WHERE tl.MaTangLuong = @MaTangLuong";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaTangLuong", maTangLuong)
            };

            DataTable dt = DBConnection.ExecuteQuery(query, parameters);

            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                tl = new TangLuongDTO
                {
                    MaTangLuong = Convert.ToInt32(row["MaTangLuong"]),
                    MaNV = Convert.ToInt32(row["MaNV"]),
                    HoTen = row["HoTen"].ToString(),
                    MaBacLuongCu = Convert.ToInt32(row["MaBacLuongCu"]),
                    TenBacCu = row["TenBacCu"].ToString(),
                    HeSoCu = Convert.ToDecimal(row["HeSoCu"]),
                    LuongCu = Convert.ToDecimal(row["LuongCu"]),
                    MaBacLuongMoi = Convert.ToInt32(row["MaBacLuongMoi"]),
                    TenBacMoi = row["TenBacMoi"].ToString(),
                    HeSoMoi = Convert.ToDecimal(row["HeSoMoi"]),
                    LuongMoi = Convert.ToDecimal(row["LuongMoi"]),
                    NgayTangLuong = Convert.ToDateTime(row["NgayTangLuong"]),
                    LyDo = row["LyDo"].ToString(),
                    GhiChu = row["GhiChu"]?.ToString()
                };
            }

            return tl;
        }

        /// <summary>
        /// Thêm thông tin tăng lương mới
        /// </summary>
        /// <param name="tangLuong">Đối tượng TangLuongDTO</param>
        /// <returns>ID của bản ghi vừa thêm, -1 nếu thất bại</returns>
        public static int Add(TangLuongDTO tangLuong)
        {
            string query = @"BEGIN TRANSACTION;
                           BEGIN TRY
                               INSERT INTO TangLuong (MaNV, MaBacLuongCu, MaBacLuongMoi, NgayTangLuong, LyDo, GhiChu)
                               VALUES (@MaNV, @MaBacLuongCu, @MaBacLuongMoi, @NgayTangLuong, @LyDo, @GhiChu);
                               
                               DECLARE @MaTangLuong int = SCOPE_IDENTITY();
                               
                               UPDATE HopDong 
                               SET LuongTheoHopDong = @MaBacLuongMoi
                               WHERE MaNV = @MaNV AND TrangThai = N'Đang hiệu lực';
                               
                               COMMIT;
                               SELECT @MaTangLuong;
                           END TRY
                           BEGIN CATCH
                               ROLLBACK;
                               SELECT -1;
                           END CATCH";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaNV", tangLuong.MaNV),
                new SqlParameter("@MaBacLuongCu", tangLuong.MaBacLuongCu),
                new SqlParameter("@MaBacLuongMoi", tangLuong.MaBacLuongMoi),
                new SqlParameter("@NgayTangLuong", tangLuong.NgayTangLuong),
                new SqlParameter("@LyDo", tangLuong.LyDo),
                new SqlParameter("@GhiChu", (object)tangLuong.GhiChu ?? DBNull.Value)
            };

            object result = DBConnection.ExecuteScalar(query, parameters);
            return result != null ? Convert.ToInt32(result) : -1;
        }

        /// <summary>
        /// Cập nhật thông tin tăng lương
        /// </summary>
        /// <param name="tangLuong">Đối tượng TangLuongDTO</param>
        /// <returns>true nếu thành công, false nếu thất bại</returns>
        public static bool Update(TangLuongDTO tangLuong)
        {
            string query = @"BEGIN TRANSACTION;
                           BEGIN TRY
                               UPDATE TangLuong 
                               SET MaNV = @MaNV,
                                   MaBacLuongCu = @MaBacLuongCu,
                                   MaBacLuongMoi = @MaBacLuongMoi,
                                   NgayTangLuong = @NgayTangLuong,
                                   LyDo = @LyDo,
                                   GhiChu = @GhiChu
                               WHERE MaTangLuong = @MaTangLuong;
                               
                               UPDATE HopDong 
                               SET LuongTheoHopDong = @MaBacLuongMoi
                               WHERE MaNV = @MaNV AND TrangThai = N'Đang hiệu lực';
                               
                               COMMIT;
                               SELECT 1;
                           END TRY
                           BEGIN CATCH
                               ROLLBACK;
                               SELECT 0;
                           END CATCH";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaTangLuong", tangLuong.MaTangLuong),
                new SqlParameter("@MaNV", tangLuong.MaNV),
                new SqlParameter("@MaBacLuongCu", tangLuong.MaBacLuongCu),
                new SqlParameter("@MaBacLuongMoi", tangLuong.MaBacLuongMoi),
                new SqlParameter("@NgayTangLuong", tangLuong.NgayTangLuong),
                new SqlParameter("@LyDo", tangLuong.LyDo),
                new SqlParameter("@GhiChu", (object)tangLuong.GhiChu ?? DBNull.Value)
            };

            object result = DBConnection.ExecuteScalar(query, parameters);
            return result != null && Convert.ToInt32(result) > 0;
        }

        /// <summary>
        /// Xóa thông tin tăng lương
        /// </summary>
        /// <param name="maTangLuong">Mã tăng lương</param>
        /// <returns>true nếu thành công, false nếu thất bại</returns>
        public static bool Delete(int maTangLuong)
        {
            string query = @"BEGIN TRANSACTION;
                           BEGIN TRY
                               DECLARE @MaNV int, @MaBacLuongCu int;
                               
                               SELECT @MaNV = MaNV, @MaBacLuongCu = MaBacLuongCu
                               FROM TangLuong
                               WHERE MaTangLuong = @MaTangLuong;
                               
                               DELETE FROM TangLuong 
                               WHERE MaTangLuong = @MaTangLuong;
                               
                               UPDATE HopDong 
                               SET LuongTheoHopDong = @MaBacLuongCu
                               WHERE MaNV = @MaNV AND TrangThai = N'Đang hiệu lực';
                               
                               COMMIT;
                               SELECT 1;
                           END TRY
                           BEGIN CATCH
                               ROLLBACK;
                               SELECT 0;
                           END CATCH";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaTangLuong", maTangLuong)
            };

            object result = DBConnection.ExecuteScalar(query, parameters);
            return result != null && Convert.ToInt32(result) > 0;
        }

        /// <summary>
        /// Lấy bậc lương hiện tại của nhân viên
        /// </summary>
        /// <param name="maNV">Mã nhân viên</param>
        /// <returns>Mã bậc lương hiện tại</returns>
        public static int GetBacLuongHienTai(int maNV)
        {
            string query = @"SELECT bl.MaBacLuong, bl.TenBac, bl.HeSo, bl.LuongCoBan,
                           hd.TrangThai, hd.NgayKetThuc, hd.LuongTheoHopDong
                   FROM HopDong hd
                   INNER JOIN BacLuong bl ON hd.LuongTheoHopDong = bl.MaBacLuong
                   WHERE hd.MaNV = @MaNV 
                   AND hd.TrangThai = N'Đang hiệu lực'
                   AND (hd.NgayKetThuc IS NULL OR hd.NgayKetThuc > GETDATE())";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaNV", maNV)
            };

            DataTable dt = DBConnection.ExecuteQuery(query, parameters);
            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                Console.WriteLine($"Found contract: Status={row["TrangThai"]}, EndDate={row["NgayKetThuc"]}, Salary={row["LuongTheoHopDong"]}");
                return Convert.ToInt32(row["MaBacLuong"]);
            }
            else
            {
                Console.WriteLine($"No active contract found for employee ID: {maNV}");
                return 0;
            }
        }
    }
}