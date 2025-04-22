using System;
using System.Collections.Generic;
using HRM.DAL;
using HRM.DTO;

namespace HRM.BLL
{
    public class LuongBLL
    {
        // Tính lương cho nhân viên biên chế
        public static decimal TinhLuongBienChe(int maNV, int thang, int nam)
        {
            // Lấy thông tin bậc lương và hệ số của nhân viên
            BacLuong bacLuong = BacLuongDAL.GetCurrentBacLuong(maNV);
            if (bacLuong == null)
                throw new Exception("Chưa cấu hình bậc lương cho nhân viên này");
            
            // Lấy tổng phụ cấp của nhân viên
            decimal tongPhuCap = PhuCapDAL.GetTongPhuCap(maNV);
            
            // Tính bảo hiểm (thường là 10.5% lương cơ bản)
            decimal baoHiem = bacLuong.LuongCoBan * 0.105m;
            
            // Tính lương dựa trên công thức: Lương = Bậc lương x Hệ số + Phụ cấp - Bảo hiểm
            decimal luong = bacLuong.LuongCoBan * (decimal)bacLuong.HeSo + tongPhuCap - baoHiem;
            
            // Lấy số ngày công trong tháng
            int soNgayCong = ChamCongDAL.GetSoNgayCong(maNV, thang, nam);
            int soNgayCongTieuChuan = GetSoNgayCongTieuChuan(thang, nam);
            
            // Điều chỉnh lương theo ngày công
            if (soNgayCongTieuChuan > 0)
                luong = luong * soNgayCong / soNgayCongTieuChuan;
            
            return luong;
        }
        
        // Tính lương cho nhân viên hợp đồng
        public static decimal TinhLuongHopDong(int maNV, int thang, int nam)
        {
            // Lấy thông tin hợp đồng của nhân viên
            HopDong hopDong = HopDongDAL.GetCurrentHopDong(maNV);
            if (hopDong == null)
                throw new Exception("Chưa có hợp đồng cho nhân viên này");
            
            // Lấy lương theo hợp đồng
            decimal luongTheoHopDong = hopDong.LuongTheoHopDong;
            
            // Lấy số ngày công trong tháng
            int soNgayCong = ChamCongDAL.GetSoNgayCong(maNV, thang, nam);
            int soNgayCongTieuChuan = GetSoNgayCongTieuChuan(thang, nam);
            
            // Điều chỉnh lương theo ngày công
            decimal luong = luongTheoHopDong;
            if (soNgayCongTieuChuan > 0)
                luong = luongTheoHopDong * soNgayCong / soNgayCongTieuChuan;
            
            return luong;
        }
        
        // Tính lương tổng hợp cho nhân viên
        public static Luong TinhLuong(int maNV, int thang, int nam)
        {
            Luong luong = new Luong();
            luong.MaNV = maNV;
            luong.Thang = thang;
            luong.Nam = nam;
            
            // Lấy thông tin nhân viên
            NhanVien nv = NhanVienDAL.GetByID(maNV);
            if (nv == null)
                throw new Exception("Không tìm thấy nhân viên");
            
            // Kiểm tra nhân viên biên chế hay hợp đồng
            if (nv.LoaiNhanVien == "Biên chế")
            {
                BacLuong bacLuong = BacLuongDAL.GetCurrentBacLuong(maNV);
                if (bacLuong == null)
                    throw new Exception("Chưa cấu hình bậc lương cho nhân viên này");
                
                luong.MaBacLuong = bacLuong.MaBacLuong;
                luong.LuongCoBan = bacLuong.LuongCoBan;
                luong.TongPhuCap = PhuCapDAL.GetTongPhuCap(maNV);
                luong.BaoHiem = bacLuong.LuongCoBan * 0.105m;
                luong.TongNgayCong = ChamCongDAL.GetSoNgayCong(maNV, thang, nam);
                
                // Tính lương
                decimal tongLuong = bacLuong.LuongCoBan * (decimal)bacLuong.HeSo + luong.TongPhuCap - luong.BaoHiem;
                int soNgayCongTieuChuan = GetSoNgayCongTieuChuan(thang, nam);
                
                if (soNgayCongTieuChuan > 0)
                    tongLuong = tongLuong * luong.TongNgayCong / soNgayCongTieuChuan;
                
                luong.TongLuong = tongLuong;
            }
            else // Hợp đồng
            {
                HopDong hopDong = HopDongDAL.GetCurrentHopDong(maNV);
                if (hopDong == null)
                    throw new Exception("Chưa có hợp đồng cho nhân viên này");
                
                luong.LuongCoBan = hopDong.LuongTheoHopDong;
                luong.TongPhuCap = PhuCapDAL.GetTongPhuCap(maNV);
                luong.BaoHiem = hopDong.LuongTheoHopDong * 0.105m;
                luong.TongNgayCong = ChamCongDAL.GetSoNgayCong(maNV, thang, nam);
                
                // Tính lương
                decimal tongLuong = hopDong.LuongTheoHopDong + luong.TongPhuCap - luong.BaoHiem;
                int soNgayCongTieuChuan = GetSoNgayCongTieuChuan(thang, nam);
                
                if (soNgayCongTieuChuan > 0)
                    tongLuong = tongLuong * luong.TongNgayCong / soNgayCongTieuChuan;
                
                luong.TongLuong = tongLuong;
            }
            
            return luong;
        }
        
        // Lấy số ngày công tiêu chuẩn trong tháng (ngày làm việc - ngày nghỉ lễ)
        public static int GetSoNgayCongTieuChuan(int thang, int nam)
        {
            DateTime startDate = new DateTime(nam, thang, 1);
            DateTime endDate = startDate.AddMonths(1).AddDays(-1);
            
            int weekendDays = 0;
            int holidays = NgayLeDAL.GetHolidaysInMonth(thang, nam).Count; // Lấy số ngày lễ trong tháng
            
            for (DateTime day = startDate; day <= endDate; day = day.AddDays(1))
            {
                if (day.DayOfWeek == DayOfWeek.Saturday || day.DayOfWeek == DayOfWeek.Sunday)
                    weekendDays++;
            }
            
            int workingDays = (endDate - startDate).Days + 1 - weekendDays - holidays;
            return workingDays;
        }
        
        // Lưu thông tin lương
        public static bool SaveLuong(Luong luong)
        {
            return LuongDAL.SaveLuong(luong);
        }
        
        // Lấy danh sách lương theo tháng
        public static List<Luong> GetLuongByThang(int thang, int nam)
        {
            return LuongDAL.GetLuongByThang(thang, nam);
        }
        
        // Lấy danh sách lương theo phòng ban
        public static List<Luong> GetLuongByPhongBan(int maPhong, int thang, int nam)
        {
            return LuongDAL.GetLuongByPhongBan(maPhong, thang, nam);
        }
        
        // Tính và lưu lương cho tất cả nhân viên
        public static bool ProcessLuongAllNhanVien(int thang, int nam)
        {
            try
            {
                List<NhanVien> dsNhanVien = NhanVienDAL.GetAll();
                foreach (NhanVien nv in dsNhanVien)
                {
                    // Kiểm tra đã tính lương cho nhân viên này trong tháng chưa
                    bool daCoLuong = LuongDAL.CheckLuongExists(nv.MaNV, thang, nam);
                    if (!daCoLuong)
                    {
                        Luong luong = TinhLuong(nv.MaNV, thang, nam);
                        SaveLuong(luong);
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
} 