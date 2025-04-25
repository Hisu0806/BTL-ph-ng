using System;
using System.Data;
using System.Windows.Forms;
using HRM.BLL;
using HRM.DTO;

namespace HRM.Forms
{
    public partial class frmHopDong : Form
    {
        private int maHopDong = 0;

        public frmHopDong()
        {
            InitializeComponent();

            FormatDataGridView();
            LoadComboBoxNhanVien();
            LoadBacLuong();
            LoadDataHopDong();

            // Đăng ký sự kiện
            this.Load += frmHopDong_Load;
            btnThem.Click += btnThem_Click;
            btnSua.Click += btnSua_Click;
            btnXoa.Click += btnXoa_Click;
            btnThoat.Click += btnThoat_Click;
            dgvHopDong.CellClick += dgvHopDong_CellClick;
            txtTimKiem.TextChanged += txtTimKiem_TextChanged;
            cboMaNV.SelectedIndexChanged += CboMaNV_SelectedIndexChanged;
            cboLoaiHopDong.SelectedIndexChanged += cboLoaiHopDong_SelectedIndexChanged;
        }

        private void LoadBacLuong()
        {
            try
            {
                DataTable dt = BacLuongBLL.Instance.GetList();
                if (dt != null && dt.Rows.Count > 0)
                {
                    // Tạo một DataTable mới với cột hiển thị tổng hợp
                    DataTable dtDisplay = new DataTable();
                    dtDisplay.Columns.Add("MaBacLuong", typeof(int));
                    dtDisplay.Columns.Add("ThongTinBac", typeof(string));
                    dtDisplay.Columns.Add("LuongCoBan", typeof(decimal));

                    foreach (DataRow row in dt.Rows)
                    {
                        string thongTinBac = $"{row["TenBac"]} (Hệ số: {row["HeSo"]}, Lương cơ bản: {string.Format("{0:N0}", row["LuongCoBan"])} VNĐ)";
                        dtDisplay.Rows.Add(
                            row["MaBacLuong"],
                            thongTinBac,
                            row["LuongCoBan"]
                        );
                    }

                    cboLuongTheoBienChe.DataSource = dtDisplay;
                    cboLuongTheoBienChe.DisplayMember = "ThongTinBac";
                    cboLuongTheoBienChe.ValueMember = "MaBacLuong";
                    cboLuongTheoBienChe.SelectedIndex = -1;
                    cboLuongTheoBienChe.Enabled = false;
                }
                else
                {
                    MessageBox.Show("Không có dữ liệu bậc lương", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu bậc lương: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cboLoaiHopDong_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboLoaiHopDong.Text == "Biên chế")
            {
                txtLuongTheoHopDong.Enabled = false;
                txtLuongTheoHopDong.Text = "";
                cboLuongTheoBienChe.Enabled = true;
            }
            else
            {
                txtLuongTheoHopDong.Enabled = true;
                cboLuongTheoBienChe.Enabled = false;
                cboLuongTheoBienChe.SelectedIndex = -1;
            }
        }

        private void FormatDataGridView()
        {
            dgvHopDong.AutoGenerateColumns = false;
            dgvHopDong.Columns.Clear();

            // Thêm các cột với độ rộng cố định
            dgvHopDong.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "STT",
                HeaderText = "STT",
                Width = 50,
                ReadOnly = true
            });

            dgvHopDong.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "MaHopDong",
                HeaderText = "Mã hợp đồng",
                Width = 100,
                ReadOnly = true
            });

