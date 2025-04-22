using System;
using System.Data;
using System.Data.SqlClient;
using HRM.DAL;
using HRM.DTO;
using HRM.Utility;

namespace HRM.BLL
{
    public class NguoiDungBLL
    {
        /// <summary>
        /// Kiểm tra đăng nhập
        /// </summary>
        /// <param name="tenDangNhap">Tên đăng nhập</param>
        /// <param name="matKhau">Mật khẩu</param>
        /// <returns>Đối tượng NguoiDung nếu đăng nhập thành công, null nếu thất bại</returns>
        public static NguoiDung DangNhap(string tenDangNhap, string matKhau)
        {
            return NguoiDungDAL.DangNhap(tenDangNhap, matKhau);
        }
        
        /// <summary>
        /// Đổi mật khẩu
        /// </summary>
        /// <param name="maNguoiDung">Mã người dùng</param>
        /// <param name="matKhauCu">Mật khẩu cũ</param>
        /// <param name="matKhauMoi">Mật khẩu mới</param>
        /// <returns>true nếu thành công, false nếu thất bại</returns>
        public static bool DoiMatKhau(string maNguoiDung, string matKhauCu, string matKhauMoi)
        {
            return NguoiDungDAL.DoiMatKhau(maNguoiDung, matKhauCu, matKhauMoi);
        }
        
        /// <summary>
        /// Lấy danh sách người dùng
        /// </summary>
        /// <returns>Danh sách người dùng</returns>
        public static DataTable GetList()
        {
            return NguoiDungDAL.GetList();
        }
        
        /// <summary>
        /// Thêm người dùng mới
        /// </summary>
        /// <param name="nguoiDung">Đối tượng NguoiDung</param>
        /// <returns>true nếu thành công, false nếu thất bại</returns>
        public static bool Add(NguoiDung nguoiDung)
        {
            return NguoiDungDAL.Add(nguoiDung);
        }
        
        /// <summary>
        /// Cập nhật thông tin người dùng
        /// </summary>
        /// <param name="nguoiDung">Đối tượng NguoiDung</param>
        /// <returns>true nếu thành công, false nếu thất bại</returns>
        public static bool Update(NguoiDung nguoiDung)
        {
            return NguoiDungDAL.Update(nguoiDung);
        }
        
        /// <summary>
        /// Xóa người dùng
        /// </summary>
        /// <param name="maNguoiDung">Mã người dùng</param>
        /// <returns>true nếu thành công, false nếu thất bại</returns>
        public static bool Delete(string maNguoiDung)
        {
            return NguoiDungDAL.Delete(maNguoiDung);
        }
    }
} 