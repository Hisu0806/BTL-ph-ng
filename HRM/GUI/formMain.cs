using System;
using System.Data;
using System.Windows.Forms;
using HRM.BLL;
using HRM.DTO;
using HRM.Utility;

namespace HRM.GUI
{
    public partial class formMain : Form
    {
        public formMain()
        {
            InitializeComponent();
        }

        private void formMain_Load(object sender, EventArgs e)
        {
            // Kiểm tra đăng nhập và hiển thị thông tin người dùng
            if (GlobalVars.CurrentUser != null)
            {
                lblTenNguoiDung.Text = GlobalVars.CurrentUser.HoTen;
                lblQuyen.Text = GlobalVars.CurrentUser.VaiTro;
                
                // Hiển thị quyền truy cập của người dùng
                if (GlobalVars.CurrentUser.VaiTro != "Admin")
                {
                    // Hạn chế quyền truy cập một số chức năng dựa trên quyền
                    mnuNguoiDung.Enabled = false;
                    mnuSaoLuu.Enabled = false;
                    mnuPhucHoi.Enabled = false;
                }
            }
            else
            {
                // Nếu chưa đăng nhập thì hiển thị form đăng nhập
                this.Close();
                formDangNhap frm = new formDangNhap();
                frm.Show();
            }
            
            // Khởi tạo dữ liệu cho combobox
            LoadComboBoxData();
            
            // Hiển thị danh sách nhân viên mặc định
            LoadDanhSachNhanVien();
        }

