using System;
using System.Data;
using System.Windows.Forms;
using HRM.BLL;
using HRM.DTO;

namespace HRM.Forms
{
    public partial class frmKyLuat : Form
    {
        private bool isAdding = false;
        private int currentMaKyLuat = 0;

        public frmKyLuat()
        {
            InitializeComponent();
            LoadData();
            LoadNhanVien();
        }

        private void LoadData()
        {
            try
            {
                DataTable dt = KyLuatBLL.GetList();
                if (dt != null && dt.Rows.Count > 0)
                {
                    dgvKyLuat.DataSource = dt;
                    lblTongSo.Text = $"Tổng số: {dt.Rows.Count}";
                }
                else
                {
                    dgvKyLuat.DataSource = null;
                    lblTongSo.Text = "Tổng số: 0";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadNhanVien()
        {
            try
            {
                DataTable dt = NhanVienBLL.GetList();
                if (dt != null && dt.Rows.Count > 0)
                {
                    cboMaNV.DataSource = dt;
                    cboMaNV.DisplayMember = "TenNV";
                    cboMaNV.ValueMember = "MaNV";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách nhân viên: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearInputs()
        {
            cboMaNV.SelectedIndex = -1;
            dtpNgayKyLuat.Value = DateTime.Now;
            txtHinhThuc.Clear();
            txtLyDo.Clear();
            txtSoTien.Clear();
            currentMaKyLuat = 0;
            isAdding = false;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                if (cboMaNV.SelectedValue == null)
                {
                    MessageBox.Show("Vui lòng chọn nhân viên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtHinhThuc.Text))
                {
                    MessageBox.Show("Vui lòng nhập hình thức kỷ luật!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                KyLuat kl = new KyLuat
                {
                    MaNV = Convert.ToInt32(cboMaNV.SelectedValue),
                    NgayKyLuat = dtpNgayKyLuat.Value,
                    HinhThuc = txtHinhThuc.Text.Trim(),
                    LyDo = txtLyDo.Text.Trim(),
                    SoTien = string.IsNullOrWhiteSpace(txtSoTien.Text) ? null : (decimal?)Convert.ToDecimal(txtSoTien.Text)
                };

                int result = KyLuatBLL.Add(kl);
                if (result > 0)
                {
                    MessageBox.Show("Thêm kỷ luật thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                    ClearInputs();
                }
                else
                {
                    MessageBox.Show("Thêm kỷ luật thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                if (currentMaKyLuat <= 0)
                {
                    MessageBox.Show("Vui lòng chọn kỷ luật cần sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (cboMaNV.SelectedValue == null)
                {
                    MessageBox.Show("Vui lòng chọn nhân viên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtHinhThuc.Text))
                {
                    MessageBox.Show("Vui lòng nhập hình thức kỷ luật!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                KyLuat kl = new KyLuat
                {
                    MaKyLuat = currentMaKyLuat,
                    MaNV = Convert.ToInt32(cboMaNV.SelectedValue),
                    NgayKyLuat = dtpNgayKyLuat.Value,
                    HinhThuc = txtHinhThuc.Text.Trim(),
                    LyDo = txtLyDo.Text.Trim(),
                    SoTien = string.IsNullOrWhiteSpace(txtSoTien.Text) ? null : (decimal?)Convert.ToDecimal(txtSoTien.Text)
                };

                if (KyLuatBLL.Update(kl))
                {
                    MessageBox.Show("Cập nhật kỷ luật thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                    ClearInputs();
                }
                else
                {
                    MessageBox.Show("Cập nhật kỷ luật thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                if (currentMaKyLuat <= 0)
                {
                    MessageBox.Show("Vui lòng chọn kỷ luật cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (MessageBox.Show("Bạn có chắc chắn muốn xóa kỷ luật này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (KyLuatBLL.Delete(currentMaKyLuat))
                    {
                        MessageBox.Show("Xóa kỷ luật thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadData();
                        ClearInputs();
                    }
                    else
                    {
                        MessageBox.Show("Xóa kỷ luật thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtTimKiem.Text))
                {
                    LoadData();
                    return;
                }

                if (int.TryParse(txtTimKiem.Text, out int maNV))
                {
                    DataTable dt = KyLuatBLL.GetByMaNV(maNV);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        dgvKyLuat.DataSource = dt;
                        lblTongSo.Text = $"Tổng số: {dt.Rows.Count}";
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy kỷ luật nào cho nhân viên này!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng nhập mã nhân viên hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvKyLuat_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvKyLuat.CurrentRow != null)
            {
                DataGridViewRow row = dgvKyLuat.CurrentRow;
                currentMaKyLuat = Convert.ToInt32(row.Cells["MaKyLuat"].Value);
                cboMaNV.SelectedValue = Convert.ToInt32(row.Cells["MaNV"].Value);
                dtpNgayKyLuat.Value = Convert.ToDateTime(row.Cells["NgayKyLuat"].Value);
                txtHinhThuc.Text = row.Cells["HinhThuc"].Value.ToString();
                txtLyDo.Text = row.Cells["LyDo"].Value.ToString();
                txtSoTien.Text = row.Cells["SoTien"].Value == DBNull.Value ? "" : row.Cells["SoTien"].Value.ToString();
                isAdding = false;
            }
        }
    }
}