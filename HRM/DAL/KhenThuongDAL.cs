using System;
using System.Data;
using System.Data.SqlClient;
using HRM.DTO;

namespace HRM.DAL
{
    public class KhenThuongDAL
    {
        /// <summary>
        /// Lấy danh sách khen thưởng
        /// </summary>
        /// <returns>DataTable chứa danh sách khen thưởng</returns>
        public static DataTable GetList()
        {
            string query = @"SELECT kt.*, nv.HoTen 
                           FROM KhenThuong kt 
                           LEFT JOIN NhanVien nv ON kt.MaNV = nv.MaNV 
                           ORDER BY kt.NgayKhenThuong DESC";
            
            return DBConnection.ExecuteQuery(query);
        }

        /// <summary>
        /// Lấy danh sách khen thưởng của một nhân viên
        /// </summary>
        /// <param name="maNV">Mã nhân viên</param>
        /// <returns>DataTable chứa danh sách khen thưởng</returns>
        public static DataTable GetByMaNV(int maNV)
        {
            string query = @"SELECT kt.*, nv.HoTen 
                           FROM KhenThuong kt 
                           LEFT JOIN NhanVien nv ON kt.MaNV = nv.MaNV 
                           WHERE kt.MaNV = @MaNV 
                           ORDER BY kt.NgayKhenThuong DESC";
            
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaNV", maNV)
            };
            
            return DBConnection.ExecuteQuery(query, parameters);
        }

        /// <summary>
        /// Lấy thông tin một khen thưởng theo mã
        /// </summary>
        /// <param name="maKhenThuong">Mã khen thưởng</param>
        /// <returns>Đối tượng KhenThuong</returns>
        public static KhenThuong GetByID(int maKhenThuong)
        {
            string query = @"SELECT kt.*, nv.HoTen 
                           FROM KhenThuong kt 
                           LEFT JOIN NhanVien nv ON kt.MaNV = nv.MaNV 
                           WHERE kt.MaKhenThuong = @MaKhenThuong";
            
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaKhenThuong", maKhenThuong)
            };
            
            DataTable dt = DBConnection.ExecuteQuery(query, parameters);
            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                return new KhenThuong
                {
                    MaKhenThuong = Convert.ToInt32(row["MaKhenThuong"]),
                    MaNV = Convert.ToInt32(row["MaNV"]),
                    NgayKhenThuong = Convert.ToDateTime(row["NgayKhenThuong"]),
                    HinhThuc = row["HinhThuc"].ToString(),
                    LyDo = row["LyDo"].ToString(),
                    SoTien = row["SoTien"] == DBNull.Value ? null : (decimal?)Convert.ToDecimal(row["SoTien"]),
                    GhiChu = row["GhiChu"] == DBNull.Value ? null : row["GhiChu"].ToString()
                };
            }
            return null;
        }

        /// <summary>
        /// Thêm khen thưởng mới
        /// </summary>
        /// <param name="kt">Đối tượng KhenThuong</param>
        /// <returns>ID của khen thưởng vừa thêm, -1 nếu thất bại</returns>
        public static int Add(KhenThuong kt)
        {
            string query = @"INSERT INTO KhenThuong(MaNV, NgayKhenThuong, HinhThuc, LyDo, SoTien, GhiChu) 
                           VALUES(@MaNV, @NgayKhenThuong, @HinhThuc, @LyDo, @SoTien, @GhiChu);
                           SELECT SCOPE_IDENTITY();";
            
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaNV", kt.MaNV),
                new SqlParameter("@NgayKhenThuong", kt.NgayKhenThuong),
                new SqlParameter("@HinhThuc", kt.HinhThuc),
                new SqlParameter("@LyDo", kt.LyDo),
                new SqlParameter("@SoTien", (object)kt.SoTien ?? DBNull.Value),
                new SqlParameter("@GhiChu", (object)kt.GhiChu ?? DBNull.Value)
            };
            
            object result = DBConnection.ExecuteScalar(query, parameters);
            return result == DBNull.Value ? -1 : Convert.ToInt32(result);
        }

        /// <summary>
        /// Cập nhật thông tin khen thưởng
        /// </summary>
        /// <param name="kt">Đối tượng KhenThuong</param>
        /// <returns>true nếu thành công, false nếu thất bại</returns>
        public static bool Update(KhenThuong kt)
        {
            string query = @"UPDATE KhenThuong 
                           SET MaNV = @MaNV, 
                               NgayKhenThuong = @NgayKhenThuong, 
                               HinhThuc = @HinhThuc, 
                               LyDo = @LyDo, 
                               SoTien = @SoTien, 
                               GhiChu = @GhiChu 
                           WHERE MaKhenThuong = @MaKhenThuong";
            
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaKhenThuong", kt.MaKhenThuong),
                new SqlParameter("@MaNV", kt.MaNV),
                new SqlParameter("@NgayKhenThuong", kt.NgayKhenThuong),
                new SqlParameter("@HinhThuc", kt.HinhThuc),
                new SqlParameter("@LyDo", kt.LyDo),
                new SqlParameter("@SoTien", (object)kt.SoTien ?? DBNull.Value),
                new SqlParameter("@GhiChu", (object)kt.GhiChu ?? DBNull.Value)
            };
            
            return DBConnection.ExecuteNonQuery(query, parameters) > 0;
        }

        /// <summary>
        /// Xóa khen thưởng
        /// </summary>
        /// <param name="maKhenThuong">Mã khen thưởng</param>
        /// <returns>true nếu thành công, false nếu thất bại</returns>
        public static bool Delete(int maKhenThuong)
        {
            string query = "DELETE FROM KhenThuong WHERE MaKhenThuong = @MaKhenThuong";
            
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaKhenThuong", maKhenThuong)
            };
            
            return DBConnection.ExecuteNonQuery(query, parameters) > 0;
        }
    }
}