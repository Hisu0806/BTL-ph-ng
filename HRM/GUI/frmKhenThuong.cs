using System;
using System.Data;
using System.Windows.Forms;
using HRM.BLL;
using HRM.DTO;

namespace HRM.Forms
{
    public partial class frmKhenThuong : Form
    {
        private int maKhenThuong = 0;

        public frmKhenThuong()
        {
            InitializeComponent();
            FormatDataGridView();
            LoadComboBoxNhanVien();
            LoadData();
        }

        private void FormatDataGridView()
        {
            dgvKhenThuong.Columns.Clear();
            dgvKhenThuong.Columns.Add("STT", "STT");
            dgvKhenThuong.Columns.Add("MaKhenThuong", "Mã khen thưởng");
            dgvKhenThuong.Columns.Add("HoTen", "Họ tên");
            dgvKhenThuong.Columns.Add("NgayKhenThuong", "Ngày khen thưởng");
            dgvKhenThuong.Columns.Add("HinhThuc", "Hình thức");
            dgvKhenThuong.Columns.Add("LyDo", "Lý do");
            dgvKhenThuong.Columns.Add("SoTien", "Số tiền");

            dgvKhenThuong.Columns["STT"].Width = 50;
            dgvKhenThuong.Columns["MaKhenThuong"].Width = 100;
            dgvKhenThuong.Columns["HoTen"].Width = 150;
            dgvKhenThuong.Columns["NgayKhenThuong"].Width = 100;
            dgvKhenThuong.Columns["HinhThuc"].Width = 150;
            dgvKhenThuong.Columns["LyDo"].Width = 200;
            dgvKhenThuong.Columns["SoTien"].Width = 100;

            dgvKhenThuong.Columns["MaKhenThuong"].Visible = false;
        }

        private void LoadComboBoxNhanVien()
        {
            try
            {
                DataTable dt = NhanVienBLL.GetList();
                cboMaNV.DataSource = dt;
                cboMaNV.DisplayMember = "HoTen";
                cboMaNV.ValueMember = "MaNV";
                cboMaNV.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách nhân viên: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadData()
        {
            try
            {
                dgvKhenThuong.Rows.Clear();
                DataTable dt = KhenThuongBLL.GetList();
                int stt = 1;
                foreach (DataRow row in dt.Rows)
                {
                    dgvKhenThuong.Rows.Add(
                        stt++,
                        row["MaKhenThuong"],
                        row["HoTen"],
                        Convert.ToDateTime(row["NgayKhenThuong"]).ToString("dd/MM/yyyy"),
                        row["HinhThuc"],
                        row["LyDo"],
                        row["SoTien"] == DBNull.Value ? "" : Convert.ToDecimal(row["SoTien"]).ToString("N0") + " VNĐ"
                    );
                }
                lblTongSo.Text = $"Tổng số: {dt.Rows.Count} khen thưởng";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

                KhenThuong kt = new KhenThuong
                {
                    MaNV = Convert.ToInt32(cboMaNV.SelectedValue),
                    NgayKhenThuong = dtpNgayKhenThuong.Value,
                    HinhThuc = txtHinhThuc.Text.Trim(),
                    LyDo = txtLyDo.Text.Trim(),
                    SoTien = string.IsNullOrEmpty(txtSoTien.Text) ? null : (decimal?)Convert.ToDecimal(txtSoTien.Text)
                };

                if (KhenThuongBLL.Add(kt) > 0)
                {
                    MessageBox.Show("Thêm khen thưởng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearInputs();
                    LoadData();
                }
                else
                {
                    MessageBox.Show("Thêm khen thưởng thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                if (maKhenThuong == 0)
                {
                    MessageBox.Show("Vui lòng chọn khen thưởng cần sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (cboMaNV.SelectedValue == null)
                {
                    MessageBox.Show("Vui lòng chọn nhân viên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                KhenThuong kt = new KhenThuong
                {
                    MaKhenThuong = maKhenThuong,
                    MaNV = Convert.ToInt32(cboMaNV.SelectedValue),
                    NgayKhenThuong = dtpNgayKhenThuong.Value,
                    HinhThuc = txtHinhThuc.Text.Trim(),
                    LyDo = txtLyDo.Text.Trim(),
                    SoTien = string.IsNullOrEmpty(txtSoTien.Text) ? null : (decimal?)Convert.ToDecimal(txtSoTien.Text)
                };

                if (KhenThuongBLL.Update(kt))
                {
                    MessageBox.Show("Cập nhật khen thưởng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearInputs();
                    LoadData();
                }
                else
                {
                    MessageBox.Show("Cập nhật khen thưởng thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                if (maKhenThuong == 0)
                {
                    MessageBox.Show("Vui lòng chọn khen thưởng cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (MessageBox.Show("Bạn có chắc chắn muốn xóa khen thưởng này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (KhenThuongBLL.Delete(maKhenThuong))
                    {
                        MessageBox.Show("Xóa khen thưởng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ClearInputs();
                        LoadData();
                    }
                    else
                    {
                        MessageBox.Show("Xóa khen thưởng thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                if (string.IsNullOrEmpty(txtTimKiem.Text))
                {
                    LoadData();
                    return;
                }

                if (int.TryParse(txtTimKiem.Text, out int maNV))
                {
                    dgvKhenThuong.Rows.Clear();
                    DataTable dt = KhenThuongBLL.GetByMaNV(maNV);
                    int stt = 1;
                    foreach (DataRow row in dt.Rows)
                    {
                        dgvKhenThuong.Rows.Add(
                            stt++,
                            row["MaKhenThuong"],
                            row["HoTen"],
                            Convert.ToDateTime(row["NgayKhenThuong"]).ToString("dd/MM/yyyy"),
                            row["HinhThuc"],
                            row["LyDo"],
                            row["SoTien"] == DBNull.Value ? "" : Convert.ToDecimal(row["SoTien"]).ToString("N0") + " VNĐ"
                        );
                    }
                    lblTongSo.Text = $"Tổng số: {dt.Rows.Count} khen thưởng";
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

        private void dgvKhenThuong_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvKhenThuong.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dgvKhenThuong.SelectedRows[0];
                maKhenThuong = Convert.ToInt32(row.Cells["MaKhenThuong"].Value);
                cboMaNV.Text = row.Cells["HoTen"].Value.ToString();

                string ngayKhenThuongStr = row.Cells["NgayKhenThuong"].Value.ToString();
                if (!string.IsNullOrEmpty(ngayKhenThuongStr))
                {
                    dtpNgayKhenThuong.Value = DateTime.ParseExact(ngayKhenThuongStr, "dd/MM/yyyy", null);
                }

                txtHinhThuc.Text = row.Cells["HinhThuc"].Value.ToString();
                txtLyDo.Text = row.Cells["LyDo"].Value.ToString();
                txtSoTien.Text = row.Cells["SoTien"].Value.ToString().Replace(" VNĐ", "").Replace(",", "");
            }
        }

        private void ClearInputs()
        {
            maKhenThuong = 0;
            cboMaNV.SelectedIndex = -1;
            dtpNgayKhenThuong.Value = DateTime.Now;
            txtHinhThuc.Clear();
            txtLyDo.Clear();
            txtSoTien.Clear();
            txtTimKiem.Clear();
            dgvKhenThuong.ClearSelection();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}