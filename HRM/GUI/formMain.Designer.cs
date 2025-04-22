using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace HRM.GUI
{
    partial class formMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.mnuHeThong = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDangXuat = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuDoiMatKhau = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuNguoiDung = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuSaoLuu = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuPhucHoi = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuThoat = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDanhMuc = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuPhongBan = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuChucDanh = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuNhanSu = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHoSoNhanVien = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHopDong = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuThanNhan = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuQuaTrinhCongTac = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuKhenThuong = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuKyLuat = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuChamCong = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuChamCongNgay = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuBangChamCong = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTinhLuong = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuBangLuong = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTangLuong = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuBaoCao = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuBaoCaoNhanSu = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuBaoCaoLuong = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuBaoCaoSinhNhat = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuBaoCaoTangLuong = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuBaoCaoNghiHuu = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTroGiup = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHuongDan = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuGioiThieu = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblTenNguoiDung = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblQuyen = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblTongSo = new System.Windows.Forms.ToolStripStatusLabel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnTimKiem = new System.Windows.Forms.Button();
            this.cboChucDanh = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cboPhong = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtTimKiem = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvNhanVien = new System.Windows.Forms.DataGridView();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNhanVien)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.menuStrip1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuStrip1.ForeColor = System.Drawing.Color.White;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuHeThong,
            this.mnuDanhMuc,
            this.mnuNhanSu,
            this.mnuChamCong,
            this.mnuTinhLuong,
            this.mnuBaoCao,
            this.mnuTroGiup});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(984, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // mnuHeThong
            // 
            this.mnuHeThong.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuDangXuat,
            this.toolStripSeparator1,
            this.mnuDoiMatKhau,
            this.mnuNguoiDung,
            this.toolStripSeparator2,
            this.mnuSaoLuu,
            this.mnuPhucHoi,
            this.toolStripSeparator3,
            this.mnuThoat});
            this.mnuHeThong.Name = "mnuHeThong";
            this.mnuHeThong.Size = new System.Drawing.Size(71, 20);
            this.mnuHeThong.Text = "Hệ thống";
            // 
            // mnuDangXuat
            // 
            this.mnuDangXuat.Name = "mnuDangXuat";
            this.mnuDangXuat.Size = new System.Drawing.Size(180, 22);
            this.mnuDangXuat.Text = "Đăng xuất";
            this.mnuDangXuat.Click += new System.EventHandler(this.mnuDangXuat_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(177, 6);
            // 
            // mnuDoiMatKhau
            // 
            this.mnuDoiMatKhau.Name = "mnuDoiMatKhau";
            this.mnuDoiMatKhau.Size = new System.Drawing.Size(180, 22);
            this.mnuDoiMatKhau.Text = "Đổi mật khẩu";
            // 
            // mnuNguoiDung
            // 
            this.mnuNguoiDung.Name = "mnuNguoiDung";
            this.mnuNguoiDung.Size = new System.Drawing.Size(180, 22);
            this.mnuNguoiDung.Text = "Quản lý người dùng";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(177, 6);
            // 
            // mnuSaoLuu
            // 
            this.mnuSaoLuu.Name = "mnuSaoLuu";
            this.mnuSaoLuu.Size = new System.Drawing.Size(180, 22);
            this.mnuSaoLuu.Text = "Sao lưu dữ liệu";
            // 
            // mnuPhucHoi
            // 
            this.mnuPhucHoi.Name = "mnuPhucHoi";
            this.mnuPhucHoi.Size = new System.Drawing.Size(180, 22);
            this.mnuPhucHoi.Text = "Phục hồi dữ liệu";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(177, 6);
            // 
            // mnuThoat
            // 
            this.mnuThoat.Name = "mnuThoat";
            this.mnuThoat.Size = new System.Drawing.Size(180, 22);
            this.mnuThoat.Text = "Thoát";
            this.mnuThoat.Click += new System.EventHandler(this.mnuThoat_Click);
            // 
            // mnuDanhMuc
            // 
            this.mnuDanhMuc.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuPhongBan,
            this.mnuChucDanh});
            this.mnuDanhMuc.Name = "mnuDanhMuc";
            this.mnuDanhMuc.Size = new System.Drawing.Size(74, 20);
            this.mnuDanhMuc.Text = "Danh mục";
            // 
            // mnuPhongBan
            // 
            this.mnuPhongBan.Name = "mnuPhongBan";
            this.mnuPhongBan.Size = new System.Drawing.Size(180, 22);
            this.mnuPhongBan.Text = "Phòng ban";
            // 
            // mnuChucDanh
            // 
            this.mnuChucDanh.Name = "mnuChucDanh";
            this.mnuChucDanh.Size = new System.Drawing.Size(180, 22);
            this.mnuChucDanh.Text = "Chức danh";
            // 
            // mnuNhanSu
            // 
            this.mnuNhanSu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuHoSoNhanVien,
            this.mnuHopDong,
            this.mnuThanNhan,
            this.mnuQuaTrinhCongTac,
            this.mnuKhenThuong,
            this.mnuKyLuat});
            this.mnuNhanSu.Name = "mnuNhanSu";
            this.mnuNhanSu.Size = new System.Drawing.Size(64, 20);
            this.mnuNhanSu.Text = "Nhân sự";
            // 
            // mnuHoSoNhanVien
            // 
            this.mnuHoSoNhanVien.Name = "mnuHoSoNhanVien";
            this.mnuHoSoNhanVien.Size = new System.Drawing.Size(180, 22);
            this.mnuHoSoNhanVien.Text = "Hồ sơ nhân viên";
            // 
            // mnuHopDong
            // 
            this.mnuHopDong.Name = "mnuHopDong";
            this.mnuHopDong.Size = new System.Drawing.Size(180, 22);
            this.mnuHopDong.Text = "Hợp đồng";
            // 
            // mnuThanNhan
            // 
            this.mnuThanNhan.Name = "mnuThanNhan";
            this.mnuThanNhan.Size = new System.Drawing.Size(180, 22);
            this.mnuThanNhan.Text = "Thân nhân";
            this.mnuThanNhan.Click += new System.EventHandler(this.mnuThanNhan_Click);
            // 
            // mnuQuaTrinhCongTac
            // 
            this.mnuQuaTrinhCongTac.Name = "mnuQuaTrinhCongTac";
            this.mnuQuaTrinhCongTac.Size = new System.Drawing.Size(180, 22);
            this.mnuQuaTrinhCongTac.Text = "Quá trình công tác";
            this.mnuQuaTrinhCongTac.Click += new System.EventHandler(this.mnuQuaTrinhCongTac_Click);
            // 
            // mnuKhenThuong
            // 
            this.mnuKhenThuong.Name = "mnuKhenThuong";
            this.mnuKhenThuong.Size = new System.Drawing.Size(180, 22);
            this.mnuKhenThuong.Text = "Khen thưởng";
            // 
            // mnuKyLuat
            // 
            this.mnuKyLuat.Name = "mnuKyLuat";
            this.mnuKyLuat.Size = new System.Drawing.Size(180, 22);
            this.mnuKyLuat.Text = "Kỷ luật";
            // 
            // mnuChamCong
            // 
            this.mnuChamCong.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuChamCongNgay,
            this.mnuBangChamCong});
            this.mnuChamCong.Name = "mnuChamCong";
            this.mnuChamCong.Size = new System.Drawing.Size(86, 20);
            this.mnuChamCong.Text = "Chấm công";
            // 
            // mnuChamCongNgay
            // 
            this.mnuChamCongNgay.Name = "mnuChamCongNgay";
            this.mnuChamCongNgay.Size = new System.Drawing.Size(180, 22);
            this.mnuChamCongNgay.Text = "Chấm công ngày";
            this.mnuChamCongNgay.Click += new System.EventHandler(this.mnuChamCongNgay_Click);
            // 
            // mnuBangChamCong
            // 
            this.mnuBangChamCong.Name = "mnuBangChamCong";
            this.mnuBangChamCong.Size = new System.Drawing.Size(180, 22);
            this.mnuBangChamCong.Text = "Bảng chấm công";
            this.mnuBangChamCong.Click += new System.EventHandler(this.mnuBangChamCong_Click);
            // 
            // mnuTinhLuong
            // 
            this.mnuTinhLuong.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuBangLuong,
            this.mnuTangLuong});
            this.mnuTinhLuong.Name = "mnuTinhLuong";
            this.mnuTinhLuong.Size = new System.Drawing.Size(79, 20);
            this.mnuTinhLuong.Text = "Tính lương";
            // 
            // mnuBangLuong
            // 
            this.mnuBangLuong.Name = "mnuBangLuong";
            this.mnuBangLuong.Size = new System.Drawing.Size(180, 22);
            this.mnuBangLuong.Text = "Bảng lương";
            this.mnuBangLuong.Click += new System.EventHandler(this.mnuBangLuong_Click);
            // 
            // mnuTangLuong
            // 
            this.mnuTangLuong.Name = "mnuTangLuong";
            this.mnuTangLuong.Size = new System.Drawing.Size(180, 22);
            this.mnuTangLuong.Text = "Tăng lương";
            this.mnuTangLuong.Click += new System.EventHandler(this.mnuTangLuong_Click);
            // 
            // mnuBaoCao
            // 
            this.mnuBaoCao.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuBaoCaoNhanSu,
            this.mnuBaoCaoLuong,
            this.mnuBaoCaoSinhNhat,
            this.mnuBaoCaoTangLuong,
            this.mnuBaoCaoNghiHuu});
            this.mnuBaoCao.Name = "mnuBaoCao";
            this.mnuBaoCao.Size = new System.Drawing.Size(61, 20);
            this.mnuBaoCao.Text = "Báo cáo";
            // 
            // mnuBaoCaoNhanSu
            // 
            this.mnuBaoCaoNhanSu.Name = "mnuBaoCaoNhanSu";
            this.mnuBaoCaoNhanSu.Size = new System.Drawing.Size(180, 22);
            this.mnuBaoCaoNhanSu.Text = "Báo cáo nhân sự";
            // 
            // mnuBaoCaoLuong
            // 
            this.mnuBaoCaoLuong.Name = "mnuBaoCaoLuong";
            this.mnuBaoCaoLuong.Size = new System.Drawing.Size(180, 22);
            this.mnuBaoCaoLuong.Text = "Báo cáo lương";
            // 
            // mnuBaoCaoSinhNhat
            // 
            this.mnuBaoCaoSinhNhat.Name = "mnuBaoCaoSinhNhat";
            this.mnuBaoCaoSinhNhat.Size = new System.Drawing.Size(180, 22);
            this.mnuBaoCaoSinhNhat.Text = "Danh sách sinh nhật";
            this.mnuBaoCaoSinhNhat.Click += new System.EventHandler(this.mnuBaoCaoSinhNhat_Click);
            // 
            // mnuBaoCaoTangLuong
            // 
            this.mnuBaoCaoTangLuong.Name = "mnuBaoCaoTangLuong";
            this.mnuBaoCaoTangLuong.Size = new System.Drawing.Size(180, 22);
            this.mnuBaoCaoTangLuong.Text = "Danh sách tăng lương";
            this.mnuBaoCaoTangLuong.Click += new System.EventHandler(this.mnuBaoCaoTangLuong_Click);
            // 
            // mnuBaoCaoNghiHuu
            // 
            this.mnuBaoCaoNghiHuu.Name = "mnuBaoCaoNghiHuu";
            this.mnuBaoCaoNghiHuu.Size = new System.Drawing.Size(180, 22);
            this.mnuBaoCaoNghiHuu.Text = "Danh sách nghỉ hưu";
            this.mnuBaoCaoNghiHuu.Click += new System.EventHandler(this.mnuBaoCaoNghiHuu_Click);
            // 
            // mnuTroGiup
            // 
            this.mnuTroGiup.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuHuongDan,
            this.mnuGioiThieu});
            this.mnuTroGiup.Name = "mnuTroGiup";
            this.mnuTroGiup.Size = new System.Drawing.Size(64, 20);
            this.mnuTroGiup.Text = "Trợ giúp";
            // 
            // mnuHuongDan
            // 
            this.mnuHuongDan.Name = "mnuHuongDan";
            this.mnuHuongDan.Size = new System.Drawing.Size(180, 22);
            this.mnuHuongDan.Text = "Hướng dẫn";
            // 
            // mnuGioiThieu
            // 
            this.mnuGioiThieu.Name = "mnuGioiThieu";
            this.mnuGioiThieu.Size = new System.Drawing.Size(180, 22);
            this.mnuGioiThieu.Text = "Giới thiệu";
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.statusStrip1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.statusStrip1.ForeColor = System.Drawing.Color.White;
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblTenNguoiDung,
            this.lblQuyen,
            this.lblTongSo});
            this.statusStrip1.Location = new System.Drawing.Point(0, 539);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(984, 22);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblTenNguoiDung
            // 
            this.lblTenNguoiDung.Name = "lblTenNguoiDung";
            this.lblTenNguoiDung.Size = new System.Drawing.Size(97, 17);
            this.lblTenNguoiDung.Text = "Tên người dùng";
            // 
            // lblQuyen
            // 
            this.lblQuyen.Name = "lblQuyen";
            this.lblQuyen.Size = new System.Drawing.Size(45, 17);
            this.lblQuyen.Text = "Quyền";
            // 
            // lblTongSo
            // 
            this.lblTongSo.Name = "lblTongSo";
            this.lblTongSo.Size = new System.Drawing.Size(118, 17);
            this.lblTongSo.Text = "Tổng số: 0 nhân viên";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel1.Controls.Add(this.btnTimKiem);
            this.panel1.Controls.Add(this.cboChucDanh);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.cboPhong);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.txtTimKiem);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 24);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(10);
            this.panel1.Size = new System.Drawing.Size(984, 60);
            this.panel1.TabIndex = 1;
            // 
            // btnTimKiem
            // 
            this.btnTimKiem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.btnTimKiem.FlatAppearance.BorderSize = 0;
            this.btnTimKiem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTimKiem.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTimKiem.ForeColor = System.Drawing.Color.White;
            this.btnTimKiem.Location = new System.Drawing.Point(840, 15);
            this.btnTimKiem.Name = "btnTimKiem";
            this.btnTimKiem.Size = new System.Drawing.Size(119, 30);
            this.btnTimKiem.TabIndex = 6;
            this.btnTimKiem.Text = "Tìm kiếm";
            this.btnTimKiem.UseVisualStyleBackColor = false;
            this.btnTimKiem.Click += new System.EventHandler(this.btnTimKiem_Click);
            // 
            // cboChucDanh
            // 
            this.cboChucDanh.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboChucDanh.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboChucDanh.FormattingEnabled = true;
            this.cboChucDanh.Location = new System.Drawing.Point(657, 17);
            this.cboChucDanh.Name = "cboChucDanh";
            this.cboChucDanh.Size = new System.Drawing.Size(170, 25);
            this.cboChucDanh.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(586, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 17);
            this.label3.TabIndex = 4;
            this.label3.Text = "Chức danh:";
            // 
            // cboPhong
            // 
            this.cboPhong.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPhong.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboPhong.FormattingEnabled = true;
            this.cboPhong.Location = new System.Drawing.Point(417, 17);
            this.cboPhong.Name = "cboPhong";
            this.cboPhong.Size = new System.Drawing.Size(156, 25);
            this.cboPhong.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(367, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "Phòng:";
            // 
            // txtTimKiem
            // 
            this.txtTimKiem.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTimKiem.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTimKiem.Location = new System.Drawing.Point(70, 17);
            this.txtTimKiem.Name = "txtTimKiem";
            this.txtTimKiem.Size = new System.Drawing.Size(286, 25);
            this.txtTimKiem.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(13, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Họ và tên:";
            // 
            // dgvNhanVien
            // 
            this.dgvNhanVien.AllowUserToAddRows = false;
            this.dgvNhanVien.AllowUserToDeleteRows = false;
            this.dgvNhanVien.BackgroundColor = System.Drawing.Color.White;
            this.dgvNhanVien.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvNhanVien.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvNhanVien.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvNhanVien.Location = new System.Drawing.Point(0, 84);
            this.dgvNhanVien.Name = "dgvNhanVien";
            this.dgvNhanVien.ReadOnly = true;
            this.dgvNhanVien.RowHeadersVisible = false;
            this.dgvNhanVien.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvNhanVien.Size = new System.Drawing.Size(984, 455);
            this.dgvNhanVien.TabIndex = 2;
            // 
            // formMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 561);
            this.Controls.Add(this.dgvNhanVien);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.statusStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(900, 600);
            this.Name = "formMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Phần mềm Quản lý Nhân sự";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.formMain_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNhanVien)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem mnuHeThong;
        private System.Windows.Forms.ToolStripMenuItem mnuDangXuat;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem mnuDoiMatKhau;
        private System.Windows.Forms.ToolStripMenuItem mnuNguoiDung;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem mnuSaoLuu;
        private System.Windows.Forms.ToolStripMenuItem mnuPhucHoi;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem mnuThoat;
        private System.Windows.Forms.ToolStripMenuItem mnuDanhMuc;
        private System.Windows.Forms.ToolStripMenuItem mnuPhongBan;
        private System.Windows.Forms.ToolStripMenuItem mnuChucDanh;
        private System.Windows.Forms.ToolStripMenuItem mnuNhanSu;
        private System.Windows.Forms.ToolStripMenuItem mnuHoSoNhanVien;
        private System.Windows.Forms.ToolStripMenuItem mnuHopDong;
        private System.Windows.Forms.ToolStripMenuItem mnuThanNhan;
        private System.Windows.Forms.ToolStripMenuItem mnuQuaTrinhCongTac;
        private System.Windows.Forms.ToolStripMenuItem mnuKhenThuong;
        private System.Windows.Forms.ToolStripMenuItem mnuKyLuat;
        private System.Windows.Forms.ToolStripMenuItem mnuChamCong;
        private System.Windows.Forms.ToolStripMenuItem mnuChamCongNgay;
        private System.Windows.Forms.ToolStripMenuItem mnuBangChamCong;
        private System.Windows.Forms.ToolStripMenuItem mnuTinhLuong;
        private System.Windows.Forms.ToolStripMenuItem mnuBangLuong;
        private System.Windows.Forms.ToolStripMenuItem mnuTangLuong;
        private System.Windows.Forms.ToolStripMenuItem mnuBaoCao;
        private System.Windows.Forms.ToolStripMenuItem mnuBaoCaoNhanSu;
        private System.Windows.Forms.ToolStripMenuItem mnuBaoCaoLuong;
        private System.Windows.Forms.ToolStripMenuItem mnuBaoCaoSinhNhat;
        private System.Windows.Forms.ToolStripMenuItem mnuBaoCaoTangLuong;
        private System.Windows.Forms.ToolStripMenuItem mnuBaoCaoNghiHuu;
        private System.Windows.Forms.ToolStripMenuItem mnuTroGiup;
        private System.Windows.Forms.ToolStripMenuItem mnuHuongDan;
        private System.Windows.Forms.ToolStripMenuItem mnuGioiThieu;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblTenNguoiDung;
        private System.Windows.Forms.ToolStripStatusLabel lblQuyen;
        private System.Windows.Forms.ToolStripStatusLabel lblTongSo;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnTimKiem;
        private System.Windows.Forms.ComboBox cboChucDanh;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cboPhong;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtTimKiem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvNhanVien;
    }
}