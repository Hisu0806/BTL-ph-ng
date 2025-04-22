using System;
using System.Data;
using System.Data.SqlClient;
using HRM.DTO;
using System.Collections.Generic;

namespace HRM.DAL
{
    public class PhongBanDAL
    {
        /// <summary>
        /// Lấy danh sách phòng ban
        /// </summary>
        /// <returns>DataTable chứa danh sách phòng ban</returns>
        public static DataTable GetList()
        {
            string query = @"SELECT pb.*, nv.HoTen AS TenTruongPhong
                           FROM PhongBan pb
                           LEFT JOIN NhanVien nv ON pb.TruongPhong = nv.MaNV
                           ORDER BY pb.TenPhong";
            
            return DBConnection.ExecuteQuery(query);
        }
        
        /// <summary>
        /// Lấy thông tin một phòng ban theo mã
        /// </summary>
        /// <param name="maPhong">Mã phòng ban</param>
        /// <returns>Đối tượng PhongBan</returns>
        public static PhongBan GetByID(int maPhong)
        {
            PhongBan phongBan = null;
            string query = @"SELECT pb.*, nv.HoTen AS TenTruongPhong
                           FROM PhongBan pb
                           LEFT JOIN NhanVien nv ON pb.TruongPhong = nv.MaNV
                           WHERE pb.MaPhong = @MaPhong";
            
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaPhong", maPhong)
            };
            
            DataTable dt = DBConnection.ExecuteQuery(query, parameters);
            
            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                phongBan = new PhongBan();
                phongBan.MaPhong = Convert.ToInt32(row["MaPhong"]);
                phongBan.TenPhong = row["TenPhong"].ToString();
                phongBan.MoTa = row["MoTa"].ToString();
                
                if (row["TruongPhong"] != DBNull.Value)
                {
                    phongBan.TruongPhong = Convert.ToInt32(row["TruongPhong"]);
                }
                
                phongBan.TenTruongPhong = row["TenTruongPhong"].ToString();
            }
            
            return phongBan;
        }
        
        /// <summary>
        /// Thêm phòng ban mới
        /// </summary>
        /// <param name="phongBan">Đối tượng PhongBan</param>
        /// <returns>ID của phòng ban vừa thêm, -1 nếu thất bại</returns>
        public static int Add(PhongBan phongBan)
        {
            string query = @"INSERT INTO PhongBan (TenPhong, MoTa, TruongPhong)
                           VALUES (@TenPhong, @MoTa, @TruongPhong);
                           SELECT SCOPE_IDENTITY()";
            
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@TenPhong", phongBan.TenPhong),
                new SqlParameter("@MoTa", (object)phongBan.MoTa ?? DBNull.Value),
                new SqlParameter("@TruongPhong", (object)phongBan.TruongPhong ?? DBNull.Value)
            };
            
            object result = DBConnection.ExecuteScalar(query, parameters);
            return result != null ? Convert.ToInt32(result) : -1;
        }
        
        /// <summary>
        /// Cập nhật thông tin phòng ban
        /// </summary>
        /// <param name="phongBan">Đối tượng PhongBan</param>
        /// <returns>true nếu thành công, false nếu thất bại</returns>
        public static bool Update(PhongBan phongBan)
        {
            string query = @"UPDATE PhongBan
                           SET TenPhong = @TenPhong,
                               MoTa = @MoTa,
                               TruongPhong = @TruongPhong
                           WHERE MaPhong = @MaPhong";
            
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaPhong", phongBan.MaPhong),
                new SqlParameter("@TenPhong", phongBan.TenPhong),
                new SqlParameter("@MoTa", (object)phongBan.MoTa ?? DBNull.Value),
                new SqlParameter("@TruongPhong", (object)phongBan.TruongPhong ?? DBNull.Value)
            };
            
            int rowsAffected = DBConnection.ExecuteNonQuery(query, parameters);
            return rowsAffected > 0;
        }
        
        /// <summary>
        /// Xóa phòng ban
        /// </summary>
        /// <param name="maPhong">Mã phòng ban</param>
        /// <returns>true nếu thành công, false nếu thất bại</returns>
        public static bool Delete(int maPhong)
        {
            string query = "DELETE FROM PhongBan WHERE MaPhong = @MaPhong";
            
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaPhong", maPhong)
            };
            
            int rowsAffected = DBConnection.ExecuteNonQuery(query, parameters);
            return rowsAffected > 0;
        }
    }
} 