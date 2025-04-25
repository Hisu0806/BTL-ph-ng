using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
using HRM.BLL;
using HRM.DAL;
using HRM.DTO;

namespace HRM.GUI
{
    public partial class frmTangLuong : Form
    {
        private int maTangLuong = 0;
        private Label lblMaBacLuongCu;

        public frmTangLuong()
        {
            InitializeComponent();

            FormatDataGridView();
            LoadComboBoxNhanVien();
            LoadBacLuong();
            LoadDataTangLuong();

            // Đăng ký sự kiện
            this.Load += frmTangLuong_Load;
            btnThem.Click += btnThem_Click;
            btnSua.Click += btnSua_Click;
            btnXoa.Click += btnXoa_Click;
            btnThoat.Click += btnThoat_Click;
            dgvTangLuong.CellClick += dgvTangLuong_CellClick;
            txtTimKiem.TextChanged += txtTimKiem_TextChanged;

            // Đảm bảo sự kiện được đăng ký sau khi ComboBox đã được khởi tạo
            cboNhanVien.SelectedIndexChanged -= CboMaNV_SelectedIndexChanged; // Xóa đăng ký cũ nếu có
            cboNhanVien.SelectedIndexChanged += CboMaNV_SelectedIndexChanged; // Đăng ký mới
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
                        string thongTinBac = $"{row["TenBac"]} (Hệ số: {row["HeSo"]}, Lương: {string.Format("{0:N0}", row["LuongCoBan"])} VNĐ)";
                        dtDisplay.Rows.Add(
                            row["MaBacLuong"],
                            thongTinBac,
                            row["LuongCoBan"]
                        );
                    }

                    // Cấu hình cho cboBacLuongCu
                    cboBacLuongCu.DataSource = dtDisplay.Copy();
                    cboBacLuongCu.DisplayMember = "ThongTinBac";
                    cboBacLuongCu.ValueMember = "MaBacLuong";
                    cboBacLuongCu.SelectedIndex = -1;

                    // Cấu hình cho cboBacLuongMoi
                    cboBacLuongMoi.DataSource = dtDisplay;
                    cboBacLuongMoi.DisplayMember = "ThongTinBac";
                    cboBacLuongMoi.ValueMember = "MaBacLuong";
                    cboBacLuongMoi.SelectedIndex = -1;
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

        private void FormatDataGridView()
        {
            dgvTangLuong.AutoGenerateColumns = false;
            dgvTangLuong.Columns.Clear();

            // Thêm các cột với độ rộng cố định
            dgvTangLuong.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "STT",
                HeaderText = "STT",
                Width = 50,
                ReadOnly = true
            });

