namespace HRM.Forms
{
    partial class frmHoSoCaNhan
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            this.dgvNhanVien = new System.Windows.Forms.DataGridView();
            this.lblTongSo = new System.Windows.Forms.Label();
            this.txtTimKiem = new System.Windows.Forms.TextBox();
            this.btnTimKiem = new System.Windows.Forms.Button();
            this.btnThem = new System.Windows.Forms.Button();
            this.btnSua = new System.Windows.Forms.Button();
            this.btnXoa = new System.Windows.Forms.Button();
            this.btnThoat = new System.Windows.Forms.Button();
            this.lblHoTen = new System.Windows.Forms.Label();
            this.txtHoTen = new System.Windows.Forms.TextBox();
            this.lblNgaySinh = new System.Windows.Forms.Label();
            this.dtpNgaySinh = new System.Windows.Forms.DateTimePicker();
            this.lblGioiTinh = new System.Windows.Forms.Label();
            this.cboGioiTinh = new System.Windows.Forms.ComboBox();
            this.lblSoCMND = new System.Windows.Forms.Label();
            this.txtSoCMND = new System.Windows.Forms.TextBox();
            this.lblTrinhDoChuyenMon = new System.Windows.Forms.Label();
            this.txtTrinhDoChuyenMon = new System.Windows.Forms.TextBox();
            this.lblTrinhDoNgoaiNgu = new System.Windows.Forms.Label();
            this.txtTrinhDoNgoaiNgu = new System.Windows.Forms.TextBox();
            this.lblHoKhauThuongTru = new System.Windows.Forms.Label();
            this.txtHoKhauThuongTru = new System.Windows.Forms.TextBox();
            this.lblDiaChi = new System.Windows.Forms.Label();
            this.txtDiaChi = new System.Windows.Forms.TextBox();
            this.lblDanToc = new System.Windows.Forms.Label();
            this.txtDanToc = new System.Windows.Forms.TextBox();
            this.lblTonGiao = new System.Windows.Forms.Label();
            this.txtTonGiao = new System.Windows.Forms.TextBox();
            this.lblNgayVaoDoan = new System.Windows.Forms.Label();
            this.dtpNgayVaoDoan = new System.Windows.Forms.DateTimePicker();
            this.lblNgayVaoDang = new System.Windows.Forms.Label();
            this.dtpNgayVaoDang = new System.Windows.Forms.DateTimePicker();
            this.lblDienChinhSach = new System.Windows.Forms.Label();
            this.txtDienChinhSach = new System.Windows.Forms.TextBox();
            this.lblPhongBan = new System.Windows.Forms.Label();
            this.cboPhongBan = new System.Windows.Forms.ComboBox();
            this.lblChucDanh = new System.Windows.Forms.Label();
            this.cboChucDanh = new System.Windows.Forms.ComboBox();
            this.lblLoaiNhanVien = new System.Windows.Forms.Label();
            this.cboLoaiNhanVien = new System.Windows.Forms.ComboBox();
            this.lblNgayVaoCongTy = new System.Windows.Forms.Label();
            this.dtpNgayVaoCongTy = new System.Windows.Forms.DateTimePicker();
            this.lblTrangThai = new System.Windows.Forms.Label();
            this.txtTrangThai = new System.Windows.Forms.TextBox();
            this.lblHinhAnh = new System.Windows.Forms.Label();
            this.picHinhAnh = new System.Windows.Forms.PictureBox();
            this.btnChonHinh = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNhanVien)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picHinhAnh)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvNhanVien
            // 
            this.dgvNhanVien.AllowUserToAddRows = false;
            this.dgvNhanVien.AllowUserToDeleteRows = false;
            this.dgvNhanVien.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvNhanVien.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvNhanVien.Location = new System.Drawing.Point(16, 15);
            this.dgvNhanVien.Margin = new System.Windows.Forms.Padding(4);
            this.dgvNhanVien.MultiSelect = false;
            this.dgvNhanVien.Name = "dgvNhanVien";
            this.dgvNhanVien.ReadOnly = true;
            this.dgvNhanVien.RowHeadersWidth = 51;
            this.dgvNhanVien.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvNhanVien.Size = new System.Drawing.Size(1035, 246);
            this.dgvNhanVien.TabIndex = 0;
            this.dgvNhanVien.SelectionChanged += new System.EventHandler(this.dgvNhanVien_SelectionChanged);
            // 
            // lblTongSo
            // 
            this.lblTongSo.AutoSize = true;
            this.lblTongSo.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.lblTongSo.Location = new System.Drawing.Point(16, 265);
            this.lblTongSo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTongSo.Name = "lblTongSo";
            this.lblTongSo.Size = new System.Drawing.Size(75, 23);
            this.lblTongSo.TabIndex = 1;
            this.lblTongSo.Text = "Tổng số:";
            // 
            // txtTimKiem
            // 
            this.txtTimKiem.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.txtTimKiem.Location = new System.Drawing.Point(16, 292);
            this.txtTimKiem.Margin = new System.Windows.Forms.Padding(4);
            this.txtTimKiem.Name = "txtTimKiem";
            this.txtTimKiem.Size = new System.Drawing.Size(265, 29);
            this.txtTimKiem.TabIndex = 2;
            // 
            // btnTimKiem
            // 
            this.btnTimKiem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.btnTimKiem.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnTimKiem.ForeColor = System.Drawing.Color.White;
            this.btnTimKiem.Location = new System.Drawing.Point(291, 282);
            this.btnTimKiem.Margin = new System.Windows.Forms.Padding(4);
            this.btnTimKiem.Name = "btnTimKiem";
            this.btnTimKiem.Size = new System.Drawing.Size(100, 39);
            this.btnTimKiem.TabIndex = 3;
            this.btnTimKiem.Text = "Tìm kiếm";
            this.btnTimKiem.UseVisualStyleBackColor = false;
            this.btnTimKiem.Click += new System.EventHandler(this.btnTimKiem_Click);
            // 
            // btnThem
            // 
            this.btnThem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.btnThem.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnThem.ForeColor = System.Drawing.Color.White;
            this.btnThem.Location = new System.Drawing.Point(399, 282);
            this.btnThem.Margin = new System.Windows.Forms.Padding(4);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(100, 39);
            this.btnThem.TabIndex = 4;
            this.btnThem.Text = "Thêm";
            this.btnThem.UseVisualStyleBackColor = false;
            this.btnThem.Click += new System.EventHandler(this.btnThem_Click);
            // 
            // btnSua
            // 
            this.btnSua.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.btnSua.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnSua.ForeColor = System.Drawing.Color.White;
            this.btnSua.Location = new System.Drawing.Point(507, 282);
            this.btnSua.Margin = new System.Windows.Forms.Padding(4);
            this.btnSua.Name = "btnSua";
            this.btnSua.Size = new System.Drawing.Size(100, 39);
            this.btnSua.TabIndex = 5;
            this.btnSua.Text = "Sửa";
            this.btnSua.UseVisualStyleBackColor = false;
            this.btnSua.Click += new System.EventHandler(this.btnSua_Click);
            // 
            // btnXoa
            // 
            this.btnXoa.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.btnXoa.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnXoa.ForeColor = System.Drawing.Color.White;
            this.btnXoa.Location = new System.Drawing.Point(615, 282);
            this.btnXoa.Margin = new System.Windows.Forms.Padding(4);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(100, 39);
            this.btnXoa.TabIndex = 6;
            this.btnXoa.Text = "Xóa";
            this.btnXoa.UseVisualStyleBackColor = false;
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
            // 
            // btnThoat
            // 
            this.btnThoat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.btnThoat.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnThoat.ForeColor = System.Drawing.Color.White;
            this.btnThoat.Location = new System.Drawing.Point(951, 282);
            this.btnThoat.Margin = new System.Windows.Forms.Padding(4);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new System.Drawing.Size(100, 39);
            this.btnThoat.TabIndex = 7;
            this.btnThoat.Text = "Thoát";
            this.btnThoat.UseVisualStyleBackColor = false;
            this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
            // 
            // lblHoTen
            // 
            this.lblHoTen.AutoSize = true;
            this.lblHoTen.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.lblHoTen.Location = new System.Drawing.Point(286, 332);
            this.lblHoTen.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblHoTen.Name = "lblHoTen";
            this.lblHoTen.Size = new System.Drawing.Size(66, 23);
            this.lblHoTen.TabIndex = 8;
            this.lblHoTen.Text = "Họ tên:";
            // 
            // txtHoTen
            // 
            this.txtHoTen.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.txtHoTen.Location = new System.Drawing.Point(290, 359);
            this.txtHoTen.Margin = new System.Windows.Forms.Padding(4);
            this.txtHoTen.Name = "txtHoTen";
            this.txtHoTen.Size = new System.Drawing.Size(210, 29);
            this.txtHoTen.TabIndex = 9;
            // 
            // lblNgaySinh
            // 
            this.lblNgaySinh.AutoSize = true;
            this.lblNgaySinh.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.lblNgaySinh.Location = new System.Drawing.Point(504, 332);
            this.lblNgaySinh.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblNgaySinh.Name = "lblNgaySinh";
            this.lblNgaySinh.Size = new System.Drawing.Size(90, 23);
            this.lblNgaySinh.TabIndex = 10;
            this.lblNgaySinh.Text = "Ngày sinh:";
            // 
            // dtpNgaySinh
            // 
            this.dtpNgaySinh.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.dtpNgaySinh.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpNgaySinh.Location = new System.Drawing.Point(508, 359);
            this.dtpNgaySinh.Margin = new System.Windows.Forms.Padding(4);
            this.dtpNgaySinh.Name = "dtpNgaySinh";
            this.dtpNgaySinh.Size = new System.Drawing.Size(265, 29);
            this.dtpNgaySinh.TabIndex = 11;
            // 
            // lblGioiTinh
            // 
            this.lblGioiTinh.AutoSize = true;
            this.lblGioiTinh.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.lblGioiTinh.Location = new System.Drawing.Point(778, 332);
            this.lblGioiTinh.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblGioiTinh.Name = "lblGioiTinh";
            this.lblGioiTinh.Size = new System.Drawing.Size(79, 23);
            this.lblGioiTinh.TabIndex = 12;
            this.lblGioiTinh.Text = "Giới tính:";
            // 
            // cboGioiTinh
            // 
            this.cboGioiTinh.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.cboGioiTinh.FormattingEnabled = true;
            this.cboGioiTinh.Items.AddRange(new object[] {
            "Nam",
            "Nữ"});
            this.cboGioiTinh.Location = new System.Drawing.Point(782, 359);
            this.cboGioiTinh.Margin = new System.Windows.Forms.Padding(4);
            this.cboGioiTinh.Name = "cboGioiTinh";
            this.cboGioiTinh.Size = new System.Drawing.Size(209, 29);
            this.cboGioiTinh.TabIndex = 13;
            // 
            // lblSoCMND
            // 
            this.lblSoCMND.AutoSize = true;
            this.lblSoCMND.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.lblSoCMND.Location = new System.Drawing.Point(291, 396);
            this.lblSoCMND.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSoCMND.Name = "lblSoCMND";
            this.lblSoCMND.Size = new System.Drawing.Size(89, 23);
            this.lblSoCMND.TabIndex = 14;
            this.lblSoCMND.Text = "Số CMND:";
            // 
            // txtSoCMND
            // 
            this.txtSoCMND.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.txtSoCMND.Location = new System.Drawing.Point(290, 423);
            this.txtSoCMND.Margin = new System.Windows.Forms.Padding(4);
            this.txtSoCMND.Name = "txtSoCMND";
            this.txtSoCMND.Size = new System.Drawing.Size(209, 29);
            this.txtSoCMND.TabIndex = 15;
            // 
            // lblTrinhDoChuyenMon
            // 
            this.lblTrinhDoChuyenMon.AutoSize = true;
            this.lblTrinhDoChuyenMon.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.lblTrinhDoChuyenMon.Location = new System.Drawing.Point(508, 396);
            this.lblTrinhDoChuyenMon.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTrinhDoChuyenMon.Name = "lblTrinhDoChuyenMon";
            this.lblTrinhDoChuyenMon.Size = new System.Drawing.Size(177, 23);
            this.lblTrinhDoChuyenMon.TabIndex = 16;
            this.lblTrinhDoChuyenMon.Text = "Trình độ chuyên môn:";
            // 
            // txtTrinhDoChuyenMon
            // 
            this.txtTrinhDoChuyenMon.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.txtTrinhDoChuyenMon.Location = new System.Drawing.Point(507, 423);
            this.txtTrinhDoChuyenMon.Margin = new System.Windows.Forms.Padding(4);
            this.txtTrinhDoChuyenMon.Name = "txtTrinhDoChuyenMon";
            this.txtTrinhDoChuyenMon.Size = new System.Drawing.Size(265, 29);
            this.txtTrinhDoChuyenMon.TabIndex = 17;
            // 
            // lblTrinhDoNgoaiNgu
            // 
            this.lblTrinhDoNgoaiNgu.AutoSize = true;
            this.lblTrinhDoNgoaiNgu.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.lblTrinhDoNgoaiNgu.Location = new System.Drawing.Point(783, 396);
            this.lblTrinhDoNgoaiNgu.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTrinhDoNgoaiNgu.Name = "lblTrinhDoNgoaiNgu";
            this.lblTrinhDoNgoaiNgu.Size = new System.Drawing.Size(160, 23);
            this.lblTrinhDoNgoaiNgu.TabIndex = 18;
            this.lblTrinhDoNgoaiNgu.Text = "Trình độ ngoại ngữ:";
            // 
            // txtTrinhDoNgoaiNgu
            // 
            this.txtTrinhDoNgoaiNgu.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.txtTrinhDoNgoaiNgu.Location = new System.Drawing.Point(782, 423);
            this.txtTrinhDoNgoaiNgu.Margin = new System.Windows.Forms.Padding(4);
            this.txtTrinhDoNgoaiNgu.Name = "txtTrinhDoNgoaiNgu";
            this.txtTrinhDoNgoaiNgu.Size = new System.Drawing.Size(208, 29);
            this.txtTrinhDoNgoaiNgu.TabIndex = 19;
            // 
            // lblHoKhauThuongTru
            // 
            this.lblHoKhauThuongTru.AutoSize = true;
            this.lblHoKhauThuongTru.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.lblHoKhauThuongTru.Location = new System.Drawing.Point(290, 470);
            this.lblHoKhauThuongTru.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblHoKhauThuongTru.Name = "lblHoKhauThuongTru";
            this.lblHoKhauThuongTru.Size = new System.Drawing.Size(166, 23);
            this.lblHoKhauThuongTru.TabIndex = 20;
            this.lblHoKhauThuongTru.Text = "Hộ khẩu thường trú:";
            // 
            // txtHoKhauThuongTru
            // 
            this.txtHoKhauThuongTru.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.txtHoKhauThuongTru.Location = new System.Drawing.Point(290, 497);
            this.txtHoKhauThuongTru.Margin = new System.Windows.Forms.Padding(4);
            this.txtHoKhauThuongTru.Name = "txtHoKhauThuongTru";
            this.txtHoKhauThuongTru.Size = new System.Drawing.Size(265, 29);
            this.txtHoKhauThuongTru.TabIndex = 21;
            // 
            // lblDiaChi
            // 
            this.lblDiaChi.AutoSize = true;
            this.lblDiaChi.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.lblDiaChi.Location = new System.Drawing.Point(565, 470);
            this.lblDiaChi.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDiaChi.Name = "lblDiaChi";
            this.lblDiaChi.Size = new System.Drawing.Size(66, 23);
            this.lblDiaChi.TabIndex = 22;
            this.lblDiaChi.Text = "Địa chỉ:";
            // 
            // txtDiaChi
            // 
            this.txtDiaChi.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.txtDiaChi.Location = new System.Drawing.Point(565, 497);
            this.txtDiaChi.Margin = new System.Windows.Forms.Padding(4);
            this.txtDiaChi.Name = "txtDiaChi";
            this.txtDiaChi.Size = new System.Drawing.Size(209, 29);
            this.txtDiaChi.TabIndex = 23;
            // 
            // lblDanToc
            // 
            this.lblDanToc.AutoSize = true;
            this.lblDanToc.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.lblDanToc.Location = new System.Drawing.Point(782, 470);
            this.lblDanToc.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDanToc.Name = "lblDanToc";
            this.lblDanToc.Size = new System.Drawing.Size(74, 23);
            this.lblDanToc.TabIndex = 24;
            this.lblDanToc.Text = "Dân tộc:";
            // 
            // txtDanToc
            // 
            this.txtDanToc.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.txtDanToc.Location = new System.Drawing.Point(782, 497);
            this.txtDanToc.Margin = new System.Windows.Forms.Padding(4);
            this.txtDanToc.Name = "txtDanToc";
            this.txtDanToc.Size = new System.Drawing.Size(265, 29);
            this.txtDanToc.TabIndex = 25;
            // 
            // lblTonGiao
            // 
            this.lblTonGiao.AutoSize = true;
            this.lblTonGiao.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.lblTonGiao.Location = new System.Drawing.Point(289, 538);
            this.lblTonGiao.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTonGiao.Name = "lblTonGiao";
            this.lblTonGiao.Size = new System.Drawing.Size(79, 23);
            this.lblTonGiao.TabIndex = 26;
            this.lblTonGiao.Text = "Tôn giáo:";
            // 
            // txtTonGiao
            // 
            this.txtTonGiao.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.txtTonGiao.Location = new System.Drawing.Point(289, 565);
            this.txtTonGiao.Margin = new System.Windows.Forms.Padding(4);
            this.txtTonGiao.Name = "txtTonGiao";
            this.txtTonGiao.Size = new System.Drawing.Size(265, 29);
            this.txtTonGiao.TabIndex = 27;
            // 
            // lblNgayVaoDoan
            // 
            this.lblNgayVaoDoan.AutoSize = true;
            this.lblNgayVaoDoan.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.lblNgayVaoDoan.Location = new System.Drawing.Point(563, 538);
            this.lblNgayVaoDoan.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblNgayVaoDoan.Name = "lblNgayVaoDoan";
            this.lblNgayVaoDoan.Size = new System.Drawing.Size(130, 23);
            this.lblNgayVaoDoan.TabIndex = 28;
            this.lblNgayVaoDoan.Text = "Ngày vào đoàn:";
            // 
            // dtpNgayVaoDoan
            // 
            this.dtpNgayVaoDoan.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.dtpNgayVaoDoan.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpNgayVaoDoan.Location = new System.Drawing.Point(563, 565);
            this.dtpNgayVaoDoan.Margin = new System.Windows.Forms.Padding(4);
            this.dtpNgayVaoDoan.Name = "dtpNgayVaoDoan";
            this.dtpNgayVaoDoan.Size = new System.Drawing.Size(265, 29);
            this.dtpNgayVaoDoan.TabIndex = 29;
            // 
            // lblNgayVaoDang
            // 
            this.lblNgayVaoDang.AutoSize = true;
            this.lblNgayVaoDang.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.lblNgayVaoDang.Location = new System.Drawing.Point(838, 538);
            this.lblNgayVaoDang.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblNgayVaoDang.Name = "lblNgayVaoDang";
            this.lblNgayVaoDang.Size = new System.Drawing.Size(130, 23);
            this.lblNgayVaoDang.TabIndex = 30;
            this.lblNgayVaoDang.Text = "Ngày vào đảng:";
            // 
            // dtpNgayVaoDang
            // 
            this.dtpNgayVaoDang.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.dtpNgayVaoDang.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpNgayVaoDang.Location = new System.Drawing.Point(838, 565);
            this.dtpNgayVaoDang.Margin = new System.Windows.Forms.Padding(4);
            this.dtpNgayVaoDang.Name = "dtpNgayVaoDang";
            this.dtpNgayVaoDang.Size = new System.Drawing.Size(209, 29);
            this.dtpNgayVaoDang.TabIndex = 31;
            // 
            // lblDienChinhSach
            // 
            this.lblDienChinhSach.AutoSize = true;
            this.lblDienChinhSach.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.lblDienChinhSach.Location = new System.Drawing.Point(289, 610);
            this.lblDienChinhSach.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDienChinhSach.Name = "lblDienChinhSach";
            this.lblDienChinhSach.Size = new System.Drawing.Size(135, 23);
            this.lblDienChinhSach.TabIndex = 32;
            this.lblDienChinhSach.Text = "Diện chính sách:";
            // 
            // txtDienChinhSach
            // 
            this.txtDienChinhSach.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.txtDienChinhSach.Location = new System.Drawing.Point(289, 637);
            this.txtDienChinhSach.Margin = new System.Windows.Forms.Padding(4);
            this.txtDienChinhSach.Name = "txtDienChinhSach";
            this.txtDienChinhSach.Size = new System.Drawing.Size(265, 29);
            this.txtDienChinhSach.TabIndex = 33;
            // 
            // lblPhongBan
            // 
            this.lblPhongBan.AutoSize = true;
            this.lblPhongBan.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.lblPhongBan.Location = new System.Drawing.Point(564, 610);
            this.lblPhongBan.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPhongBan.Name = "lblPhongBan";
            this.lblPhongBan.Size = new System.Drawing.Size(98, 23);
            this.lblPhongBan.TabIndex = 34;
            this.lblPhongBan.Text = "Phòng ban:";
            // 
            // cboPhongBan
            // 
            this.cboPhongBan.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.cboPhongBan.FormattingEnabled = true;
            this.cboPhongBan.Location = new System.Drawing.Point(564, 637);
            this.cboPhongBan.Margin = new System.Windows.Forms.Padding(4);
            this.cboPhongBan.Name = "cboPhongBan";
            this.cboPhongBan.Size = new System.Drawing.Size(265, 29);
            this.cboPhongBan.TabIndex = 35;
            // 
            // lblChucDanh
            // 
            this.lblChucDanh.AutoSize = true;
            this.lblChucDanh.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.lblChucDanh.Location = new System.Drawing.Point(838, 610);
            this.lblChucDanh.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblChucDanh.Name = "lblChucDanh";
            this.lblChucDanh.Size = new System.Drawing.Size(97, 23);
            this.lblChucDanh.TabIndex = 36;
            this.lblChucDanh.Text = "Chức danh:";
            // 
            // cboChucDanh
            // 
            this.cboChucDanh.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.cboChucDanh.FormattingEnabled = true;
            this.cboChucDanh.Location = new System.Drawing.Point(838, 637);
            this.cboChucDanh.Margin = new System.Windows.Forms.Padding(4);
            this.cboChucDanh.Name = "cboChucDanh";
            this.cboChucDanh.Size = new System.Drawing.Size(208, 29);
            this.cboChucDanh.TabIndex = 37;
            // 
            // lblLoaiNhanVien
            // 
            this.lblLoaiNhanVien.AutoSize = true;
            this.lblLoaiNhanVien.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.lblLoaiNhanVien.Location = new System.Drawing.Point(288, 681);
            this.lblLoaiNhanVien.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblLoaiNhanVien.Name = "lblLoaiNhanVien";
            this.lblLoaiNhanVien.Size = new System.Drawing.Size(125, 23);
            this.lblLoaiNhanVien.TabIndex = 38;
            this.lblLoaiNhanVien.Text = "Loại nhân viên:";
            // 
            // cboLoaiNhanVien
            // 
            this.cboLoaiNhanVien.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.cboLoaiNhanVien.FormattingEnabled = true;
            this.cboLoaiNhanVien.Items.AddRange(new object[] {
            "Biên chế",
            "Hợp đồng"});
            this.cboLoaiNhanVien.Location = new System.Drawing.Point(288, 708);
            this.cboLoaiNhanVien.Margin = new System.Windows.Forms.Padding(4);
            this.cboLoaiNhanVien.Name = "cboLoaiNhanVien";
            this.cboLoaiNhanVien.Size = new System.Drawing.Size(209, 29);
            this.cboLoaiNhanVien.TabIndex = 39;
            // 
            // lblNgayVaoCongTy
            // 
            this.lblNgayVaoCongTy.AutoSize = true;
            this.lblNgayVaoCongTy.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.lblNgayVaoCongTy.Location = new System.Drawing.Point(507, 681);
            this.lblNgayVaoCongTy.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblNgayVaoCongTy.Name = "lblNgayVaoCongTy";
            this.lblNgayVaoCongTy.Size = new System.Drawing.Size(148, 23);
            this.lblNgayVaoCongTy.TabIndex = 40;
            this.lblNgayVaoCongTy.Text = "Ngày vào công ty:";
            // 
            // dtpNgayVaoCongTy
            // 
            this.dtpNgayVaoCongTy.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.dtpNgayVaoCongTy.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpNgayVaoCongTy.Location = new System.Drawing.Point(507, 708);
            this.dtpNgayVaoCongTy.Margin = new System.Windows.Forms.Padding(4);
            this.dtpNgayVaoCongTy.Name = "dtpNgayVaoCongTy";
            this.dtpNgayVaoCongTy.Size = new System.Drawing.Size(265, 29);
            this.dtpNgayVaoCongTy.TabIndex = 41;
            // 
            // lblTrangThai
            // 
            this.lblTrangThai.AutoSize = true;
            this.lblTrangThai.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.lblTrangThai.Location = new System.Drawing.Point(782, 681);
            this.lblTrangThai.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTrangThai.Name = "lblTrangThai";
            this.lblTrangThai.Size = new System.Drawing.Size(91, 23);
            this.lblTrangThai.TabIndex = 42;
            this.lblTrangThai.Text = "Trạng thái:";
            // 
            // txtTrangThai
            // 
            this.txtTrangThai.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.txtTrangThai.Location = new System.Drawing.Point(782, 708);
            this.txtTrangThai.Margin = new System.Windows.Forms.Padding(4);
            this.txtTrangThai.Name = "txtTrangThai";
            this.txtTrangThai.Size = new System.Drawing.Size(265, 29);
            this.txtTrangThai.TabIndex = 43;
            // 
            // lblHinhAnh
            // 
            this.lblHinhAnh.AutoSize = true;
            this.lblHinhAnh.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.lblHinhAnh.Location = new System.Drawing.Point(25, 332);
            this.lblHinhAnh.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblHinhAnh.Name = "lblHinhAnh";
            this.lblHinhAnh.Size = new System.Drawing.Size(84, 23);
            this.lblHinhAnh.TabIndex = 44;
            this.lblHinhAnh.Text = "Hình ảnh:";
            // 
            // picHinhAnh
            // 
            this.picHinhAnh.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picHinhAnh.Location = new System.Drawing.Point(29, 402);
            this.picHinhAnh.Margin = new System.Windows.Forms.Padding(4);
            this.picHinhAnh.Name = "picHinhAnh";
            this.picHinhAnh.Size = new System.Drawing.Size(211, 264);
            this.picHinhAnh.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picHinhAnh.TabIndex = 45;
            this.picHinhAnh.TabStop = false;
            // 
            // btnChonHinh
            // 
            this.btnChonHinh.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.btnChonHinh.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnChonHinh.ForeColor = System.Drawing.Color.White;
            this.btnChonHinh.Location = new System.Drawing.Point(28, 355);
            this.btnChonHinh.Margin = new System.Windows.Forms.Padding(4);
            this.btnChonHinh.Name = "btnChonHinh";
            this.btnChonHinh.Size = new System.Drawing.Size(211, 39);
            this.btnChonHinh.TabIndex = 46;
            this.btnChonHinh.Text = "Chọn hình";
            this.btnChonHinh.UseVisualStyleBackColor = false;
            this.btnChonHinh.Click += new System.EventHandler(this.btnChonHinh_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
            // 
            // frmHoSoCaNhan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1067, 800);
            this.Controls.Add(this.btnChonHinh);
            this.Controls.Add(this.picHinhAnh);
            this.Controls.Add(this.lblHinhAnh);
            this.Controls.Add(this.txtTrangThai);
            this.Controls.Add(this.lblTrangThai);
            this.Controls.Add(this.dtpNgayVaoCongTy);
            this.Controls.Add(this.lblNgayVaoCongTy);
            this.Controls.Add(this.cboLoaiNhanVien);
            this.Controls.Add(this.lblLoaiNhanVien);
            this.Controls.Add(this.cboChucDanh);
            this.Controls.Add(this.lblChucDanh);
            this.Controls.Add(this.cboPhongBan);
            this.Controls.Add(this.lblPhongBan);
            this.Controls.Add(this.txtDienChinhSach);
            this.Controls.Add(this.lblDienChinhSach);
            this.Controls.Add(this.dtpNgayVaoDang);
            this.Controls.Add(this.lblNgayVaoDang);
            this.Controls.Add(this.dtpNgayVaoDoan);
            this.Controls.Add(this.lblNgayVaoDoan);
            this.Controls.Add(this.txtTonGiao);
            this.Controls.Add(this.lblTonGiao);
            this.Controls.Add(this.txtDanToc);
            this.Controls.Add(this.lblDanToc);
            this.Controls.Add(this.txtDiaChi);
            this.Controls.Add(this.lblDiaChi);
            this.Controls.Add(this.txtHoKhauThuongTru);
            this.Controls.Add(this.lblHoKhauThuongTru);
            this.Controls.Add(this.txtTrinhDoNgoaiNgu);
            this.Controls.Add(this.lblTrinhDoNgoaiNgu);
            this.Controls.Add(this.txtTrinhDoChuyenMon);
            this.Controls.Add(this.lblTrinhDoChuyenMon);
            this.Controls.Add(this.txtSoCMND);
            this.Controls.Add(this.lblSoCMND);
            this.Controls.Add(this.cboGioiTinh);
            this.Controls.Add(this.lblGioiTinh);
            this.Controls.Add(this.dtpNgaySinh);
            this.Controls.Add(this.lblNgaySinh);
            this.Controls.Add(this.txtHoTen);
            this.Controls.Add(this.lblHoTen);
            this.Controls.Add(this.btnThoat);
            this.Controls.Add(this.btnXoa);
            this.Controls.Add(this.btnSua);
            this.Controls.Add(this.btnThem);
            this.Controls.Add(this.btnTimKiem);
            this.Controls.Add(this.txtTimKiem);
            this.Controls.Add(this.lblTongSo);
            this.Controls.Add(this.dgvNhanVien);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmHoSoCaNhan";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quản lý hồ sơ cá nhân";
            this.Load += new System.EventHandler(this.frmHoSoCaNhan_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvNhanVien)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picHinhAnh)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvNhanVien;
        private System.Windows.Forms.Label lblTongSo;
        private System.Windows.Forms.TextBox txtTimKiem;
        private System.Windows.Forms.Button btnTimKiem;
        private System.Windows.Forms.Button btnThem;
        private System.Windows.Forms.Button btnSua;
        private System.Windows.Forms.Button btnXoa;
        private System.Windows.Forms.Button btnThoat;
        private System.Windows.Forms.Label lblHoTen;
        private System.Windows.Forms.TextBox txtHoTen;
        private System.Windows.Forms.Label lblNgaySinh;
        private System.Windows.Forms.DateTimePicker dtpNgaySinh;
        private System.Windows.Forms.Label lblGioiTinh;
        private System.Windows.Forms.ComboBox cboGioiTinh;
        private System.Windows.Forms.Label lblSoCMND;
        private System.Windows.Forms.TextBox txtSoCMND;
        private System.Windows.Forms.Label lblTrinhDoChuyenMon;
        private System.Windows.Forms.TextBox txtTrinhDoChuyenMon;
        private System.Windows.Forms.Label lblTrinhDoNgoaiNgu;
        private System.Windows.Forms.TextBox txtTrinhDoNgoaiNgu;
        private System.Windows.Forms.Label lblHoKhauThuongTru;
        private System.Windows.Forms.TextBox txtHoKhauThuongTru;
        private System.Windows.Forms.Label lblDiaChi;
        private System.Windows.Forms.TextBox txtDiaChi;
        private System.Windows.Forms.Label lblDanToc;
        private System.Windows.Forms.TextBox txtDanToc;
        private System.Windows.Forms.Label lblTonGiao;
        private System.Windows.Forms.TextBox txtTonGiao;
        private System.Windows.Forms.Label lblNgayVaoDoan;
        private System.Windows.Forms.DateTimePicker dtpNgayVaoDoan;
        private System.Windows.Forms.Label lblNgayVaoDang;
        private System.Windows.Forms.DateTimePicker dtpNgayVaoDang;
        private System.Windows.Forms.Label lblDienChinhSach;
        private System.Windows.Forms.TextBox txtDienChinhSach;
        private System.Windows.Forms.Label lblPhongBan;
        private System.Windows.Forms.ComboBox cboPhongBan;
        private System.Windows.Forms.Label lblChucDanh;
        private System.Windows.Forms.ComboBox cboChucDanh;
        private System.Windows.Forms.Label lblLoaiNhanVien;
        private System.Windows.Forms.ComboBox cboLoaiNhanVien;
        private System.Windows.Forms.Label lblNgayVaoCongTy;
        private System.Windows.Forms.DateTimePicker dtpNgayVaoCongTy;
        private System.Windows.Forms.Label lblTrangThai;
        private System.Windows.Forms.TextBox txtTrangThai;
        private System.Windows.Forms.Label lblHinhAnh;
        private System.Windows.Forms.PictureBox picHinhAnh;
        private System.Windows.Forms.Button btnChonHinh;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
    }
}