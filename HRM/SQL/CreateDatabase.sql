-- Tạo bảng PhongBan
CREATE TABLE PhongBan (
    MaPhong INT IDENTITY(1,1) PRIMARY KEY,
    TenPhong NVARCHAR(100) NOT NULL,
    MoTa NVARCHAR(255),
    TruongPhong INT
);
GO

-- Tạo bảng ChucDanh
CREATE TABLE ChucDanh (
    MaChucDanh INT IDENTITY(1,1) PRIMARY KEY,
    TenChucDanh NVARCHAR(100) NOT NULL,
    MoTa NVARCHAR(255)
);
GO

-- Tạo bảng NhanVien
CREATE TABLE NhanVien (
    MaNV INT IDENTITY(1,1) PRIMARY KEY,
    HoTen NVARCHAR(100) NOT NULL,
    NgaySinh DATE NOT NULL,
    GioiTinh NVARCHAR(10) NOT NULL,
    SoCMND VARCHAR(15) UNIQUE NOT NULL,
    TrinhDoChuyenMon NVARCHAR(100),
    TrinhDoNgoaiNgu NVARCHAR(100),
    HoKhauThuongTru NVARCHAR(255),
    DiaChi NVARCHAR(255),
    DanToc NVARCHAR(50),
    TonGiao NVARCHAR(50),
    NgayVaoDoan DATE,
    NgayVaoDang DATE,
    DienChinhSach NVARCHAR(100),
    MaPhong INT FOREIGN KEY REFERENCES PhongBan(MaPhong),
    MaChucDanh INT FOREIGN KEY REFERENCES ChucDanh(MaChucDanh),
    LoaiNhanVien NVARCHAR(20) CHECK (LoaiNhanVien IN (N'Biên chế', N'Hợp đồng')),
    NgayVaoCongTy DATE NOT NULL,
    NgayNghiViec DATE,
    TrangThai NVARCHAR(20) DEFAULT N'Đang làm việc',
    HinhAnh VARBINARY(MAX)
);
GO

-- Tạo khóa ngoại cho bảng PhongBan
ALTER TABLE PhongBan ADD CONSTRAINT FK_PhongBan_NhanVien FOREIGN KEY (TruongPhong) REFERENCES NhanVien(MaNV);
GO

-- Tạo bảng ThongTinGiaDinh
CREATE TABLE ThongTinGiaDinh (
    MaThongTin INT IDENTITY(1,1) PRIMARY KEY,
    MaNV INT FOREIGN KEY REFERENCES NhanVien(MaNV),
    HoTen NVARCHAR(100) NOT NULL,
    QuanHe NVARCHAR(50) NOT NULL,
    NgaySinh DATE,
    NgheNghiep NVARCHAR(100),
    DiaChi NVARCHAR(255),
    GhiChu NVARCHAR(255)
);
GO

-- Tạo bảng QuaTrinhCongTac
CREATE TABLE QuaTrinhCongTac (
    MaQuaTrinh INT IDENTITY(1,1) PRIMARY KEY,
    MaNV INT FOREIGN KEY REFERENCES NhanVien(MaNV),
    TuNgay DATE NOT NULL,
    DenNgay DATE,
    ChucDanh NVARCHAR(100),
    PhongBan NVARCHAR(100),
    MoTaCongViec NVARCHAR(255),
    GhiChu NVARCHAR(255)
);
GO

-- Tạo bảng KhenThuong
CREATE TABLE KhenThuong (
    MaKhenThuong INT IDENTITY(1,1) PRIMARY KEY,
    MaNV INT FOREIGN KEY REFERENCES NhanVien(MaNV),
    NgayKhenThuong DATE NOT NULL,
    HinhThuc NVARCHAR(100) NOT NULL,
    LyDo NVARCHAR(255),
    SoTien DECIMAL(18,2),
    GhiChu NVARCHAR(255)
);
GO

-- Tạo bảng KyLuat
CREATE TABLE KyLuat (
    MaKyLuat INT IDENTITY(1,1) PRIMARY KEY,
    MaNV INT FOREIGN KEY REFERENCES NhanVien(MaNV),
    NgayKyLuat DATE NOT NULL,
    HinhThuc NVARCHAR(100) NOT NULL,
    LyDo NVARCHAR(255),
    SoTien DECIMAL(18,2),
    GhiChu NVARCHAR(255)
);
GO

-- Tạo bảng BacLuong
CREATE TABLE BacLuong (
    MaBacLuong INT IDENTITY(1,1) PRIMARY KEY,
    TenBac NVARCHAR(50) NOT NULL,
    HeSo FLOAT NOT NULL,
    LuongCoBan DECIMAL(18,2) NOT NULL
);
GO

