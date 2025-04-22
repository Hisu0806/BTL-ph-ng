using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using HRM.BLL;
using HRM.DTO;

namespace HRM.GUI
{
    public partial class formBangLuong : Form
    {
        private int _thang = DateTime.Now.Month;
        private int _nam = DateTime.Now.Year;
        private DataTable _dtBangLuong;
        
        public formBangLuong()
        {
            InitializeComponent();
        }

        private void formBangLuong_Load(object sender, EventArgs e)
        {
            try
            {
                // Khởi tạo combobox tháng và năm
                InitComboboxes();
                
                // Tải dữ liệu bảng lương
                LoadBangLuong();
                
                // Định dạng DataGridView
                FormatDataGridView();
                
                // Căn giữa form
                this.CenterToScreen();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void InitComboboxes()
        {
            // Combobox tháng
            cboThang.Items.Clear();
            for (int i = 1; i <= 12; i++)
            {
                cboThang.Items.Add(i);
            }
            cboThang.SelectedItem = _thang;
            
            // Combobox năm
            cboNam.Items.Clear();
            int currentYear = DateTime.Now.Year;
            for (int i = currentYear - 5; i <= currentYear + 1; i++)
            {
                cboNam.Items.Add(i);
            }
            cboNam.SelectedItem = _nam;
            
            // Combobox Phòng ban
            DataTable dtPhong = PhongBanBLL.GetList();
            cboPhong.DataSource = dtPhong;
            cboPhong.DisplayMember = "TenPhong";
            cboPhong.ValueMember = "MaPhong";
            cboPhong.SelectedIndex = -1;
        }
        
        private void LoadBangLuong()
        {
            try
            {
                // Lấy thông tin tìm kiếm
                string maNV = txtMaNV.Text.Trim();
                string tenNV = txtTenNV.Text.Trim();
                string maPhong = cboPhong.SelectedValue != null ? cboPhong.SelectedValue.ToString() : null;
                
                // Gọi hàm lấy dữ liệu bảng lương tháng
                
                // Hiển thị dữ liệu trên DataGridView
                dgvBangLuong.DataSource = _dtBangLuong;
                
                // Hiển thị tổng số và các thống kê
                UpdateThongKe();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải bảng lương: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void UpdateThongKe()
        {
            try
            {
                if (_dtBangLuong == null || _dtBangLuong.Rows.Count == 0)
                {
                    lblTongSo.Text = "Tổng số: 0 nhân viên";
                    lblTongLuong.Text = "Tổng lương: 0 VNĐ";
                    return;
                }
                
                // Tính tổng lương
                decimal tongLuong = 0;
                foreach (DataRow row in _dtBangLuong.Rows)
                {
                    if (row["ThucLanh"] != DBNull.Value)
                    {
                        tongLuong += Convert.ToDecimal(row["ThucLanh"]);
                    }
                }
                
                // Cập nhật labels
                lblTongSo.Text = "Tổng số: " + _dtBangLuong.Rows.Count + " nhân viên";
                lblTongLuong.Text = "Tổng lương: " + string.Format("{0:N0}", tongLuong) + " VNĐ";
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi UpdateThongKe: " + ex.Message);
            }
        }
        
        private void FormatDataGridView()
        {
            try
            {
                if (dgvBangLuong.Columns.Count == 0) return;
                
                // Định dạng các cột
                if (dgvBangLuong.Columns.Contains("STT"))
                {
                    dgvBangLuong.Columns["STT"].HeaderText = "STT";
                    dgvBangLuong.Columns["STT"].Width = 50;
                }
                
                if (dgvBangLuong.Columns.Contains("MaNV"))
                {
                    dgvBangLuong.Columns["MaNV"].HeaderText = "Mã NV";
                    dgvBangLuong.Columns["MaNV"].Width = 80;
                }
                
                if (dgvBangLuong.Columns.Contains("HoTen"))
                {
                    dgvBangLuong.Columns["HoTen"].HeaderText = "Họ và tên";
                    dgvBangLuong.Columns["HoTen"].Width = 180;
                }
                
                if (dgvBangLuong.Columns.Contains("TenPhong"))
                {
                    dgvBangLuong.Columns["TenPhong"].HeaderText = "Phòng ban";
                    dgvBangLuong.Columns["TenPhong"].Width = 150;
                }
                
                if (dgvBangLuong.Columns.Contains("TenChucDanh"))
                {
                    dgvBangLuong.Columns["TenChucDanh"].HeaderText = "Chức danh";
                    dgvBangLuong.Columns["TenChucDanh"].Width = 150;
                }
                
                if (dgvBangLuong.Columns.Contains("LuongCoBan"))
                {
                    dgvBangLuong.Columns["LuongCoBan"].HeaderText = "Lương cơ bản";
                    dgvBangLuong.Columns["LuongCoBan"].Width = 120;
                    dgvBangLuong.Columns["LuongCoBan"].DefaultCellStyle.Format = "N0";
                    dgvBangLuong.Columns["LuongCoBan"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }
                
                if (dgvBangLuong.Columns.Contains("HeSoLuong"))
                {
                    dgvBangLuong.Columns["HeSoLuong"].HeaderText = "Hệ số lương";
                    dgvBangLuong.Columns["HeSoLuong"].Width = 100;
                    dgvBangLuong.Columns["HeSoLuong"].DefaultCellStyle.Format = "N2";
                    dgvBangLuong.Columns["HeSoLuong"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }
                
                if (dgvBangLuong.Columns.Contains("PhuCap"))
                {
                    dgvBangLuong.Columns["PhuCap"].HeaderText = "Phụ cấp";
                    dgvBangLuong.Columns["PhuCap"].Width = 100;
                    dgvBangLuong.Columns["PhuCap"].DefaultCellStyle.Format = "N0";
                    dgvBangLuong.Columns["PhuCap"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }
                
                if (dgvBangLuong.Columns.Contains("SoNgayCong"))
                {
                    dgvBangLuong.Columns["SoNgayCong"].HeaderText = "Số ngày công";
                    dgvBangLuong.Columns["SoNgayCong"].Width = 100;
                    dgvBangLuong.Columns["SoNgayCong"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
                
                if (dgvBangLuong.Columns.Contains("KhenThuong"))
                {
                    dgvBangLuong.Columns["KhenThuong"].HeaderText = "Khen thưởng";
                    dgvBangLuong.Columns["KhenThuong"].Width = 100;
                    dgvBangLuong.Columns["KhenThuong"].DefaultCellStyle.Format = "N0";
                    dgvBangLuong.Columns["KhenThuong"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }
                
                if (dgvBangLuong.Columns.Contains("KyLuat"))
                {
                    dgvBangLuong.Columns["KyLuat"].HeaderText = "Kỷ luật";
                    dgvBangLuong.Columns["KyLuat"].Width = 100;
                    dgvBangLuong.Columns["KyLuat"].DefaultCellStyle.Format = "N0";
                    dgvBangLuong.Columns["KyLuat"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }
                
                if (dgvBangLuong.Columns.Contains("TamUng"))
                {
                    dgvBangLuong.Columns["TamUng"].HeaderText = "Tạm ứng";
                    dgvBangLuong.Columns["TamUng"].Width = 100;
                    dgvBangLuong.Columns["TamUng"].DefaultCellStyle.Format = "N0";
                    dgvBangLuong.Columns["TamUng"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }
                
                if (dgvBangLuong.Columns.Contains("ThucLanh"))
                {
                    dgvBangLuong.Columns["ThucLanh"].HeaderText = "Thực lãnh";
                    dgvBangLuong.Columns["ThucLanh"].Width = 120;
                    dgvBangLuong.Columns["ThucLanh"].DefaultCellStyle.Format = "N0";
                    dgvBangLuong.Columns["ThucLanh"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgvBangLuong.Columns["ThucLanh"].DefaultCellStyle.Font = new Font(dgvBangLuong.DefaultCellStyle.Font, FontStyle.Bold);
                }
                
                if (dgvBangLuong.Columns.Contains("GhiChu"))
                {
                    dgvBangLuong.Columns["GhiChu"].HeaderText = "Ghi chú";
                    dgvBangLuong.Columns["GhiChu"].Width = 200;
                }
                
                // Ẩn các cột không cần hiển thị
                if (dgvBangLuong.Columns.Contains("MaLuong"))
                    dgvBangLuong.Columns["MaLuong"].Visible = false;
                
                if (dgvBangLuong.Columns.Contains("MaPhong"))
                    dgvBangLuong.Columns["MaPhong"].Visible = false;
                    
                if (dgvBangLuong.Columns.Contains("MaChucDanh"))
                    dgvBangLuong.Columns["MaChucDanh"].Visible = false;
                
                // Định dạng màu sắc cho DataGridView
                dgvBangLuong.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(0, 122, 204);
                dgvBangLuong.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                dgvBangLuong.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
                dgvBangLuong.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
                dgvBangLuong.ColumnHeadersHeight = 35;
                dgvBangLuong.EnableHeadersVisualStyles = false;
                
                dgvBangLuong.RowHeadersVisible = false;
                dgvBangLuong.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(240, 240, 240);
                dgvBangLuong.DefaultCellStyle.SelectionBackColor = Color.FromArgb(0, 122, 204);
                dgvBangLuong.DefaultCellStyle.SelectionForeColor = Color.White;
                dgvBangLuong.AllowUserToAddRows = false;
                dgvBangLuong.AllowUserToDeleteRows = false;
                dgvBangLuong.AllowUserToResizeRows = false;
                dgvBangLuong.ReadOnly = true;
                
                // Sự kiện CellFormatting
                dgvBangLuong.CellFormatting += DgvBangLuong_CellFormatting;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi định dạng DataGridView: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void DgvBangLuong_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
                
                // Định dạng STT
                if (e.ColumnIndex == dgvBangLuong.Columns["STT"].Index)
                {
                    e.Value = e.RowIndex + 1;
                    e.FormattingApplied = true;
                    return;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi CellFormatting: " + ex.Message);
            }
        }
        
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            try
            {
                // Cập nhật tháng, năm
                _thang = Convert.ToInt32(cboThang.SelectedItem);
                _nam = Convert.ToInt32(cboNam.SelectedItem);
                
                // Tải lại bảng lương
                LoadBangLuong();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tìm kiếm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void btnTinhLuong_Click(object sender, EventArgs e)
        {
            try
            {
                // Xác nhận tính lương
                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn tính lương cho tháng " + _thang + "/" + _nam + " không?", 
                    "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    
                if (result == DialogResult.Yes)
                {
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tính lương: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void btnIn_Click(object sender, EventArgs e)
        {
            try
            {
                if (_dtBangLuong == null || _dtBangLuong.Rows.Count == 0)
                {
                    MessageBox.Show("Không có dữ liệu để in!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                
                // Tạo báo cáo bảng lương
                string title = "BẢNG LƯƠNG THÁNG " + _thang + " NĂM " + _nam;
                string fileTemplate = "BangLuong.xlsx";
                
                // Hiển thị dialog lưu file
                SaveFileDialog saveDialog = new SaveFileDialog();
                saveDialog.Filter = "Excel files (*.xlsx)|*.xlsx";
                saveDialog.FileName = "BangLuong_T" + _thang + "_" + _nam + ".xlsx";
                
                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi in báo cáo: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        
        private void cboThang_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboThang.SelectedItem != null && cboNam.SelectedItem != null)
            {
                _thang = Convert.ToInt32(cboThang.SelectedItem);
                _nam = Convert.ToInt32(cboNam.SelectedItem);
            }
        }
        
        private void cboNam_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboThang.SelectedItem != null && cboNam.SelectedItem != null)
            {
                _thang = Convert.ToInt32(cboThang.SelectedItem);
                _nam = Convert.ToInt32(cboNam.SelectedItem);
            }
        }
    }
}