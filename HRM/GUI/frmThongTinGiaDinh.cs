using System;
using System.Data;
using System.Windows.Forms;
using HRM.BLL;
using HRM.DTO;

namespace HRM.Forms
{
    public partial class frmThongTinGiaDinh : Form
    {
        private bool isAdding = false;
        private int currentMaThongTin = 0;

        public frmThongTinGiaDinh()
        {
            InitializeComponent();
            LoadData();
            LoadNhanVien();
        }

        private void LoadData()
        {
            try
            {
                DataTable dt = ThongTinGiaDinhBLL.GetList();
                if (dt != null && dt.Rows.Count > 0)
                {
                    dgvThongTinGiaDinh.DataSource = dt;
                    lblTongSo.Text = $"Tổng số: {dt.Rows.Count}";
                }
                else
                {
                    dgvThongTinGiaDinh.DataSource = null;
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
            txtHoTen.Clear();
            txtQuanHe.Clear();
            dtpNgaySinh.Value = DateTime.Now;
            txtNgheNghiep.Clear();
            txtDiaChi.Clear();
            txtGhiChu.Clear();
            currentMaThongTin = 0;
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

                if (string.IsNullOrWhiteSpace(txtHoTen.Text))
                {
                    MessageBox.Show("Vui lòng nhập họ tên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtQuanHe.Text))
                {
                    MessageBox.Show("Vui lòng nhập quan hệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                ThongTinGiaDinhDTO ttg = new ThongTinGiaDinhDTO
                {
                    MaNV = Convert.ToInt32(cboMaNV.SelectedValue),
                    HoTen = txtHoTen.Text.Trim(),
                    QuanHe = txtQuanHe.Text.Trim(),
                    NgaySinh = dtpNgaySinh.Value,
                    NgheNghiep = string.IsNullOrWhiteSpace(txtNgheNghiep.Text) ? null : txtNgheNghiep.Text.Trim(),
                    DiaChi = string.IsNullOrWhiteSpace(txtDiaChi.Text) ? null : txtDiaChi.Text.Trim(),
                    GhiChu = string.IsNullOrWhiteSpace(txtGhiChu.Text) ? null : txtGhiChu.Text.Trim()
                };

                int result = ThongTinGiaDinhBLL.Add(ttg);
                if (result > 0)
                {
                    MessageBox.Show("Thêm thông tin gia đình thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                    ClearInputs();
                }
                else
                {
                    MessageBox.Show("Thêm thông tin gia đình thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                if (currentMaThongTin <= 0)
                {
                    MessageBox.Show("Vui lòng chọn thông tin gia đình cần sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (cboMaNV.SelectedValue == null)
                {
                    MessageBox.Show("Vui lòng chọn nhân viên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtHoTen.Text))
                {
                    MessageBox.Show("Vui lòng nhập họ tên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtQuanHe.Text))
                {
                    MessageBox.Show("Vui lòng nhập quan hệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                ThongTinGiaDinhDTO ttg = new ThongTinGiaDinhDTO
                {
                    MaThongTin = currentMaThongTin,
                    MaNV = Convert.ToInt32(cboMaNV.SelectedValue),
                    HoTen = txtHoTen.Text.Trim(),
                    QuanHe = txtQuanHe.Text.Trim(),
                    NgaySinh = dtpNgaySinh.Value,
                    NgheNghiep = string.IsNullOrWhiteSpace(txtNgheNghiep.Text) ? null : txtNgheNghiep.Text.Trim(),
                    DiaChi = string.IsNullOrWhiteSpace(txtDiaChi.Text) ? null : txtDiaChi.Text.Trim(),
                    GhiChu = string.IsNullOrWhiteSpace(txtGhiChu.Text) ? null : txtGhiChu.Text.Trim()
                };

                if (ThongTinGiaDinhBLL.Update(ttg))
                {
                    MessageBox.Show("Cập nhật thông tin gia đình thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                    ClearInputs();
                }
                else
                {
                    MessageBox.Show("Cập nhật thông tin gia đình thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                if (currentMaThongTin <= 0)
                {
                    MessageBox.Show("Vui lòng chọn thông tin gia đình cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (MessageBox.Show("Bạn có chắc chắn muốn xóa thông tin gia đình này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (ThongTinGiaDinhBLL.Delete(currentMaThongTin))
                    {
                        MessageBox.Show("Xóa thông tin gia đình thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadData();
                        ClearInputs();
                    }
                    else
                    {
                        MessageBox.Show("Xóa thông tin gia đình thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    DataTable dt = ThongTinGiaDinhBLL.GetByMaNV(maNV);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        dgvThongTinGiaDinh.DataSource = dt;
                        lblTongSo.Text = $"Tổng số: {dt.Rows.Count}";
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy thông tin gia đình nào cho nhân viên này!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void dgvThongTinGiaDinh_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvThongTinGiaDinh.CurrentRow != null)
            {
                DataGridViewRow row = dgvThongTinGiaDinh.CurrentRow;
                currentMaThongTin = Convert.ToInt32(row.Cells["MaThongTin"].Value);
                cboMaNV.SelectedValue = Convert.ToInt32(row.Cells["MaNV"].Value);
                txtHoTen.Text = row.Cells["HoTen"].Value.ToString();
                txtQuanHe.Text = row.Cells["QuanHe"].Value.ToString();
                dtpNgaySinh.Value = row.Cells["NgaySinh"].Value == DBNull.Value ? DateTime.Now : Convert.ToDateTime(row.Cells["NgaySinh"].Value);
                txtNgheNghiep.Text = row.Cells["NgheNghiep"].Value?.ToString() ?? "";
                txtDiaChi.Text = row.Cells["DiaChi"].Value?.ToString() ?? "";
                txtGhiChu.Text = row.Cells["GhiChu"].Value?.ToString() ?? "";
                isAdding = false;
            }
        }
    }
}