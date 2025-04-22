using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.DTO
{
    public class ChamCong
    {
        public int MaChamCong { get; set; }
        public int MaNV { get; set; }
        public DateTime NgayChamCong { get; set; }
        public DateTime? GioVao { get; set; }
        public DateTime? GioRa { get; set; }
        public string TrangThai { get; set; }  // "Có mặt", "Vắng có phép", "Vắng không phép", "Đi công tác", "Nghỉ ốm"
        public string GhiChu { get; set; }

        // Thông tin mở rộng (để hiển thị trên giao diện)
        public string TenNV { get; set; }
        public string PhongBan { get; set; }
        public string ChucDanh { get; set; }
    }
} 