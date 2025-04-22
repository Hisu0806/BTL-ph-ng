using System;

namespace HRM.DTO
{
    public class TangLuong
    {
        public int MaTangLuong { get; set; }
        public int MaNV { get; set; }
        public DateTime NgayTangLuong { get; set; }
        public decimal LuongCuCoBan { get; set; }
        public decimal HeSoCu { get; set; }
        public decimal LuongMoiCoBan { get; set; }
        public decimal HeSoMoi { get; set; }
        public string LyDo { get; set; }
        public string NguoiKy { get; set; }
        
        // Thông tin phụ - không lưu trong DB
        public string HoTenNV { get; set; }
        public string TenPhong { get; set; }
        public string TenChucDanh { get; set; }
    }
} 