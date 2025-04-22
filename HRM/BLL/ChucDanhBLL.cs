using System;
using System.Data;
using HRM.DAL;
using HRM.DTO;

namespace HRM.BLL
{
    public class ChucDanhBLL
    {
        /// <summary>
        /// Lấy danh sách chức danh
        /// </summary>
        /// <returns>DataTable chứa danh sách chức danh</returns>
        public static DataTable GetList()
        {
            return ChucDanhDAL.GetList();
        }
        
        /// <summary>
        /// Lấy thông tin một chức danh theo mã
        /// </summary>
        /// <param name="maChucDanh">Mã chức danh</param>
        /// <returns>Đối tượng ChucDanh</returns>
        public static ChucDanh GetByID(int maChucDanh)
        {
            return ChucDanhDAL.GetByID(maChucDanh);
        }
        
        /// <summary>
        /// Thêm chức danh mới
        /// </summary>
        /// <param name="chucDanh">Đối tượng ChucDanh</param>
        /// <returns>ID của chức danh vừa thêm, -1 nếu thất bại</returns>
        public static int Add(ChucDanh chucDanh)
        {
            // Kiểm tra thông tin chức danh
            if (string.IsNullOrEmpty(chucDanh.TenChucDanh))
            {
                throw new Exception("Tên chức danh không được để trống");
            }
            
            return ChucDanhDAL.Add(chucDanh);
        }
        
        /// <summary>
        /// Cập nhật thông tin chức danh
        /// </summary>
        /// <param name="chucDanh">Đối tượng ChucDanh</param>
        /// <returns>true nếu thành công, false nếu thất bại</returns>
        public static bool Update(ChucDanh chucDanh)
        {
            // Kiểm tra thông tin chức danh
            if (string.IsNullOrEmpty(chucDanh.TenChucDanh))
            {
                throw new Exception("Tên chức danh không được để trống");
            }
            
            return ChucDanhDAL.Update(chucDanh);
        }
        
        /// <summary>
        /// Xóa chức danh
        /// </summary>
        /// <param name="maChucDanh">Mã chức danh</param>
        /// <returns>true nếu thành công, false nếu thất bại</returns>
        public static bool Delete(int maChucDanh)
        {
            // Kiểm tra xem chức danh có nhân viên không
            DataTable dt = NhanVienDAL.GetByChucDanh(maChucDanh);
            if (dt.Rows.Count > 0)
            {
                throw new Exception("Không thể xóa chức danh vì có nhân viên đang giữ chức danh này");
            }
            
            return ChucDanhDAL.Delete(maChucDanh);
        }
    }
} 