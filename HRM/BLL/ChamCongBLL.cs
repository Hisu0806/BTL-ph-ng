using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRM.DAL;
using HRM.DTO;

namespace HRM.BLL
{
    public class ChamCongBLL
    {
        /// <summary>
        /// Lấy danh sách chấm công theo ngày
        /// </summary>
        /// <param name="ngay">Ngày cần lấy danh sách chấm công</param>
        /// <returns>Danh sách chấm công</returns>
        public static List<ChamCong> LayDanhSachTheoNgay(DateTime ngay)
        {
            try
            {
                return ChamCongDAL.LayDanhSachTheoNgay(ngay);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi lấy danh sách chấm công: " + ex.Message);
            }
        }

        /// <summary>
        /// Chấm công cho nhân viên (vào/ra)
        /// </summary>
        /// <param name="maNV">Mã nhân viên</param>
        /// <param name="isVao">True: chấm giờ vào, False: chấm giờ ra</param>
        /// <returns>Kết quả chấm công</returns>
        public static string ChamCongNhanVien(int maNV, bool isVao)
        {
            try
            {
                DateTime ngayHienTai = DateTime.Now.Date;
                
                // Kiểm tra xem đã có bản ghi chấm công cho nhân viên trong ngày này chưa
                bool daChamCong = ChamCongDAL.KiemTraDaChamCong(maNV, ngayHienTai);
                
                if (isVao) // Chấm giờ vào
                {
                    if (!daChamCong)
                    {
                        // Tạo bản ghi chấm công mới
                        ChamCong chamCong = new ChamCong
                        {
                            MaNV = maNV,
                            NgayChamCong = ngayHienTai,
                            GioVao = DateTime.Now,
                            TrangThai = "Có mặt"
                        };
                        
                        int maChamCong = ChamCongDAL.ThemChamCong(chamCong);
                        if (maChamCong > 0)
                            return "Chấm công vào thành công";
                        else
                            return "Không thể chấm công vào";
                    }
                    else
                    {
                        return "Nhân viên đã chấm công vào hôm nay";
                    }
                }
                else // Chấm giờ ra
                {
                    if (daChamCong)
                    {
                        // Lấy thông tin chấm công của nhân viên trong ngày
                        List<ChamCong> dsChamCong = ChamCongDAL.LayDanhSachTheoNgay(ngayHienTai);
                        ChamCong chamCongNhanVien = dsChamCong.FirstOrDefault(cc => cc.MaNV == maNV);
                        
                        if (chamCongNhanVien != null)
                        {
                            // Cập nhật giờ ra
                            chamCongNhanVien.GioRa = DateTime.Now;
                            
                            int result = ChamCongDAL.CapNhatChamCong(chamCongNhanVien);
                            if (result > 0)
                                return "Chấm công ra thành công";
                            else
                                return "Không thể chấm công ra";
                        }
                        else
                        {
                            return "Không tìm thấy thông tin chấm công vào của nhân viên";
                        }
                    }
                    else
                    {
                        return "Nhân viên chưa chấm công vào hôm nay";
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi chấm công: " + ex.Message);
            }
        }

        /// <summary>
        /// Cập nhật thông tin chấm công
        /// </summary>
        /// <param name="chamCong">Đối tượng chấm công cần cập nhật</param>
        /// <returns>Kết quả cập nhật</returns>
        public static bool CapNhatChamCong(ChamCong chamCong)
        {
            try
            {
                int result = ChamCongDAL.CapNhatChamCong(chamCong);
                return result > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi cập nhật chấm công: " + ex.Message);
            }
        }

        /// <summary>
        /// Xóa bản ghi chấm công
        /// </summary>
        /// <param name="maChamCong">Mã chấm công cần xóa</param>
        /// <returns>Kết quả xóa</returns>
        public static bool XoaChamCong(int maChamCong)
        {
            try
            {
                int result = ChamCongDAL.XoaChamCong(maChamCong);
                return result > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi xóa chấm công: " + ex.Message);
            }
        }

        /// <summary>
        /// Lấy tất cả bản ghi chấm công
        /// </summary>
        /// <returns>Danh sách chấm công</returns>
        public static List<ChamCong> LayTatCa()
        {
            try
            {
                return ChamCongDAL.GetAll();
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi lấy danh sách chấm công: " + ex.Message);
            }
        }

        /// <summary>
        /// Lấy bản ghi chấm công theo ID
        /// </summary>
        /// <param name="maChamCong">Mã chấm công</param>
        /// <returns>Đối tượng chấm công</returns>
        public static ChamCong LayTheoID(int maChamCong)
        {
            try
            {
                return ChamCongDAL.GetByID(maChamCong);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi lấy thông tin chấm công: " + ex.Message);
            }
        }

        /// <summary>
        /// Lấy danh sách chấm công theo tháng/năm
        /// </summary>
        /// <param name="thang">Tháng</param>
        /// <param name="nam">Năm</param>
        /// <returns>Danh sách chấm công</returns>
        public static List<ChamCong> LayTheoThangNam(int thang, int nam)
        {
            try
            {
                if (thang < 1 || thang > 12)
                    throw new Exception("Tháng không hợp lệ");
                if (nam < 2000 || nam > 2100)
                    throw new Exception("Năm không hợp lệ");

                return ChamCongDAL.GetByMonthYear(thang, nam);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi lấy danh sách chấm công theo tháng/năm: " + ex.Message);
            }
        }

        /// <summary>
        /// Lấy danh sách chấm công theo nhân viên trong tháng/năm
        /// </summary>
        /// <param name="maNV">Mã nhân viên</param>
        /// <param name="thang">Tháng</param>
        /// <param name="nam">Năm</param>
        /// <returns>Danh sách chấm công</returns>
        public static List<ChamCong> LayTheoNhanVienThangNam(int maNV, int thang, int nam)
        {
            try
            {
                if (thang < 1 || thang > 12)
                    throw new Exception("Tháng không hợp lệ");
                if (nam < 2000 || nam > 2100)
                    throw new Exception("Năm không hợp lệ");

                return ChamCongDAL.GetByNhanVienAndMonthYear(maNV, thang, nam);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi lấy danh sách chấm công theo nhân viên trong tháng/năm: " + ex.Message);
            }
        }

        /// <summary>
        /// Lấy số ngày công trong tháng của nhân viên
        /// </summary>
        /// <param name="maNV">Mã nhân viên</param>
        /// <param name="thang">Tháng</param>
        /// <param name="nam">Năm</param>
        /// <returns>Số ngày công</returns>
        public static int LaySoNgayCongTrongThang(int maNV, int thang, int nam)
        {
            try
            {
                if (thang < 1 || thang > 12)
                    throw new Exception("Tháng không hợp lệ");
                if (nam < 2000 || nam > 2100)
                    throw new Exception("Năm không hợp lệ");

                return ChamCongDAL.GetWorkingDaysInMonth(maNV, thang, nam);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi lấy số ngày công trong tháng: " + ex.Message);
            }
        }

        /// <summary>
        /// Tự động chấm công cho tất cả nhân viên trong một ngày
        /// </summary>
        /// <param name="ngay">Ngày cần chấm công</param>
        public static void ChamCongNgay(DateTime ngay)
        {
            try
            {
                // Kiểm tra ngày hợp lệ
                if (ngay.Date > DateTime.Now.Date)
                {
                    throw new Exception("Không thể chấm công cho ngày trong tương lai");
                }

                // Lấy danh sách tất cả nhân viên đang làm việc
                List<NhanVien> dsNhanVien = NhanVienBLL.GetAll().Where(nv => nv.TrangThai == "Đang làm việc").ToList();

                foreach (var nhanVien in dsNhanVien)
                {
                    // Kiểm tra xem nhân viên đã được chấm công trong ngày này chưa
                    bool daChamCong = ChamCongDAL.KiemTraDaChamCong(nhanVien.MaNV, ngay);

                    if (!daChamCong)
                    {
                        // Tạo dữ liệu chấm công mặc định
                        ChamCong chamCong = new ChamCong
                        {
                            MaNV = nhanVien.MaNV,
                            NgayChamCong = ngay,
                            GioVao = new DateTime(ngay.Year, ngay.Month, ngay.Day, 8, 0, 0), // Mặc định 8:00
                            GioRa = new DateTime(ngay.Year, ngay.Month, ngay.Day, 17, 0, 0), // Mặc định 17:00
                            TrangThai = "Đi làm"
                        };

                        // Thêm vào CSDL
                        ChamCongDAL.Insert(chamCong);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi chấm công tự động: " + ex.Message);
            }
        }
    }
} 