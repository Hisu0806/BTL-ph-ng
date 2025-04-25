using System;
using System.Data;
using HRM.DTO;
using HRM.DAL;
using System.Data.SqlClient;

namespace HRM.BLL
{
    /// <summary>
    /// Lớp xử lý nghiệp vụ cho hợp đồng
    /// </summary>
    public class HopDongBLL
    {
        /// <summary>
        /// Lấy danh sách hợp đồng
        /// </summary>
        /// <returns>DataTable chứa danh sách hợp đồng</returns>
        public static DataTable GetList()
        {
            try
            {
                return HopDongDAL.GetList();
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi lấy danh sách hợp đồng: " + ex.Message);
            }
        }

        /// <summary>
        /// Lấy danh sách hợp đồng của một nhân viên
        /// </summary>
        /// <param name="maNV">Mã nhân viên</param>
        /// <returns>DataTable chứa danh sách hợp đồng</returns>
        public static DataTable GetByMaNV(int maNV)
        {
            try
            {
                return HopDongDAL.GetByMaNV(maNV);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi lấy danh sách hợp đồng theo mã nhân viên: " + ex.Message);
            }
        }

        /// <summary>
        /// Lấy thông tin một hợp đồng theo mã
        /// </summary>
        /// <param name="maHopDong">Mã hợp đồng</param>
        /// <returns>Đối tượng HopDong</returns>
        public static HopDongDTO GetByID(int maHopDong)
        {
            try
            {
                return HopDongDAL.GetByID(maHopDong);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi lấy thông tin hợp đồng: " + ex.Message);
            }
        }

        /// <summary>
        /// Thêm hợp đồng mới
        /// </summary>
        /// <param name="hd">Đối tượng HopDong</param>
        /// <returns>ID của hợp đồng vừa thêm, -1 nếu thất bại</returns>
        public static int Add(HopDongDTO hd)
        {
            try
            {
                // Kiểm tra dữ liệu đầu vào
                if (string.IsNullOrEmpty(hd.SoHopDong))
                    throw new Exception("Số hợp đồng không được để trống");
                if (string.IsNullOrEmpty(hd.LoaiHopDong))
                    throw new Exception("Loại hợp đồng không được để trống");
                if (hd.NgayBatDau < hd.NgayKy)
                    throw new Exception("Ngày bắt đầu phải sau hoặc bằng ngày ký");
                if (hd.NgayKetThuc.HasValue && hd.NgayKetThuc.Value < hd.NgayBatDau)
                    throw new Exception("Ngày kết thúc phải sau ngày bắt đầu");
                if (hd.LuongTheoHopDong <= 0)
                    throw new Exception("Lương theo hợp đồng phải lớn hơn 0");
                if (string.IsNullOrEmpty(hd.TrangThai))
                    throw new Exception("Trạng thái không được để trống");

                return HopDongDAL.Add(hd);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi thêm hợp đồng: " + ex.Message);
            }
        }

        /// <summary>
        /// Cập nhật thông tin hợp đồng
        /// </summary>
        /// <param name="hd">Đối tượng HopDong</param>
        /// <returns>true nếu thành công, false nếu thất bại</returns>
        public static bool Update(HopDongDTO hd)
        {
            try
            {
                // Kiểm tra dữ liệu đầu vào
                if (string.IsNullOrEmpty(hd.SoHopDong))
                    throw new Exception("Số hợp đồng không được để trống");
                if (string.IsNullOrEmpty(hd.LoaiHopDong))
                    throw new Exception("Loại hợp đồng không được để trống");
                if (hd.NgayBatDau < hd.NgayKy)
                    throw new Exception("Ngày bắt đầu phải sau hoặc bằng ngày ký");
                if (hd.NgayKetThuc.HasValue && hd.NgayKetThuc.Value < hd.NgayBatDau)
                    throw new Exception("Ngày kết thúc phải sau ngày bắt đầu");
                if (hd.LuongTheoHopDong <= 0)
                    throw new Exception("Lương theo hợp đồng phải lớn hơn 0");
                if (string.IsNullOrEmpty(hd.TrangThai))
                    throw new Exception("Trạng thái không được để trống");

                return HopDongDAL.Update(hd);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi cập nhật hợp đồng: " + ex.Message);
            }
        }

        /// <summary>
        /// Xóa hợp đồng
        /// </summary>
        /// <param name="maHopDong">Mã hợp đồng</param>
        /// <returns>true nếu thành công, false nếu thất bại</returns>
        public static bool Delete(int maHopDong)
        {
            try
            {
                return HopDongDAL.Delete(maHopDong);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi xóa hợp đồng: " + ex.Message);
            }
        }

        /// <summary>
        /// Lấy loại nhân viên theo mã nhân viên
        /// </summary>
        /// <param name="maNV">Mã nhân viên</param>
        /// <returns>Loại nhân viên</returns>
        /// <summary>
        /// Lấy loại nhân viên từ bảng NhanVien
        /// </summary>
        /// <param name="maNV">Mã nhân viên</param>
        /// <returns>Loại nhân viên</returns>
        /// <summary>
        /// Lấy loại nhân viên từ bảng NhanVien
        /// </summary>
        /// <param name="maNV">Mã nhân viên</param>
        /// <returns>Loại nhân viên</returns>
        public static string GetLoaiNhanVien(int maNV)
        {
            try
            {
                NhanVien nv = NhanVienBLL.GetByID(maNV);
                return nv != null ? nv.LoaiNhanVien : string.Empty;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi lấy loại nhân viên: " + ex.Message);
            }
        }

        /// <summary>
        /// Lấy hợp đồng hiện tại của nhân viên
        /// </summary>
        /// <param name="maNV">Mã nhân viên</param>
        /// <returns>Đối tượng HopDongDTO của hợp đồng hiện tại</returns>
        public static HopDongDTO GetHopDongHienTai(int maNV)
        {
            try
            {
                DataTable dt = GetByMaNV(maNV);
                if (dt != null && dt.Rows.Count > 0)
                {
                    // Lấy hợp đồng có trạng thái "Đang hiệu lực" hoặc hợp đồng mới nhất
                    DataRow[] rows = dt.Select("TrangThai = 'Đang hiệu lực'", "NgayKy DESC");
                    if (rows.Length == 0)
                    {
                        rows = dt.Select("", "NgayKy DESC");
                    }

                    if (rows.Length > 0)
                    {
                        return new HopDongDTO
                        {
                            MaHopDong = Convert.ToInt32(rows[0]["MaHopDong"]),
                            SoHopDong = rows[0]["SoHopDong"].ToString(),
                            MaNV = Convert.ToInt32(rows[0]["MaNV"]),
                            LoaiHopDong = rows[0]["LoaiHopDong"].ToString(),
                            NgayKy = Convert.ToDateTime(rows[0]["NgayKy"]),
                            NgayBatDau = Convert.ToDateTime(rows[0]["NgayBatDau"]),
                            NgayKetThuc = rows[0]["NgayKetThuc"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(rows[0]["NgayKetThuc"]) : null,
                            LuongTheoHopDong = Convert.ToDecimal(rows[0]["LuongTheoHopDong"]),
                            TrangThai = rows[0]["TrangThai"].ToString()
                        };
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi lấy hợp đồng hiện tại: " + ex.Message);
            }
        }

    }
}