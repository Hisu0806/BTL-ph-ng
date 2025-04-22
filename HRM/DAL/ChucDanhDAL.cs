using System;
using System.Data;
using System.Data.SqlClient;
using HRM.DTO;
using System.Collections.Generic;

namespace HRM.DAL
{
    public class ChucDanhDAL
    {
        /// <summary>
        /// Lấy danh sách chức danh
        /// </summary>
        /// <returns>DataTable chứa danh sách chức danh</returns>
        public static DataTable GetList()
        {
            string query = "SELECT * FROM ChucDanh ORDER BY TenChucDanh";
            
            return DBConnection.ExecuteQuery(query);
        }
        
        /// <summary>
        /// Lấy thông tin một chức danh theo mã
        /// </summary>
        /// <param name="maChucDanh">Mã chức danh</param>
        /// <returns>Đối tượng ChucDanh</returns>
        public static ChucDanh GetByID(int maChucDanh)
        {
            ChucDanh chucDanh = null;
            string query = "SELECT * FROM ChucDanh WHERE MaChucDanh = @MaChucDanh";
            
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaChucDanh", maChucDanh)
            };
            
            DataTable dt = DBConnection.ExecuteQuery(query, parameters);
            
            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                chucDanh = new ChucDanh();
                chucDanh.MaChucDanh = Convert.ToInt32(row["MaChucDanh"]);
                chucDanh.TenChucDanh = row["TenChucDanh"].ToString();
                chucDanh.MoTa = row["MoTa"].ToString();
            }
            
            return chucDanh;
        }
        
        /// <summary>
        /// Thêm chức danh mới
        /// </summary>
        /// <param name="chucDanh">Đối tượng ChucDanh</param>
        /// <returns>ID của chức danh vừa thêm, -1 nếu thất bại</returns>
        public static int Add(ChucDanh chucDanh)
        {
            string query = @"INSERT INTO ChucDanh (TenChucDanh, MoTa)
                           VALUES (@TenChucDanh, @MoTa);
                           SELECT SCOPE_IDENTITY()";
            
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@TenChucDanh", chucDanh.TenChucDanh),
                new SqlParameter("@MoTa", (object)chucDanh.MoTa ?? DBNull.Value)
            };
            
            object result = DBConnection.ExecuteScalar(query, parameters);
            return result != null ? Convert.ToInt32(result) : -1;
        }
        
        /// <summary>
        /// Cập nhật thông tin chức danh
        /// </summary>
        /// <param name="chucDanh">Đối tượng ChucDanh</param>
        /// <returns>true nếu thành công, false nếu thất bại</returns>
        public static bool Update(ChucDanh chucDanh)
        {
            string query = @"UPDATE ChucDanh
                           SET TenChucDanh = @TenChucDanh,
                               MoTa = @MoTa
                           WHERE MaChucDanh = @MaChucDanh";
            
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaChucDanh", chucDanh.MaChucDanh),
                new SqlParameter("@TenChucDanh", chucDanh.TenChucDanh),
                new SqlParameter("@MoTa", (object)chucDanh.MoTa ?? DBNull.Value)
            };
            
            int rowsAffected = DBConnection.ExecuteNonQuery(query, parameters);
            return rowsAffected > 0;
        }
        
        /// <summary>
        /// Xóa chức danh
        /// </summary>
        /// <param name="maChucDanh">Mã chức danh</param>
        /// <returns>true nếu thành công, false nếu thất bại</returns>
        public static bool Delete(int maChucDanh)
        {
            string query = "DELETE FROM ChucDanh WHERE MaChucDanh = @MaChucDanh";
            
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaChucDanh", maChucDanh)
            };
            
            int rowsAffected = DBConnection.ExecuteNonQuery(query, parameters);
            return rowsAffected > 0;
        }
    }
} 