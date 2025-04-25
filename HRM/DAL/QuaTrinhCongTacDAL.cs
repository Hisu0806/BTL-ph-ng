using System;
using System.Data;
using System.Data.SqlClient;
using HRM.DTO;

namespace HRM.DAL
{
    public class QuaTrinhCongTacDAL
    {
        /// <summary>
        /// Lấy danh sách quá trình công tác
        /// </summary>
        /// <returns>DataTable chứa danh sách quá trình công tác</returns>
        public static DataTable GetList()
        {
            string query = @"SELECT qt.*, nv.HoTen, cd.TenChucDanh, pb.TenPhong
                           FROM QuaTrinhCongTac qt
                           LEFT JOIN NhanVien nv ON qt.MaNV = nv.MaNV
                           LEFT JOIN ChucDanh cd ON nv.MaChucDanh = cd.MaChucDanh
                           LEFT JOIN PhongBan pb ON nv.MaPhong = pb.MaPhong
                           ORDER BY qt.TuNgay DESC";

            return DBConnection.ExecuteQuery(query);
        }

        /// <summary>
        /// Lấy danh sách quá trình công tác của một nhân viên
        /// </summary>
        /// <param name="maNV">Mã nhân viên</param>
        /// <returns>DataTable chứa danh sách quá trình công tác</returns>
        public static DataTable GetByMaNV(int maNV)
        {
            string query = @"SELECT qt.*, nv.HoTen, cd.TenChucDanh, pb.TenPhong
                           FROM QuaTrinhCongTac qt
                           LEFT JOIN NhanVien nv ON qt.MaNV = nv.MaNV
                           LEFT JOIN ChucDanh cd ON nv.MaChucDanh = cd.MaChucDanh
                           LEFT JOIN PhongBan pb ON nv.MaPhong = pb.MaPhong
                           WHERE qt.MaNV = @MaNV
                           ORDER BY qt.TuNgay DESC";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaNV", maNV)
            };

            return DBConnection.ExecuteQuery(query, parameters);
        }

        /// <summary>
        /// Lấy thông tin một quá trình công tác theo mã
        /// </summary>
        /// <param name="maQuaTrinh">Mã quá trình công tác</param>
        /// <returns>Đối tượng QuaTrinhCongTac</returns>
        public static QuaTrinhCongTac GetByID(int maQuaTrinh)
        {
            QuaTrinhCongTac qt = null;
            string query = @"SELECT qt.*, nv.HoTen, cd.TenChucDanh, pb.TenPhong
                           FROM QuaTrinhCongTac qt
                           LEFT JOIN NhanVien nv ON qt.MaNV = nv.MaNV
                           LEFT JOIN ChucDanh cd ON nv.MaChucDanh = cd.MaChucDanh
                           LEFT JOIN PhongBan pb ON nv.MaPhong = pb.MaPhong
                           WHERE qt.MaQuaTrinh = @MaQuaTrinh";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaQuaTrinh", maQuaTrinh)
            };

            DataTable dt = DBConnection.ExecuteQuery(query, parameters);

            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                qt = new QuaTrinhCongTac();
                qt.MaQuaTrinh = Convert.ToInt32(row["MaQuaTrinh"]);
                qt.MaNV = Convert.ToInt32(row["MaNV"]);
                qt.TuNgay = Convert.ToDateTime(row["TuNgay"]);
                qt.DenNgay = row["DenNgay"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(row["DenNgay"]);
                qt.ChucDanh = row["ChucDanh"].ToString();
                qt.PhongBan = row["PhongBan"].ToString();
                qt.MoTaCongViec = row["MoTaCongViec"].ToString();
                qt.GhiChu = row["GhiChu"].ToString();
            }

            return qt;
        }

        /// <summary>
        /// Thêm quá trình công tác mới
        /// </summary>
        /// <param name="qt">Đối tượng QuaTrinhCongTac</param>
        /// <returns>ID của quá trình công tác vừa thêm, -1 nếu thất bại</returns>
        public static int Add(QuaTrinhCongTac qt)
        {
            string query = @"INSERT INTO QuaTrinhCongTac (MaNV, TuNgay, DenNgay, ChucDanh, PhongBan, MoTaCongViec, GhiChu)
                           VALUES (@MaNV, @TuNgay, @DenNgay, @ChucDanh, @PhongBan, @MoTaCongViec, @GhiChu);
                           SELECT SCOPE_IDENTITY()";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaNV", qt.MaNV),
                new SqlParameter("@TuNgay", qt.TuNgay),
                new SqlParameter("@DenNgay", (object)qt.DenNgay ?? DBNull.Value),
                new SqlParameter("@ChucDanh", (object)qt.ChucDanh ?? DBNull.Value),
                new SqlParameter("@PhongBan", (object)qt.PhongBan ?? DBNull.Value),
                new SqlParameter("@MoTaCongViec", (object)qt.MoTaCongViec ?? DBNull.Value),
                new SqlParameter("@GhiChu", (object)qt.GhiChu ?? DBNull.Value)
            };

            object result = DBConnection.ExecuteScalar(query, parameters);
            return result != null ? Convert.ToInt32(result) : -1;
        }

        /// <summary>
        /// Cập nhật thông tin quá trình công tác
        /// </summary>
        /// <param name="qt">Đối tượng QuaTrinhCongTac</param>
        /// <returns>true nếu thành công, false nếu thất bại</returns>
        public static bool Update(QuaTrinhCongTac qt)
        {
            string query = @"UPDATE QuaTrinhCongTac
                           SET MaNV = @MaNV,
                               TuNgay = @TuNgay,
                               DenNgay = @DenNgay,
                               ChucDanh = @ChucDanh,
                               PhongBan = @PhongBan,
                               MoTaCongViec = @MoTaCongViec,
                               GhiChu = @GhiChu
                           WHERE MaQuaTrinh = @MaQuaTrinh";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaQuaTrinh", qt.MaQuaTrinh),
                new SqlParameter("@MaNV", qt.MaNV),
                new SqlParameter("@TuNgay", qt.TuNgay),
                new SqlParameter("@DenNgay", (object)qt.DenNgay ?? DBNull.Value),
                new SqlParameter("@ChucDanh", (object)qt.ChucDanh ?? DBNull.Value),
                new SqlParameter("@PhongBan", (object)qt.PhongBan ?? DBNull.Value),
                new SqlParameter("@MoTaCongViec", (object)qt.MoTaCongViec ?? DBNull.Value),
                new SqlParameter("@GhiChu", (object)qt.GhiChu ?? DBNull.Value)
            };

            int rowsAffected = DBConnection.ExecuteNonQuery(query, parameters);
            return rowsAffected > 0;
        }

        /// <summary>
        /// Xóa quá trình công tác
        /// </summary>
        /// <param name="maQuaTrinh">Mã quá trình công tác</param>
        /// <returns>true nếu thành công, false nếu thất bại</returns>
        public static bool Delete(int maQuaTrinh)
        {
            string query = "DELETE FROM QuaTrinhCongTac WHERE MaQuaTrinh = @MaQuaTrinh";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaQuaTrinh", maQuaTrinh)
            };

            int rowsAffected = DBConnection.ExecuteNonQuery(query, parameters);
            return rowsAffected > 0;
        }
    }
}