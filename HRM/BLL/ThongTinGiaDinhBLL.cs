using System;
using System.Data;
using HRM.DAL;
using HRM.DTO;

namespace HRM.BLL
{
    public class ThongTinGiaDinhBLL
    {
        public static DataTable GetList()
        {
            try
            {
                return ThongTinGiaDinhDAL.GetList();
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi lấy danh sách thông tin gia đình: " + ex.Message);
            }
        }

        public static DataTable GetByMaNV(int maNV)
        {
            try
            {
                if (maNV <= 0)
                    throw new Exception("Mã nhân viên không hợp lệ");

                return ThongTinGiaDinhDAL.GetByMaNV(maNV);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi lấy thông tin gia đình theo mã nhân viên: " + ex.Message);
            }
        }

        public static ThongTinGiaDinhDTO GetByID(int maThongTin)
        {
            try
            {
                if (maThongTin <= 0)
                    throw new Exception("Mã thông tin không hợp lệ");

                return ThongTinGiaDinhDAL.GetByID(maThongTin);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi lấy thông tin gia đình theo mã: " + ex.Message);
            }
        }

        public static int Add(ThongTinGiaDinhDTO ttg)
        {
            try
            {
                if (ttg == null)
                    throw new Exception("Thông tin gia đình không được để trống");

                if (ttg.MaNV <= 0)
                    throw new Exception("Mã nhân viên không hợp lệ");

                if (string.IsNullOrWhiteSpace(ttg.HoTen))
                    throw new Exception("Họ tên không được để trống");

                if (string.IsNullOrWhiteSpace(ttg.QuanHe))
                    throw new Exception("Quan hệ không được để trống");

                return ThongTinGiaDinhDAL.Add(ttg);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi thêm thông tin gia đình: " + ex.Message);
            }
        }

        public static bool Update(ThongTinGiaDinhDTO ttg)
        {
            try
            {
                if (ttg == null)
                    throw new Exception("Thông tin gia đình không được để trống");

                if (ttg.MaThongTin <= 0)
                    throw new Exception("Mã thông tin không hợp lệ");

                if (ttg.MaNV <= 0)
                    throw new Exception("Mã nhân viên không hợp lệ");

                if (string.IsNullOrWhiteSpace(ttg.HoTen))
                    throw new Exception("Họ tên không được để trống");

                if (string.IsNullOrWhiteSpace(ttg.QuanHe))
                    throw new Exception("Quan hệ không được để trống");

                return ThongTinGiaDinhDAL.Update(ttg);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi cập nhật thông tin gia đình: " + ex.Message);
            }
        }

        public static bool Delete(int maThongTin)
        {
            try
            {
                if (maThongTin <= 0)
                    throw new Exception("Mã thông tin không hợp lệ");

                return ThongTinGiaDinhDAL.Delete(maThongTin);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi xóa thông tin gia đình: " + ex.Message);
            }
        }
    }
}