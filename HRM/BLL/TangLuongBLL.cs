using System;
using System.Collections.Generic;
using HRM.DAL;
using HRM.DTO;

namespace HRM.BLL
{
    public class TangLuongBLL
    {
        public static List<TangLuong> GetAll()
        {
            return TangLuongDAL.GetAll();
        }
        
        public static TangLuong GetByID(int maTangLuong)
        {
            return TangLuongDAL.GetByID(maTangLuong);
        }
        
        public static List<TangLuong> GetByNhanVien(int maNV)
        {
            return TangLuongDAL.GetByNhanVien(maNV);
        }
        
        public static bool Insert(TangLuong tangLuong)
        {
            // Kiểm tra dữ liệu đầu vào
            if (tangLuong.MaNV <= 0)
                throw new Exception("Mã nhân viên không hợp lệ");
                
            if (tangLuong.MaBacLuongCu <= 0)
                throw new Exception("Bậc lương cũ không hợp lệ");
                
            if (tangLuong.MaBacLuongMoi <= 0)
                throw new Exception("Bậc lương mới không hợp lệ");
                
            if (tangLuong.MaBacLuongCu == tangLuong.MaBacLuongMoi)
                throw new Exception("Bậc lương mới phải khác bậc lương cũ");
                
            return TangLuongDAL.Insert(tangLuong);
        }
        
        public static bool Update(TangLuong tangLuong)
        {
            // Kiểm tra dữ liệu đầu vào
            if (tangLuong.MaNV <= 0)
                throw new Exception("Mã nhân viên không hợp lệ");
                
            if (tangLuong.MaBacLuongCu <= 0)
                throw new Exception("Bậc lương cũ không hợp lệ");
                
            if (tangLuong.MaBacLuongMoi <= 0)
                throw new Exception("Bậc lương mới không hợp lệ");
                
            if (tangLuong.MaBacLuongCu == tangLuong.MaBacLuongMoi)
                throw new Exception("Bậc lương mới phải khác bậc lương cũ");
                
            return TangLuongDAL.Update(tangLuong);
        }
        
        public static bool Delete(int maTangLuong)
        {
            return TangLuongDAL.Delete(maTangLuong);
        }
        
        // Lấy danh sách nhân viên được tăng lương (3 năm một lần từ lần tăng gần nhất)
        public static List<NhanVien> GetDanhSachTangLuong()
        {
            return TangLuongDAL.GetDanhSachTangLuong();
        }
        
        // Kiểm tra nhân viên có đủ điều kiện tăng lương không
        public static bool CheckDuDieuKienTangLuong(int maNV)
        {
            // Lấy lần tăng lương gần nhất
            TangLuong tangLuongGanNhat = TangLuongDAL.GetLanTangLuongGanNhat(maNV);
            
            // Nếu chưa từng tăng lương và đã làm việc >= 3 năm thì đủ điều kiện
            if (tangLuongGanNhat == null)
            {
                NhanVien nv = NhanVienDAL.GetByID(maNV);
                if (nv != null)
                {
                    TimeSpan thoiGianLamViec = DateTime.Now - nv.NgayVaoCongTy;
                    return thoiGianLamViec.TotalDays >= 365 * 3; // 3 năm
                }
                return false;
            }
            
            // Nếu đã tăng lương trước đó, kiểm tra xem đã đủ 3 năm chưa
            TimeSpan thoiGianTuLanTruoc = DateTime.Now - tangLuongGanNhat.NgayTangLuong;
            return thoiGianTuLanTruoc.TotalDays >= 365 * 3; // 3 năm
        }
        
        // Thực hiện tăng lương cho nhân viên
        public static bool ThucHienTangLuong(int maNV, int maBacLuongMoi, string lyDo)
        {
            // Kiểm tra nhân viên có tồn tại không
            NhanVien nv = NhanVienDAL.GetByID(maNV);
            if (nv == null)
                throw new Exception("Không tìm thấy nhân viên");
                
            // Kiểm tra bậc lương mới có tồn tại không
            BacLuong bacLuongMoi = BacLuongDAL.GetByID(maBacLuongMoi);
            if (bacLuongMoi == null)
                throw new Exception("Không tìm thấy bậc lương mới");
                
            // Lấy bậc lương hiện tại của nhân viên
            BacLuong bacLuongHienTai = BacLuongDAL.GetCurrentBacLuong(maNV);
            if (bacLuongHienTai == null)
                throw new Exception("Chưa có thông tin bậc lương hiện tại của nhân viên");
                
            if (bacLuongHienTai.MaBacLuong == maBacLuongMoi)
                throw new Exception("Bậc lương mới phải khác bậc lương hiện tại");
                
            // Tạo đối tượng tăng lương
            TangLuong tangLuong = new TangLuong();
            tangLuong.MaNV = maNV;
            tangLuong.NgayTangLuong = DateTime.Now;
            tangLuong.MaBacLuongCu = bacLuongHienTai.MaBacLuong;
            tangLuong.MaBacLuongMoi = maBacLuongMoi;
            tangLuong.LyDo = lyDo;
            
            return TangLuongDAL.Insert(tangLuong);
        }
    }
} 