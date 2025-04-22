using System;

namespace HRM.DTO
{
    public class HopDong
    {
        public int MaHopDong { get; set; }
        public int MaNV { get; set; }
        public string SoHopDong { get; set; }
        public string LoaiHopDong { get; set; } // "Biên chế", "Hợp đồng"
        public DateTime TuNgay { get; set; }
        public DateTime? DenNgay { get; set; }
        public DateTime NgayKy { get; set; }
        public string NoiDung { get; set; }
        public decimal LuongCoBan { get; set; }
        public decimal PhuCap { get; set; }
        
        // Thông tin phụ - không lưu trong DB
        public string HoTenNV { get; set; }
        public string TenPhong { get; set; }
        public string TenChucDanh { get; set; }
    }
}