using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using HRM.BLL;

namespace HRM.GUI
{
    public partial class formBangChamCong : Form
    {
        private int _thang = DateTime.Now.Month;
        private int _nam = DateTime.Now.Year;
        private DataTable _dtChamCong;
        
        public formBangChamCong()
        {
            InitializeComponent();
        }

        private void formBangChamCong_Load(object sender, EventArgs e)
        {
            try
            {
                // Khởi tạo combobox tháng và năm
                InitComboboxes();
                
                // Tải dữ liệu bảng chấm công
                LoadBangChamCong();
                
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
        
        private void LoadBangChamCong()
        {
            try
            {
                // Lấy thông tin tìm kiếm
                string maNV = txtMaNV.Text.Trim();
                string tenNV = txtTenNV.Text.Trim();
                string maPhong = cboPhong.SelectedValue != null ? cboPhong.SelectedValue.ToString() : null;
                
                dgvBangChamCong.DataSource = _dtChamCong;
                
                // Hiển thị số lượng nhân viên
                lblTongSo.Text = "Tổng số: " + _dtChamCong.Rows.Count + " nhân viên";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải bảng chấm công: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void FormatDataGridView()
        {
            try
            {
                if (dgvBangChamCong.Columns.Count == 0) return;
                
                // Định dạng các cột
                if (dgvBangChamCong.Columns.Contains("STT"))
                {
                    dgvBangChamCong.Columns["STT"].HeaderText = "STT";
                    dgvBangChamCong.Columns["STT"].Width = 50;
                }
                
                if (dgvBangChamCong.Columns.Contains("MaNV"))
                {
                    dgvBangChamCong.Columns["MaNV"].HeaderText = "Mã NV";
                    dgvBangChamCong.Columns["MaNV"].Width = 80;
                }
                
                if (dgvBangChamCong.Columns.Contains("HoTen"))
                {
                    dgvBangChamCong.Columns["HoTen"].HeaderText = "Họ và tên";
                    dgvBangChamCong.Columns["HoTen"].Width = 150;
                }
                
                if (dgvBangChamCong.Columns.Contains("TenPhong"))
                {
                    dgvBangChamCong.Columns["TenPhong"].HeaderText = "Phòng ban";
                    dgvBangChamCong.Columns["TenPhong"].Width = 120;
                }
                
                if (dgvBangChamCong.Columns.Contains("TenChucDanh"))
                {
                    dgvBangChamCong.Columns["TenChucDanh"].HeaderText = "Chức danh";
                    dgvBangChamCong.Columns["TenChucDanh"].Width = 120;
                }
                
                // Định dạng các cột ngày
                int daysInMonth = DateTime.DaysInMonth(_nam, _thang);
                for (int i = 1; i <= daysInMonth; i++)
                {
                    string colName = "D" + i;
                    if (dgvBangChamCong.Columns.Contains(colName))
                    {
                        dgvBangChamCong.Columns[colName].HeaderText = i.ToString();
                        dgvBangChamCong.Columns[colName].Width = 30;
                        
                        // Sự kiện CellFormatting sẽ được xử lý riêng
                    }
                }
                
                if (dgvBangChamCong.Columns.Contains("DiLam"))
                {
                    dgvBangChamCong.Columns["DiLam"].HeaderText = "Đi làm";
                    dgvBangChamCong.Columns["DiLam"].Width = 60;
                }
                
                if (dgvBangChamCong.Columns.Contains("NghiPhep"))
                {
                    dgvBangChamCong.Columns["NghiPhep"].HeaderText = "N.Phép";
                    dgvBangChamCong.Columns["NghiPhep"].Width = 60;
                }
                
                if (dgvBangChamCong.Columns.Contains("NghiKhongPhep"))
                {
                    dgvBangChamCong.Columns["NghiKhongPhep"].HeaderText = "N.K.Phép";
                    dgvBangChamCong.Columns["NghiKhongPhep"].Width = 60;
                }
                
                if (dgvBangChamCong.Columns.Contains("DiTre"))
                {
                    dgvBangChamCong.Columns["DiTre"].HeaderText = "Đi trễ";
                    dgvBangChamCong.Columns["DiTre"].Width = 60;
                }
                
                if (dgvBangChamCong.Columns.Contains("VeSom"))
                {
                    dgvBangChamCong.Columns["VeSom"].HeaderText = "Về sớm";
                    dgvBangChamCong.Columns["VeSom"].Width = 60;
                }
                
                // Ẩn các cột không cần hiển thị
                if (dgvBangChamCong.Columns.Contains("MaPhong"))
                    dgvBangChamCong.Columns["MaPhong"].Visible = false;
                    
                if (dgvBangChamCong.Columns.Contains("MaChucDanh"))
                    dgvBangChamCong.Columns["MaChucDanh"].Visible = false;
                
                // Định dạng màu sắc
                dgvBangChamCong.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(0, 122, 204);
                dgvBangChamCong.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                dgvBangChamCong.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
                dgvBangChamCong.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
                dgvBangChamCong.ColumnHeadersHeight = 35;
                dgvBangChamCong.EnableHeadersVisualStyles = false;
                
                dgvBangChamCong.RowHeadersVisible = false;
                dgvBangChamCong.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(240, 240, 240);
                dgvBangChamCong.DefaultCellStyle.SelectionBackColor = Color.FromArgb(0, 122, 204);
                dgvBangChamCong.DefaultCellStyle.SelectionForeColor = Color.White;
                dgvBangChamCong.AllowUserToAddRows = false;
                dgvBangChamCong.AllowUserToDeleteRows = false;
                dgvBangChamCong.AllowUserToResizeRows = false;
                dgvBangChamCong.ReadOnly = true;
                
                // Sự kiện CellFormatting cho định dạng các ô trạng thái chấm công
                dgvBangChamCong.CellFormatting += DgvBangChamCong_CellFormatting;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi định dạng DataGridView: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void DgvBangChamCong_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
                
                // Định dạng STT
                if (e.ColumnIndex == dgvBangChamCong.Columns["STT"].Index)
                {
                    e.Value = e.RowIndex + 1;
                    e.FormattingApplied = true;
                    return;
                }
                
                // Định dạng ô trạng thái chấm công (D1, D2, ...)
                string colName = dgvBangChamCong.Columns[e.ColumnIndex].Name;
                if (colName.StartsWith("D") && colName.Length > 1 && int.TryParse(colName.Substring(1), out int day))
                {
                    if (e.Value == null || e.Value == DBNull.Value)
                    {
                        e.Value = "";
                        e.FormattingApplied = true;
                        return;
                    }
                    
                    string value = e.Value.ToString();
                    
                    switch (value)
                    {
                        case "Đ": // Đi làm
                            e.Value = "X";
                            e.CellStyle.BackColor = Color.LightGreen;
                            break;
                        case "P": // Nghỉ phép
                            e.Value = "P";
                            e.CellStyle.BackColor = Color.LightBlue;
                            break;
                        case "K": // Nghỉ không phép
                            e.Value = "K";
                            e.CellStyle.BackColor = Color.Red;
                            e.CellStyle.ForeColor = Color.White;
                            break;
                        case "T": // Đi trễ
                            e.Value = "T";
                            e.CellStyle.BackColor = Color.Yellow;
                            break;
                        case "S": // Về sớm
                            e.Value = "S";
                            e.CellStyle.BackColor = Color.Orange;
                            break;
                        case "N": // Ngày nghỉ/chủ nhật
                            e.Value = "";
                            e.CellStyle.BackColor = Color.LightGray;
                            break;
                        default:
                            e.Value = "";
                            break;
                    }
                    
                    e.FormattingApplied = true;
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
                
                // Tải lại bảng chấm công
                LoadBangChamCong();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tìm kiếm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void btnIn_Click(object sender, EventArgs e)
        {
            try
            {
                if (_dtChamCong == null || _dtChamCong.Rows.Count == 0)
                {
                    MessageBox.Show("Không có dữ liệu để in!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                
                // Tạo báo cáo bảng chấm công
                string title = "BẢNG CHẤM CÔNG THÁNG " + _thang + " NĂM " + _nam;
                string fileTemplate = "BangChamCong.xlsx";
                
                // Hiển thị dialog lưu file
                SaveFileDialog saveDialog = new SaveFileDialog();
                saveDialog.Filter = "Excel files (*.xlsx)|*.xlsx";
                saveDialog.FileName = "BangChamCong_T" + _thang + "_" + _nam + ".xlsx";
                
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