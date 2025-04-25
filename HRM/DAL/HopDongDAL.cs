using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using HRM.DTO;

namespace HRM.DAL
{
    public class HopDongDAL
    {
        private static readonly string connectionString = ConfigurationManager.ConnectionStrings["HRMConnectionString"].ConnectionString;
        public static string GetLoaiHopDongByMaNV(int maNV)
        {
            string query = @"SELECT LoaiHopDong FROM HopDong WHERE MaNV = @MaNV AND NgayKetThuc > GETDATE()";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaNV", maNV);

                conn.Open();
                object result = cmd.ExecuteScalar();
                return result?.ToString();
            }
        }

        public static int GetMaBacLuongByMaNV(int maNV)
        {
            string query = @"SELECT MaBacLuong FROM HopDong WHERE MaNV = @MaNV AND NgayKetThuc > GETDATE()";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaNV", maNV);

                conn.Open();
                object result = cmd.ExecuteScalar();
                return result != null ? Convert.ToInt32(result) : 0;
            }
        }
        /// <summary>
        /// Lấy danh sách hợp đồng
        /// </summary>
        /// <returns>DataTable chứa danh sách hợp đồng</returns>
        public static DataTable GetList()
        {
            string query = @"SELECT hd.*, nv.HoTen as TenNV 
                           FROM HopDong hd 
                           LEFT JOIN NhanVien nv ON hd.MaNV = nv.MaNV 
                           ORDER BY hd.MaHopDong DESC";

            return DBConnection.ExecuteQuery(query);
        }

        /// <summary>
        /// Lấy danh sách hợp đồng của một nhân viên
        /// </summary>
        /// <param name="maNV">Mã nhân viên</param>
        /// <returns>DataTable chứa danh sách hợp đồng</returns>
        public static DataTable GetByMaNV(int maNV)
        {
            string query = @"SELECT hd.*, nv.HoTen as TenNV 
                           FROM HopDong hd 
                           LEFT JOIN NhanVien nv ON hd.MaNV = nv.MaNV 
                           WHERE hd.MaNV = @MaNV 
                           ORDER BY hd.MaHopDong DESC";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaNV", maNV)
            };

            return DBConnection.ExecuteQuery(query, parameters);
        }

        /// <summary>
        /// Lấy thông tin một hợp đồng theo mã
        /// </summary>
        /// <param name="maHopDong">Mã hợp đồng</param>
        /// <returns>Đối tượng HopDong</returns>
        public static HopDongDTO GetByID(int maHopDong)
        {
            HopDongDTO hd = null;
            string query = @"SELECT hd.*, nv.HoTen as TenNV 
                           FROM HopDong hd 
                           LEFT JOIN NhanVien nv ON hd.MaNV = nv.MaNV 
                           WHERE hd.MaHopDong = @MaHopDong";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaHopDong", maHopDong)
            };

            DataTable dt = DBConnection.ExecuteQuery(query, parameters);

            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                hd = new HopDongDTO();
                hd.MaHopDong = Convert.ToInt32(row["MaHopDong"]);
                hd.MaNV = Convert.ToInt32(row["MaNV"]);
                hd.SoHopDong = row["SoHopDong"].ToString();
                hd.LoaiHopDong = row["LoaiHopDong"].ToString();
                hd.NgayKy = Convert.ToDateTime(row["NgayKy"]);
                hd.NgayBatDau = Convert.ToDateTime(row["NgayBatDau"]);
                hd.NgayKetThuc = row["NgayKetThuc"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(row["NgayKetThuc"]);
                hd.LuongTheoHopDong = Convert.ToDecimal(row["LuongTheoHopDong"]);
                hd.TrangThai = row["TrangThai"].ToString();
                hd.NoiDung = row["NoiDung"]?.ToString();
            }

            return hd;
        }

        /// <summary>
        /// Thêm hợp đồng mới
        /// </summary>
        /// <param name="hd">Đối tượng HopDong</param>
        /// <returns>ID của hợp đồng vừa thêm, -1 nếu thất bại</returns>
        public static int Add(HopDongDTO hd)
        {
            string query = @"INSERT INTO HopDong(MaNV, SoHopDong, LoaiHopDong, NgayKy, NgayBatDau, NgayKetThuc, LuongTheoHopDong, TrangThai, NoiDung) 
                           VALUES(@MaNV, @SoHopDong, @LoaiHopDong, @NgayKy, @NgayBatDau, @NgayKetThuc, @LuongTheoHopDong, @TrangThai, @NoiDung);
                           SELECT SCOPE_IDENTITY()";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaNV", hd.MaNV),
                new SqlParameter("@SoHopDong", hd.SoHopDong),
                new SqlParameter("@LoaiHopDong", hd.LoaiHopDong),
                new SqlParameter("@NgayKy", hd.NgayKy),
                new SqlParameter("@NgayBatDau", hd.NgayBatDau),
                new SqlParameter("@NgayKetThuc", (object)hd.NgayKetThuc ?? DBNull.Value),
                new SqlParameter("@LuongTheoHopDong", hd.LuongTheoHopDong),
                new SqlParameter("@TrangThai", hd.TrangThai),
                new SqlParameter("@NoiDung", (object)hd.NoiDung ?? DBNull.Value)
            };

            object result = DBConnection.ExecuteScalar(query, parameters);
            return result != null ? Convert.ToInt32(result) : -1;
        }

        /// <summary>
        /// Cập nhật thông tin hợp đồng
        /// </summary>
        /// <param name="hd">Đối tượng HopDong</param>
        /// <returns>true nếu thành công, false nếu thất bại</returns>
        public static bool Update(HopDongDTO hd)
        {
            string query = @"UPDATE HopDong 
                           SET MaNV = @MaNV, 
                               SoHopDong = @SoHopDong, 
                               LoaiHopDong = @LoaiHopDong, 
                               NgayKy = @NgayKy, 
                               NgayBatDau = @NgayBatDau, 
                               NgayKetThuc = @NgayKetThuc, 
                               LuongTheoHopDong = @LuongTheoHopDong, 
                               TrangThai = @TrangThai, 
                               NoiDung = @NoiDung 
                           WHERE MaHopDong = @MaHopDong";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaHopDong", hd.MaHopDong),
                new SqlParameter("@MaNV", hd.MaNV),
                new SqlParameter("@SoHopDong", hd.SoHopDong),
                new SqlParameter("@LoaiHopDong", hd.LoaiHopDong),
                new SqlParameter("@NgayKy", hd.NgayKy),
                new SqlParameter("@NgayBatDau", hd.NgayBatDau),
                new SqlParameter("@NgayKetThuc", (object)hd.NgayKetThuc ?? DBNull.Value),
                new SqlParameter("@LuongTheoHopDong", hd.LuongTheoHopDong),
                new SqlParameter("@TrangThai", hd.TrangThai),
                new SqlParameter("@NoiDung", (object)hd.NoiDung ?? DBNull.Value)
            };

            int rowsAffected = DBConnection.ExecuteNonQuery(query, parameters);
            return rowsAffected > 0;
        }

        /// <summary>
        /// Xóa hợp đồng
        /// </summary>
        /// <param name="maHopDong">Mã hợp đồng</param>
        /// <returns>true nếu thành công, false nếu thất bại</returns>
        public static bool Delete(int maHopDong)
        {
            string query = "DELETE FROM HopDong WHERE MaHopDong = @MaHopDong";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaHopDong", maHopDong)
            };

            int rowsAffected = DBConnection.ExecuteNonQuery(query, parameters);
            return rowsAffected > 0;
        }

        /// <summary>
        /// Lấy loại nhân viên theo mã nhân viên
        /// </summary>
        /// <param name="maNV">Mã nhân viên</param>
        /// <returns>Loại nhân viên</returns>
        public static string GetLoaiNhanVien(int maNV)
        {
            string query = "SELECT LoaiNhanVien FROM NhanVien WHERE MaNV = @MaNV";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaNV", maNV)
            };

            object result = DBConnection.ExecuteScalar(query, parameters);
            return result?.ToString();
        }
    }
}