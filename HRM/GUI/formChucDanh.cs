using System;
using System.Data;
using System.Windows.Forms;
using HRM.BLL;
using HRM.DTO;

namespace HRM.GUI
{
    public partial class formChucDanh : Form
    {
        public formChucDanh()
        {
            InitializeComponent();
            FormatDataGridView();
            LoadDataChucDanh();

            // Đăng ký các event
            this.Load += formChucDanh_Load;
            btnThem.Click += btnThem_Click;
            btnSua.Click += btnSua_Click;
            btnXoa.Click += btnXoa_Click;
            btnDong.Click += btnDong_Click;
            dgvChucDanh.SelectionChanged += dgvChucDanh_SelectionChanged;
            dgvChucDanh.CellFormatting += dgvChucDanh_CellFormatting;
        }

        private void formChucDanh_Load(object sender, EventArgs e)
        {
            LoadDataChucDanh();
        }

        private void FormatDataGridView()
        {
            dgvChucDanh.AutoGenerateColumns = false;
            dgvChucDanh.Columns.Clear();

            // Thêm các cột
            dgvChucDanh.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "STT",
                HeaderText = "STT",
                Width = 50
            });

            dgvChucDanh.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "MaChucDanh",
                HeaderText = "Mã chức danh",
                DataPropertyName = "MaChucDanh",
                Width = 100
            });

            dgvChucDanh.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "TenChucDanh",
                HeaderText = "Tên chức danh",
                DataPropertyName = "TenChucDanh",
                Width = 200
            });

            dgvChucDanh.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "MoTa",
                HeaderText = "Mô tả",
                DataPropertyName = "MoTa",
                Width = 300
            });
        }

        private void LoadDataChucDanh()
        {
            try
            {
                DataTable dt = ChucDanhBLL.GetList();
                dgvChucDanh.DataSource = dt;

                // Cập nhật số lượng
                int tongSo = dt.Rows.Count;
                lblTongSo.Text = $"Tổng số: {tongSo} chức danh";

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
            txtChuDanh.Text = "";
            txtMoTa.Text = "";
            if (dgvChucDanh.Rows.Count > 0)
                dgvChucDanh.ClearSelection();
        }

        private void dgvChucDanh_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvChucDanh.SelectedRows.Count > 0)
            {
                var row = dgvChucDanh.SelectedRows[0];
                txtChuDanh.Text = row.Cells["TenChucDanh"].Value?.ToString();
                txtMoTa.Text = row.Cells["MoTa"].Value?.ToString();
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtChuDanh.Text))
                {
                    MessageBox.Show("Vui lòng nhập tên chức danh", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                ChucDanhDTO cd = new ChucDanhDTO
                {
                    TenChucDanh = txtChuDanh.Text.Trim(),
                    MoTa = txtMoTa.Text.Trim()
                };

                int result = ChucDanhBLL.Add(cd);
                if (result > 0)
                {
                    MessageBox.Show("Thêm chức danh thành công", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDataChucDanh();
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
                if (dgvChucDanh.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Vui lòng chọn chức danh cần sửa", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtChuDanh.Text))
                {
                    MessageBox.Show("Vui lòng nhập tên chức danh", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var row = dgvChucDanh.SelectedRows[0];
                ChucDanhDTO cd = new ChucDanhDTO
                {
                    MaChucDanh = Convert.ToInt32(row.Cells["MaChucDanh"].Value),
                    TenChucDanh = txtChuDanh.Text.Trim(),
                    MoTa = txtMoTa.Text.Trim()
                };

                if (ChucDanhBLL.Update(cd))
                {
                    MessageBox.Show("Cập nhật chức danh thành công", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDataChucDanh();
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
                if (dgvChucDanh.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Vui lòng chọn chức danh cần xóa", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var row = dgvChucDanh.SelectedRows[0];
                int maChucDanh = Convert.ToInt32(row.Cells["MaChucDanh"].Value);
                string tenChucDanh = row.Cells["TenChucDanh"].Value.ToString();

                DialogResult dr = MessageBox.Show(
                    $"Bạn có chắc chắn muốn xóa chức danh '{tenChucDanh}'?",
                    "Xác nhận xóa",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (dr == DialogResult.Yes)
                {
                    if (ChucDanhBLL.Delete(maChucDanh))
                    {
                        MessageBox.Show("Xóa chức danh thành công", "Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadDataChucDanh();
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

        private void dgvChucDanh_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == dgvChucDanh.Columns["STT"].Index)
            {
                e.Value = e.RowIndex + 1;
            }
        }
    }
}