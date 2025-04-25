using System;
using System.Data;
using HRM.DAL;
using HRM.DTO;

namespace HRM.BLL
{
    public class QuaTrinhCongTacBLL
    {
        /// <summary>
        /// Lấy danh sách quá trình công tác
        /// </summary>
        /// <returns>DataTable chứa danh sách quá trình công tác</returns>
        public static DataTable GetList()
        {
            return QuaTrinhCongTacDAL.GetList();
        }

        /// <summary>
        /// Lấy danh sách quá trình công tác của một nhân viên
        /// </summary>
        /// <param name="maNV">Mã nhân viên</param>
        /// <returns>DataTable chứa danh sách quá trình công tác</returns>
        public static DataTable GetByMaNV(int maNV)
        {
            return QuaTrinhCongTacDAL.GetByMaNV(maNV);
        }

        /// <summary>
        /// Lấy thông tin một quá trình công tác theo mã
        /// </summary>
        /// <param name="maQuaTrinh">Mã quá trình công tác</param>
        /// <returns>Đối tượng QuaTrinhCongTac</returns>
        public static QuaTrinhCongTac GetByID(int maQuaTrinh)
        {
            return QuaTrinhCongTacDAL.GetByID(maQuaTrinh);
        }

        /// <summary>
        /// Thêm quá trình công tác mới
        /// </summary>
        /// <param name="qt">Đối tượng QuaTrinhCongTac</param>
        /// <returns>ID của quá trình công tác vừa thêm, -1 nếu thất bại</returns>
        public static int Add(QuaTrinhCongTac qt)
        {
            // Kiểm tra dữ liệu đầu vào
            if (qt.MaNV <= 0)
                throw new Exception("Mã nhân viên không hợp lệ");

            if (qt.TuNgay == DateTime.MinValue)
                throw new Exception("Vui lòng chọn ngày bắt đầu");

            if (qt.DenNgay.HasValue && qt.DenNgay.Value < qt.TuNgay)
                throw new Exception("Ngày kết thúc không thể nhỏ hơn ngày bắt đầu");

            if (string.IsNullOrWhiteSpace(qt.ChucDanh))
                throw new Exception("Vui lòng nhập chức danh");

            if (string.IsNullOrWhiteSpace(qt.PhongBan))
                throw new Exception("Vui lòng nhập phòng ban");

            return QuaTrinhCongTacDAL.Add(qt);
        }

        /// <summary>
        /// Cập nhật thông tin quá trình công tác
        /// </summary>
        /// <param name="qt">Đối tượng QuaTrinhCongTac</param>
        /// <returns>true nếu thành công, false nếu thất bại</returns>
        public static bool Update(QuaTrinhCongTac qt)
        {
            // Kiểm tra dữ liệu đầu vào
            if (qt.MaQuaTrinh <= 0)
                throw new Exception("Mã quá trình công tác không hợp lệ");

            if (qt.MaNV <= 0)
                throw new Exception("Mã nhân viên không hợp lệ");

            if (qt.TuNgay == DateTime.MinValue)
                throw new Exception("Vui lòng chọn ngày bắt đầu");

            if (qt.DenNgay.HasValue && qt.DenNgay.Value < qt.TuNgay)
                throw new Exception("Ngày kết thúc không thể nhỏ hơn ngày bắt đầu");

            if (string.IsNullOrWhiteSpace(qt.ChucDanh))
                throw new Exception("Vui lòng nhập chức danh");

            if (string.IsNullOrWhiteSpace(qt.PhongBan))
                throw new Exception("Vui lòng nhập phòng ban");

            return QuaTrinhCongTacDAL.Update(qt);
        }

        /// <summary>
        /// Xóa quá trình công tác
        /// </summary>
        /// <param name="maQuaTrinh">Mã quá trình công tác</param>
        /// <returns>true nếu thành công, false nếu thất bại</returns>
        public static bool Delete(int maQuaTrinh)
        {
            if (maQuaTrinh <= 0)
                throw new Exception("Mã quá trình công tác không hợp lệ");

            return QuaTrinhCongTacDAL.Delete(maQuaTrinh);
        }
    }
}