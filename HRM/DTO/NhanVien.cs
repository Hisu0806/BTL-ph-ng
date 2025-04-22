using System;

namespace HRM.DTO
{
    public class NhanVien
    {
        public int MaNV { get; set; }
        public string HoTen { get; set; }
        public DateTime NgaySinh { get; set; }
        public string GioiTinh { get; set; }
        public string SoCMND { get; set; }
        public string TrinhDoChuyenMon { get; set; }
        public string TrinhDoNgoaiNgu { get; set; }
        public string HoKhauThuongTru { get; set; }
        public string DiaChi { get; set; }
        public string DanToc { get; set; }
        public string TonGiao { get; set; }
        public DateTime? NgayVaoDoan { get; set; }
        public DateTime? NgayVaoDang { get; set; }
        public string DienChinhSach { get; set; }
        public int MaPhong { get; set; }
        public int MaChucDanh { get; set; }
        public string LoaiNhanVien { get; set; }
        public DateTime NgayVaoCongTy { get; set; }
        public DateTime? NgayNghiViec { get; set; }
        public string TrangThai { get; set; }
        public byte[] HinhAnh { get; set; }

        // Thông tin phụ - không lưu trong DB
        public string TenPhong { get; set; }
        public string TenChucDanh { get; set; }
        public int Tuoi 
        { 
            get 
            {
                return DateTime.Now.Year - NgaySinh.Year;
            } 
        }
    }
} 