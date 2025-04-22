using System;
using System.Data;
using HRM.DAL;
using HRM.DTO;

namespace HRM.BLL
{
    public class PhongBanBLL
    {
        /// <summary>
        /// Lấy danh sách phòng ban
        /// </summary>
        /// <returns>DataTable chứa danh sách phòng ban</returns>
        public static DataTable GetList()
        {
            return PhongBanDAL.GetList();
        }
        
        /// <summary>
        /// Lấy thông tin một phòng ban theo mã
        /// </summary>
        /// <param name="maPhong">Mã phòng ban</param>
        /// <returns>Đối tượng PhongBan</returns>
        public static PhongBan GetByID(int maPhong)
        {
            return PhongBanDAL.GetByID(maPhong);
        }
        
        /// <summary>
        /// Thêm phòng ban mới
        /// </summary>
        /// <param name="phongBan">Đối tượng PhongBan</param>
        /// <returns>ID của phòng ban vừa thêm, -1 nếu thất bại</returns>
        public static int Add(PhongBan phongBan)
        {
            // Kiểm tra thông tin phòng ban
            if (string.IsNullOrEmpty(phongBan.TenPhong))
            {
                throw new Exception("Tên phòng ban không được để trống");
            }
            
            return PhongBanDAL.Add(phongBan);
        }
        
        /// <summary>
        /// Cập nhật thông tin phòng ban
        /// </summary>
        /// <param name="phongBan">Đối tượng PhongBan</param>
        /// <returns>true nếu thành công, false nếu thất bại</returns>
        public static bool Update(PhongBan phongBan)
        {
            // Kiểm tra thông tin phòng ban
            if (string.IsNullOrEmpty(phongBan.TenPhong))
            {
                throw new Exception("Tên phòng ban không được để trống");
            }
            
            return PhongBanDAL.Update(phongBan);
        }
        
        /// <summary>
        /// Xóa phòng ban
        /// </summary>
        /// <param name="maPhong">Mã phòng ban</param>
        /// <returns>true nếu thành công, false nếu thất bại</returns>
        public static bool Delete(int maPhong)
        {
            // Kiểm tra xem phòng ban có nhân viên không
            DataTable dt = NhanVienDAL.GetByPhongBan(maPhong);
            if (dt.Rows.Count > 0)
            {
                throw new Exception("Không thể xóa phòng ban vì có nhân viên đang thuộc phòng ban này");
            }
            
            return PhongBanDAL.Delete(maPhong);
        }
    }
} 