-- Tạo bảng TangLuong
CREATE TABLE TangLuong (
    MaTangLuong INT IDENTITY(1,1) PRIMARY KEY,
    MaNV INT FOREIGN KEY REFERENCES NhanVien(MaNV),
    NgayTangLuong DATE NOT NULL,
    MaBacLuongCu INT FOREIGN KEY REFERENCES BacLuong(MaBacLuong),
    MaBacLuongMoi INT FOREIGN KEY REFERENCES BacLuong(MaBacLuong),
    LyDo NVARCHAR(255),
    GhiChu NVARCHAR(255)
);
GO

-- Tạo bảng ChamCong
CREATE TABLE ChamCong (
    MaChamCong INT IDENTITY(1,1) PRIMARY KEY,
    MaNV INT FOREIGN KEY REFERENCES NhanVien(MaNV),
    NgayChamCong DATE NOT NULL,
    TrangThai NVARCHAR(20) CHECK (TrangThai IN (N'Đi làm', N'Nghỉ phép', N'Nghỉ ốm', N'Nghỉ không lương', N'Đi công tác')),
    GioVao TIME,
    GioRa TIME,
    GhiChu NVARCHAR(255),
    CONSTRAINT UQ_ChamCong UNIQUE (MaNV, NgayChamCong)
);
GO

-- Tạo bảng PhuCap
CREATE TABLE PhuCap (
    MaPhuCap INT IDENTITY(1,1) PRIMARY KEY,
    TenPhuCap NVARCHAR(100) NOT NULL,
    GiaTri DECIMAL(18,2) NOT NULL,
    MoTa NVARCHAR(255)
);
GO

-- Tạo bảng NhanVien_PhuCap
CREATE TABLE NhanVien_PhuCap (
    MaNV INT FOREIGN KEY REFERENCES NhanVien(MaNV),
    MaPhuCap INT FOREIGN KEY REFERENCES PhuCap(MaPhuCap),
    PRIMARY KEY (MaNV, MaPhuCap)
);
GO

-- Tạo bảng HopDong
CREATE TABLE HopDong (
    MaHopDong INT IDENTITY(1,1) PRIMARY KEY,
    MaNV INT FOREIGN KEY REFERENCES NhanVien(MaNV),
    SoHopDong NVARCHAR(50) NOT NULL,
    LoaiHopDong NVARCHAR(50) NOT NULL,
    NgayKy DATE NOT NULL,
    NgayBatDau DATE NOT NULL,
    NgayKetThuc DATE,
    LuongTheoHopDong DECIMAL(18,2) NOT NULL,
    NoiDung NVARCHAR(MAX),
    TrangThai NVARCHAR(50) DEFAULT N'Đang hiệu lực'
);
GO

-- Tạo bảng Luong
CREATE TABLE Luong (
    MaLuong INT IDENTITY(1,1) PRIMARY KEY,
    MaNV INT FOREIGN KEY REFERENCES NhanVien(MaNV),
    Thang INT NOT NULL,
    Nam INT NOT NULL,
    MaBacLuong INT FOREIGN KEY REFERENCES BacLuong(MaBacLuong),
    LuongCoBan DECIMAL(18,2) NOT NULL,
    TongPhuCap DECIMAL(18,2) NOT NULL,
    BaoHiem DECIMAL(18,2) NOT NULL,
    TongNgayCong INT NOT NULL,
    TongLuong DECIMAL(18,2) NOT NULL,
    GhiChu NVARCHAR(255),
    CONSTRAINT UQ_Luong UNIQUE (MaNV, Thang, Nam)
);
GO

-- Tạo bảng NgayLe
CREATE TABLE NgayLe (
    MaNgayLe INT IDENTITY(1,1) PRIMARY KEY,
    TenNgayLe NVARCHAR(100) NOT NULL,
    Ngay DATE NOT NULL,
    MoTa NVARCHAR(255)
);
GO

-- Tạo bảng NguoiDung
CREATE TABLE NguoiDung (
    MaNguoiDung INT IDENTITY(1,1) PRIMARY KEY,
    TenDangNhap NVARCHAR(50) NOT NULL UNIQUE,
    MatKhau NVARCHAR(100) NOT NULL,
    HoTen NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100),
    DienThoai NVARCHAR(20),
    VaiTro NVARCHAR(20) NOT NULL CHECK (VaiTro IN (N'Admin', N'Quản lý', N'Nhân viên')),
    NgayTao DATETIME NOT NULL,
    TrangThai BIT NOT NULL DEFAULT 1,
    MaNV INT FOREIGN KEY REFERENCES NhanVien(MaNV)
);
GO

