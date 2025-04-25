using System;

namespace HRM.DTO
{
    /// <summary>
    /// Lớp đại diện cho thông tin hợp đồng
    /// </summary>
    public class HopDongDTO
    {
        /// <summary>
        /// Mã hợp đồng
        /// </summary>
        public int MaHopDong { get; set; }

        /// <summary>
        /// Mã nhân viên
        /// </summary>
        public int MaNV { get; set; }

        /// <summary>
        /// Số hợp đồng
        /// </summary>
        public string SoHopDong { get; set; }

        /// <summary>
        /// Loại hợp đồng
        /// </summary>
        public string LoaiHopDong { get; set; }

        /// <summary>
        /// Ngày ký hợp đồng
        /// </summary>
        public DateTime NgayKy { get; set; }

        /// <summary>
        /// Ngày bắt đầu hợp đồng
        /// </summary>
        public DateTime NgayBatDau { get; set; }

        /// <summary>
        /// Ngày kết thúc hợp đồng
        /// </summary>
        public DateTime? NgayKetThuc { get; set; }

        /// <summary>
        /// Lương theo hợp đồng
        /// </summary>
        public decimal LuongTheoHopDong { get; set; }

        /// <summary>
        /// Trạng thái hợp đồng
        /// </summary>
        public string TrangThai { get; set; }

        /// <summary>
        /// Nội dung hợp đồng
        /// </summary>
        public string NoiDung { get; set; }
    }
}