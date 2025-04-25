using System;

namespace HRM.DTO
{
    public class TangLuongDTO
    {
        public int MaTangLuong { get; set; }
        public int MaNV { get; set; }
        public int MaBacLuongCu { get; set; }
        public int MaBacLuongMoi { get; set; }
        public DateTime NgayTangLuong { get; set; }
        public string LyDo { get; set; }
        public string GhiChu { get; set; }

        // Additional properties for display
        public string HoTen { get; set; }
        public string TenBacCu { get; set; }
        public string TenBacMoi { get; set; }
        public decimal LuongCu { get; set; }
        public decimal LuongMoi { get; set; }
        public decimal HeSoCu { get; set; }
        public decimal HeSoMoi { get; set; }
    }
}