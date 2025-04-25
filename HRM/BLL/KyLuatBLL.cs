using System;
using System.Data;
using HRM.DAL;
using HRM.DTO;

namespace HRM.BLL
{
    public class KyLuatBLL
    {
        /// <summary>
        /// Lấy danh sách kỷ luật
        /// </summary>
        /// <returns>DataTable chứa danh sách kỷ luật</returns>
        public static DataTable GetList()
        {
            try
            {
                return KyLuatDAL.GetList();
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi lấy danh sách kỷ luật: " + ex.Message);
            }
        }

        /// <summary>
        /// Lấy danh sách kỷ luật của một nhân viên
        /// </summary>
        /// <param name="maNV">Mã nhân viên</param>
        /// <returns>DataTable chứa danh sách kỷ luật</returns>
        public static DataTable GetByMaNV(int maNV)
        {
            try
            {
                return KyLuatDAL.GetByMaNV(maNV);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi lấy danh sách kỷ luật của nhân viên: " + ex.Message);
            }
        }

        /// <summary>
        /// Lấy thông tin một kỷ luật theo mã
        /// </summary>
        /// <param name="maKyLuat">Mã kỷ luật</param>
        /// <returns>Đối tượng KyLuat</returns>
        public static KyLuat GetByID(int maKyLuat)
        {
            try
            {
                return KyLuatDAL.GetByID(maKyLuat);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi lấy thông tin kỷ luật: " + ex.Message);
            }
        }

        /// <summary>
        /// Thêm kỷ luật mới
        /// </summary>
        /// <param name="kl">Đối tượng KyLuat</param>
        /// <returns>ID của kỷ luật vừa thêm, -1 nếu thất bại</returns>
        public static int Add(KyLuat kl)
        {
            try
            {
                // Kiểm tra dữ liệu đầu vào
                if (kl.MaNV <= 0)
                    throw new Exception("Mã nhân viên không hợp lệ");

                if (string.IsNullOrWhiteSpace(kl.HinhThuc))
                    throw new Exception("Hình thức kỷ luật không được để trống");

                return KyLuatDAL.Add(kl);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi thêm kỷ luật: " + ex.Message);
            }
        }

        /// <summary>
        /// Cập nhật thông tin kỷ luật
        /// </summary>
        /// <param name="kl">Đối tượng KyLuat</param>
        /// <returns>true nếu thành công, false nếu thất bại</returns>
        public static bool Update(KyLuat kl)
        {
            try
            {
                // Kiểm tra dữ liệu đầu vào
                if (kl.MaKyLuat <= 0)
                    throw new Exception("Mã kỷ luật không hợp lệ");

                if (kl.MaNV <= 0)
                    throw new Exception("Mã nhân viên không hợp lệ");

                if (string.IsNullOrWhiteSpace(kl.HinhThuc))
                    throw new Exception("Hình thức kỷ luật không được để trống");

                return KyLuatDAL.Update(kl);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi cập nhật kỷ luật: " + ex.Message);
            }
        }

        /// <summary>
        /// Xóa kỷ luật
        /// </summary>
        /// <param name="maKyLuat">Mã kỷ luật</param>
        /// <returns>true nếu thành công, false nếu thất bại</returns>
        public static bool Delete(int maKyLuat)
        {
            try
            {
                if (maKyLuat <= 0)
                    throw new Exception("Mã kỷ luật không hợp lệ");

                return KyLuatDAL.Delete(maKyLuat);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi xóa kỷ luật: " + ex.Message);
            }
        }
    }
}