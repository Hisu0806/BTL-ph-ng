using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using HRM.DTO;
using HRM.Utility;

namespace HRM.DAL
{
    public class ChamCongDAL
    {
        /// <summary>
        /// Lấy danh sách chấm công theo ngày
        /// </summary>
        /// <param name="ngay">Ngày chấm công</param>
        /// <returns>Danh sách chấm công</returns>
        public static List<ChamCong> LayDanhSachTheoNgay(DateTime ngay)
        {
            List<ChamCong> listChamCong = new List<ChamCong>();
            try
            {
                string sql = @"SELECT cc.MaChamCong, cc.MaNV, cc.NgayChamCong, cc.GioVao, cc.GioRa, cc.TrangThai,
                                nv.HoTen as HoTenNV, pb.TenPhong, cd.TenChucDanh
                               FROM ChamCong cc
                               INNER JOIN NhanVien nv ON cc.MaNV = nv.MaNV
                               INNER JOIN PhongBan pb ON nv.MaPhong = pb.MaPhong
                               INNER JOIN ChucDanh cd ON nv.MaChucDanh = cd.MaChucDanh
                               WHERE CONVERT(date, cc.NgayChamCong) = @Ngay
                               ORDER BY nv.HoTen";

                SqlParameter[] parameters = new SqlParameter[] {
                    new SqlParameter("@Ngay", SqlDbType.Date) { Value = ngay.Date }
                };

                DataTable dt = SqlHelper.ExecuteDataTable(sql, parameters);
                foreach (DataRow row in dt.Rows)
                {
                    ChamCong chamCong = new ChamCong();
                    chamCong.MaChamCong = Convert.ToInt32(row["MaChamCong"]);
                    chamCong.MaNV = Convert.ToInt32(row["MaNV"]);
                    chamCong.NgayChamCong = Convert.ToDateTime(row["NgayChamCong"]);
                    chamCong.GioVao = row["GioVao"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(row["GioVao"]) : null;
                    chamCong.GioRa = row["GioRa"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(row["GioRa"]) : null;
                    chamCong.TrangThai = row["TrangThai"].ToString();
                    chamCong.HoTenNV = row["HoTenNV"].ToString();
                    chamCong.TenPhong = row["TenPhong"].ToString();
                    chamCong.TenChucDanh = row["TenChucDanh"].ToString();
                    listChamCong.Add(chamCong);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listChamCong;
        }

        /// <summary>
        /// Thêm bản ghi chấm công mới
        /// </summary>
        /// <param name="chamCong">Đối tượng chấm công</param>
        /// <returns>Id của bản ghi vừa thêm</returns>
        public static int ThemChamCong(ChamCong chamCong)
        {
            try
            {
                string sql = @"INSERT INTO ChamCong (MaNV, NgayChamCong, GioVao, GioRa, TrangThai)
                               VALUES (@MaNV, @NgayChamCong, @GioVao, @GioRa, @TrangThai);
                               SELECT SCOPE_IDENTITY()";

                SqlParameter[] parameters = new SqlParameter[] {
                    new SqlParameter("@MaNV", SqlDbType.Int) { Value = chamCong.MaNV },
                    new SqlParameter("@NgayChamCong", SqlDbType.DateTime) { Value = chamCong.NgayChamCong },
                    new SqlParameter("@GioVao", SqlDbType.DateTime) { Value = (object)chamCong.GioVao ?? DBNull.Value },
                    new SqlParameter("@GioRa", SqlDbType.DateTime) { Value = (object)chamCong.GioRa ?? DBNull.Value },
                    new SqlParameter("@TrangThai", SqlDbType.NVarChar, 50) { Value = chamCong.TrangThai }
                };

                object result = SqlHelper.ExecuteScalar(sql, parameters);
                return Convert.ToInt32(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Cập nhật bản ghi chấm công
        /// </summary>
        /// <param name="chamCong">Đối tượng chấm công</param>
        /// <returns>Số bản ghi bị ảnh hưởng</returns>
        public static int CapNhatChamCong(ChamCong chamCong)
        {
            try
            {
                string sql = @"UPDATE ChamCong
                               SET GioVao = @GioVao, GioRa = @GioRa, TrangThai = @TrangThai
                               WHERE MaChamCong = @MaChamCong";

                SqlParameter[] parameters = new SqlParameter[] {
                    new SqlParameter("@MaChamCong", SqlDbType.Int) { Value = chamCong.MaChamCong },
                    new SqlParameter("@GioVao", SqlDbType.DateTime) { Value = (object)chamCong.GioVao ?? DBNull.Value },
                    new SqlParameter("@GioRa", SqlDbType.DateTime) { Value = (object)chamCong.GioRa ?? DBNull.Value },
                    new SqlParameter("@TrangThai", SqlDbType.NVarChar, 50) { Value = chamCong.TrangThai }
                };

                return SqlHelper.ExecuteNonQuery(sql, parameters);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Xóa bản ghi chấm công
        /// </summary>
        /// <param name="maChamCong">Mã chấm công</param>
        /// <returns>Số bản ghi bị ảnh hưởng</returns>
        public static int XoaChamCong(int maChamCong)
        {
            try
            {
                string sql = "DELETE FROM ChamCong WHERE MaChamCong = @MaChamCong";
                SqlParameter[] parameters = new SqlParameter[] {
                    new SqlParameter("@MaChamCong", SqlDbType.Int) { Value = maChamCong }
                };

                return SqlHelper.ExecuteNonQuery(sql, parameters);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Kiểm tra xem nhân viên đã được chấm công trong ngày hay chưa
        /// </summary>
        /// <param name="maNV">Mã nhân viên</param>
        /// <param name="ngay">Ngày chấm công</param>
        /// <returns>True nếu đã có bản ghi chấm công</returns>
        public static bool KiemTraDaChamCong(int maNV, DateTime ngay)
        {
            try
            {
                string sql = @"SELECT COUNT(*) FROM ChamCong 
                               WHERE MaNV = @MaNV AND CONVERT(date, NgayChamCong) = @Ngay";

                SqlParameter[] parameters = new SqlParameter[] {
                    new SqlParameter("@MaNV", SqlDbType.Int) { Value = maNV },
                    new SqlParameter("@Ngay", SqlDbType.Date) { Value = ngay.Date }
                };

                int count = Convert.ToInt32(SqlHelper.ExecuteScalar(sql, parameters));
                return count > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<ChamCong> GetAll()
        {
            string query = @"SELECT cc.*, nv.HoTen, pb.TenPhong, cd.TenChucDanh
                            FROM ChamCong cc
                            JOIN NhanVien nv ON cc.MaNV = nv.MaNV
                            JOIN PhongBan pb ON nv.MaPhong = pb.MaPhong
                            JOIN ChucDanh cd ON nv.MaChucDanh = cd.MaChucDanh
                            ORDER BY cc.NgayChamCong DESC, nv.HoTen";
            
            DataTable dt = DBConnection.ExecuteQuery(query);
            
            List<ChamCong> list = new List<ChamCong>();
            foreach (DataRow row in dt.Rows)
            {
                ChamCong cc = CreateFromDataRow(row);
                list.Add(cc);
            }
            
            return list;
        }
        
        public static ChamCong GetByID(int maChamCong)
        {
            string query = @"SELECT cc.*, nv.HoTen, pb.TenPhong, cd.TenChucDanh
                            FROM ChamCong cc
                            JOIN NhanVien nv ON cc.MaNV = nv.MaNV
                            JOIN PhongBan pb ON nv.MaPhong = pb.MaPhong
                            JOIN ChucDanh cd ON nv.MaChucDanh = cd.MaChucDanh
                            WHERE cc.MaChamCong = @MaChamCong";
            
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@MaChamCong", maChamCong);
            
            DataTable dt = DBConnection.ExecuteQuery(query, parameters);
            
            if (dt.Rows.Count == 0)
                return null;
                
            return CreateFromDataRow(dt.Rows[0]);
        }
        
        public static List<ChamCong> GetByDate(DateTime ngayChamCong)
        {
            string query = @"SELECT cc.*, nv.HoTen, pb.TenPhong, cd.TenChucDanh
                            FROM ChamCong cc
                            JOIN NhanVien nv ON cc.MaNV = nv.MaNV
                            JOIN PhongBan pb ON nv.MaPhong = pb.MaPhong
                            JOIN ChucDanh cd ON nv.MaChucDanh = cd.MaChucDanh
                            WHERE cc.NgayChamCong = @NgayChamCong
                            ORDER BY nv.HoTen";
            
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@NgayChamCong", ngayChamCong);
            
            DataTable dt = DBConnection.ExecuteQuery(query, parameters);
            
            List<ChamCong> list = new List<ChamCong>();
            foreach (DataRow row in dt.Rows)
            {
                ChamCong cc = CreateFromDataRow(row);
                list.Add(cc);
            }
            
            return list;
        }
        
        public static List<ChamCong> GetByMonthYear(int month, int year)
        {
            string query = @"SELECT cc.*, nv.HoTen, pb.TenPhong, cd.TenChucDanh
                            FROM ChamCong cc
                            JOIN NhanVien nv ON cc.MaNV = nv.MaNV
                            JOIN PhongBan pb ON nv.MaPhong = pb.MaPhong
                            JOIN ChucDanh cd ON nv.MaChucDanh = cd.MaChucDanh
                            WHERE MONTH(cc.NgayChamCong) = @Month AND YEAR(cc.NgayChamCong) = @Year
                            ORDER BY cc.NgayChamCong, nv.HoTen";
            
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("@Month", month);
            parameters[1] = new SqlParameter("@Year", year);
            
            DataTable dt = DBConnection.ExecuteQuery(query, parameters);
            
            List<ChamCong> list = new List<ChamCong>();
            foreach (DataRow row in dt.Rows)
            {
                ChamCong cc = CreateFromDataRow(row);
                list.Add(cc);
            }
            
            return list;
        }
        
        public static List<ChamCong> GetByNhanVienAndMonthYear(int maNV, int month, int year)
        {
            string query = @"SELECT cc.*, nv.HoTen, pb.TenPhong, cd.TenChucDanh
                            FROM ChamCong cc
                            JOIN NhanVien nv ON cc.MaNV = nv.MaNV
                            JOIN PhongBan pb ON nv.MaPhong = pb.MaPhong
                            JOIN ChucDanh cd ON nv.MaChucDanh = cd.MaChucDanh
                            WHERE cc.MaNV = @MaNV AND MONTH(cc.NgayChamCong) = @Month AND YEAR(cc.NgayChamCong) = @Year
                            ORDER BY cc.NgayChamCong";
            
            SqlParameter[] parameters = new SqlParameter[3];
            parameters[0] = new SqlParameter("@MaNV", maNV);
            parameters[1] = new SqlParameter("@Month", month);
            parameters[2] = new SqlParameter("@Year", year);
            
            DataTable dt = DBConnection.ExecuteQuery(query, parameters);
            
            List<ChamCong> list = new List<ChamCong>();
            foreach (DataRow row in dt.Rows)
            {
                ChamCong cc = CreateFromDataRow(row);
                list.Add(cc);
            }
            
            return list;
        }
        
        public static int Insert(ChamCong cc)
        {
            string query = @"INSERT INTO ChamCong (MaNV, NgayChamCong, GioVao, GioRa, TrangThai)
                            VALUES (@MaNV, @NgayChamCong, @GioVao, @GioRa, @TrangThai);
                            SELECT SCOPE_IDENTITY();";
            
            SqlParameter[] parameters = new SqlParameter[5];
            parameters[0] = new SqlParameter("@MaNV", cc.MaNV);
            parameters[1] = new SqlParameter("@NgayChamCong", cc.NgayChamCong);
            parameters[2] = new SqlParameter("@GioVao", (object)cc.GioVao ?? DBNull.Value);
            parameters[3] = new SqlParameter("@GioRa", (object)cc.GioRa ?? DBNull.Value);
            parameters[4] = new SqlParameter("@TrangThai", cc.TrangThai);
            
            object result = DBConnection.ExecuteScalar(query, parameters);
            return Convert.ToInt32(result);
        }
        
        public static bool Update(ChamCong cc)
        {
            string query = @"UPDATE ChamCong
                            SET MaNV = @MaNV,
                                NgayChamCong = @NgayChamCong,
                                GioVao = @GioVao,
                                GioRa = @GioRa,
                                TrangThai = @TrangThai
                            WHERE MaChamCong = @MaChamCong";
            
            SqlParameter[] parameters = new SqlParameter[6];
            parameters[0] = new SqlParameter("@MaChamCong", cc.MaChamCong);
            parameters[1] = new SqlParameter("@MaNV", cc.MaNV);
            parameters[2] = new SqlParameter("@NgayChamCong", cc.NgayChamCong);
            parameters[3] = new SqlParameter("@GioVao", (object)cc.GioVao ?? DBNull.Value);
            parameters[4] = new SqlParameter("@GioRa", (object)cc.GioRa ?? DBNull.Value);
            parameters[5] = new SqlParameter("@TrangThai", cc.TrangThai);
            
            int rowsAffected = DBConnection.ExecuteNonQuery(query, parameters);
            return rowsAffected > 0;
        }
        
        public static bool Delete(int maChamCong)
        {
            string query = "DELETE FROM ChamCong WHERE MaChamCong = @MaChamCong";
            
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@MaChamCong", maChamCong);
            
            int rowsAffected = DBConnection.ExecuteNonQuery(query, parameters);
            return rowsAffected > 0;
        }
        
        public static int GetWorkingDaysInMonth(int maNV, int month, int year)
        {
            string query = @"SELECT COUNT(*) 
                            FROM ChamCong 
                            WHERE MaNV = @MaNV 
                            AND MONTH(NgayChamCong) = @Month 
                            AND YEAR(NgayChamCong) = @Year 
                            AND TrangThai = N'Có mặt'";
            
            SqlParameter[] parameters = new SqlParameter[3];
            parameters[0] = new SqlParameter("@MaNV", maNV);
            parameters[1] = new SqlParameter("@Month", month);
            parameters[2] = new SqlParameter("@Year", year);
            
            object result = DBConnection.ExecuteScalar(query, parameters);
            return Convert.ToInt32(result);
        }
        
        private static ChamCong CreateFromDataRow(DataRow row)
        {
            ChamCong cc = new ChamCong();
            cc.MaChamCong = Convert.ToInt32(row["MaChamCong"]);
            cc.MaNV = Convert.ToInt32(row["MaNV"]);
            cc.NgayChamCong = Convert.ToDateTime(row["NgayChamCong"]);
            cc.GioVao = row["GioVao"] != DBNull.Value ? (TimeSpan?)TimeSpan.Parse(row["GioVao"].ToString()) : null;
            cc.GioRa = row["GioRa"] != DBNull.Value ? (TimeSpan?)TimeSpan.Parse(row["GioRa"].ToString()) : null;
            cc.TrangThai = row["TrangThai"].ToString();
            
            // Thông tin phụ
            cc.HoTenNV = row["HoTen"] != DBNull.Value ? row["HoTen"].ToString() : "";
            cc.TenPhong = row["TenPhong"] != DBNull.Value ? row["TenPhong"].ToString() : "";
            cc.TenChucDanh = row["TenChucDanh"] != DBNull.Value ? row["TenChucDanh"].ToString() : "";
            
            return cc;
        }
    }
} 