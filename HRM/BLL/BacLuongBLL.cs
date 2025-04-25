using System;
using System.Data;
using HRM.DTO;
using HRM.DAL;

namespace HRM.BLL
{
    public class BacLuongBLL
    {
        private static BacLuongBLL instance;
        private static readonly object lockObject = new object();

        public static BacLuongBLL Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (lockObject)
                    {
                        if (instance == null)
                        {
                            instance = new BacLuongBLL();
                        }
                    }
                }
                return instance;
            }
        }

        private BacLuongBLL() { }

        public DataTable GetList()
        {
            try
            {
                DataTable dt = BacLuongDAL.GetList();
                if (dt == null)
                {
                    throw new Exception("Không thể lấy danh sách bậc lương từ cơ sở dữ liệu");
                }
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi lấy danh sách bậc lương: " + ex.Message);
            }
        }

        public BacLuongDTO GetByID(int maBacLuong)
        {
            try
            {
                if (maBacLuong <= 0)
                    throw new Exception("Mã bậc lương không hợp lệ");

                return BacLuongDAL.GetByID(maBacLuong);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi lấy thông tin bậc lương: " + ex.Message);
            }
        }

        public int Add(BacLuongDTO bl)
        {
            try
            {
                if (string.IsNullOrEmpty(bl.TenBac))
                    throw new Exception("Tên bậc lương không được để trống");

                if (bl.HeSo <= 0)
                    throw new Exception("Hệ số lương phải lớn hơn 0");

                if (bl.LuongCoBan <= 0)
                    throw new Exception("Lương cơ bản phải lớn hơn 0");

                return BacLuongDAL.Add(bl);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi thêm bậc lương: " + ex.Message);
            }
        }

        public bool Update(BacLuongDTO bl)
        {
            try
            {
                if (bl.MaBacLuong <= 0)
                    throw new Exception("Mã bậc lương không hợp lệ");

                if (string.IsNullOrEmpty(bl.TenBac))
                    throw new Exception("Tên bậc lương không được để trống");

                if (bl.HeSo <= 0)
                    throw new Exception("Hệ số lương phải lớn hơn 0");

                if (bl.LuongCoBan <= 0)
                    throw new Exception("Lương cơ bản phải lớn hơn 0");

                return BacLuongDAL.Update(bl);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi cập nhật bậc lương: " + ex.Message);
            }
        }

        public bool Delete(int maBacLuong)
        {
            try
            {
                if (maBacLuong <= 0)
                    throw new Exception("Mã bậc lương không hợp lệ");

                return BacLuongDAL.Delete(maBacLuong);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi xóa bậc lương: " + ex.Message);
            }
        }

        public BacLuongDTO GetCurrentBacLuong(int maNV)
        {
            try
            {
                if (maNV <= 0)
                {
                    throw new ArgumentException("Mã nhân viên không hợp lệ");
                }

                return BacLuongDAL.GetCurrentBacLuong(maNV);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi lấy bậc lương hiện tại: " + ex.Message);
            }
        }
    }
}