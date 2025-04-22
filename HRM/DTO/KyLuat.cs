using System;

namespace HRM.DTO
{
    public class KyLuat
    {
        public int MaKyLuat { get; set; }
        public int MaNV { get; set; }
        public DateTime NgayKyLuat { get; set; }
        public string HinhThuc { get; set; }
        public string LyDo { get; set; }
        public decimal SoTien { get; set; }
        public string NguoiKy { get; set; }
        
        // Thông tin phụ - không lưu trong DB
        public string HoTenNV { get; set; }
        public string TenPhong { get; set; }
        public string TenChucDanh { get; set; }
    }
} 