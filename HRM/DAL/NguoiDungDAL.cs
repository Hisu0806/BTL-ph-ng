using System;
using System.Data;
using System.Data.SqlClient;
using HRM.DTO;
using HRM.Utility;

namespace HRM.DAL
{
    public class NguoiDungDAL
    {
        /// <summary>
        /// Kiểm tra đăng nhập
        /// </summary>
        /// <param name="tenDangNhap">Tên đăng nhập</param>
        /// <param name="matKhau">Mật khẩu</param>
        /// <returns>Đối tượng NguoiDung nếu đăng nhập thành công, null nếu thất bại</returns>
        public static NguoiDung DangNhap(string tenDangNhap, string matKhau)
        {
            NguoiDung nguoiDung = null;
            SqlConnection con = DBConnection.GetSqlConnection();
            
            try
            {
                con.Open();
                
                SqlCommand cmd = new SqlCommand("SELECT * FROM NguoiDung WHERE TenDangNhap = @TenDangNhap AND MatKhau = @MatKhau", con);
                cmd.Parameters.AddWithValue("@TenDangNhap", tenDangNhap);
                cmd.Parameters.AddWithValue("@MatKhau", matKhau);
                
                SqlDataReader reader = cmd.ExecuteReader();
                
                if (reader.Read())
                {
                    nguoiDung = new NguoiDung();
                    nguoiDung.MaNguoiDung = reader["MaNguoiDung"].ToString();
                    nguoiDung.TenDangNhap = reader["TenDangNhap"].ToString();
                    nguoiDung.MatKhau = reader["MatKhau"].ToString();
                    nguoiDung.HoTen = reader["HoTen"].ToString();
                    nguoiDung.Email = reader["Email"].ToString();
                    nguoiDung.DienThoai = reader["DienThoai"].ToString();
                    nguoiDung.VaiTro = reader["VaiTro"].ToString();
                    nguoiDung.NgayTao = Convert.ToDateTime(reader["NgayTao"]);
                    nguoiDung.TrangThai = Convert.ToBoolean(reader["TrangThai"]);
                    
                    // Gán giá trị cho Quyen từ VaiTro để tương thích ngược
                    nguoiDung.Quyen = reader["VaiTro"].ToString();
                    
                    if (reader["MaNV"] != DBNull.Value)
                    {
                        nguoiDung.MaNV = Convert.ToInt32(reader["MaNV"]);
                    }
                }
                
                reader.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
            
            return nguoiDung;
        }
        
        /// <summary>
        /// Đổi mật khẩu
        /// </summary>
        /// <param name="maNguoiDung">Mã người dùng</param>
        /// <param name="matKhauCu">Mật khẩu cũ</param>
        /// <param name="matKhauMoi">Mật khẩu mới</param>
        /// <returns>true nếu thành công, false nếu thất bại</returns>
        public static bool DoiMatKhau(string maNguoiDung, string matKhauCu, string matKhauMoi)
        {
            int result = 0;
            SqlConnection con = DBConnection.GetSqlConnection();
            
            try
            {
                con.Open();
                
                // Kiểm tra mật khẩu cũ
                SqlCommand cmdCheck = new SqlCommand("SELECT COUNT(*) FROM NguoiDung WHERE MaNguoiDung = @MaNguoiDung AND MatKhau = @MatKhauCu", con);
                cmdCheck.Parameters.AddWithValue("@MaNguoiDung", maNguoiDung);
                cmdCheck.Parameters.AddWithValue("@MatKhauCu", matKhauCu);
                
                int count = (int)cmdCheck.ExecuteScalar();
                if (count == 0)
                {
                    return false; // Mật khẩu cũ không đúng
                }
                
                // Đổi mật khẩu
                SqlCommand cmd = new SqlCommand("UPDATE NguoiDung SET MatKhau = @MatKhauMoi WHERE MaNguoiDung = @MaNguoiDung", con);
                cmd.Parameters.AddWithValue("@MaNguoiDung", maNguoiDung);
                cmd.Parameters.AddWithValue("@MatKhauMoi", matKhauMoi);
                
                result = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
            
            return (result > 0);
        }
        
        /// <summary>
        /// Lấy danh sách người dùng
        /// </summary>
        /// <returns>Danh sách người dùng</returns>
        public static DataTable GetList()
        {
            DataTable dt = new DataTable();
            SqlConnection con = DBConnection.GetSqlConnection();
            
            try
            {
                con.Open();
                
                SqlCommand cmd = new SqlCommand("SELECT * FROM NguoiDung", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
            
            return dt;
        }
        
        /// <summary>
        /// Thêm người dùng mới
        /// </summary>
        /// <param name="nguoiDung">Đối tượng NguoiDung</param>
        /// <returns>true nếu thành công, false nếu thất bại</returns>
        public static bool Add(NguoiDung nguoiDung)
        {
            int result = 0;
            SqlConnection con = DBConnection.GetSqlConnection();
            
            try
            {
                con.Open();
                
                SqlCommand cmd = new SqlCommand(@"INSERT INTO NguoiDung(TenDangNhap, MatKhau, HoTen, Email, DienThoai, VaiTro, NgayTao, TrangThai, MaNV) 
                                                 VALUES (@TenDangNhap, @MatKhau, @HoTen, @Email, @DienThoai, @VaiTro, @NgayTao, @TrangThai, @MaNV)", con);
                
                cmd.Parameters.AddWithValue("@TenDangNhap", nguoiDung.TenDangNhap);
                cmd.Parameters.AddWithValue("@MatKhau", nguoiDung.MatKhau);
                cmd.Parameters.AddWithValue("@HoTen", nguoiDung.HoTen);
                cmd.Parameters.AddWithValue("@Email", (object)nguoiDung.Email ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@DienThoai", (object)nguoiDung.DienThoai ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@VaiTro", nguoiDung.VaiTro);
                cmd.Parameters.AddWithValue("@NgayTao", DateTime.Now);
                cmd.Parameters.AddWithValue("@TrangThai", true);
                cmd.Parameters.AddWithValue("@MaNV", (object)nguoiDung.MaNV ?? DBNull.Value);
                
                result = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
            
            return (result > 0);
        }
        
        /// <summary>
        /// Cập nhật thông tin người dùng
        /// </summary>
        /// <param name="nguoiDung">Đối tượng NguoiDung</param>
        /// <returns>true nếu thành công, false nếu thất bại</returns>
        public static bool Update(NguoiDung nguoiDung)
        {
            int result = 0;
            SqlConnection con = DBConnection.GetSqlConnection();
            
            try
            {
                con.Open();
                
                SqlCommand cmd = new SqlCommand(@"UPDATE NguoiDung 
                                                SET TenDangNhap = @TenDangNhap, 
                                                    HoTen = @HoTen, 
                                                    Email = @Email,
                                                    DienThoai = @DienThoai,
                                                    VaiTro = @VaiTro,
                                                    TrangThai = @TrangThai,
                                                    MaNV = @MaNV
                                                WHERE MaNguoiDung = @MaNguoiDung", con);
                                                    
                cmd.Parameters.AddWithValue("@MaNguoiDung", nguoiDung.MaNguoiDung);
                cmd.Parameters.AddWithValue("@TenDangNhap", nguoiDung.TenDangNhap);
                cmd.Parameters.AddWithValue("@HoTen", nguoiDung.HoTen);
                cmd.Parameters.AddWithValue("@Email", (object)nguoiDung.Email ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@DienThoai", (object)nguoiDung.DienThoai ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@VaiTro", nguoiDung.VaiTro);
                cmd.Parameters.AddWithValue("@TrangThai", nguoiDung.TrangThai);
                cmd.Parameters.AddWithValue("@MaNV", (object)nguoiDung.MaNV ?? DBNull.Value);
                
                result = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
            
            return (result > 0);
        }
        
        /// <summary>
        /// Xóa người dùng
        /// </summary>
        /// <param name="maNguoiDung">Mã người dùng</param>
        /// <returns>true nếu thành công, false nếu thất bại</returns>
        public static bool Delete(string maNguoiDung)
        {
            int result = 0;
            SqlConnection con = DBConnection.GetSqlConnection();
            
            try
            {
                con.Open();
                
                SqlCommand cmd = new SqlCommand("DELETE FROM NguoiDung WHERE MaNguoiDung = @MaNguoiDung", con);
                cmd.Parameters.AddWithValue("@MaNguoiDung", maNguoiDung);
                
                result = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
            
            return (result > 0);
        }
    }
} 