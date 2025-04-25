using System;
using System.Data;
using System.Windows.Forms;
using HRM.BLL;
using HRM.DTO;

namespace HRM.GUI
{
    public partial class formPhongBan : Form
    {
        public formPhongBan()
        {
            InitializeComponent();
            FormatDataGridView();
            LoadDataPhongBan();

            // Đăng ký các event
            this.Load += formPhongBan_Load;
            btnThem.Click += btnThem_Click;
            btnSua.Click += btnSua_Click;
            btnXoa.Click += btnXoa_Click;
            btnDong.Click += btnDong_Click;
            dgvPhongBan.SelectionChanged += dgvPhongBan_SelectionChanged;
            dgvPhongBan.CellFormatting += dgvPhongBan_CellFormatting;
        }

        private void formPhongBan_Load(object sender, EventArgs e)
        {
            LoadDataPhongBan();
        }

        private void FormatDataGridView()
        {
            dgvPhongBan.AutoGenerateColumns = false;
            dgvPhongBan.Columns.Clear();

            // Thêm các cột
            dgvPhongBan.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "STT",
                HeaderText = "STT",
                Width = 50
            });

            dgvPhongBan.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "MaPhong",
                HeaderText = "Mã phòng",
                DataPropertyName = "MaPhong",
                Width = 100
            });

            dgvPhongBan.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "TenPhong",
                HeaderText = "Tên phòng",
                DataPropertyName = "TenPhong",
                Width = 200
            });

            dgvPhongBan.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "MoTa",
                HeaderText = "Mô tả",
                DataPropertyName = "MoTa",
                Width = 300
            });

            dgvPhongBan.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "TenTruongPhong",
                HeaderText = "Trưởng phòng",
                DataPropertyName = "TenTruongPhong",
                Width = 200
            });
        }

        private void LoadDataPhongBan()
        {
            try
            {
                DataTable dt = PhongBanBLL.GetList();
                dgvPhongBan.DataSource = dt;

                // Cập nhật số lượng
                int tongSo = dt.Rows.Count;
                lblTongSo.Text = $"Tổng số: {tongSo} phòng ban";

                // Reset form
                ResetForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ResetForm()
        {
            txtTenPhong.Text = "";
            txtMoTa.Text = "";
            if (dgvPhongBan.Rows.Count > 0)
                dgvPhongBan.ClearSelection();
        }

        private void dgvPhongBan_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvPhongBan.SelectedRows.Count > 0)
            {
                var row = dgvPhongBan.SelectedRows[0];
                txtTenPhong.Text = row.Cells["TenPhong"].Value?.ToString();
                txtMoTa.Text = row.Cells["MoTa"].Value?.ToString();
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtTenPhong.Text))
                {
                    MessageBox.Show("Vui lòng nhập tên phòng ban", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                PhongBanDTO pb = new PhongBanDTO
                {
                    TenPhong = txtTenPhong.Text.Trim(),
                    MoTa = txtMoTa.Text.Trim()
                };

                int result = PhongBanBLL.Add(pb);
                if (result > 0)
                {
                    MessageBox.Show("Thêm phòng ban thành công", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDataPhongBan();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvPhongBan.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Vui lòng chọn phòng ban cần sửa", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtTenPhong.Text))
                {
                    MessageBox.Show("Vui lòng nhập tên phòng ban", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var row = dgvPhongBan.SelectedRows[0];
                PhongBanDTO pb = new PhongBanDTO
                {
                    MaPhong = Convert.ToInt32(row.Cells["MaPhong"].Value),
                    TenPhong = txtTenPhong.Text.Trim(),
                    MoTa = txtMoTa.Text.Trim()
                    // Giữ nguyên TruongPhong từ dữ liệu cũ
                };

                if (PhongBanBLL.Update(pb))
                {
                    MessageBox.Show("Cập nhật phòng ban thành công", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDataPhongBan();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvPhongBan.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Vui lòng chọn phòng ban cần xóa", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var row = dgvPhongBan.SelectedRows[0];
                int maPhong = Convert.ToInt32(row.Cells["MaPhong"].Value);
                string tenPhong = row.Cells["TenPhong"].Value.ToString();

                DialogResult dr = MessageBox.Show(
                    $"Bạn có chắc chắn muốn xóa phòng ban '{tenPhong}'?",
                    "Xác nhận xóa",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (dr == DialogResult.Yes)
                {
                    if (PhongBanBLL.Delete(maPhong))
                    {
                        MessageBox.Show("Xóa phòng ban thành công", "Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadDataPhongBan();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvPhongBan_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == dgvPhongBan.Columns["STT"].Index)
            {
                e.Value = e.RowIndex + 1;
            }
        }
    }
}