            dgvHopDong.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "TenNV",
                HeaderText = "Họ tên",
                Width = 150,
                ReadOnly = true
            });

            dgvHopDong.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "SoHopDong",
                HeaderText = "Số hợp đồng",
                Width = 120,
                ReadOnly = true
            });

            dgvHopDong.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "LoaiHopDong",
                HeaderText = "Loại hợp đồng",
                Width = 120,
                ReadOnly = true
            });

            dgvHopDong.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "NgayKy",
                HeaderText = "Ngày ký",
                Width = 100,
                ReadOnly = true
            });

            dgvHopDong.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "NgayBatDau",
                HeaderText = "Ngày bắt đầu",
                Width = 100,
                ReadOnly = true
            });

            dgvHopDong.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "NgayKetThuc",
                HeaderText = "Ngày kết thúc",
                Width = 100,
                ReadOnly = true
            });

            dgvHopDong.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "LuongTheoHopDong",
                HeaderText = "Lương theo hợp đồng",
                Width = 150,
                ReadOnly = true
            });

            dgvHopDong.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "TrangThai",
                HeaderText = "Trạng thái",
                Width = 100,
                ReadOnly = true
            });

            // Cấu hình DataGridView
            dgvHopDong.AllowUserToAddRows = false;
            dgvHopDong.AllowUserToDeleteRows = false;
            dgvHopDong.AllowUserToResizeRows = false;
            dgvHopDong.MultiSelect = false;
            dgvHopDong.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvHopDong.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            dgvHopDong.ScrollBars = ScrollBars.Both;
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

        private void LoadThongTinNhanVien(string maNV)
        {
            try
            {
                var nhanVien = NhanVienBLL.GetByID(Convert.ToInt32(maNV));
                if (nhanVien != null)
                {
                    cboLoaiHopDong.Text = nhanVien.LoaiNhanVien;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải thông tin nhân viên: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadDataHopDong()
        {
            try
            {
                dgvHopDong.Rows.Clear();
                DataTable dt = HopDongBLL.GetList();

                int stt = 1;
                foreach (DataRow row in dt.Rows)
                {
                    dgvHopDong.Rows.Add(
                        stt++,
                        row["MaHopDong"],
                        row["TenNV"],
                        row["SoHopDong"],
                        row["LoaiHopDong"],
                        Convert.ToDateTime(row["NgayKy"]).ToString("dd/MM/yyyy"),
                        Convert.ToDateTime(row["NgayBatDau"]).ToString("dd/MM/yyyy"),
                        row["NgayKetThuc"] == DBNull.Value ? "" : Convert.ToDateTime(row["NgayKetThuc"]).ToString("dd/MM/yyyy"),
                        row["LuongTheoHopDong"],
                        row["TrangThai"]
                    );
                }
                lblTongSo.Text = $"Tổng số: {dt.Rows.Count} hợp đồng";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ResetForm()
        {
            maHopDong = 0;
            txtSoHopDong.Clear();
            cboMaNV.SelectedIndex = -1;
            cboLoaiHopDong.Text = "";
            dtpNgayKy.Value = DateTime.Now;
            dtpNgayBatDau.Value = DateTime.Now;
            dtpNgayKetThuc.Value = DateTime.Now;
            txtLuongTheoHopDong.Clear();
            cboLuongTheoBienChe.SelectedIndex = -1;
            txtTrangThai.Clear();
            dgvHopDong.ClearSelection();
        }

        private void dgvHopDong_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvHopDong.Rows[e.RowIndex];
                maHopDong = Convert.ToInt32(row.Cells["MaHopDong"].Value);
                txtSoHopDong.Text = row.Cells["SoHopDong"].Value.ToString();
                cboMaNV.Text = row.Cells["TenNV"].Value.ToString();
                cboLoaiHopDong.Text = row.Cells["LoaiHopDong"].Value.ToString();

                string ngayKyStr = row.Cells["NgayKy"].Value.ToString();
                if (!string.IsNullOrEmpty(ngayKyStr))
                {
                    dtpNgayKy.Value = DateTime.ParseExact(ngayKyStr, "dd/MM/yyyy", null);
                }

                string ngayBatDauStr = row.Cells["NgayBatDau"].Value.ToString();
                if (!string.IsNullOrEmpty(ngayBatDauStr))
                {
                    dtpNgayBatDau.Value = DateTime.ParseExact(ngayBatDauStr, "dd/MM/yyyy", null);
                }

                string ngayKetThucStr = row.Cells["NgayKetThuc"].Value.ToString();
                if (!string.IsNullOrEmpty(ngayKetThucStr))
                {
                    dtpNgayKetThuc.Value = DateTime.ParseExact(ngayKetThucStr, "dd/MM/yyyy", null);
                }

                if (cboLoaiHopDong.Text == "Biên chế")
                {
                    txtLuongTheoHopDong.Text = "";
                    txtLuongTheoHopDong.Enabled = false;
                    cboLuongTheoBienChe.Enabled = true;

                    // Tìm bậc lương dựa trên mức lương cơ bản
                    decimal luongHopDong = Convert.ToDecimal(row.Cells["LuongTheoHopDong"].Value);
                    DataTable dtBacLuong = BacLuongBLL.Instance.GetList();
                    foreach (DataRow bacLuongRow in dtBacLuong.Rows)
                    {
                        decimal luongCoBan = Convert.ToDecimal(bacLuongRow["LuongCoBan"]);
                        decimal heSo = Convert.ToDecimal(bacLuongRow["HeSo"]);
                        if (Math.Abs(luongCoBan * heSo - luongHopDong) < 0.01m)
                        {
                            cboLuongTheoBienChe.SelectedValue = bacLuongRow["MaBacLuong"];
                            break;
                        }
                    }
                }
                else
                {
                    txtLuongTheoHopDong.Enabled = true;
                    cboLuongTheoBienChe.Enabled = false;
                    txtLuongTheoHopDong.Text = row.Cells["LuongTheoHopDong"].Value.ToString();
                    cboLuongTheoBienChe.SelectedIndex = -1;
                }

                txtTrangThai.Text = row.Cells["TrangThai"].Value.ToString();
            }
        }

        private void CboMaNV_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboMaNV.SelectedValue != null)
            {
                LoadThongTinNhanVien(cboMaNV.SelectedValue.ToString());
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(cboMaNV.Text))
                {
                    MessageBox.Show("Vui lòng chọn nhân viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cboMaNV.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtSoHopDong.Text))
                {
                    MessageBox.Show("Vui lòng nhập số hợp đồng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtSoHopDong.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(cboLoaiHopDong.Text))
                {
                    MessageBox.Show("Vui lòng chọn loại hợp đồng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cboLoaiHopDong.Focus();
                    return;
                }

                if (dtpNgayKy.Value > dtpNgayBatDau.Value)
                {
                    MessageBox.Show("Ngày ký không được lớn hơn ngày bắt đầu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    dtpNgayKy.Focus();
                    return;
                }

                if (dtpNgayBatDau.Value > dtpNgayKetThuc.Value)
                {
                    MessageBox.Show("Ngày bắt đầu không được lớn hơn ngày kết thúc", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    dtpNgayBatDau.Focus();
                    return;
                }

                // Kiểm tra và lấy giá trị lương dựa vào loại hợp đồng
                decimal luongTheoHopDong = 0;
                if (cboLoaiHopDong.Text == "Biên chế")
                {
                    if (cboLuongTheoBienChe.SelectedValue == null)
                    {
                        MessageBox.Show("Vui lòng chọn bậc lương", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        cboLuongTheoBienChe.Focus();
                        return;
                    }
                    var bacLuong = BacLuongBLL.Instance.GetByID(Convert.ToInt32(cboLuongTheoBienChe.SelectedValue));
                    luongTheoHopDong = bacLuong.LuongCoBan * (decimal)bacLuong.HeSo;
                }
                else
                {
                    if (string.IsNullOrEmpty(txtLuongTheoHopDong.Text))
                    {
                        MessageBox.Show("Vui lòng nhập lương theo hợp đồng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtLuongTheoHopDong.Focus();
                        return;
                    }
                    luongTheoHopDong = Convert.ToDecimal(txtLuongTheoHopDong.Text);
                }

                HopDongDTO hd = new HopDongDTO();
                hd.MaNV = Convert.ToInt32(cboMaNV.SelectedValue);
                hd.SoHopDong = txtSoHopDong.Text;
                hd.LoaiHopDong = cboLoaiHopDong.Text;
                hd.NgayKy = dtpNgayKy.Value;
                hd.NgayBatDau = dtpNgayBatDau.Value;
                hd.NgayKetThuc = dtpNgayKetThuc.Value;
                hd.LuongTheoHopDong = luongTheoHopDong;
                hd.TrangThai = "Đang hiệu lực";
                hd.NoiDung = txtNoiDung.Text;

                if (HopDongBLL.Add(hd) > 0)
                {
                    MessageBox.Show("Thêm hợp đồng thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDataHopDong();
                    ResetForm();
                }
                else
                {
                    MessageBox.Show("Thêm hợp đồng thất bại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(cboMaNV.Text))
                {
                    MessageBox.Show("Vui lòng chọn nhân viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cboMaNV.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtSoHopDong.Text))
                {
                    MessageBox.Show("Vui lòng nhập số hợp đồng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtSoHopDong.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(cboLoaiHopDong.Text))
                {
                    MessageBox.Show("Vui lòng chọn loại hợp đồng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cboLoaiHopDong.Focus();
                    return;
                }

                if (dtpNgayKy.Value > dtpNgayBatDau.Value)
                {
                    MessageBox.Show("Ngày ký không được lớn hơn ngày bắt đầu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    dtpNgayKy.Focus();
                    return;
                }

                if (dtpNgayBatDau.Value > dtpNgayKetThuc.Value)
                {
                    MessageBox.Show("Ngày bắt đầu không được lớn hơn ngày kết thúc", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    dtpNgayBatDau.Focus();
                    return;
                }

                // Kiểm tra và lấy giá trị lương dựa vào loại hợp đồng
                decimal luongTheoHopDong = 0;
                if (cboLoaiHopDong.Text == "Biên chế")
                {
                    if (cboLuongTheoBienChe.SelectedValue == null)
                    {
                        MessageBox.Show("Vui lòng chọn bậc lương", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        cboLuongTheoBienChe.Focus();
                        return;
                    }
                    var bacLuong = BacLuongBLL.Instance.GetByID(Convert.ToInt32(cboLuongTheoBienChe.SelectedValue));
                    luongTheoHopDong = bacLuong.LuongCoBan * (decimal)bacLuong.HeSo;
                }
                else
                {
                    if (string.IsNullOrEmpty(txtLuongTheoHopDong.Text))
                    {
                        MessageBox.Show("Vui lòng nhập lương theo hợp đồng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtLuongTheoHopDong.Focus();
                        return;
                    }
                    luongTheoHopDong = Convert.ToDecimal(txtLuongTheoHopDong.Text);
                }

                HopDongDTO hd = new HopDongDTO();
                hd.MaHopDong = maHopDong;
                hd.MaNV = Convert.ToInt32(cboMaNV.SelectedValue);
                hd.SoHopDong = txtSoHopDong.Text;
                hd.LoaiHopDong = cboLoaiHopDong.Text;
                hd.NgayKy = dtpNgayKy.Value;
                hd.NgayBatDau = dtpNgayBatDau.Value;
                hd.NgayKetThuc = dtpNgayKetThuc.Value;
                hd.LuongTheoHopDong = luongTheoHopDong;
                hd.TrangThai = "Đang hiệu lực";
                hd.NoiDung = txtNoiDung.Text;

                if (HopDongBLL.Update(hd))
                {
                    MessageBox.Show("Cập nhật hợp đồng thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDataHopDong();
                    ResetForm();
                }
                else
                {
                    MessageBox.Show("Cập nhật hợp đồng thất bại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                if (maHopDong == 0)
                {
                    MessageBox.Show("Vui lòng chọn hợp đồng cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (MessageBox.Show("Bạn có chắc chắn muốn xóa hợp đồng này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (HopDongBLL.Delete(maHopDong))
                    {
                        MessageBox.Show("Xóa hợp đồng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadDataHopDong();
                        ResetForm();
                    }
                    else
                    {
                        MessageBox.Show("Xóa hợp đồng thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
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

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string keyword = txtTimKiem.Text.Trim();
                if (string.IsNullOrEmpty(keyword))
                {
                    LoadDataHopDong();
                    return;
                }

                dgvHopDong.Rows.Clear();
                DataTable dt = HopDongBLL.GetList();
                int stt = 1;
                foreach (DataRow row in dt.Rows)
                {
                    if (row["TenNV"].ToString().ToLower().Contains(keyword.ToLower()) ||
                        row["SoHopDong"].ToString().ToLower().Contains(keyword.ToLower()))
                    {
                        dgvHopDong.Rows.Add(
                            stt++,
                            row["MaHopDong"],
                            row["TenNV"],
                            row["SoHopDong"],
                            row["LoaiHopDong"],
                            Convert.ToDateTime(row["NgayKy"]).ToString("dd/MM/yyyy"),
                            Convert.ToDateTime(row["NgayBatDau"]).ToString("dd/MM/yyyy"),
                            row["NgayKetThuc"] == DBNull.Value ? "" : Convert.ToDateTime(row["NgayKetThuc"]).ToString("dd/MM/yyyy"),
                            row["LuongTheoHopDong"],
                            row["TrangThai"]
                        );
                    }
                }
                lblTongSo.Text = $"Tổng số: {dgvHopDong.Rows.Count} hợp đồng";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tìm kiếm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmHopDong_Load(object sender, EventArgs e)
        {
            LoadDataHopDong();
        }
    }
}