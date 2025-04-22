using System;
using System.Data;
using System.Windows.Forms;
using HRM.BLL;
using HRM.DTO;

namespace HRM.GUI
{
    public partial class formChamCongNgay : Form
    {
        private DataTable _dtNhanVien;
        
        public formChamCongNgay()
        {
            InitializeComponent();
        }

        private void formChamCongNgay_Load(object sender, EventArgs e)
        {
            try
            {
                // Đặt ngày chấm công mặc định là ngày hiện tại
                dtpNgayChamCong.Value = DateTime.Now;
                
                // Khởi tạo DataGridView
                InitDataGridView();
                
                // Tải danh sách chấm công
                LoadDanhSachChamCong();
                
                // Vô hiệu hóa một số control
                DisableControls();
                
                // Căn giữa form
                this.CenterToScreen();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void InitDataGridView()
        {
            // Định dạng cho DataGridView
            dgvChamCong.AutoGenerateColumns = false;
            
            // Tạo các cột cho dgvChamCong
            dgvChamCong.Columns.Clear();
            
            // Cột STT
            DataGridViewTextBoxColumn colSTT = new DataGridViewTextBoxColumn();
            colSTT.HeaderText = "STT";
            colSTT.Name = "colSTT";
            colSTT.Width = 50;
            dgvChamCong.Columns.Add(colSTT);
            
            // Cột Mã nhân viên
            DataGridViewTextBoxColumn colMaNV = new DataGridViewTextBoxColumn();
            colMaNV.DataPropertyName = "MaNV";
            colMaNV.HeaderText = "Mã NV";
            colMaNV.Name = "colMaNV";
            colMaNV.Width = 80;
            dgvChamCong.Columns.Add(colMaNV);
            
            // Cột Họ và tên
            DataGridViewTextBoxColumn colHoTen = new DataGridViewTextBoxColumn();
            colHoTen.DataPropertyName = "HoTen";
            colHoTen.HeaderText = "Họ và tên";
            colHoTen.Name = "colHoTen";
            colHoTen.Width = 180;
            dgvChamCong.Columns.Add(colHoTen);
            
            // Cột Phòng ban
            DataGridViewTextBoxColumn colPhongBan = new DataGridViewTextBoxColumn();
            colPhongBan.DataPropertyName = "TenPhong";
            colPhongBan.HeaderText = "Phòng ban";
            colPhongBan.Name = "colPhongBan";
            colPhongBan.Width = 150;
            dgvChamCong.Columns.Add(colPhongBan);
            
            // Cột Chức danh
            DataGridViewTextBoxColumn colChucDanh = new DataGridViewTextBoxColumn();
            colChucDanh.DataPropertyName = "TenChucDanh";
            colChucDanh.HeaderText = "Chức danh";
            colChucDanh.Name = "colChucDanh";
            colChucDanh.Width = 150;
            dgvChamCong.Columns.Add(colChucDanh);
            
            // Cột Trạng thái
            DataGridViewTextBoxColumn colTrangThai = new DataGridViewTextBoxColumn();
            colTrangThai.DataPropertyName = "TrangThaiChamCong";
            colTrangThai.HeaderText = "Trạng thái";
            colTrangThai.Name = "colTrangThai";
            colTrangThai.Width = 100;
            dgvChamCong.Columns.Add(colTrangThai);
            
            // Cột ID chấm công (ẩn)
            DataGridViewTextBoxColumn colMaChamCong = new DataGridViewTextBoxColumn();
            colMaChamCong.DataPropertyName = "MaChamCong";
            colMaChamCong.Name = "colMaChamCong";
            colMaChamCong.Visible = false;
            dgvChamCong.Columns.Add(colMaChamCong);
            
            // Định dạng DataGridView
            dgvChamCong.RowHeadersVisible = false;
            dgvChamCong.AllowUserToAddRows = false;
            dgvChamCong.AllowUserToDeleteRows = false;
            dgvChamCong.AllowUserToResizeRows = false;
            dgvChamCong.MultiSelect = false;
            dgvChamCong.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvChamCong.ReadOnly = true;
            
            // Sự kiện CellFormatting để định dạng STT
            dgvChamCong.CellFormatting += (s, e) => {
                if (e.ColumnIndex == colSTT.Index && e.RowIndex >= 0)
                {
                    e.Value = e.RowIndex + 1;
                }
            };
            
            // Định dạng màu sắc
            dgvChamCong.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);
            dgvChamCong.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(0, 122, 204);
            dgvChamCong.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.White;
            dgvChamCong.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            dgvChamCong.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(0, 122, 204);
            dgvChamCong.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
            dgvChamCong.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvChamCong.ColumnHeadersHeight = 30;
            dgvChamCong.EnableHeadersVisualStyles = false;
        }

        private void LoadDanhSachChamCong()
        {
            try
            {
                DateTime ngayChamCong = dtpNgayChamCong.Value.Date;
                
                // Lấy danh sách chấm công theo ngày
                
                // Xóa dữ liệu cũ
                dgvChamCong.DataSource = null;
                // Reset các control
                ResetControls();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách chấm công: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void ResetControls()
        {
            // Xóa thông tin chi tiết
            cboTrangThai.SelectedIndex = -1;
            txtGhiChu.Clear();
            
            // Vô hiệu hóa các nút Sửa, Xóa
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            
            // Ẩn panel chi tiết
            pnlChiTietChamCong.Visible = false;
        }
        
        private void DisableControls()
        {
            // Vô hiệu hóa các control
            cboTrangThai.Enabled = false;
            txtGhiChu.Enabled = false;
            btnOK.Enabled = false;
            btnHuy.Enabled = false;
            
            // Ẩn panel chi tiết
            pnlChiTietChamCong.Visible = false;
        }
        
        private void EnableControls()
        {
            // Kích hoạt các control
            cboTrangThai.Enabled = true;
            txtGhiChu.Enabled = true;
            btnOK.Enabled = true;
            btnHuy.Enabled = true;
            
            // Hiển thị panel chi tiết
            pnlChiTietChamCong.Visible = true;
        }
        
        private void dtpNgayChamCong_ValueChanged(object sender, EventArgs e)
        {
            // Tải lại danh sách chấm công khi thay đổi ngày
            LoadDanhSachChamCong();
        }
        
        private void dgvChamCong_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvChamCong.SelectedRows.Count > 0)
            {
                // Kích hoạt các nút Sửa, Xóa
                btnSua.Enabled = true;
                btnXoa.Enabled = true;
            }
            else
            {
                // Vô hiệu hóa các nút Sửa, Xóa
                btnSua.Enabled = false;
                btnXoa.Enabled = false;
            }
        }
        
        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra xem đã có dữ liệu chấm công chưa
                if (_dtNhanVien.Rows.Count > 0)
                {
                    MessageBox.Show("Đã có dữ liệu chấm công cho ngày này. Vui lòng sử dụng chức năng Sửa để cập nhật.", 
                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                
                // Nếu chưa có dữ liệu, hiển thị form xác nhận chấm công cho tất cả nhân viên
                DialogResult result = MessageBox.Show("Bạn có muốn chấm công cho tất cả nhân viên không?", 
                    "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    
                if (result == DialogResult.Yes)
                {
                    // Tạo bản ghi chấm công với trạng thái mặc định cho tất cả nhân viên
                    DateTime ngayChamCong = dtpNgayChamCong.Value.Date;
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm dữ liệu chấm công: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dgvChamCong.SelectedRows.Count > 0)
            {
                // Lấy dữ liệu hàng được chọn
                DataGridViewRow selectedRow = dgvChamCong.SelectedRows[0];
                
                // Hiển thị thông tin chi tiết
                string maNV = selectedRow.Cells["colMaNV"].Value.ToString();
                string hoTen = selectedRow.Cells["colHoTen"].Value.ToString();
                
                // Hiển thị thông tin nhân viên được chọn
                lblNhanVien.Text = maNV + " - " + hoTen;
                
                // Hiển thị trạng thái chấm công
                string trangThai = selectedRow.Cells["colTrangThai"].Value.ToString();
                
                if (trangThai == "Đi làm")
                    cboTrangThai.SelectedIndex = 0;
                else if (trangThai == "Nghỉ phép")
                    cboTrangThai.SelectedIndex = 1;
                else if (trangThai == "Nghỉ không phép")
                    cboTrangThai.SelectedIndex = 2;
                else if (trangThai == "Đi trễ")
                    cboTrangThai.SelectedIndex = 3;
                else if (trangThai == "Về sớm")
                    cboTrangThai.SelectedIndex = 4;
                else
                    cboTrangThai.SelectedIndex = -1;
                
                // Hiển thị ghi chú nếu có
                if (selectedRow.Cells["colGhiChu"] != null && selectedRow.Cells["colGhiChu"].Value != null)
                    txtGhiChu.Text = selectedRow.Cells["colGhiChu"].Value.ToString();
                else
                    txtGhiChu.Clear();
                
                // Kích hoạt các control
                EnableControls();
            }
        }
        
        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvChamCong.SelectedRows.Count > 0)
                {
                    // Xác nhận xóa
                    DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa bản ghi chấm công này không?", 
                        "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        
                    if (result == DialogResult.Yes)
                    {
                        // Lấy ID chấm công
                        DataGridViewRow selectedRow = dgvChamCong.SelectedRows[0];
                        int maChamCong = Convert.ToInt32(selectedRow.Cells["colMaChamCong"].Value);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa bản ghi chấm công: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void btnHuy_Click(object sender, EventArgs e)
        {
            // Ẩn panel chi tiết và reset các control
            ResetControls();
        }
        
        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvChamCong.SelectedRows.Count > 0)
                {
                    // Lấy dữ liệu hàng được chọn
                    DataGridViewRow selectedRow = dgvChamCong.SelectedRows[0];
                    
                    // Lấy ID chấm công
                    int maChamCong = Convert.ToInt32(selectedRow.Cells["colMaChamCong"].Value);
                    
                    // Lấy trạng thái chấm công mới
                    string trangThai = cboTrangThai.SelectedItem.ToString();
                    
                    // Lấy ghi chú
                    string ghiChu = txtGhiChu.Text.Trim();
                    
                    // Cập nhật bản ghi

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật bản ghi chấm công: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void btnChamCongTatCa_Click(object sender, EventArgs e)
        {
            try
            {
                // Xác nhận chấm công cho tất cả
                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn đánh dấu tất cả nhân viên đi làm không?", 
                    "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    
                if (result == DialogResult.Yes)
                {
                    // Đánh dấu tất cả nhân viên đi làm
                    DateTime ngayChamCong = dtpNgayChamCong.Value.Date;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi chấm công tất cả: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            // Tải lại danh sách chấm công
            LoadDanhSachChamCong();
        }
        
        private void btnDong_Click(object sender, EventArgs e)
        {
            // Đóng form
            this.Close();
        }
    }
}