using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using HRM.DTO;

namespace HRM.DAL
{
    public class BacLuongDAL
    {
        private static readonly string connectionString = ConfigurationManager.ConnectionStrings["HRMConnectionString"].ConnectionString;

        public static DataTable GetList()
        {
            string query = @"SELECT MaBacLuong, TenBac, HeSo, LuongCoBan 
                           FROM BacLuong 
                           ORDER BY MaBacLuong";
            return DBConnection.ExecuteQuery(query);
        }

        public static BacLuongDTO GetByID(int maBacLuong)
        {
            string query = @"SELECT MaBacLuong, TenBac, HeSo, LuongCoBan 
                           FROM BacLuong 
                           WHERE MaBacLuong = @MaBacLuong";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaBacLuong", maBacLuong)
            };

            DataTable dt = DBConnection.ExecuteQuery(query, parameters);
            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                return new BacLuongDTO
                {
                    MaBacLuong = Convert.ToInt32(row["MaBacLuong"]),
                    TenBac = row["TenBac"].ToString(),
                    HeSo = Convert.ToDecimal(row["HeSo"]),
                    LuongCoBan = Convert.ToDecimal(row["LuongCoBan"])
                };
            }
            return null;
        }

        public static int Add(BacLuongDTO bacLuong)
        {
            string query = @"INSERT INTO BacLuong (TenBac, HeSo, LuongCoBan)
                           VALUES (@TenBac, @HeSo, @LuongCoBan);
                           SELECT SCOPE_IDENTITY();";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@TenBac", bacLuong.TenBac);
                command.Parameters.AddWithValue("@HeSo", bacLuong.HeSo);
                command.Parameters.AddWithValue("@LuongCoBan", bacLuong.LuongCoBan);

                connection.Open();
                return Convert.ToInt32(command.ExecuteScalar());
            }
        }

        public static bool Update(BacLuongDTO bacLuong)
        {
            string query = @"UPDATE BacLuong 
                           SET TenBac = @TenBac,
                               HeSo = @HeSo,
                               LuongCoBan = @LuongCoBan
                           WHERE MaBacLuong = @MaBacLuong";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@MaBacLuong", bacLuong.MaBacLuong);
                command.Parameters.AddWithValue("@TenBac", bacLuong.TenBac);
                command.Parameters.AddWithValue("@HeSo", bacLuong.HeSo);
                command.Parameters.AddWithValue("@LuongCoBan", bacLuong.LuongCoBan);

                connection.Open();
                return command.ExecuteNonQuery() > 0;
            }
        }

        public static bool Delete(int maBacLuong)
        {
            string query = "DELETE FROM BacLuong WHERE MaBacLuong = @MaBacLuong";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@MaBacLuong", maBacLuong);

                connection.Open();
                return command.ExecuteNonQuery() > 0;
            }
        }

        public static BacLuongDTO GetCurrentBacLuong(int maNV)
        {
            string query = @"SELECT TOP 1 b.MaBacLuong, b.TenBac, b.HeSo, b.LuongCoBan
                           FROM BacLuong b
                           INNER JOIN TangLuong tl ON b.MaBacLuong = tl.MaBacLuong
                           WHERE tl.MaNV = @MaNV
                           ORDER BY tl.NgayTangLuong DESC";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@MaNV", maNV);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    return new BacLuongDTO
                    {
                        MaBacLuong = Convert.ToInt32(reader["MaBacLuong"]),
                        TenBac = reader["TenBac"].ToString(),
                        HeSo = Convert.ToDecimal(reader["HeSo"]),
                        LuongCoBan = Convert.ToDecimal(reader["LuongCoBan"])
                    };
                }
            }

            return null;
        }
    }
}