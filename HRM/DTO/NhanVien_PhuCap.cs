using System;

namespace HRM.DTO
{
    public class NhanVien_PhuCap
    {
        public int MaNV { get; set; }
        public int MaPhuCap { get; set; }
        
        // Thông tin phụ
        public string HoTenNV { get; set; }
        public string TenPhuCap { get; set; }
        public decimal GiaTri { get; set; }
    }
} 