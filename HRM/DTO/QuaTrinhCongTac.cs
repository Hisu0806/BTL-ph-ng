using System;

namespace HRM.DTO
{
    public class QuaTrinhCongTac
    {
        public int MaQTCT { get; set; }
        public int MaNV { get; set; }
        public DateTime TuNgay { get; set; }
        public DateTime? DenNgay { get; set; }
        public string ChucVu { get; set; }
        public string DonViCongTac { get; set; }
        public string MoTaCongViec { get; set; }
        
        // Thông tin phụ - không lưu trong DB
        public string HoTenNV { get; set; }
    }
} 