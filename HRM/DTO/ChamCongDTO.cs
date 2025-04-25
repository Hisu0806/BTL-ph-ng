using System;

namespace HRM.DTO
{
    public class ChamCongDTO
    {
        public int MaChamCong { get; set; }
        public int MaNV { get; set; }
        public string HoTen { get; set; }
        public string TenPhong { get; set; }
        public string TenChucDanh { get; set; }
        public DateTime NgayChamCong { get; set; }
        public DateTime? GioVao { get; set; }
        public DateTime? GioRa { get; set; }
        public string TrangThai { get; set; }
        public string GhiChu { get; set; }
    }
}