using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace HRM.DAL
{
    public class DBConnection
    {
        public static SqlConnection GetSqlConnection()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["HRMConnectionString"].ConnectionString;
            SqlConnection connection = new SqlConnection(connectionString);
            return connection;
        }

        public static DataTable ExecuteQuery(string query, params SqlParameter[] parameters)
        {
            DataTable dt = new DataTable();

            using (SqlConnection conn = GetSqlConnection())
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(query, conn);
                if (parameters != null)
                {
                    cmd.Parameters.AddRange(parameters);
                }

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);

                conn.Close();
            }

            return dt;
        }

        public static int ExecuteNonQuery(string query, params SqlParameter[] parameters)
        {
            int result = 0;

            using (SqlConnection conn = GetSqlConnection())
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(query, conn);
                if (parameters != null)
                {
                    cmd.Parameters.AddRange(parameters);
                }

                result = cmd.ExecuteNonQuery();

                conn.Close();
            }

            return result;
        }

        public static object ExecuteScalar(string query, params SqlParameter[] parameters)
        {
            object result = null;

            using (SqlConnection conn = GetSqlConnection())
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(query, conn);
                if (parameters != null)
                {
                    cmd.Parameters.AddRange(parameters);
                }

                result = cmd.ExecuteScalar();

                conn.Close();
            }

            return result;
        }
    }
} 