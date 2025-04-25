using System;

namespace HRM.DTO
{
    public class QuaTrinhCongTac
    {
        public int MaQuaTrinh { get; set; }
        public int MaNV { get; set; }
        public DateTime TuNgay { get; set; }
        public DateTime? DenNgay { get; set; }
        public string ChucDanh { get; set; }
        public string PhongBan { get; set; }
        public string MoTaCongViec { get; set; }
        public string GhiChu { get; set; }
    }
}