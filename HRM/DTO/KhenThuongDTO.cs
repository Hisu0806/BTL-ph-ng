using System;

namespace HRM.DTO
{
    public class KhenThuong
    {
        public int MaKhenThuong { get; set; }
        public int MaNV { get; set; }
        public DateTime NgayKhenThuong { get; set; }
        public string HinhThuc { get; set; }
        public string LyDo { get; set; }
        public decimal? SoTien { get; set; }
        public string GhiChu { get; set; }
    }
}