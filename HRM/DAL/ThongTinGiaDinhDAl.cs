using System;
using System.Data;
using System.Data.SqlClient;
using HRM.DTO;

namespace HRM.DAL
{
    public class ThongTinGiaDinhDAL
    {
        public static DataTable GetList()
        {
            string query = @"SELECT ttg.*, nv.HoTen as TenNV 
                           FROM ThongTinGiaDinh ttg 
                           LEFT JOIN NhanVien nv ON ttg.MaNV = nv.MaNV 
                           ORDER BY ttg.MaThongTin DESC";

            return DBConnection.ExecuteQuery(query);
        }

        public static DataTable GetByMaNV(int maNV)
        {
            string query = @"SELECT ttg.*, nv.HoTen as TenNV 
                           FROM ThongTinGiaDinh ttg 
                           LEFT JOIN NhanVien nv ON ttg.MaNV = nv.MaNV 
                           WHERE ttg.MaNV = @MaNV 
                           ORDER BY ttg.MaThongTin DESC";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaNV", maNV)
            };

            return DBConnection.ExecuteQuery(query, parameters);
        }

        public static ThongTinGiaDinhDTO GetByID(int maThongTin)
        {
            string query = @"SELECT ttg.*, nv.HoTen as TenNV 
                           FROM ThongTinGiaDinh ttg 
                           LEFT JOIN NhanVien nv ON ttg.MaNV = nv.MaNV 
                           WHERE ttg.MaThongTin = @MaThongTin";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaThongTin", maThongTin)
            };

            DataTable dt = DBConnection.ExecuteQuery(query, parameters);
            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                return new ThongTinGiaDinhDTO
                {
                    MaThongTin = Convert.ToInt32(row["MaThongTin"]),
                    MaNV = Convert.ToInt32(row["MaNV"]),
                    HoTen = row["HoTen"].ToString(),
                    QuanHe = row["QuanHe"].ToString(),
                    NgaySinh = row["NgaySinh"] == DBNull.Value ? null : (DateTime?)Convert.ToDateTime(row["NgaySinh"]),
                    NgheNghiep = row["NgheNghiep"]?.ToString(),
                    DiaChi = row["DiaChi"]?.ToString(),
                    GhiChu = row["GhiChu"]?.ToString()
                };
            }
            return null;
        }

        public static int Add(ThongTinGiaDinhDTO ttg)
        {
            string query = @"INSERT INTO ThongTinGiaDinh(MaNV, HoTen, QuanHe, NgaySinh, NgheNghiep, DiaChi, GhiChu) 
                           VALUES(@MaNV, @HoTen, @QuanHe, @NgaySinh, @NgheNghiep, @DiaChi, @GhiChu);
                           SELECT SCOPE_IDENTITY();";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaNV", ttg.MaNV),
                new SqlParameter("@HoTen", ttg.HoTen),
                new SqlParameter("@QuanHe", ttg.QuanHe),
                new SqlParameter("@NgaySinh", (object)ttg.NgaySinh ?? DBNull.Value),
                new SqlParameter("@NgheNghiep", (object)ttg.NgheNghiep ?? DBNull.Value),
                new SqlParameter("@DiaChi", (object)ttg.DiaChi ?? DBNull.Value),
                new SqlParameter("@GhiChu", (object)ttg.GhiChu ?? DBNull.Value)
            };

            object result = DBConnection.ExecuteScalar(query, parameters);
            return result == DBNull.Value ? -1 : Convert.ToInt32(result);
        }

        public static bool Update(ThongTinGiaDinhDTO ttg)
        {
            string query = @"UPDATE ThongTinGiaDinh 
                           SET MaNV = @MaNV, 
                               HoTen = @HoTen, 
                               QuanHe = @QuanHe, 
                               NgaySinh = @NgaySinh, 
                               NgheNghiep = @NgheNghiep, 
                               DiaChi = @DiaChi, 
                               GhiChu = @GhiChu 
                           WHERE MaThongTin = @MaThongTin";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaThongTin", ttg.MaThongTin),
                new SqlParameter("@MaNV", ttg.MaNV),
                new SqlParameter("@HoTen", ttg.HoTen),
                new SqlParameter("@QuanHe", ttg.QuanHe),
                new SqlParameter("@NgaySinh", (object)ttg.NgaySinh ?? DBNull.Value),
                new SqlParameter("@NgheNghiep", (object)ttg.NgheNghiep ?? DBNull.Value),
                new SqlParameter("@DiaChi", (object)ttg.DiaChi ?? DBNull.Value),
                new SqlParameter("@GhiChu", (object)ttg.GhiChu ?? DBNull.Value)
            };

            return DBConnection.ExecuteNonQuery(query, parameters) > 0;
        }

        public static bool Delete(int maThongTin)
        {
            string query = "DELETE FROM ThongTinGiaDinh WHERE MaThongTin = @MaThongTin";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaThongTin", maThongTin)
            };

            return DBConnection.ExecuteNonQuery(query, parameters) > 0;
        }
    }
}