        private void LoadComboBoxData()
        {
            try
            {
                // Load dữ liệu phòng ban
                DataTable dtPhong = PhongBanBLL.GetList();
                cboPhong.DataSource = dtPhong;
                cboPhong.DisplayMember = "TenPhong";
                cboPhong.ValueMember = "MaPhong";
                cboPhong.SelectedIndex = -1;
                
                // Load dữ liệu chức danh
                DataTable dtChucDanh = ChucDanhBLL.GetList();
                cboChucDanh.DataSource = dtChucDanh;
                cboChucDanh.DisplayMember = "TenChucDanh";
                cboChucDanh.ValueMember = "MaChucDanh";
                cboChucDanh.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu combo box: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadDanhSachNhanVien()
        {
            try
            {
                // Tải dữ liệu nhân viên lên DataGridView
                int totalRecords;
                var danhSachNhanVien = NhanVienBLL.GetPagedAndFiltered(1, 10, out totalRecords);
                
                dgvNhanVien.DataSource = danhSachNhanVien;
                
                // Định dạng hiển thị của DataGridView
                FormatDataGridView();
                
                // Cập nhật thông tin tổng số nhân viên
                lblTongSo.Text = "Tổng số: " + totalRecords.ToString() + " nhân viên";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách nhân viên: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void FormatDataGridView()
        {
            try
            {
                // Định dạng hiển thị của DataGridView
                if (dgvNhanVien.Columns.Contains("MaNV"))
                    dgvNhanVien.Columns["MaNV"].HeaderText = "Mã NV";
                    
                if (dgvNhanVien.Columns.Contains("HoTen"))
                    dgvNhanVien.Columns["HoTen"].HeaderText = "Họ tên";
                    
                if (dgvNhanVien.Columns.Contains("NgaySinh"))
                {
                    dgvNhanVien.Columns["NgaySinh"].HeaderText = "Ngày sinh";
                    dgvNhanVien.Columns["NgaySinh"].DefaultCellStyle.Format = "dd/MM/yyyy";
                }
                
                if (dgvNhanVien.Columns.Contains("GioiTinh"))
                    dgvNhanVien.Columns["GioiTinh"].HeaderText = "Giới tính";
                    
                if (dgvNhanVien.Columns.Contains("SoCMND"))
                    dgvNhanVien.Columns["SoCMND"].HeaderText = "Số CMND";
                    
                if (dgvNhanVien.Columns.Contains("DiaChi"))
                    dgvNhanVien.Columns["DiaChi"].HeaderText = "Địa chỉ";
                    
                if (dgvNhanVien.Columns.Contains("TenPhong"))
                    dgvNhanVien.Columns["TenPhong"].HeaderText = "Phòng ban";
                    
                if (dgvNhanVien.Columns.Contains("TenChucDanh"))
                    dgvNhanVien.Columns["TenChucDanh"].HeaderText = "Chức danh";
                
                // Ẩn một số cột không cần hiển thị
                string[] hiddenColumns = new string[] { 
                    "TrinhDoChuyenMon", "TrinhDoNgoaiNgu", "HoKhauThuongTru", 
                    "DanToc", "TonGiao", "NgayVaoDoan", "NgayVaoDang", 
                    "DienChinhSach", "MaPhong", "MaChucDanh", "LoaiNhanVien", 
                    "NgayVaoCongTy", "NgayNghiViec", "TrangThai", "HinhAnh" 
                };
                
                foreach (string colName in hiddenColumns)
                {
                    if (dgvNhanVien.Columns.Contains(colName))
                        dgvNhanVien.Columns[colName].Visible = false;
                }
                
                // Định dạng kích thước cột
                if (dgvNhanVien.Columns.Contains("MaNV"))
                    dgvNhanVien.Columns["MaNV"].Width = 80;
                    
                if (dgvNhanVien.Columns.Contains("HoTen"))
                    dgvNhanVien.Columns["HoTen"].Width = 150;
                    
                if (dgvNhanVien.Columns.Contains("NgaySinh"))
                    dgvNhanVien.Columns["NgaySinh"].Width = 100;
                    
                if (dgvNhanVien.Columns.Contains("GioiTinh"))
                    dgvNhanVien.Columns["GioiTinh"].Width = 80;
                    
                if (dgvNhanVien.Columns.Contains("SoCMND"))
                    dgvNhanVien.Columns["SoCMND"].Width = 100;
                    
                if (dgvNhanVien.Columns.Contains("DiaChi"))
                    dgvNhanVien.Columns["DiaChi"].Width = 200;
                    
                if (dgvNhanVien.Columns.Contains("TenPhong"))
                    dgvNhanVien.Columns["TenPhong"].Width = 150;
                    
                if (dgvNhanVien.Columns.Contains("TenChucDanh"))
                    dgvNhanVien.Columns["TenChucDanh"].Width = 150;
                
                // Đặt kiểu cho DataGridView
                dgvNhanVien.RowHeadersVisible = false;
                dgvNhanVien.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);
                dgvNhanVien.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(0, 122, 204);
                dgvNhanVien.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.White;
                dgvNhanVien.AllowUserToAddRows = false;
                dgvNhanVien.AllowUserToResizeRows = false;
                dgvNhanVien.BorderStyle = System.Windows.Forms.BorderStyle.None;
                dgvNhanVien.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
                dgvNhanVien.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(0, 122, 204);
                dgvNhanVien.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
                dgvNhanVien.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
                dgvNhanVien.ColumnHeadersHeight = 30;
                dgvNhanVien.EnableHeadersVisualStyles = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi định dạng DataGridView: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            // Lấy thông tin tìm kiếm
            string hoTen = txtTimKiem.Text.Trim();
            string phong = cboPhong.SelectedValue != null ? cboPhong.SelectedValue.ToString() : null;
            string chucDanh = cboChucDanh.SelectedValue != null ? cboChucDanh.SelectedValue.ToString() : null;
            
            try
            {
                // Tải dữ liệu nhân viên với các điều kiện lọc
                int totalRecords;
                var danhSachNhanVien = NhanVienBLL.GetPagedAndFiltered(1, 10, out totalRecords, hoTen, phong, chucDanh);
                
                dgvNhanVien.DataSource = danhSachNhanVien;
                
                // Cập nhật thông tin tổng số nhân viên
                lblTongSo.Text = "Tổng số: " + totalRecords.ToString() + " nhân viên";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tìm kiếm nhân viên: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void mnuDangXuat_Click(object sender, EventArgs e)
        {
            // Đăng xuất khỏi hệ thống
            GlobalVars.CurrentUser = null;
            this.Close();
            
            // Hiển thị form đăng nhập
            formDangNhap frm = new formDangNhap();
            frm.Show();
        }

        private void mnuThoat_Click(object sender, EventArgs e)
        {
            // Thoát ứng dụng
            Application.Exit();
        }

        // Xử lý sự kiện cho các menu item mới
        
        private void mnuHoSoNhanVien_Click(object sender, EventArgs e)
        {
            try
            {
                // Hiển thị form quản lý hồ sơ nhân viên
                formNhanVien frm = new formNhanVien();
                frm.ShowDialog();
                // Sau khi đóng form, cập nhật lại danh sách nhân viên
                LoadDanhSachNhanVien();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void mnuHopDong_Click(object sender, EventArgs e)
        {
            try
            {
                // Hiển thị form quản lý hợp đồng
                formHopDong frm = new formHopDong();
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void mnuThanNhan_Click(object sender, EventArgs e)
        {
            try
            {
                // Hiển thị form quản lý thân nhân
                formThanNhan frm = new formThanNhan();
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void mnuQuaTrinhCongTac_Click(object sender, EventArgs e)
        {
            try
            {
                // Hiển thị form quản lý quá trình công tác
                formQuaTrinhCongTac frm = new formQuaTrinhCongTac();
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void mnuKhenThuong_Click(object sender, EventArgs e)
        {
            try
            {
                // Hiển thị form quản lý khen thưởng
                formKhenThuong frm = new formKhenThuong();
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void mnuKyLuat_Click(object sender, EventArgs e)
        {
            try
            {
                // Hiển thị form quản lý kỷ luật
                formKyLuat frm = new formKyLuat();
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void mnuChamCongNgay_Click(object sender, EventArgs e)
        {
            try
            {
                // Hiển thị form chấm công ngày
                formChamCongNgay frm = new formChamCongNgay();
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void mnuBangChamCong_Click(object sender, EventArgs e)
        {
            try
            {
                // Hiển thị form bảng chấm công theo tháng
                formBangChamCong frm = new formBangChamCong();
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void mnuBangLuong_Click(object sender, EventArgs e)
        {
            try
            {
                // Hiển thị form tính lương
                formBangLuong frm = new formBangLuong();
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void mnuTangLuong_Click(object sender, EventArgs e)
        {
            try
            {
                // Hiển thị form tăng lương
                formTangLuong frm = new formTangLuong();
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void mnuBaoCaoNhanSu_Click(object sender, EventArgs e)
        {
            try
            {
                // Hiển thị form báo cáo nhân sự
                formBaoCaoNhanSu frm = new formBaoCaoNhanSu();
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void mnuBaoCaoLuong_Click(object sender, EventArgs e)
        {
            try
            {
                // Hiển thị form báo cáo lương
                formBaoCaoLuong frm = new formBaoCaoLuong();
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void mnuBaoCaoSinhNhat_Click(object sender, EventArgs e)
        {
            try
            {
                // Hiển thị form báo cáo danh sách sinh nhật
                formBaoCaoSinhNhat frm = new formBaoCaoSinhNhat();
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void mnuBaoCaoTangLuong_Click(object sender, EventArgs e)
        {
            try
            {
                // Hiển thị form báo cáo danh sách tăng lương
                formBaoCaoTangLuong frm = new formBaoCaoTangLuong();
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void mnuBaoCaoNghiHuu_Click(object sender, EventArgs e)
        {
            try
            {
                // Hiển thị form báo cáo danh sách nghỉ hưu
                formBaoCaoNghiHuu frm = new formBaoCaoNghiHuu();
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}