using System;

namespace HRM.DTO
{
    public class Luong
    {
        public int MaLuong { get; set; }
        public int MaNV { get; set; }
        public int Thang { get; set; }
        public int Nam { get; set; }
        public int SoNgayCong { get; set; }
        public decimal LuongCoBan { get; set; }
        public decimal HeSoLuong { get; set; }
        public decimal PhuCap { get; set; }
        public decimal BaoHiem { get; set; }
        public decimal KhenThuong { get; set; }
        public decimal KyLuat { get; set; }
        public decimal ThucLanh { get; set; }
        public string GhiChu { get; set; }
        
        // Thông tin phụ - không lưu trong DB
        public string HoTenNV { get; set; }
        public string TenPhong { get; set; }
        public string TenChucDanh { get; set; }
    }
} 