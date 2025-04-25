using System;

namespace HRM.DTO
{
    public class PhongBanDTO
    {
        public int MaPhong { get; set; }
        public string TenPhong { get; set; }
        public string MoTa { get; set; }
        public int? TruongPhong { get; set; }
        
        // Thông tin bổ sung
        public string TenTruongPhong { get; set; }
    }
} 