using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using HRM.DAL;

namespace HRM.Utility
{
    public class SqlHelper
    {
        private static readonly string connectionString = ConfigurationManager.ConnectionStrings["HRMConnectionString"].ConnectionString;
        
        // Tạo database nếu chưa tồn tại
        public static bool CreateDatabaseIfNotExists()
        {
            try
            {
                // Tạo connection string đến master database
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(connectionString);
                string database = builder.InitialCatalog;
                builder.InitialCatalog = "master";
                
                // Kiểm tra database đã tồn tại chưa
                string checkDbQuery = "SELECT COUNT(*) FROM sys.databases WHERE name = '" + database + "'";
                
                using (SqlConnection conn = new SqlConnection(builder.ConnectionString))
                {
                    conn.Open();
                    
                    using (SqlCommand cmd = new SqlCommand(checkDbQuery, conn))
                    {
                        int count = Convert.ToInt32(cmd.ExecuteScalar());
                        
                        if (count == 0)
                        {
                            // Nếu chưa tồn tại thì tạo database mới
                            string createDbQuery = "CREATE DATABASE " + database;
                            using (SqlCommand createCmd = new SqlCommand(createDbQuery, conn))
                            {
                                createCmd.ExecuteNonQuery();
                            }
                            
                            // Tạo bảng và dữ liệu mẫu
                            CreateTables(database);
                            return true;
                        }
                    }
                }
                
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tạo cơ sở dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
        
        // Tạo bảng và dữ liệu mẫu
        private static void CreateTables(string database)
        {
            try
            {
                // Đọc file SQL từ resources
                string sqlScript = GetSqlScript();
                
                // Thực thi script
                ExecuteSqlScript(sqlScript, database);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tạo bảng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        // Lấy script SQL từ file
        private static string GetSqlScript()
        {
            try
            {
                string scriptPath = Path.Combine(Application.StartupPath, "SqlScript", "CreateDatabase.sql");
                
                if (File.Exists(scriptPath))
                {
                    return File.ReadAllText(scriptPath);
                }
                else
                {
                    // Tạo script mặc định nếu không tìm thấy file
                    return GenerateDefaultSqlScript();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi đọc file script: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "";
            }
        }
        
        // Thực thi script SQL
        private static void ExecuteSqlScript(string script, string database)
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(connectionString);
            builder.InitialCatalog = database;
            
            using (SqlConnection conn = new SqlConnection(builder.ConnectionString))
            {
                conn.Open();
                
                // Chia script thành các batch để thực thi riêng biệt
                string[] batches = script.Split(new[] { "GO" }, StringSplitOptions.RemoveEmptyEntries);
                
                foreach (string batch in batches)
                {
                    if (!string.IsNullOrWhiteSpace(batch))
                    {
                        using (SqlCommand cmd = new SqlCommand(batch, conn))
                        {
                            try
                            {
                                cmd.ExecuteNonQuery();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Lỗi khi thực thi câu lệnh SQL: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }
        }
        
        // Tạo script SQL mặc định
        private static string GenerateDefaultSqlScript()
        {
            StringBuilder sb = new StringBuilder();
            
            // Tạo bảng PhongBan
            sb.AppendLine("CREATE TABLE PhongBan (");
            sb.AppendLine("    MaPhong INT IDENTITY(1,1) PRIMARY KEY,");
            sb.AppendLine("    TenPhong NVARCHAR(100) NOT NULL,");
            sb.AppendLine("    MoTa NVARCHAR(255),");
            sb.AppendLine("    TruongPhong INT");
            sb.AppendLine(");");
            sb.AppendLine("GO");
            
            // Tạo bảng ChucDanh
            sb.AppendLine("CREATE TABLE ChucDanh (");
            sb.AppendLine("    MaChucDanh INT IDENTITY(1,1) PRIMARY KEY,");
            sb.AppendLine("    TenChucDanh NVARCHAR(100) NOT NULL,");
            sb.AppendLine("    MoTa NVARCHAR(255)");
            sb.AppendLine(");");
            sb.AppendLine("GO");
            
            // Tạo bảng NhanVien
            sb.AppendLine("CREATE TABLE NhanVien (");
            sb.AppendLine("    MaNV INT IDENTITY(1,1) PRIMARY KEY,");
            sb.AppendLine("    HoTen NVARCHAR(100) NOT NULL,");
            sb.AppendLine("    NgaySinh DATE NOT NULL,");
            sb.AppendLine("    GioiTinh NVARCHAR(10) NOT NULL,");
            sb.AppendLine("    SoCMND VARCHAR(15) UNIQUE NOT NULL,");
            sb.AppendLine("    TrinhDoChuyenMon NVARCHAR(100),");
            sb.AppendLine("    TrinhDoNgoaiNgu NVARCHAR(100),");
            sb.AppendLine("    HoKhauThuongTru NVARCHAR(255),");
            sb.AppendLine("    DiaChi NVARCHAR(255),");
            sb.AppendLine("    DanToc NVARCHAR(50),");
            sb.AppendLine("    TonGiao NVARCHAR(50),");
            sb.AppendLine("    NgayVaoDoan DATE,");
            sb.AppendLine("    NgayVaoDang DATE,");
            sb.AppendLine("    DienChinhSach NVARCHAR(100),");
            sb.AppendLine("    MaPhong INT FOREIGN KEY REFERENCES PhongBan(MaPhong),");
            sb.AppendLine("    MaChucDanh INT FOREIGN KEY REFERENCES ChucDanh(MaChucDanh),");
            sb.AppendLine("    LoaiNhanVien NVARCHAR(20) CHECK (LoaiNhanVien IN (N'Biên chế', N'Hợp đồng')),");
            sb.AppendLine("    NgayVaoCongTy DATE NOT NULL,");
            sb.AppendLine("    NgayNghiViec DATE,");
            sb.AppendLine("    TrangThai NVARCHAR(20) DEFAULT N'Đang làm việc',");
            sb.AppendLine("    HinhAnh VARBINARY(MAX)");
            sb.AppendLine(");");
            sb.AppendLine("GO");
            
            // Tạo khóa ngoại cho bảng PhongBan
            sb.AppendLine("ALTER TABLE PhongBan ADD CONSTRAINT FK_PhongBan_NhanVien FOREIGN KEY (TruongPhong) REFERENCES NhanVien(MaNV);");
            sb.AppendLine("GO");
            
            // Tạo bảng ThongTinGiaDinh
            sb.AppendLine("CREATE TABLE ThongTinGiaDinh (");
            sb.AppendLine("    MaThongTin INT IDENTITY(1,1) PRIMARY KEY,");
            sb.AppendLine("    MaNV INT FOREIGN KEY REFERENCES NhanVien(MaNV),");
            sb.AppendLine("    HoTen NVARCHAR(100) NOT NULL,");
            sb.AppendLine("    QuanHe NVARCHAR(50) NOT NULL,");
            sb.AppendLine("    NgaySinh DATE,");
            sb.AppendLine("    NgheNghiep NVARCHAR(100),");
            sb.AppendLine("    DiaChi NVARCHAR(255),");
            sb.AppendLine("    GhiChu NVARCHAR(255)");
            sb.AppendLine(");");
            sb.AppendLine("GO");
            
            // Tạo bảng QuaTrinhCongTac
            sb.AppendLine("CREATE TABLE QuaTrinhCongTac (");
            sb.AppendLine("    MaQuaTrinh INT IDENTITY(1,1) PRIMARY KEY,");
            sb.AppendLine("    MaNV INT FOREIGN KEY REFERENCES NhanVien(MaNV),");
            sb.AppendLine("    TuNgay DATE NOT NULL,");
            sb.AppendLine("    DenNgay DATE,");
            sb.AppendLine("    ChucDanh NVARCHAR(100),");
            sb.AppendLine("    PhongBan NVARCHAR(100),");
            sb.AppendLine("    MoTaCongViec NVARCHAR(255),");
            sb.AppendLine("    GhiChu NVARCHAR(255)");
            sb.AppendLine(");");
            sb.AppendLine("GO");
            
            // Tạo bảng KhenThuong
            sb.AppendLine("CREATE TABLE KhenThuong (");
            sb.AppendLine("    MaKhenThuong INT IDENTITY(1,1) PRIMARY KEY,");
            sb.AppendLine("    MaNV INT FOREIGN KEY REFERENCES NhanVien(MaNV),");
            sb.AppendLine("    NgayKhenThuong DATE NOT NULL,");
            sb.AppendLine("    HinhThuc NVARCHAR(100) NOT NULL,");
            sb.AppendLine("    LyDo NVARCHAR(255),");
            sb.AppendLine("    SoTien DECIMAL(18,2),");
            sb.AppendLine("    GhiChu NVARCHAR(255)");
            sb.AppendLine(");");
            sb.AppendLine("GO");
            
            // Tạo bảng KyLuat
            sb.AppendLine("CREATE TABLE KyLuat (");
            sb.AppendLine("    MaKyLuat INT IDENTITY(1,1) PRIMARY KEY,");
            sb.AppendLine("    MaNV INT FOREIGN KEY REFERENCES NhanVien(MaNV),");
            sb.AppendLine("    NgayKyLuat DATE NOT NULL,");
            sb.AppendLine("    HinhThuc NVARCHAR(100) NOT NULL,");
            sb.AppendLine("    LyDo NVARCHAR(255),");
            sb.AppendLine("    SoTien DECIMAL(18,2),");
            sb.AppendLine("    GhiChu NVARCHAR(255)");
            sb.AppendLine(");");
            sb.AppendLine("GO");
            
            // Tạo bảng BacLuong
            sb.AppendLine("CREATE TABLE BacLuong (");
            sb.AppendLine("    MaBacLuong INT IDENTITY(1,1) PRIMARY KEY,");
            sb.AppendLine("    TenBac NVARCHAR(50) NOT NULL,");
            sb.AppendLine("    HeSo FLOAT NOT NULL,");
            sb.AppendLine("    LuongCoBan DECIMAL(18,2) NOT NULL");
            sb.AppendLine(");");
            sb.AppendLine("GO");
            
            // Tạo bảng TangLuong
            sb.AppendLine("CREATE TABLE TangLuong (");
            sb.AppendLine("    MaTangLuong INT IDENTITY(1,1) PRIMARY KEY,");
            sb.AppendLine("    MaNV INT FOREIGN KEY REFERENCES NhanVien(MaNV),");
            sb.AppendLine("    NgayTangLuong DATE NOT NULL,");
            sb.AppendLine("    MaBacLuongCu INT FOREIGN KEY REFERENCES BacLuong(MaBacLuong),");
            sb.AppendLine("    MaBacLuongMoi INT FOREIGN KEY REFERENCES BacLuong(MaBacLuong),");
            sb.AppendLine("    LyDo NVARCHAR(255),");
            sb.AppendLine("    GhiChu NVARCHAR(255)");
            sb.AppendLine(");");
            sb.AppendLine("GO");
            
            // Tạo bảng ChamCong
            sb.AppendLine("CREATE TABLE ChamCong (");
            sb.AppendLine("    MaChamCong INT IDENTITY(1,1) PRIMARY KEY,");
            sb.AppendLine("    MaNV INT FOREIGN KEY REFERENCES NhanVien(MaNV),");
            sb.AppendLine("    NgayChamCong DATE NOT NULL,");
            sb.AppendLine("    TrangThai NVARCHAR(20) CHECK (TrangThai IN (N'Đi làm', N'Nghỉ phép', N'Nghỉ ốm', N'Nghỉ không lương', N'Đi công tác')),");
            sb.AppendLine("    GioVao TIME,");
            sb.AppendLine("    GioRa TIME,");
            sb.AppendLine("    GhiChu NVARCHAR(255),");
            sb.AppendLine("    CONSTRAINT UQ_ChamCong UNIQUE (MaNV, NgayChamCong)");
            sb.AppendLine(");");
            sb.AppendLine("GO");
            
            // Tạo bảng PhuCap
            sb.AppendLine("CREATE TABLE PhuCap (");
            sb.AppendLine("    MaPhuCap INT IDENTITY(1,1) PRIMARY KEY,");
            sb.AppendLine("    TenPhuCap NVARCHAR(100) NOT NULL,");
            sb.AppendLine("    GiaTri DECIMAL(18,2) NOT NULL,");
            sb.AppendLine("    MoTa NVARCHAR(255)");
            sb.AppendLine(");");
            sb.AppendLine("GO");
            
            // Tạo bảng NhanVien_PhuCap
            sb.AppendLine("CREATE TABLE NhanVien_PhuCap (");
            sb.AppendLine("    MaNV INT FOREIGN KEY REFERENCES NhanVien(MaNV),");
            sb.AppendLine("    MaPhuCap INT FOREIGN KEY REFERENCES PhuCap(MaPhuCap),");
            sb.AppendLine("    PRIMARY KEY (MaNV, MaPhuCap)");
            sb.AppendLine(");");
            sb.AppendLine("GO");
            
            // Tạo bảng HopDong
            sb.AppendLine("CREATE TABLE HopDong (");
            sb.AppendLine("    MaHopDong INT IDENTITY(1,1) PRIMARY KEY,");
            sb.AppendLine("    MaNV INT FOREIGN KEY REFERENCES NhanVien(MaNV),");
            sb.AppendLine("    SoHopDong NVARCHAR(50) NOT NULL,");
            sb.AppendLine("    LoaiHopDong NVARCHAR(50) NOT NULL,");
            sb.AppendLine("    NgayKy DATE NOT NULL,");
            sb.AppendLine("    NgayBatDau DATE NOT NULL,");
            sb.AppendLine("    NgayKetThuc DATE,");
            sb.AppendLine("    LuongTheoHopDong DECIMAL(18,2) NOT NULL,");
            sb.AppendLine("    NoiDung NVARCHAR(MAX),");
            sb.AppendLine("    TrangThai NVARCHAR(50) DEFAULT N'Đang hiệu lực'");
            sb.AppendLine(");");
            sb.AppendLine("GO");
            
            // Tạo bảng Luong
            sb.AppendLine("CREATE TABLE Luong (");
            sb.AppendLine("    MaLuong INT IDENTITY(1,1) PRIMARY KEY,");
            sb.AppendLine("    MaNV INT FOREIGN KEY REFERENCES NhanVien(MaNV),");
            sb.AppendLine("    Thang INT NOT NULL,");
            sb.AppendLine("    Nam INT NOT NULL,");
            sb.AppendLine("    MaBacLuong INT FOREIGN KEY REFERENCES BacLuong(MaBacLuong),");
            sb.AppendLine("    LuongCoBan DECIMAL(18,2) NOT NULL,");
            sb.AppendLine("    TongPhuCap DECIMAL(18,2) NOT NULL,");
            sb.AppendLine("    BaoHiem DECIMAL(18,2) NOT NULL,");
            sb.AppendLine("    TongNgayCong INT NOT NULL,");
            sb.AppendLine("    TongLuong DECIMAL(18,2) NOT NULL,");
            sb.AppendLine("    GhiChu NVARCHAR(255),");
            sb.AppendLine("    CONSTRAINT UQ_Luong UNIQUE (MaNV, Thang, Nam)");
            sb.AppendLine(");");
            sb.AppendLine("GO");
            
            // Tạo bảng NgayLe
            sb.AppendLine("CREATE TABLE NgayLe (");
            sb.AppendLine("    MaNgayLe INT IDENTITY(1,1) PRIMARY KEY,");
            sb.AppendLine("    TenNgayLe NVARCHAR(100) NOT NULL,");
            sb.AppendLine("    Ngay DATE NOT NULL,");
            sb.AppendLine("    MoTa NVARCHAR(255)");
            sb.AppendLine(");");
            sb.AppendLine("GO");
            
            // Tạo bảng NguoiDung
            sb.AppendLine("CREATE TABLE NguoiDung (");
            sb.AppendLine("    MaNguoiDung INT IDENTITY(1,1) PRIMARY KEY,");
            sb.AppendLine("    TenDangNhap NVARCHAR(50) NOT NULL UNIQUE,");
            sb.AppendLine("    MatKhau NVARCHAR(100) NOT NULL,");
            sb.AppendLine("    HoTen NVARCHAR(100) NOT NULL,");
            sb.AppendLine("    Email NVARCHAR(100),");
            sb.AppendLine("    DienThoai NVARCHAR(20),");
            sb.AppendLine("    VaiTro NVARCHAR(20) NOT NULL CHECK (VaiTro IN (N'Admin', N'Quản lý', N'Nhân viên')),");
            sb.AppendLine("    NgayTao DATETIME NOT NULL,");
            sb.AppendLine("    TrangThai BIT NOT NULL DEFAULT 1,");
            sb.AppendLine("    MaNV INT FOREIGN KEY REFERENCES NhanVien(MaNV)");
            sb.AppendLine(");");
            sb.AppendLine("GO");
            
            // Thêm dữ liệu mẫu
            // Thêm phòng ban
            sb.AppendLine("INSERT INTO PhongBan (TenPhong, MoTa) VALUES (N'Ban Giám đốc', N'Ban lãnh đạo công ty');");
            sb.AppendLine("INSERT INTO PhongBan (TenPhong, MoTa) VALUES (N'Phòng Nhân sự', N'Quản lý nhân sự và tuyển dụng');");
            sb.AppendLine("INSERT INTO PhongBan (TenPhong, MoTa) VALUES (N'Phòng Kế toán', N'Quản lý tài chính, kế toán');");
            sb.AppendLine("INSERT INTO PhongBan (TenPhong, MoTa) VALUES (N'Phòng Công nghệ', N'Phát triển công nghệ và kỹ thuật');");
            sb.AppendLine("GO");
            
            // Thêm chức danh
            sb.AppendLine("INSERT INTO ChucDanh (TenChucDanh, MoTa) VALUES (N'Giám đốc', N'Giám đốc điều hành');");
            sb.AppendLine("INSERT INTO ChucDanh (TenChucDanh, MoTa) VALUES (N'Trưởng phòng', N'Quản lý phòng ban');");
            sb.AppendLine("INSERT INTO ChucDanh (TenChucDanh, MoTa) VALUES (N'Phó phòng', N'Phó quản lý phòng ban');");
            sb.AppendLine("INSERT INTO ChucDanh (TenChucDanh, MoTa) VALUES (N'Nhân viên', N'Nhân viên thông thường');");
            sb.AppendLine("GO");
            
            // Thêm bậc lương
            sb.AppendLine("INSERT INTO BacLuong (TenBac, HeSo, LuongCoBan) VALUES (N'Bậc 1', 1.0, 5000000);");
            sb.AppendLine("INSERT INTO BacLuong (TenBac, HeSo, LuongCoBan) VALUES (N'Bậc 2', 1.2, 5000000);");
            sb.AppendLine("INSERT INTO BacLuong (TenBac, HeSo, LuongCoBan) VALUES (N'Bậc 3', 1.5, 5000000);");
            sb.AppendLine("INSERT INTO BacLuong (TenBac, HeSo, LuongCoBan) VALUES (N'Bậc 4', 2.0, 5000000);");
            sb.AppendLine("INSERT INTO BacLuong (TenBac, HeSo, LuongCoBan) VALUES (N'Bậc 5', 2.5, 5000000);");
            sb.AppendLine("GO");
            
            // Thêm phụ cấp
            sb.AppendLine("INSERT INTO PhuCap (TenPhuCap, GiaTri, MoTa) VALUES (N'Phụ cấp ăn trưa', 1000000, N'Phụ cấp ăn trưa hàng tháng');");
            sb.AppendLine("INSERT INTO PhuCap (TenPhuCap, GiaTri, MoTa) VALUES (N'Phụ cấp xăng xe', 500000, N'Phụ cấp xăng xe hàng tháng');");
            sb.AppendLine("INSERT INTO PhuCap (TenPhuCap, GiaTri, MoTa) VALUES (N'Phụ cấp điện thoại', 200000, N'Phụ cấp điện thoại hàng tháng');");
            sb.AppendLine("INSERT INTO PhuCap (TenPhuCap, GiaTri, MoTa) VALUES (N'Phụ cấp chức vụ', 2000000, N'Phụ cấp dành cho cấp quản lý');");
            sb.AppendLine("GO");
            
            // Thêm tài khoản admin
            sb.AppendLine("INSERT INTO NguoiDung (TenDangNhap, MatKhau, HoTen, Email, VaiTro, NgayTao) VALUES ('admin', '123456', N'Quản trị viên', 'admin@example.com', N'Admin', GETDATE());");
            sb.AppendLine("GO");
            
            // Thêm ngày lễ
            sb.AppendLine("INSERT INTO NgayLe (TenNgayLe, Ngay, MoTa) VALUES (N'Tết Dương lịch', '2023-01-01', N'Ngày 1/1 hàng năm');");
            sb.AppendLine("INSERT INTO NgayLe (TenNgayLe, Ngay, MoTa) VALUES (N'Tết Nguyên đán', '2023-01-22', N'Từ ngày 30/12 đến mùng 5/1 âm lịch');");
            sb.AppendLine("INSERT INTO NgayLe (TenNgayLe, Ngay, MoTa) VALUES (N'Tết Nguyên đán', '2023-01-23', N'Từ ngày 30/12 đến mùng 5/1 âm lịch');");
            sb.AppendLine("INSERT INTO NgayLe (TenNgayLe, Ngay, MoTa) VALUES (N'Tết Nguyên đán', '2023-01-24', N'Từ ngày 30/12 đến mùng 5/1 âm lịch');");
            sb.AppendLine("INSERT INTO NgayLe (TenNgayLe, Ngay, MoTa) VALUES (N'Tết Nguyên đán', '2023-01-25', N'Từ ngày 30/12 đến mùng 5/1 âm lịch');");
            sb.AppendLine("INSERT INTO NgayLe (TenNgayLe, Ngay, MoTa) VALUES (N'Giỗ Tổ Hùng Vương', '2023-04-29', N'Mùng 10/3 âm lịch');");
            sb.AppendLine("INSERT INTO NgayLe (TenNgayLe, Ngay, MoTa) VALUES (N'Ngày Giải phóng', '2023-04-30', N'Ngày 30/4 hàng năm');");
            sb.AppendLine("INSERT INTO NgayLe (TenNgayLe, Ngay, MoTa) VALUES (N'Ngày Quốc tế Lao động', '2023-05-01', N'Ngày 1/5 hàng năm');");
            sb.AppendLine("INSERT INTO NgayLe (TenNgayLe, Ngay, MoTa) VALUES (N'Quốc khánh', '2023-09-02', N'Ngày 2/9 hàng năm');");
            sb.AppendLine("GO");
            
            return sb.ToString();
        }

        // Truy vấn ghi nhật ký
        public static void Log(string message, Exception exception = null)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Truy vấn ghi nhật ký
                    string logQuery = "INSERT INTO Logs (LogTime, LogLevel, Message, Exception) VALUES (@LogTime, @LogLevel, @Message, @Exception)";

                    // Tạo câu lệnh SQL
                    var command = new SqlCommand(logQuery, connection);
                    command.Parameters.AddWithValue("@LogTime", DateTime.Now);
                    command.Parameters.AddWithValue("@LogLevel", "Info"); // Thay đổi thành level thích hợp
                    command.Parameters.AddWithValue("@Message", message);
                    command.Parameters.AddWithValue("@Exception", exception != null ? exception.ToString() : null);

                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi ghi nhật ký: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Thêm dữ liệu mẫu cho cơ sở dữ liệu
        /// </summary>
        /// <returns>true nếu thành công, false nếu thất bại</returns>
        public static bool InsertSampleData()
        {
            try
            {
                SqlConnection con = DBConnection.GetSqlConnection();
                con.Open();
                
                // Thêm nhân viên mẫu
                string nhanVienScript = @"
                -- Thêm nhân viên
                INSERT INTO NhanVien (HoTen, NgaySinh, GioiTinh, SoCMND, TrinhDoChuyenMon, DiaChi, MaPhong, MaChucDanh, LoaiNhanVien, NgayVaoCongTy)
                VALUES (N'Nguyễn Văn An', '1990-01-15', N'Nam', '123456789012', N'Đại học', N'Hà Nội', 1, 1, N'Biên chế', '2020-01-01');
                
                INSERT INTO NhanVien (HoTen, NgaySinh, GioiTinh, SoCMND, TrinhDoChuyenMon, DiaChi, MaPhong, MaChucDanh, LoaiNhanVien, NgayVaoCongTy)
                VALUES (N'Trần Thị Bình', '1992-05-20', N'Nữ', '123456789013', N'Thạc sĩ', N'Hồ Chí Minh', 2, 2, N'Biên chế', '2020-02-01');
                
                INSERT INTO NhanVien (HoTen, NgaySinh, GioiTinh, SoCMND, TrinhDoChuyenMon, DiaChi, MaPhong, MaChucDanh, LoaiNhanVien, NgayVaoCongTy)
                VALUES (N'Lê Văn Cường', '1988-10-10', N'Nam', '123456789014', N'Đại học', N'Đà Nẵng', 3, 2, N'Biên chế', '2020-03-01');
                
                INSERT INTO NhanVien (HoTen, NgaySinh, GioiTinh, SoCMND, TrinhDoChuyenMon, DiaChi, MaPhong, MaChucDanh, LoaiNhanVien, NgayVaoCongTy)
                VALUES (N'Phạm Thị Dung', '1995-12-15', N'Nữ', '123456789015', N'Cao đẳng', N'Hà Nội', 4, 4, N'Hợp đồng', '2021-01-15');
                
                INSERT INTO NhanVien (HoTen, NgaySinh, GioiTinh, SoCMND, TrinhDoChuyenMon, DiaChi, MaPhong, MaChucDanh, LoaiNhanVien, NgayVaoCongTy)
                VALUES (N'Hoàng Văn Em', '1993-08-25', N'Nam', '123456789016', N'Đại học', N'Hải Phòng', 2, 3, N'Hợp đồng', '2021-02-15');";
                
                // Thêm thông tin gia đình mẫu
                string giadinhScript = @"
                -- Thêm thông tin gia đình
                INSERT INTO ThongTinGiaDinh (MaNV, HoTen, QuanHe, NgaySinh, NgheNghiep, DiaChi)
                VALUES (1, N'Nguyễn Văn Cha', N'Bố', '1965-01-01', N'Giáo viên', N'Hà Nội');
                
                INSERT INTO ThongTinGiaDinh (MaNV, HoTen, QuanHe, NgaySinh, NgheNghiep, DiaChi)
                VALUES (1, N'Nguyễn Thị Mẹ', N'Mẹ', '1968-05-10', N'Nội trợ', N'Hà Nội');
                
                INSERT INTO ThongTinGiaDinh (MaNV, HoTen, QuanHe, NgaySinh, NgheNghiep, DiaChi)
                VALUES (2, N'Trần Văn Cha', N'Bố', '1962-03-15', N'Kỹ sư', N'Hồ Chí Minh');
                
                INSERT INTO ThongTinGiaDinh (MaNV, HoTen, QuanHe, NgaySinh, NgheNghiep, DiaChi)
                VALUES (2, N'Trần Thị Mẹ', N'Mẹ', '1965-07-20', N'Bác sĩ', N'Hồ Chí Minh');
                
                INSERT INTO ThongTinGiaDinh (MaNV, HoTen, QuanHe, NgaySinh, NgheNghiep, DiaChi)
                VALUES (3, N'Lê Thị Vợ', N'Vợ', '1990-12-25', N'Giáo viên', N'Đà Nẵng');";
                
                // Thêm hợp đồng mẫu
                string hopdongScript = @"
                -- Thêm hợp đồng
                INSERT INTO HopDong (MaNV, SoHopDong, LoaiHopDong, NgayKy, NgayBatDau, NgayKetThuc, LuongTheoHopDong, NoiDung)
                VALUES (1, 'HD2020-001', N'Không thời hạn', '2020-01-01', '2020-01-01', NULL, 10000000, N'Hợp đồng không thời hạn');
                
                INSERT INTO HopDong (MaNV, SoHopDong, LoaiHopDong, NgayKy, NgayBatDau, NgayKetThuc, LuongTheoHopDong, NoiDung)
                VALUES (2, 'HD2020-002', N'Không thời hạn', '2020-02-01', '2020-02-01', NULL, 12000000, N'Hợp đồng không thời hạn');
                
                INSERT INTO HopDong (MaNV, SoHopDong, LoaiHopDong, NgayKy, NgayBatDau, NgayKetThuc, LuongTheoHopDong, NoiDung)
                VALUES (3, 'HD2020-003', N'Không thời hạn', '2020-03-01', '2020-03-01', NULL, 11000000, N'Hợp đồng không thời hạn');
                
                INSERT INTO HopDong (MaNV, SoHopDong, LoaiHopDong, NgayKy, NgayBatDau, NgayKetThuc, LuongTheoHopDong, NoiDung)
                VALUES (4, 'HD2021-001', N'Có thời hạn 12 tháng', '2021-01-15', '2021-01-15', '2022-01-15', 9000000, N'Hợp đồng có thời hạn 12 tháng');
                
                INSERT INTO HopDong (MaNV, SoHopDong, LoaiHopDong, NgayKy, NgayBatDau, NgayKetThuc, LuongTheoHopDong, NoiDung)
                VALUES (5, 'HD2021-002', N'Có thời hạn 12 tháng', '2021-02-15', '2021-02-15', '2022-02-15', 9500000, N'Hợp đồng có thời hạn 12 tháng');";
                
                // Thêm lương mẫu
                string luongScript = @"
                -- Thêm lương
                INSERT INTO Luong (MaNV, Thang, Nam, MaBacLuong, LuongCoBan, TongPhuCap, BaoHiem, TongNgayCong, TongLuong)
                VALUES (1, 1, 2023, 4, 10000000, 3200000, 1000000, 22, 12200000);
                
                INSERT INTO Luong (MaNV, Thang, Nam, MaBacLuong, LuongCoBan, TongPhuCap, BaoHiem, TongNgayCong, TongLuong)
                VALUES (2, 1, 2023, 3, 7500000, 2700000, 750000, 22, 9450000);
                
                INSERT INTO Luong (MaNV, Thang, Nam, MaBacLuong, LuongCoBan, TongPhuCap, BaoHiem, TongNgayCong, TongLuong)
                VALUES (3, 1, 2023, 3, 7500000, 2500000, 750000, 22, 9250000);
                
                INSERT INTO Luong (MaNV, Thang, Nam, MaBacLuong, LuongCoBan, TongPhuCap, BaoHiem, TongNgayCong, TongLuong)
                VALUES (4, 1, 2023, 1, 5000000, 1200000, 500000, 22, 5700000);
                
                INSERT INTO Luong (MaNV, Thang, Nam, MaBacLuong, LuongCoBan, TongPhuCap, BaoHiem, TongNgayCong, TongLuong)
                VALUES (5, 1, 2023, 2, 6000000, 1200000, 600000, 22, 6600000);";
                
                // Thêm khen thưởng mẫu
                string khenthuongScript = @"
                -- Thêm khen thưởng
                INSERT INTO KhenThuong (MaNV, NgayKhenThuong, HinhThuc, LyDo, SoTien)
                VALUES (1, '2023-01-15', N'Thưởng năm', N'Hoàn thành xuất sắc nhiệm vụ', 5000000);
                
                INSERT INTO KhenThuong (MaNV, NgayKhenThuong, HinhThuc, LyDo, SoTien)
                VALUES (2, '2023-01-15', N'Thưởng năm', N'Hoàn thành xuất sắc nhiệm vụ', 3000000);
                
                INSERT INTO KhenThuong (MaNV, NgayKhenThuong, HinhThuc, LyDo, SoTien)
                VALUES (3, '2023-01-15', N'Thưởng năm', N'Hoàn thành tốt nhiệm vụ', 2000000);
                
                INSERT INTO KhenThuong (MaNV, NgayKhenThuong, HinhThuc, LyDo, SoTien)
                VALUES (4, '2023-03-08', N'Thưởng lễ', N'Ngày Quốc tế Phụ nữ', 500000);
                
                INSERT INTO KhenThuong (MaNV, NgayKhenThuong, HinhThuc, LyDo, SoTien)
                VALUES (5, '2023-05-01', N'Thưởng lễ', N'Ngày Quốc tế Lao động', 500000);";
                
                // Chèn các script của bạn trên cơ sở dữ liệu
                ExecuteNonQueryScript(nhanVienScript, con);
                ExecuteNonQueryScript(giadinhScript, con);
                ExecuteNonQueryScript(hopdongScript, con);
                ExecuteNonQueryScript(luongScript, con);
                ExecuteNonQueryScript(khenthuongScript, con);
                
                // Cập nhật trưởng phòng sau khi có nhân viên
                string updateTruongPhongScript = @"
                -- Cập nhật trưởng phòng
                UPDATE PhongBan SET TruongPhong = 1 WHERE MaPhong = 1;
                UPDATE PhongBan SET TruongPhong = 2 WHERE MaPhong = 2;
                UPDATE PhongBan SET TruongPhong = 3 WHERE MaPhong = 3;";
                
                ExecuteNonQueryScript(updateTruongPhongScript, con);
                
                con.Close();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm dữ liệu mẫu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private static void ExecuteNonQueryScript(string script, SqlConnection con)
        {
            using (SqlCommand cmd = new SqlCommand(script, con))
            {
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi thực thi script: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
} 