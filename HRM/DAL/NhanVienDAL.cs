using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using HRM.DTO;

namespace HRM.DAL
{
    public class NhanVienDAL
    {
        public static List<NhanVien> GetAll()
        {
            string query = @"SELECT nv.*, pb.TenPhong, cd.TenChucDanh 
                            FROM NhanVien nv
                            LEFT JOIN PhongBan pb ON nv.MaPhong = pb.MaPhong
                            LEFT JOIN ChucDanh cd ON nv.MaChucDanh = cd.MaChucDanh
                            WHERE nv.TrangThai = N'Đang làm việc'
                            ORDER BY nv.HoTen";
            DataTable dt = DBConnection.ExecuteQuery(query);
            
            List<NhanVien> list = new List<NhanVien>();
            foreach (DataRow row in dt.Rows)
            {
                NhanVien nv = new NhanVien();
                nv.MaNV = Convert.ToInt32(row["MaNV"]);
                nv.HoTen = row["HoTen"].ToString();
                nv.NgaySinh = Convert.ToDateTime(row["NgaySinh"]);
                nv.GioiTinh = row["GioiTinh"].ToString();
                nv.SoCMND = row["SoCMND"].ToString();
                nv.TrinhDoChuyenMon = row["TrinhDoChuyenMon"] != DBNull.Value ? row["TrinhDoChuyenMon"].ToString() : null;
                nv.TrinhDoNgoaiNgu = row["TrinhDoNgoaiNgu"] != DBNull.Value ? row["TrinhDoNgoaiNgu"].ToString() : null;
                nv.HoKhauThuongTru = row["HoKhauThuongTru"] != DBNull.Value ? row["HoKhauThuongTru"].ToString() : null;
                nv.DiaChi = row["DiaChi"] != DBNull.Value ? row["DiaChi"].ToString() : null;
                nv.DanToc = row["DanToc"] != DBNull.Value ? row["DanToc"].ToString() : null;
                nv.TonGiao = row["TonGiao"] != DBNull.Value ? row["TonGiao"].ToString() : null;
                nv.NgayVaoDoan = row["NgayVaoDoan"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(row["NgayVaoDoan"]) : null;
                nv.NgayVaoDang = row["NgayVaoDang"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(row["NgayVaoDang"]) : null;
                nv.DienChinhSach = row["DienChinhSach"] != DBNull.Value ? row["DienChinhSach"].ToString() : null;
                nv.MaPhong = Convert.ToInt32(row["MaPhong"]);
                nv.MaChucDanh = Convert.ToInt32(row["MaChucDanh"]);
                nv.LoaiNhanVien = row["LoaiNhanVien"].ToString();
                nv.NgayVaoCongTy = Convert.ToDateTime(row["NgayVaoCongTy"]);
                nv.NgayNghiViec = row["NgayNghiViec"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(row["NgayNghiViec"]) : null;
                nv.TrangThai = row["TrangThai"].ToString();
                nv.HinhAnh = row["HinhAnh"] != DBNull.Value ? (byte[])row["HinhAnh"] : null;
                
                // Thông tin phụ
                nv.TenPhong = row["TenPhong"] != DBNull.Value ? row["TenPhong"].ToString() : "";
                nv.TenChucDanh = row["TenChucDanh"] != DBNull.Value ? row["TenChucDanh"].ToString() : "";
                
                list.Add(nv);
            }
            
            return list;
        }
        
        public static NhanVien GetByID(int maNV)
        {
            string query = @"SELECT nv.*, pb.TenPhong, cd.TenChucDanh 
                            FROM NhanVien nv
                            LEFT JOIN PhongBan pb ON nv.MaPhong = pb.MaPhong
                            LEFT JOIN ChucDanh cd ON nv.MaChucDanh = cd.MaChucDanh
                            WHERE nv.MaNV = @MaNV";
            
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@MaNV", maNV);
            
            DataTable dt = DBConnection.ExecuteQuery(query, parameters);
            
            if (dt.Rows.Count == 0)
                return null;
                
            DataRow row = dt.Rows[0];
            NhanVien nv = new NhanVien();
            nv.MaNV = Convert.ToInt32(row["MaNV"]);
            nv.HoTen = row["HoTen"].ToString();
            nv.NgaySinh = Convert.ToDateTime(row["NgaySinh"]);
            nv.GioiTinh = row["GioiTinh"].ToString();
            nv.SoCMND = row["SoCMND"].ToString();
            nv.TrinhDoChuyenMon = row["TrinhDoChuyenMon"] != DBNull.Value ? row["TrinhDoChuyenMon"].ToString() : null;
            nv.TrinhDoNgoaiNgu = row["TrinhDoNgoaiNgu"] != DBNull.Value ? row["TrinhDoNgoaiNgu"].ToString() : null;
            nv.HoKhauThuongTru = row["HoKhauThuongTru"] != DBNull.Value ? row["HoKhauThuongTru"].ToString() : null;
            nv.DiaChi = row["DiaChi"] != DBNull.Value ? row["DiaChi"].ToString() : null;
            nv.DanToc = row["DanToc"] != DBNull.Value ? row["DanToc"].ToString() : null;
            nv.TonGiao = row["TonGiao"] != DBNull.Value ? row["TonGiao"].ToString() : null;
            nv.NgayVaoDoan = row["NgayVaoDoan"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(row["NgayVaoDoan"]) : null;
            nv.NgayVaoDang = row["NgayVaoDang"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(row["NgayVaoDang"]) : null;
            nv.DienChinhSach = row["DienChinhSach"] != DBNull.Value ? row["DienChinhSach"].ToString() : null;
            nv.MaPhong = Convert.ToInt32(row["MaPhong"]);
            nv.MaChucDanh = Convert.ToInt32(row["MaChucDanh"]);
            nv.LoaiNhanVien = row["LoaiNhanVien"].ToString();
            nv.NgayVaoCongTy = Convert.ToDateTime(row["NgayVaoCongTy"]);
            nv.NgayNghiViec = row["NgayNghiViec"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(row["NgayNghiViec"]) : null;
            nv.TrangThai = row["TrangThai"].ToString();
            nv.HinhAnh = row["HinhAnh"] != DBNull.Value ? (byte[])row["HinhAnh"] : null;
            
            // Thông tin phụ
            nv.TenPhong = row["TenPhong"] != DBNull.Value ? row["TenPhong"].ToString() : "";
            nv.TenChucDanh = row["TenChucDanh"] != DBNull.Value ? row["TenChucDanh"].ToString() : "";
            
            return nv;
        }
        
        public static int Insert(NhanVien nv)
        {
            string query = @"INSERT INTO NhanVien (HoTen, NgaySinh, GioiTinh, SoCMND, TrinhDoChuyenMon, 
                            TrinhDoNgoaiNgu, HoKhauThuongTru, DiaChi, DanToc, TonGiao, NgayVaoDoan, 
                            NgayVaoDang, DienChinhSach, MaPhong, MaChucDanh, LoaiNhanVien, NgayVaoCongTy, 
                            TrangThai, HinhAnh)
                            VALUES (@HoTen, @NgaySinh, @GioiTinh, @SoCMND, @TrinhDoChuyenMon, 
                            @TrinhDoNgoaiNgu, @HoKhauThuongTru, @DiaChi, @DanToc, @TonGiao, @NgayVaoDoan, 
                            @NgayVaoDang, @DienChinhSach, @MaPhong, @MaChucDanh, @LoaiNhanVien, @NgayVaoCongTy, 
                            @TrangThai, @HinhAnh);
                            SELECT SCOPE_IDENTITY();";
            
            SqlParameter[] parameters = new SqlParameter[19];
            parameters[0] = new SqlParameter("@HoTen", nv.HoTen);
            parameters[1] = new SqlParameter("@NgaySinh", nv.NgaySinh);
            parameters[2] = new SqlParameter("@GioiTinh", nv.GioiTinh);
            parameters[3] = new SqlParameter("@SoCMND", nv.SoCMND);
            parameters[4] = new SqlParameter("@TrinhDoChuyenMon", (object)nv.TrinhDoChuyenMon ?? DBNull.Value);
            parameters[5] = new SqlParameter("@TrinhDoNgoaiNgu", (object)nv.TrinhDoNgoaiNgu ?? DBNull.Value);
            parameters[6] = new SqlParameter("@HoKhauThuongTru", (object)nv.HoKhauThuongTru ?? DBNull.Value);
            parameters[7] = new SqlParameter("@DiaChi", (object)nv.DiaChi ?? DBNull.Value);
            parameters[8] = new SqlParameter("@DanToc", (object)nv.DanToc ?? DBNull.Value);
            parameters[9] = new SqlParameter("@TonGiao", (object)nv.TonGiao ?? DBNull.Value);
            parameters[10] = new SqlParameter("@NgayVaoDoan", (object)nv.NgayVaoDoan ?? DBNull.Value);
            parameters[11] = new SqlParameter("@NgayVaoDang", (object)nv.NgayVaoDang ?? DBNull.Value);
            parameters[12] = new SqlParameter("@DienChinhSach", (object)nv.DienChinhSach ?? DBNull.Value);
            parameters[13] = new SqlParameter("@MaPhong", nv.MaPhong);
            parameters[14] = new SqlParameter("@MaChucDanh", nv.MaChucDanh);
            parameters[15] = new SqlParameter("@LoaiNhanVien", nv.LoaiNhanVien);
            parameters[16] = new SqlParameter("@NgayVaoCongTy", nv.NgayVaoCongTy);
            parameters[17] = new SqlParameter("@TrangThai", nv.TrangThai);
            parameters[18] = new SqlParameter("@HinhAnh", (object)nv.HinhAnh ?? DBNull.Value);
            
            object result = DBConnection.ExecuteScalar(query, parameters);
            return Convert.ToInt32(result);
        }
        
        public static bool Update(NhanVien nv)
        {
            string query = @"UPDATE NhanVien SET 
                            HoTen = @HoTen, 
                            NgaySinh = @NgaySinh, 
                            GioiTinh = @GioiTinh, 
                            SoCMND = @SoCMND, 
                            TrinhDoChuyenMon = @TrinhDoChuyenMon, 
                            TrinhDoNgoaiNgu = @TrinhDoNgoaiNgu, 
                            HoKhauThuongTru = @HoKhauThuongTru, 
                            DiaChi = @DiaChi, 
                            DanToc = @DanToc, 
                            TonGiao = @TonGiao, 
                            NgayVaoDoan = @NgayVaoDoan, 
                            NgayVaoDang = @NgayVaoDang, 
                            DienChinhSach = @DienChinhSach, 
                            MaPhong = @MaPhong, 
                            MaChucDanh = @MaChucDanh, 
                            LoaiNhanVien = @LoaiNhanVien, 
                            NgayVaoCongTy = @NgayVaoCongTy, 
                            NgayNghiViec = @NgayNghiViec, 
                            TrangThai = @TrangThai, 
                            HinhAnh = @HinhAnh
                            WHERE MaNV = @MaNV";
            
            SqlParameter[] parameters = new SqlParameter[21];
            parameters[0] = new SqlParameter("@MaNV", nv.MaNV);
            parameters[1] = new SqlParameter("@HoTen", nv.HoTen);
            parameters[2] = new SqlParameter("@NgaySinh", nv.NgaySinh);
            parameters[3] = new SqlParameter("@GioiTinh", nv.GioiTinh);
            parameters[4] = new SqlParameter("@SoCMND", nv.SoCMND);
            parameters[5] = new SqlParameter("@TrinhDoChuyenMon", (object)nv.TrinhDoChuyenMon ?? DBNull.Value);
            parameters[6] = new SqlParameter("@TrinhDoNgoaiNgu", (object)nv.TrinhDoNgoaiNgu ?? DBNull.Value);
            parameters[7] = new SqlParameter("@HoKhauThuongTru", (object)nv.HoKhauThuongTru ?? DBNull.Value);
            parameters[8] = new SqlParameter("@DiaChi", (object)nv.DiaChi ?? DBNull.Value);
            parameters[9] = new SqlParameter("@DanToc", (object)nv.DanToc ?? DBNull.Value);
            parameters[10] = new SqlParameter("@TonGiao", (object)nv.TonGiao ?? DBNull.Value);
            parameters[11] = new SqlParameter("@NgayVaoDoan", (object)nv.NgayVaoDoan ?? DBNull.Value);
            parameters[12] = new SqlParameter("@NgayVaoDang", (object)nv.NgayVaoDang ?? DBNull.Value);
            parameters[13] = new SqlParameter("@DienChinhSach", (object)nv.DienChinhSach ?? DBNull.Value);
            parameters[14] = new SqlParameter("@MaPhong", nv.MaPhong);
            parameters[15] = new SqlParameter("@MaChucDanh", nv.MaChucDanh);
            parameters[16] = new SqlParameter("@LoaiNhanVien", nv.LoaiNhanVien);
            parameters[17] = new SqlParameter("@NgayVaoCongTy", nv.NgayVaoCongTy);
            parameters[18] = new SqlParameter("@NgayNghiViec", (object)nv.NgayNghiViec ?? DBNull.Value);
            parameters[19] = new SqlParameter("@TrangThai", nv.TrangThai);
            parameters[20] = new SqlParameter("@HinhAnh", (object)nv.HinhAnh ?? DBNull.Value);
            
            int result = DBConnection.ExecuteNonQuery(query, parameters);
            return result > 0;
        }
        
        public static bool Delete(int maNV)
        {
            string query = "UPDATE NhanVien SET TrangThai = N'Đã nghỉ việc' WHERE MaNV = @MaNV";
            
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@MaNV", maNV);
            
            int result = DBConnection.ExecuteNonQuery(query, parameters);
            return result > 0;
        }
        
        public static List<NhanVien> Search(string hoTen, int? tuoi, string diaChi, int? bacLuong, 
                                         string trinhDo, bool? isDangVien, int? maPhong, 
                                         string congViec, string dienChinhSach)
        {
            string query = @"SELECT nv.*, pb.TenPhong, cd.TenChucDanh 
                            FROM NhanVien nv
                            LEFT JOIN PhongBan pb ON nv.MaPhong = pb.MaPhong
                            LEFT JOIN ChucDanh cd ON nv.MaChucDanh = cd.MaChucDanh
                            WHERE nv.TrangThai = N'Đang làm việc'";
            
            List<SqlParameter> paramList = new List<SqlParameter>();
            
            if (!string.IsNullOrEmpty(hoTen))
            {
                query += " AND nv.HoTen LIKE @HoTen";
                paramList.Add(new SqlParameter("@HoTen", "%" + hoTen + "%"));
            }
            
            if (tuoi.HasValue)
            {
                query += " AND DATEDIFF(YEAR, nv.NgaySinh, GETDATE()) = @Tuoi";
                paramList.Add(new SqlParameter("@Tuoi", tuoi.Value));
            }
            
            if (!string.IsNullOrEmpty(diaChi))
            {
                query += " AND (nv.DiaChi LIKE @DiaChi OR nv.HoKhauThuongTru LIKE @DiaChi)";
                paramList.Add(new SqlParameter("@DiaChi", "%" + diaChi + "%"));
            }
            
            if (bacLuong.HasValue)
            {
                query += @" AND EXISTS (SELECT 1 FROM TangLuong tl 
                                      WHERE tl.MaNV = nv.MaNV 
                                      AND tl.MaBacLuongMoi = @BacLuong
                                      AND tl.NgayTangLuong = (SELECT MAX(NgayTangLuong) 
                                                           FROM TangLuong
                                                           WHERE MaNV = nv.MaNV))";
                paramList.Add(new SqlParameter("@BacLuong", bacLuong.Value));
            }
            
            if (!string.IsNullOrEmpty(trinhDo))
            {
                query += " AND nv.TrinhDoChuyenMon LIKE @TrinhDo";
                paramList.Add(new SqlParameter("@TrinhDo", "%" + trinhDo + "%"));
            }
            
            if (isDangVien.HasValue && isDangVien.Value)
            {
                query += " AND nv.NgayVaoDang IS NOT NULL";
            }
            
            if (maPhong.HasValue)
            {
                query += " AND nv.MaPhong = @MaPhong";
                paramList.Add(new SqlParameter("@MaPhong", maPhong.Value));
            }
            
            if (!string.IsNullOrEmpty(congViec))
            {
                query += @" AND EXISTS (SELECT 1 FROM QuaTrinhCongTac qt 
                                      WHERE qt.MaNV = nv.MaNV 
                                      AND qt.MoTaCongViec LIKE @CongViec)";
                paramList.Add(new SqlParameter("@CongViec", "%" + congViec + "%"));
            }
            
            if (!string.IsNullOrEmpty(dienChinhSach))
            {
                query += " AND nv.DienChinhSach LIKE @DienChinhSach";
                paramList.Add(new SqlParameter("@DienChinhSach", "%" + dienChinhSach + "%"));
            }
            
            query += " ORDER BY nv.HoTen";
            
            DataTable dt = DBConnection.ExecuteQuery(query, paramList.ToArray());
            
            List<NhanVien> list = new List<NhanVien>();
            foreach (DataRow row in dt.Rows)
            {
                NhanVien nv = new NhanVien();
                nv.MaNV = Convert.ToInt32(row["MaNV"]);
                nv.HoTen = row["HoTen"].ToString();
                nv.NgaySinh = Convert.ToDateTime(row["NgaySinh"]);
                nv.GioiTinh = row["GioiTinh"].ToString();
                nv.SoCMND = row["SoCMND"].ToString();
                nv.TrinhDoChuyenMon = row["TrinhDoChuyenMon"] != DBNull.Value ? row["TrinhDoChuyenMon"].ToString() : null;
                nv.TrinhDoNgoaiNgu = row["TrinhDoNgoaiNgu"] != DBNull.Value ? row["TrinhDoNgoaiNgu"].ToString() : null;
                nv.HoKhauThuongTru = row["HoKhauThuongTru"] != DBNull.Value ? row["HoKhauThuongTru"].ToString() : null;
                nv.DiaChi = row["DiaChi"] != DBNull.Value ? row["DiaChi"].ToString() : null;
                nv.DanToc = row["DanToc"] != DBNull.Value ? row["DanToc"].ToString() : null;
                nv.TonGiao = row["TonGiao"] != DBNull.Value ? row["TonGiao"].ToString() : null;
                nv.NgayVaoDoan = row["NgayVaoDoan"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(row["NgayVaoDoan"]) : null;
                nv.NgayVaoDang = row["NgayVaoDang"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(row["NgayVaoDang"]) : null;
                nv.DienChinhSach = row["DienChinhSach"] != DBNull.Value ? row["DienChinhSach"].ToString() : null;
                nv.MaPhong = Convert.ToInt32(row["MaPhong"]);
                nv.MaChucDanh = Convert.ToInt32(row["MaChucDanh"]);
                nv.LoaiNhanVien = row["LoaiNhanVien"].ToString();
                nv.NgayVaoCongTy = Convert.ToDateTime(row["NgayVaoCongTy"]);
                nv.NgayNghiViec = row["NgayNghiViec"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(row["NgayNghiViec"]) : null;
                nv.TrangThai = row["TrangThai"].ToString();
                nv.HinhAnh = row["HinhAnh"] != DBNull.Value ? (byte[])row["HinhAnh"] : null;
                
                // Thông tin phụ
                nv.TenPhong = row["TenPhong"] != DBNull.Value ? row["TenPhong"].ToString() : "";
                nv.TenChucDanh = row["TenChucDanh"] != DBNull.Value ? row["TenChucDanh"].ToString() : "";
                
                list.Add(nv);
            }
            
            return list;
        }
        
        public static List<NhanVien> GetBirthdayInMonth(int month)
        {
            string query = @"SELECT nv.*, pb.TenPhong, cd.TenChucDanh 
                            FROM NhanVien nv
                            LEFT JOIN PhongBan pb ON nv.MaPhong = pb.MaPhong
                            LEFT JOIN ChucDanh cd ON nv.MaChucDanh = cd.MaChucDanh
                            WHERE nv.TrangThai = N'Đang làm việc'
                            AND MONTH(nv.NgaySinh) = @Month
                            ORDER BY DAY(nv.NgaySinh)";
            
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@Month", month);
            
            DataTable dt = DBConnection.ExecuteQuery(query, parameters);
            
            List<NhanVien> list = new List<NhanVien>();
            foreach (DataRow row in dt.Rows)
            {
                NhanVien nv = new NhanVien();
                nv.MaNV = Convert.ToInt32(row["MaNV"]);
                nv.HoTen = row["HoTen"].ToString();
                nv.NgaySinh = Convert.ToDateTime(row["NgaySinh"]);
                nv.GioiTinh = row["GioiTinh"].ToString();
                nv.SoCMND = row["SoCMND"].ToString();
                nv.TrinhDoChuyenMon = row["TrinhDoChuyenMon"] != DBNull.Value ? row["TrinhDoChuyenMon"].ToString() : null;
                nv.TrinhDoNgoaiNgu = row["TrinhDoNgoaiNgu"] != DBNull.Value ? row["TrinhDoNgoaiNgu"].ToString() : null;
                nv.HoKhauThuongTru = row["HoKhauThuongTru"] != DBNull.Value ? row["HoKhauThuongTru"].ToString() : null;
                nv.DiaChi = row["DiaChi"] != DBNull.Value ? row["DiaChi"].ToString() : null;
                nv.DanToc = row["DanToc"] != DBNull.Value ? row["DanToc"].ToString() : null;
                nv.TonGiao = row["TonGiao"] != DBNull.Value ? row["TonGiao"].ToString() : null;
                nv.NgayVaoDoan = row["NgayVaoDoan"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(row["NgayVaoDoan"]) : null;
                nv.NgayVaoDang = row["NgayVaoDang"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(row["NgayVaoDang"]) : null;
                nv.DienChinhSach = row["DienChinhSach"] != DBNull.Value ? row["DienChinhSach"].ToString() : null;
                nv.MaPhong = Convert.ToInt32(row["MaPhong"]);
                nv.MaChucDanh = Convert.ToInt32(row["MaChucDanh"]);
                nv.LoaiNhanVien = row["LoaiNhanVien"].ToString();
                nv.NgayVaoCongTy = Convert.ToDateTime(row["NgayVaoCongTy"]);
                nv.NgayNghiViec = row["NgayNghiViec"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(row["NgayNghiViec"]) : null;
                nv.TrangThai = row["TrangThai"].ToString();
                nv.HinhAnh = row["HinhAnh"] != DBNull.Value ? (byte[])row["HinhAnh"] : null;
                
                // Thông tin phụ
                nv.TenPhong = row["TenPhong"] != DBNull.Value ? row["TenPhong"].ToString() : "";
                nv.TenChucDanh = row["TenChucDanh"] != DBNull.Value ? row["TenChucDanh"].ToString() : "";
                
                list.Add(nv);
            }
            
            return list;
        }
        
        public static List<NhanVien> GetRetirementList()
        {
            string query = @"SELECT nv.*, pb.TenPhong, cd.TenChucDanh 
                            FROM NhanVien nv
                            LEFT JOIN PhongBan pb ON nv.MaPhong = pb.MaPhong
                            LEFT JOIN ChucDanh cd ON nv.MaChucDanh = cd.MaChucDanh
                            WHERE nv.TrangThai = N'Đang làm việc'
                            AND (
                                (nv.GioiTinh = N'Nam' AND DATEDIFF(YEAR, nv.NgaySinh, GETDATE()) >= 60)
                                OR
                                (nv.GioiTinh = N'Nữ' AND DATEDIFF(YEAR, nv.NgaySinh, GETDATE()) >= 55)
                            )
                            ORDER BY nv.NgaySinh";
            
            DataTable dt = DBConnection.ExecuteQuery(query);
            
            List<NhanVien> list = new List<NhanVien>();
            foreach (DataRow row in dt.Rows)
            {
                NhanVien nv = new NhanVien();
                nv.MaNV = Convert.ToInt32(row["MaNV"]);
                nv.HoTen = row["HoTen"].ToString();
                nv.NgaySinh = Convert.ToDateTime(row["NgaySinh"]);
                nv.GioiTinh = row["GioiTinh"].ToString();
                nv.SoCMND = row["SoCMND"].ToString();
                nv.TrinhDoChuyenMon = row["TrinhDoChuyenMon"] != DBNull.Value ? row["TrinhDoChuyenMon"].ToString() : null;
                nv.TrinhDoNgoaiNgu = row["TrinhDoNgoaiNgu"] != DBNull.Value ? row["TrinhDoNgoaiNgu"].ToString() : null;
                nv.HoKhauThuongTru = row["HoKhauThuongTru"] != DBNull.Value ? row["HoKhauThuongTru"].ToString() : null;
                nv.DiaChi = row["DiaChi"] != DBNull.Value ? row["DiaChi"].ToString() : null;
                nv.DanToc = row["DanToc"] != DBNull.Value ? row["DanToc"].ToString() : null;
                nv.TonGiao = row["TonGiao"] != DBNull.Value ? row["TonGiao"].ToString() : null;
                nv.NgayVaoDoan = row["NgayVaoDoan"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(row["NgayVaoDoan"]) : null;
                nv.NgayVaoDang = row["NgayVaoDang"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(row["NgayVaoDang"]) : null;
                nv.DienChinhSach = row["DienChinhSach"] != DBNull.Value ? row["DienChinhSach"].ToString() : null;
                nv.MaPhong = Convert.ToInt32(row["MaPhong"]);
                nv.MaChucDanh = Convert.ToInt32(row["MaChucDanh"]);
                nv.LoaiNhanVien = row["LoaiNhanVien"].ToString();
                nv.NgayVaoCongTy = Convert.ToDateTime(row["NgayVaoCongTy"]);
                nv.NgayNghiViec = row["NgayNghiViec"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(row["NgayNghiViec"]) : null;
                nv.TrangThai = row["TrangThai"].ToString();
                nv.HinhAnh = row["HinhAnh"] != DBNull.Value ? (byte[])row["HinhAnh"] : null;
                
                // Thông tin phụ
                nv.TenPhong = row["TenPhong"] != DBNull.Value ? row["TenPhong"].ToString() : "";
                nv.TenChucDanh = row["TenChucDanh"] != DBNull.Value ? row["TenChucDanh"].ToString() : "";
                
                list.Add(nv);
            }
            
            return list;
        }
        
        public static List<NhanVien> GetPaged(int pageNumber, int pageSize, out int totalRecords)
        {
            string countQuery = @"SELECT COUNT(*) 
                                FROM NhanVien nv
                                WHERE nv.TrangThai = N'Đang làm việc'";
            
            totalRecords = Convert.ToInt32(DBConnection.ExecuteScalar(countQuery));
            
            string query = @"SELECT nv.*, pb.TenPhong, cd.TenChucDanh 
                            FROM NhanVien nv
                            LEFT JOIN PhongBan pb ON nv.MaPhong = pb.MaPhong
                            LEFT JOIN ChucDanh cd ON nv.MaChucDanh = cd.MaChucDanh
                            WHERE nv.TrangThai = N'Đang làm việc'
                            ORDER BY nv.HoTen
                            OFFSET @Offset ROWS
                            FETCH NEXT @PageSize ROWS ONLY";
            
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("@Offset", (pageNumber - 1) * pageSize);
            parameters[1] = new SqlParameter("@PageSize", pageSize);
            
            DataTable dt = DBConnection.ExecuteQuery(query, parameters);
            
            List<NhanVien> list = new List<NhanVien>();
            foreach (DataRow row in dt.Rows)
            {
                NhanVien nv = new NhanVien();
                nv.MaNV = Convert.ToInt32(row["MaNV"]);
                nv.HoTen = row["HoTen"].ToString();
                nv.NgaySinh = Convert.ToDateTime(row["NgaySinh"]);
                nv.GioiTinh = row["GioiTinh"].ToString();
                nv.SoCMND = row["SoCMND"].ToString();
                nv.TrinhDoChuyenMon = row["TrinhDoChuyenMon"] != DBNull.Value ? row["TrinhDoChuyenMon"].ToString() : null;
                nv.TrinhDoNgoaiNgu = row["TrinhDoNgoaiNgu"] != DBNull.Value ? row["TrinhDoNgoaiNgu"].ToString() : null;
                nv.HoKhauThuongTru = row["HoKhauThuongTru"] != DBNull.Value ? row["HoKhauThuongTru"].ToString() : null;
                nv.DiaChi = row["DiaChi"] != DBNull.Value ? row["DiaChi"].ToString() : null;
                nv.DanToc = row["DanToc"] != DBNull.Value ? row["DanToc"].ToString() : null;
                nv.TonGiao = row["TonGiao"] != DBNull.Value ? row["TonGiao"].ToString() : null;
                nv.NgayVaoDoan = row["NgayVaoDoan"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(row["NgayVaoDoan"]) : null;
                nv.NgayVaoDang = row["NgayVaoDang"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(row["NgayVaoDang"]) : null;
                nv.DienChinhSach = row["DienChinhSach"] != DBNull.Value ? row["DienChinhSach"].ToString() : null;
                nv.MaPhong = Convert.ToInt32(row["MaPhong"]);
                nv.MaChucDanh = Convert.ToInt32(row["MaChucDanh"]);
                nv.LoaiNhanVien = row["LoaiNhanVien"].ToString();
                nv.NgayVaoCongTy = Convert.ToDateTime(row["NgayVaoCongTy"]);
                nv.NgayNghiViec = row["NgayNghiViec"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(row["NgayNghiViec"]) : null;
                nv.TrangThai = row["TrangThai"].ToString();
                nv.HinhAnh = row["HinhAnh"] != DBNull.Value ? (byte[])row["HinhAnh"] : null;
                
                // Thông tin phụ
                nv.TenPhong = row["TenPhong"] != DBNull.Value ? row["TenPhong"].ToString() : "";
                nv.TenChucDanh = row["TenChucDanh"] != DBNull.Value ? row["TenChucDanh"].ToString() : "";
                
                list.Add(nv);
            }
            
            return list;
        }
        
        public static List<NhanVien> GetPagedAndFiltered(int pageNumber, int pageSize, out int totalRecords,
                                               string hoTen = null, string phong = null, string chucDanh = null)
        {
            // Bộ lọc WHERE cho các điều kiện tìm kiếm
            string whereClause = "nv.TrangThai = N'Đang làm việc'";
            
            if (!string.IsNullOrEmpty(hoTen))
            {
                whereClause += " AND nv.HoTen LIKE @HoTen";
            }

            if (!string.IsNullOrEmpty(phong))
            {
                whereClause += " AND nv.MaPhong = @MaPhong";
            }

            if (!string.IsNullOrEmpty(chucDanh))
            {
                whereClause += " AND nv.MaChucDanh = @MaChucDanh";
            }

            // SQL để đếm tổng số bản ghi thỏa mãn điều kiện
            string countQuery = "SELECT COUNT(*) FROM NhanVien nv WHERE " + whereClause;

            // Tạo danh sách các tham số cho truy vấn đếm
            List<SqlParameter> countParamList = new List<SqlParameter>();
            if (!string.IsNullOrEmpty(hoTen))
            {
                countParamList.Add(new SqlParameter("@HoTen", "%" + hoTen + "%"));
            }
            if (!string.IsNullOrEmpty(phong))
            {
                countParamList.Add(new SqlParameter("@MaPhong", Convert.ToInt32(phong)));
            }
            if (!string.IsNullOrEmpty(chucDanh))
            {
                countParamList.Add(new SqlParameter("@MaChucDanh", Convert.ToInt32(chucDanh)));
            }

            // Lấy tổng số bản ghi thỏa mãn điều kiện
            totalRecords = Convert.ToInt32(DBConnection.ExecuteScalar(countQuery, countParamList.ToArray()));

            // SQL để lấy dữ liệu phân trang với điều kiện lọc
            string query = @"SELECT 
                            nv.MaNV, nv.HoTen, nv.NgaySinh, nv.GioiTinh, nv.SoCMND, 
                            nv.TrinhDoChuyenMon, nv.TrinhDoNgoaiNgu, nv.HoKhauThuongTru, 
                            nv.DiaChi, nv.DanToc, nv.TonGiao, nv.NgayVaoDoan, 
                            nv.NgayVaoDang, nv.DienChinhSach, nv.MaPhong, nv.MaChucDanh, 
                            nv.LoaiNhanVien, nv.NgayVaoCongTy, nv.NgayNghiViec, 
                            nv.TrangThai, nv.HinhAnh, 
                            pb.TenPhong, cd.TenChucDanh
                            FROM NhanVien nv
                            LEFT JOIN PhongBan pb ON nv.MaPhong = pb.MaPhong
                            LEFT JOIN ChucDanh cd ON nv.MaChucDanh = cd.MaChucDanh
                            WHERE " + whereClause + @"
                            ORDER BY nv.HoTen
                            OFFSET @Offset ROWS
                            FETCH NEXT @PageSize ROWS ONLY";

            // Tạo danh sách tham số mới cho truy vấn dữ liệu phân trang
            List<SqlParameter> queryParamList = new List<SqlParameter>();
            if (!string.IsNullOrEmpty(hoTen))
            {
                queryParamList.Add(new SqlParameter("@HoTen", "%" + hoTen + "%"));
            }
            if (!string.IsNullOrEmpty(phong))
            {
                queryParamList.Add(new SqlParameter("@MaPhong", Convert.ToInt32(phong)));
            }
            if (!string.IsNullOrEmpty(chucDanh))
            {
                queryParamList.Add(new SqlParameter("@MaChucDanh", Convert.ToInt32(chucDanh)));
            }
            queryParamList.Add(new SqlParameter("@Offset", (pageNumber - 1) * pageSize));
            queryParamList.Add(new SqlParameter("@PageSize", pageSize));

            // Thực hiện truy vấn và lấy kết quả
            DataTable dt = DBConnection.ExecuteQuery(query, queryParamList.ToArray());

            // Chuyển đổi kết quả thành danh sách đối tượng NhanVien
            List<NhanVien> list = new List<NhanVien>();
            foreach (DataRow row in dt.Rows)
            {
                NhanVien nv = new NhanVien();
                
                try
                {
                    nv.MaNV = Convert.ToInt32(row["MaNV"]);
                    nv.HoTen = row["HoTen"] != DBNull.Value ? row["HoTen"].ToString() : "";
                    nv.NgaySinh = Convert.ToDateTime(row["NgaySinh"]);
                    nv.GioiTinh = row["GioiTinh"] != DBNull.Value ? row["GioiTinh"].ToString() : "";
                    nv.SoCMND = row["SoCMND"] != DBNull.Value ? row["SoCMND"].ToString() : "";
                    nv.TrinhDoChuyenMon = row["TrinhDoChuyenMon"] != DBNull.Value ? row["TrinhDoChuyenMon"].ToString() : null;
                    nv.TrinhDoNgoaiNgu = row["TrinhDoNgoaiNgu"] != DBNull.Value ? row["TrinhDoNgoaiNgu"].ToString() : null;
                    nv.HoKhauThuongTru = row["HoKhauThuongTru"] != DBNull.Value ? row["HoKhauThuongTru"].ToString() : null;
                    nv.DiaChi = row["DiaChi"] != DBNull.Value ? row["DiaChi"].ToString() : null;
                    nv.DanToc = row["DanToc"] != DBNull.Value ? row["DanToc"].ToString() : null;
                    nv.TonGiao = row["TonGiao"] != DBNull.Value ? row["TonGiao"].ToString() : null;
                    nv.NgayVaoDoan = row["NgayVaoDoan"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(row["NgayVaoDoan"]) : null;
                    nv.NgayVaoDang = row["NgayVaoDang"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(row["NgayVaoDang"]) : null;
                    nv.DienChinhSach = row["DienChinhSach"] != DBNull.Value ? row["DienChinhSach"].ToString() : null;
                    nv.MaPhong = Convert.ToInt32(row["MaPhong"]);
                    nv.MaChucDanh = Convert.ToInt32(row["MaChucDanh"]);
                    nv.LoaiNhanVien = row["LoaiNhanVien"] != DBNull.Value ? row["LoaiNhanVien"].ToString() : "";
                    nv.NgayVaoCongTy = Convert.ToDateTime(row["NgayVaoCongTy"]);
                    nv.NgayNghiViec = row["NgayNghiViec"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(row["NgayNghiViec"]) : null;
                    nv.TrangThai = row["TrangThai"] != DBNull.Value ? row["TrangThai"].ToString() : "";
                    nv.HinhAnh = row["HinhAnh"] != DBNull.Value ? (byte[])row["HinhAnh"] : null;
                    
                    // Thông tin phụ
                    nv.TenPhong = row["TenPhong"] != DBNull.Value ? row["TenPhong"].ToString() : "";
                    nv.TenChucDanh = row["TenChucDanh"] != DBNull.Value ? row["TenChucDanh"].ToString() : "";
                }
                catch (Exception ex)
                {
                    throw new Exception("Lỗi khi chuyển đổi dữ liệu: " + ex.Message);
                }
                
                list.Add(nv);
            }

            return list;
        }

        /// <summary>
        /// Lấy danh sách nhân viên theo phòng ban
        /// </summary>
        /// <param name="maPhong">Mã phòng ban</param>
        /// <returns>DataTable chứa danh sách nhân viên</returns>
        public static DataTable GetByPhongBan(int maPhong)
        {
            string query = "SELECT * FROM NhanVien WHERE MaPhong = @MaPhong";
            
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaPhong", maPhong)
            };
            
            return DBConnection.ExecuteQuery(query, parameters);
        }

        /// <summary>
        /// Lấy danh sách nhân viên theo chức danh
        /// </summary>
        /// <param name="maChucDanh">Mã chức danh</param>
        /// <returns>DataTable chứa danh sách nhân viên</returns>
        public static DataTable GetByChucDanh(int maChucDanh)
        {
            string query = "SELECT * FROM NhanVien WHERE MaChucDanh = @MaChucDanh";
            
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaChucDanh", maChucDanh)
            };
            
            return DBConnection.ExecuteQuery(query, parameters);
        }
    }
} 