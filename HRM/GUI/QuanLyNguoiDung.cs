using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using HRM.BLL;
using HRM.DAL;
using HRM.DTO;

namespace HRM.GUI
{
    public partial class QuanLyNguoiDung : Form
    {

        public QuanLyNguoiDung()
        {
            InitializeComponent();
            LoadComboBoxMaNV();
        }

        private void QuanLyNguoiDung_Load(object sender, EventArgs e)
        {
            FormatDataGridView();
            LoadDataNguoiDung();
            LoadComboBoxMaNV();
        }


        private void LoadComboBoxMaNV()
        {
            try
            {
                var danhSachMaNV = NhanVienBLL.GetDanhSachMaNV();

                // Clear và set lại DataSource
                cbMaNV.DataSource = null;
                cbMaNV.Items.Clear();

                // Thêm một item trống nếu cần
                // cbMaNV.Items.Add("");

                // Set DataSource mới
                cbMaNV.DataSource = danhSachMaNV;

                // Nếu có dữ liệu thì select item đầu tiên
                if (cbMaNV.Items.Count > 0)
                {
                    cbMaNV.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải danh sách mã nhân viên: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }




        private void LoadDataNguoiDung()
        {
            dgvNguoiDung.DataSource = NguoiDungDAL.GetList();
        }

        private void FormatDataGridView()
        {
            dgvNguoiDung.AutoGenerateColumns = false;
            dgvNguoiDung.Columns.Clear();

            dgvNguoiDung.Columns.Add(new DataGridViewTextBoxColumn() { Name = "STT", HeaderText = "STT" });
            dgvNguoiDung.Columns.Add(new DataGridViewTextBoxColumn() { DataPropertyName = "MaNguoiDung", HeaderText = "Mã người dùng", Name = "MaNguoiDung" });
            dgvNguoiDung.Columns.Add(new DataGridViewTextBoxColumn() { DataPropertyName = "TenDangNhap", HeaderText = "Tên đăng nhập" });
            dgvNguoiDung.Columns.Add(new DataGridViewTextBoxColumn() { DataPropertyName = "HoTen", HeaderText = "Họ tên" });
            dgvNguoiDung.Columns.Add(new DataGridViewTextBoxColumn() { DataPropertyName = "Email", HeaderText = "Email" });
            dgvNguoiDung.Columns.Add(new DataGridViewTextBoxColumn() { DataPropertyName = "DienThoai", HeaderText = "Điện thoại" });
            dgvNguoiDung.Columns.Add(new DataGridViewTextBoxColumn() { DataPropertyName = "VaiTro", HeaderText = "Vai trò" });
            dgvNguoiDung.Columns.Add(new DataGridViewCheckBoxColumn() { DataPropertyName = "TrangThai", HeaderText = "Trạng thái" });
            dgvNguoiDung.Columns.Add(new DataGridViewTextBoxColumn() { DataPropertyName = "MaNV", HeaderText = "Mã NV" });

            dgvNguoiDung.CellFormatting += dgvNguoiDung_CellFormatting;
            dgvNguoiDung.SelectionChanged += dgvNguoiDung_SelectionChanged;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(cbMaNV.Text))
            {
                MessageBox.Show("Vui lòng chọn Mã nhân viên.");
                return;
            }

            NguoiDungDTO nd = new NguoiDungDTO()
            {
                TenDangNhap = txtTenDangNhap.Text.Trim(),
                MatKhau = txtMatKhau.Text.Trim(),
                HoTen = txtHoTen.Text.Trim(),
                Email = string.IsNullOrWhiteSpace(txtEmail.Text) ? null : txtEmail.Text.Trim(),
                DienThoai = string.IsNullOrWhiteSpace(txtDienThoai.Text) ? null : txtDienThoai.Text.Trim(),
                VaiTro = cbVaiTro.Text.Trim(),
                MaNV = cbMaNV.SelectedValue != null ? (int?)Convert.ToInt32(cbMaNV.SelectedValue) : null,
                TrangThai = chkTrangThai.GetItemChecked(0) // giả sử "Đang hoạt động" ở vị trí 0
            };

            if (nd.MaNV == null)
            {
                MessageBox.Show("Mã nhân viên không hợp lệ.");
                return;
            }

            if (NguoiDungDAL.Add(nd))
            {
                MessageBox.Show("Thêm người dùng thành công.");
                LoadDataNguoiDung();
            }
            else
            {
                MessageBox.Show("Thêm thất bại.");
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dgvNguoiDung.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn dòng cần sửa.");
                return;
            }

            string maNguoiDung = dgvNguoiDung.SelectedRows[0].Cells["MaNguoiDung"].Value.ToString();

            if (string.IsNullOrWhiteSpace(cbMaNV.Text) || !int.TryParse(cbMaNV.Text, out int maNV))
            {
                MessageBox.Show("Mã nhân viên không hợp lệ.");
                return;
            }

            NguoiDungDTO nd = new NguoiDungDTO()
            {
                MaNguoiDung = maNguoiDung,
                TenDangNhap = txtTenDangNhap.Text.Trim(),
                HoTen = txtHoTen.Text.Trim(),
                Email = string.IsNullOrWhiteSpace(txtEmail.Text) ? null : txtEmail.Text.Trim(),
                DienThoai = string.IsNullOrWhiteSpace(txtDienThoai.Text) ? null : txtDienThoai.Text.Trim(),
                VaiTro = cbVaiTro.Text.Trim(),
                MaNV = cbMaNV.SelectedValue != null ? Convert.ToInt32(cbMaNV.SelectedValue) : 0,
                TrangThai = chkTrangThai.GetItemChecked(0)
            };

            if (NguoiDungDAL.Update(nd))
            {
                MessageBox.Show("Cập nhật thành công.");
                LoadDataNguoiDung();
            }
            else
            {
                MessageBox.Show("Cập nhật thất bại.");
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvNguoiDung.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn dòng cần xóa.");
                return;
            }

            string maNguoiDung = dgvNguoiDung.SelectedRows[0].Cells["MaNguoiDung"].Value.ToString();

            DialogResult dr = MessageBox.Show("Bạn có chắc chắn muốn xóa người dùng này?", "Xác nhận", MessageBoxButtons.YesNo);
            if (dr == DialogResult.Yes)
            {
                if (NguoiDungDAL.Delete(maNguoiDung))
                {
                    MessageBox.Show("Xóa thành công.");
                    LoadDataNguoiDung();
                }
                else
                {
                    MessageBox.Show("Xóa thất bại.");
                }
            }
        }

        private void dgvNguoiDung_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

                if (e.ColumnIndex == dgvNguoiDung.Columns["STT"].Index)
                {
                    e.Value = e.RowIndex + 1;
                    e.FormattingApplied = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi CellFormatting: " + ex.Message);
            }
        }

        private void dgvNguoiDung_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvNguoiDung.SelectedRows.Count == 0) return;

            var row = dgvNguoiDung.SelectedRows[0];
            txtTenDangNhap.Text = row.Cells["TenDangNhap"].Value?.ToString();
            txtHoTen.Text = row.Cells["HoTen"].Value?.ToString();
            txtEmail.Text = row.Cells["Email"].Value?.ToString();
            txtDienThoai.Text = row.Cells["DienThoai"].Value?.ToString();
            cbVaiTro.Text = row.Cells["VaiTro"].Value?.ToString();
            cbMaNV.Text = row.Cells["MaNV"].Value?.ToString();

            // Cập nhật trạng thái
            bool trangThai = Convert.ToBoolean(row.Cells["TrangThai"].Value);
            chkTrangThai.SetItemChecked(0, trangThai);
            chkTrangThai.SetItemChecked(1, !trangThai);
        }

        private void chkTrangThai_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            for (int i = 0; i < chkTrangThai.Items.Count; i++)
            {
                if (i != e.Index)
                {
                    chkTrangThai.SetItemChecked(i, false);
                }
            }
        }

        private void btnDong_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
