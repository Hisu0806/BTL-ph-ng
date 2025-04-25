using System;
using System.Data;
using System.Data.SqlClient;
using HRM.DTO;

namespace HRM.DAL
{
    public class KyLuatDAL
    {
        /// <summary>
        /// Lấy danh sách kỷ luật
        /// </summary>
        /// <returns>DataTable chứa danh sách kỷ luật</returns>
        public static DataTable GetList()
        {
            string query = @"SELECT kl.*, nv.HoTen 
                           FROM KyLuat kl 
                           LEFT JOIN NhanVien nv ON kl.MaNV = nv.MaNV 
                           ORDER BY kl.NgayKyLuat DESC";

            return DBConnection.ExecuteQuery(query);
        }

        /// <summary>
        /// Lấy danh sách kỷ luật của một nhân viên
        /// </summary>
        /// <param name="maNV">Mã nhân viên</param>
        /// <returns>DataTable chứa danh sách kỷ luật</returns>
        public static DataTable GetByMaNV(int maNV)
        {
            string query = @"SELECT kl.*, nv.HoTen 
                           FROM KyLuat kl 
                           LEFT JOIN NhanVien nv ON kl.MaNV = nv.MaNV 
                           WHERE kl.MaNV = @MaNV 
                           ORDER BY kl.NgayKyLuat DESC";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaNV", maNV)
            };

            return DBConnection.ExecuteQuery(query, parameters);
        }

        /// <summary>
        /// Lấy thông tin một kỷ luật theo mã
        /// </summary>
        /// <param name="maKyLuat">Mã kỷ luật</param>
        /// <returns>Đối tượng KyLuat</returns>
        public static KyLuat GetByID(int maKyLuat)
        {
            string query = @"SELECT kl.*, nv.HoTen 
                           FROM KyLuat kl 
                           LEFT JOIN NhanVien nv ON kl.MaNV = nv.MaNV 
                           WHERE kl.MaKyLuat = @MaKyLuat";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaKyLuat", maKyLuat)
            };

            DataTable dt = DBConnection.ExecuteQuery(query, parameters);
            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                return new KyLuat
                {
                    MaKyLuat = Convert.ToInt32(row["MaKyLuat"]),
                    MaNV = Convert.ToInt32(row["MaNV"]),
                    NgayKyLuat = Convert.ToDateTime(row["NgayKyLuat"]),
                    HinhThuc = row["HinhThuc"].ToString(),
                    LyDo = row["LyDo"].ToString(),
                    SoTien = row["SoTien"] == DBNull.Value ? null : (decimal?)Convert.ToDecimal(row["SoTien"])
                };
            }
            return null;
        }

        /// <summary>
        /// Thêm kỷ luật mới
        /// </summary>
        /// <param name="kl">Đối tượng KyLuat</param>
        /// <returns>ID của kỷ luật vừa thêm, -1 nếu thất bại</returns>
        public static int Add(KyLuat kl)
        {
            string query = @"INSERT INTO KyLuat(MaNV, NgayKyLuat, HinhThuc, LyDo, SoTien) 
                           VALUES(@MaNV, @NgayKyLuat, @HinhThuc, @LyDo, @SoTien);
                           SELECT SCOPE_IDENTITY();";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaNV", kl.MaNV),
                new SqlParameter("@NgayKyLuat", kl.NgayKyLuat),
                new SqlParameter("@HinhThuc", kl.HinhThuc),
                new SqlParameter("@LyDo", kl.LyDo),
                new SqlParameter("@SoTien", (object)kl.SoTien ?? DBNull.Value)
            };

            object result = DBConnection.ExecuteScalar(query, parameters);
            return result == DBNull.Value ? -1 : Convert.ToInt32(result);
        }

        /// <summary>
        /// Cập nhật thông tin kỷ luật
        /// </summary>
        /// <param name="kl">Đối tượng KyLuat</param>
        /// <returns>true nếu thành công, false nếu thất bại</returns>
        public static bool Update(KyLuat kl)
        {
            string query = @"UPDATE KyLuat 
                           SET MaNV = @MaNV, 
                               NgayKyLuat = @NgayKyLuat, 
                               HinhThuc = @HinhThuc, 
                               LyDo = @LyDo, 
                               SoTien = @SoTien 
                           WHERE MaKyLuat = @MaKyLuat";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaKyLuat", kl.MaKyLuat),
                new SqlParameter("@MaNV", kl.MaNV),
                new SqlParameter("@NgayKyLuat", kl.NgayKyLuat),
                new SqlParameter("@HinhThuc", kl.HinhThuc),
                new SqlParameter("@LyDo", kl.LyDo),
                new SqlParameter("@SoTien", (object)kl.SoTien ?? DBNull.Value)
            };

            return DBConnection.ExecuteNonQuery(query, parameters) > 0;
        }

        /// <summary>
        /// Xóa kỷ luật
        /// </summary>
        /// <param name="maKyLuat">Mã kỷ luật</param>
        /// <returns>true nếu thành công, false nếu thất bại</returns>
        public static bool Delete(int maKyLuat)
        {
            string query = "DELETE FROM KyLuat WHERE MaKyLuat = @MaKyLuat";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaKyLuat", maKyLuat)
            };

            return DBConnection.ExecuteNonQuery(query, parameters) > 0;
        }
    }
}