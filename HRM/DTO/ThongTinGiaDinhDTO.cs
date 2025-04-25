using System;

namespace HRM.DTO
{
    public class ThongTinGiaDinhDTO
    {
        public int MaThongTin { get; set; }
        public int MaNV { get; set; }
        public string HoTen { get; set; }
        public string QuanHe { get; set; }
        public DateTime? NgaySinh { get; set; }
        public string NgheNghiep { get; set; }
        public string DiaChi { get; set; }
        public string GhiChu { get; set; }
    }
}