-- Thêm dữ liệu mẫu
-- Thêm phòng ban
INSERT INTO PhongBan (TenPhong, MoTa) VALUES (N'Ban Giám đốc', N'Ban lãnh đạo công ty');
INSERT INTO PhongBan (TenPhong, MoTa) VALUES (N'Phòng Nhân sự', N'Quản lý nhân sự và tuyển dụng');
INSERT INTO PhongBan (TenPhong, MoTa) VALUES (N'Phòng Kế toán', N'Quản lý tài chính, kế toán');
INSERT INTO PhongBan (TenPhong, MoTa) VALUES (N'Phòng Công nghệ', N'Phát triển công nghệ và kỹ thuật');
GO

-- Thêm chức danh
INSERT INTO ChucDanh (TenChucDanh, MoTa) VALUES (N'Giám đốc', N'Giám đốc điều hành');
INSERT INTO ChucDanh (TenChucDanh, MoTa) VALUES (N'Trưởng phòng', N'Quản lý phòng ban');
INSERT INTO ChucDanh (TenChucDanh, MoTa) VALUES (N'Phó phòng', N'Phó quản lý phòng ban');
INSERT INTO ChucDanh (TenChucDanh, MoTa) VALUES (N'Nhân viên', N'Nhân viên thông thường');
GO

-- Thêm bậc lương
INSERT INTO BacLuong (TenBac, HeSo, LuongCoBan) VALUES (N'Bậc 1', 1.0, 5000000);
INSERT INTO BacLuong (TenBac, HeSo, LuongCoBan) VALUES (N'Bậc 2', 1.2, 5000000);
INSERT INTO BacLuong (TenBac, HeSo, LuongCoBan) VALUES (N'Bậc 3', 1.5, 5000000);
INSERT INTO BacLuong (TenBac, HeSo, LuongCoBan) VALUES (N'Bậc 4', 2.0, 5000000);
INSERT INTO BacLuong (TenBac, HeSo, LuongCoBan) VALUES (N'Bậc 5', 2.5, 5000000);
GO

-- Thêm phụ cấp
INSERT INTO PhuCap (TenPhuCap, GiaTri, MoTa) VALUES (N'Phụ cấp ăn trưa', 1000000, N'Phụ cấp ăn trưa hàng tháng');
INSERT INTO PhuCap (TenPhuCap, GiaTri, MoTa) VALUES (N'Phụ cấp xăng xe', 500000, N'Phụ cấp xăng xe hàng tháng');
INSERT INTO PhuCap (TenPhuCap, GiaTri, MoTa) VALUES (N'Phụ cấp điện thoại', 200000, N'Phụ cấp điện thoại hàng tháng');
INSERT INTO PhuCap (TenPhuCap, GiaTri, MoTa) VALUES (N'Phụ cấp chức vụ', 2000000, N'Phụ cấp dành cho cấp quản lý');
GO

-- Thêm tài khoản admin
INSERT INTO NguoiDung (TenDangNhap, MatKhau, HoTen, Email, VaiTro, NgayTao) VALUES ('admin', '123456', N'Quản trị viên', 'admin@example.com', N'Admin', GETDATE());
GO

-- Thêm ngày lễ
INSERT INTO NgayLe (TenNgayLe, Ngay, MoTa) VALUES (N'Tết Dương lịch', '2023-01-01', N'Ngày 1/1 hàng năm');
INSERT INTO NgayLe (TenNgayLe, Ngay, MoTa) VALUES (N'Tết Nguyên đán', '2023-01-22', N'Từ ngày 30/12 đến mùng 5/1 âm lịch');
INSERT INTO NgayLe (TenNgayLe, Ngay, MoTa) VALUES (N'Tết Nguyên đán', '2023-01-23', N'Từ ngày 30/12 đến mùng 5/1 âm lịch');
INSERT INTO NgayLe (TenNgayLe, Ngay, MoTa) VALUES (N'Tết Nguyên đán', '2023-01-24', N'Từ ngày 30/12 đến mùng 5/1 âm lịch');
INSERT INTO NgayLe (TenNgayLe, Ngay, MoTa) VALUES (N'Tết Nguyên đán', '2023-01-25', N'Từ ngày 30/12 đến mùng 5/1 âm lịch');
INSERT INTO NgayLe (TenNgayLe, Ngay, MoTa) VALUES (N'Giỗ Tổ Hùng Vương', '2023-04-29', N'Mùng 10/3 âm lịch');
INSERT INTO NgayLe (TenNgayLe, Ngay, MoTa) VALUES (N'Ngày Giải phóng', '2023-04-30', N'Ngày 30/4 hàng năm');
INSERT INTO NgayLe (TenNgayLe, Ngay, MoTa) VALUES (N'Ngày Quốc tế Lao động', '2023-05-01', N'Ngày 1/5 hàng năm');
INSERT INTO NgayLe (TenNgayLe, Ngay, MoTa) VALUES (N'Quốc khánh', '2023-09-02', N'Ngày 2/9 hàng năm');
GO 