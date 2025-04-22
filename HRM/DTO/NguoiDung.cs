using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.DTO
{
    public class NguoiDung
    {
        public string MaNguoiDung { get; set; }
        public string TenDangNhap { get; set; }
        public string MatKhau { get; set; }
        public string HoTen { get; set; }
        public string Email { get; set; }
        public string DienThoai { get; set; }
        public string VaiTro { get; set; }
        public DateTime NgayTao { get; set; }
        public bool TrangThai { get; set; }
        public int? MaNV { get; set; }
        
        // Thông tin phụ
        public string TenNhanVien { get; set; }
        public string Quyen { get; set; }
    }
} 