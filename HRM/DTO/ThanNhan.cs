using System;

namespace HRM.DTO
{
    public class ThanNhan
    {
        public int MaThanNhan { get; set; }
        public int MaNV { get; set; }
        public string HoTen { get; set; }
        public string QuanHe { get; set; }
        public DateTime? NgaySinh { get; set; }
        public string NgheNghiep { get; set; }
        public string DiaChi { get; set; }
        public string DienThoai { get; set; }
        
        // Thông tin phụ - không lưu trong DB
        public string HoTenNV { get; set; }
    }
} 