            dgvTangLuong.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "MaTangLuong",
                HeaderText = "Mã tăng lương",
                Width = 100,
                ReadOnly = true
            });

            dgvTangLuong.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "HoTen",
                HeaderText = "Họ tên",
                Width = 150,
                ReadOnly = true
            });

            dgvTangLuong.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "TenBacCu",
                HeaderText = "Bậc lương cũ",
                Width = 120,
                ReadOnly = true
            });

            dgvTangLuong.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "LuongCoBanCu",
                HeaderText = "Lương cũ",
                Width = 120,
                ReadOnly = true
            });

            dgvTangLuong.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "TenBacMoi",
                HeaderText = "Bậc lương mới",
                Width = 120,
                ReadOnly = true
            });

            dgvTangLuong.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "LuongCoBanMoi",
                HeaderText = "Lương mới",
                Width = 120,
                ReadOnly = true
            });

            dgvTangLuong.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "NgayTangLuong",
                HeaderText = "Ngày tăng lương",
                Width = 120,
                ReadOnly = true
            });

            dgvTangLuong.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "LyDo",
                HeaderText = "Lý do",
                Width = 200,
                ReadOnly = true
            });

            dgvTangLuong.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "GhiChu",
                HeaderText = "Ghi chú",
                Width = 200,
                ReadOnly = true
            });

            // Cấu hình DataGridView
            dgvTangLuong.AllowUserToAddRows = false;
            dgvTangLuong.AllowUserToDeleteRows = false;
            dgvTangLuong.AllowUserToResizeRows = false;
            dgvTangLuong.MultiSelect = false;
            dgvTangLuong.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvTangLuong.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            dgvTangLuong.ScrollBars = ScrollBars.Both;
        }

        private void LoadComboBoxNhanVien()
        {
            try
            {
                DataTable dt = TangLuongDAL.GetNhanVienBienChe();
                cboNhanVien.DataSource = dt;
                cboNhanVien.DisplayMember = "HoTen";
                cboNhanVien.ValueMember = "MaNV";
                cboNhanVien.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách nhân viên: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadBacLuongHienTai(int maNV)
        {
            try
            {
                string query = @"SELECT bl.MaBacLuong, bl.TenBac, bl.HeSo, bl.LuongCoBan
                        FROM HopDong hd
                        INNER JOIN BacLuong bl ON hd.LuongTheoHopDong = bl.MaBacLuong
                        WHERE hd.MaNV = @MaNV 
                        AND hd.TrangThai = N'Đang hiệu lực'
                        AND (hd.NgayKetThuc IS NULL OR hd.NgayKetThuc > GETDATE())";

                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@MaNV", maNV)
                };

                DataTable dt = DBConnection.ExecuteQuery(query, parameters);
                if (dt != null && dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    cboBacLuongCu.SelectedValue = row["MaBacLuong"];
                }
                else
                {
                    cboBacLuongCu.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải bậc lương hiện tại: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void LoadDataTangLuong()
        {
            try
            {
                dgvTangLuong.Rows.Clear();
                DataTable dt = TangLuongBLL.GetList();

                int stt = 1;
                foreach (DataRow row in dt.Rows)
                {
                    dgvTangLuong.Rows.Add(
                        stt++,
                        row["MaTangLuong"],
                        row["HoTen"],
                        row["TenBacCu"],
                        string.Format("{0:N0}", row["LuongCu"]),
                        row["TenBacMoi"],
                        string.Format("{0:N0}", row["LuongMoi"]),
                        Convert.ToDateTime(row["NgayTangLuong"]).ToString("dd/MM/yyyy"),
                        row["LyDo"],
                        row["GhiChu"]
                    );
                }
                lblTongSo.Text = $"Tổng số: {dt.Rows.Count} lần tăng lương";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ResetForm()
        {
            maTangLuong = 0;
            cboNhanVien.SelectedIndex = -1;
            cboBacLuongCu.SelectedIndex = -1;
            lblMaBacLuongCu.Text = "";
            cboBacLuongMoi.SelectedIndex = -1;
            dtpNgayTangLuong.Value = DateTime.Now;
            txtLyDo.Clear();
            txtGhiChu.Clear();
            dgvTangLuong.ClearSelection();
        }

        private void dgvTangLuong_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && e.RowIndex < dgvTangLuong.Rows.Count)
                {
                    DataGridViewRow row = dgvTangLuong.Rows[e.RowIndex];
                    if (row.Cells["MaTangLuong"].Value != null)
                    {
                        maTangLuong = Convert.ToInt32(row.Cells["MaTangLuong"].Value);
                        TangLuongDTO tangLuong = TangLuongBLL.GetByID(maTangLuong);

                        if (tangLuong != null)
                        {
                            // Tìm và chọn nhân viên trong combobox
                            foreach (DataRowView item in cboNhanVien.Items)
                            {
                                if (item["MaNV"].ToString() == tangLuong.MaNV.ToString())
                                {
                                    cboNhanVien.SelectedItem = item;
                                    break;
                                }
                            }

                            // Chọn bậc lương cũ và mới
                            cboBacLuongCu.SelectedValue = tangLuong.MaBacLuongCu;
                            cboBacLuongMoi.SelectedValue = tangLuong.MaBacLuongMoi;

                            // Cập nhật các thông tin khác
                            dtpNgayTangLuong.Value = tangLuong.NgayTangLuong;
                            txtLyDo.Text = tangLuong.LyDo ?? "";
                            txtGhiChu.Text = tangLuong.GhiChu ?? "";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi chọn dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CboMaNV_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboNhanVien.SelectedValue != null)
            {
                int maNV = Convert.ToInt32(cboNhanVien.SelectedValue);
                Console.WriteLine($"Selected MaNV: {maNV}"); // Debug line
                LoadBacLuongHienTai(maNV);
            }
            else
            {
                cboBacLuongCu.SelectedIndex = -1;
                lblMaBacLuongCu.Text = "";
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(cboNhanVien.Text))
                {
                    MessageBox.Show("Vui lòng chọn nhân viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cboNhanVien.Focus();
                    return;
                }

                if (cboBacLuongCu.SelectedValue == null)
                {
                    MessageBox.Show("Vui lòng chọn bậc lương cũ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cboBacLuongCu.Focus();
                    return;
                }

                if (cboBacLuongMoi.SelectedValue == null)
                {
                    MessageBox.Show("Vui lòng chọn bậc lương mới", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cboBacLuongMoi.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtLyDo.Text))
                {
                    MessageBox.Show("Vui lòng nhập lý do tăng lương", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtLyDo.Focus();
                    return;
                }

                TangLuongDTO tl = new TangLuongDTO
                {
                    MaNV = Convert.ToInt32(cboNhanVien.SelectedValue),
                    MaBacLuongCu = Convert.ToInt32(cboBacLuongCu.SelectedValue),
                    MaBacLuongMoi = Convert.ToInt32(cboBacLuongMoi.SelectedValue),
                    NgayTangLuong = dtpNgayTangLuong.Value,
                    LyDo = txtLyDo.Text,
                    GhiChu = txtGhiChu.Text
                };

                if (TangLuongBLL.Add(tl) > 0)
                {
                    MessageBox.Show("Thêm thông tin tăng lương thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDataTangLuong();
                    ResetForm();
                }
                else
                {
                    MessageBox.Show("Thêm thông tin tăng lương thất bại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                if (maTangLuong == 0)
                {
                    MessageBox.Show("Vui lòng chọn thông tin tăng lương cần sửa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrEmpty(cboNhanVien.Text))
                {
                    MessageBox.Show("Vui lòng chọn nhân viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cboNhanVien.Focus();
                    return;
                }

                if (cboBacLuongCu.SelectedValue == null)
                {
                    MessageBox.Show("Vui lòng chọn bậc lương cũ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cboBacLuongCu.Focus();
                    return;
                }

                if (cboBacLuongMoi.SelectedValue == null)
                {
                    MessageBox.Show("Vui lòng chọn bậc lương mới", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cboBacLuongMoi.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtLyDo.Text))
                {
                    MessageBox.Show("Vui lòng nhập lý do tăng lương", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtLyDo.Focus();
                    return;
                }

                TangLuongDTO tl = new TangLuongDTO
                {
                    MaTangLuong = maTangLuong,
                    MaNV = Convert.ToInt32(cboNhanVien.SelectedValue),
                    MaBacLuongCu = Convert.ToInt32(cboBacLuongCu.SelectedValue),
                    MaBacLuongMoi = Convert.ToInt32(cboBacLuongMoi.SelectedValue),
                    NgayTangLuong = dtpNgayTangLuong.Value,
                    LyDo = txtLyDo.Text,
                    GhiChu = txtGhiChu.Text
                };

                if (TangLuongBLL.Update(tl))
                {
                    MessageBox.Show("Cập nhật thông tin tăng lương thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDataTangLuong();
                    ResetForm();
                }
                else
                {
                    MessageBox.Show("Cập nhật thông tin tăng lương thất bại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                if (maTangLuong == 0)
                {
                    MessageBox.Show("Vui lòng chọn thông tin tăng lương cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (MessageBox.Show("Bạn có chắc chắn muốn xóa thông tin tăng lương này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (TangLuongBLL.Delete(maTangLuong))
                    {
                        MessageBox.Show("Xóa thông tin tăng lương thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadDataTangLuong();
                        ResetForm();
                    }
                    else
                    {
                        MessageBox.Show("Xóa thông tin tăng lương thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    LoadDataTangLuong();
                    return;
                }

                dgvTangLuong.Rows.Clear();
                DataTable dt = TangLuongBLL.GetList();
                int stt = 1;
                foreach (DataRow row in dt.Rows)
                {
                    if (row["HoTen"].ToString().ToLower().Contains(keyword.ToLower()))
                    {
                        dgvTangLuong.Rows.Add(
                            stt++,
                            row["MaTangLuong"],
                            row["HoTen"],
                            row["TenBacCu"],
                            string.Format("{0:N0}", row["LuongCu"]),
                            row["TenBacMoi"],
                            string.Format("{0:N0}", row["LuongMoi"]),
                            Convert.ToDateTime(row["NgayTangLuong"]).ToString("dd/MM/yyyy"),
                            row["LyDo"]
                        );
                    }
                }
                lblTongSo.Text = $"Tổng số: {dgvTangLuong.Rows.Count} lần tăng lương";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tìm kiếm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmTangLuong_Load(object sender, EventArgs e)
        {
            LoadDataTangLuong();
        }
    }
}