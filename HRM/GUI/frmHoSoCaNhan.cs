using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using HRM.BLL;
using HRM.DAL;
using HRM.DTO;

namespace HRM.Forms
{
    public partial class frmHoSoCaNhan : Form
    {

        private bool isAdding = false;
        private int currentMaNV = 0;
        private string currentImagePath = "";
        private byte[] currentImageBytes = null;
        private OpenFileDialog openFileDialog1;

        public frmHoSoCaNhan()
        {
            InitializeComponent();
            LoadData();
            LoadComboBoxes();
            // Khởi tạo OpenFileDialog
            openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
            openFileDialog1.Title = "Chọn ảnh nhân viên";
        }

        private void LoadData()
        {
            try
            {
                DataTable dt = NhanVienBLL.GetList();
                if (dt != null && dt.Rows.Count > 0)
                {
                    dgvNhanVien.DataSource = dt;
                    FormatDataGridView();
                    lblTongSo.Text = $"Tổng số: {dt.Rows.Count}";
                }
                else
                {
                    dgvNhanVien.DataSource = null;
                    lblTongSo.Text = "Tổng số: 0";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormatDataGridView()
        {
            // Enable horizontal and vertical scrolling
            dgvNhanVien.ScrollBars = ScrollBars.Both;
            dgvNhanVien.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            dgvNhanVien.AllowUserToResizeColumns = true;
            dgvNhanVien.AllowUserToResizeRows = false;

            // Set column headers
            dgvNhanVien.Columns["MaNV"].HeaderText = "Mã NV";
            dgvNhanVien.Columns["HoTen"].HeaderText = "Họ tên";
            dgvNhanVien.Columns["NgaySinh"].HeaderText = "Ngày sinh";
            dgvNhanVien.Columns["GioiTinh"].HeaderText = "Giới tính";
            dgvNhanVien.Columns["SoCMND"].HeaderText = "Số CMND";
            dgvNhanVien.Columns["TrinhDoChuyenMon"].HeaderText = "Trình độ chuyên môn";
            dgvNhanVien.Columns["TrinhDoNgoaiNgu"].HeaderText = "Trình độ ngoại ngữ";
            dgvNhanVien.Columns["HoKhauThuongTru"].HeaderText = "Hộ khẩu thường trú";
            dgvNhanVien.Columns["DiaChi"].HeaderText = "Địa chỉ";
            dgvNhanVien.Columns["DanToc"].HeaderText = "Dân tộc";
            dgvNhanVien.Columns["TonGiao"].HeaderText = "Tôn giáo";
            dgvNhanVien.Columns["NgayVaoDoan"].HeaderText = "Ngày vào đoàn";
            dgvNhanVien.Columns["NgayVaoDang"].HeaderText = "Ngày vào đảng";
            dgvNhanVien.Columns["DienChinhSach"].HeaderText = "Diện chính sách";
            dgvNhanVien.Columns["MaPhong"].HeaderText = "Mã phòng";
            dgvNhanVien.Columns["MaChucDanh"].HeaderText = "Mã chức danh";
            dgvNhanVien.Columns["LoaiNhanVien"].HeaderText = "Loại nhân viên";
            dgvNhanVien.Columns["NgayVaoCongTy"].HeaderText = "Ngày vào công ty";
            dgvNhanVien.Columns["TrangThai"].HeaderText = "Trạng thái";

            // Set column widths based on content
            dgvNhanVien.Columns["MaNV"].Width = 60;
            dgvNhanVien.Columns["HoTen"].Width = 150;
            dgvNhanVien.Columns["NgaySinh"].Width = 100;
            dgvNhanVien.Columns["GioiTinh"].Width = 80;
            dgvNhanVien.Columns["SoCMND"].Width = 120;
            dgvNhanVien.Columns["TrinhDoChuyenMon"].Width = 150;
            dgvNhanVien.Columns["TrinhDoNgoaiNgu"].Width = 150;
            dgvNhanVien.Columns["HoKhauThuongTru"].Width = 200;
            dgvNhanVien.Columns["DiaChi"].Width = 200;
            dgvNhanVien.Columns["DanToc"].Width = 100;
            dgvNhanVien.Columns["TonGiao"].Width = 100;
            dgvNhanVien.Columns["NgayVaoDoan"].Width = 100;
            dgvNhanVien.Columns["NgayVaoDang"].Width = 100;
            dgvNhanVien.Columns["DienChinhSach"].Width = 120;
            dgvNhanVien.Columns["MaPhong"].Width = 80;
            dgvNhanVien.Columns["MaChucDanh"].Width = 80;
            dgvNhanVien.Columns["LoaiNhanVien"].Width = 120;
            dgvNhanVien.Columns["NgayVaoCongTy"].Width = 100;
            dgvNhanVien.Columns["TrangThai"].Width = 120;

            // Hide the image column as it's not needed in the grid
            dgvNhanVien.Columns["HinhAnh"].Visible = false;

            // Set row height
            dgvNhanVien.RowTemplate.Height = 25;

            // Enable column header sorting
            foreach (DataGridViewColumn column in dgvNhanVien.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.Automatic;
            }
        }

        private void LoadComboBoxes()
        {
            try
            {
                // Load phòng ban
                DataTable dtPhongBan = PhongBanBLL.GetList();
                if (dtPhongBan != null && dtPhongBan.Rows.Count > 0)
                {
                    cboPhongBan.DataSource = dtPhongBan;
                    cboPhongBan.DisplayMember = "TenPhong";
                    cboPhongBan.ValueMember = "MaPhong";
                }

                // Load chức danh
                DataTable dtChucDanh = ChucDanhBLL.GetList();
                if (dtChucDanh != null && dtChucDanh.Rows.Count > 0)
                {
                    cboChucDanh.DataSource = dtChucDanh;
                    cboChucDanh.DisplayMember = "TenChucDanh";
                    cboChucDanh.ValueMember = "MaChucDanh";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearInputs()
        {
            txtHoTen.Clear();
            dtpNgaySinh.Value = DateTime.Now;
            cboGioiTinh.SelectedIndex = -1;
            txtSoCMND.Clear();
            txtTrinhDoChuyenMon.Clear();
            txtTrinhDoNgoaiNgu.Clear();
            txtHoKhauThuongTru.Clear();
            txtDiaChi.Clear();
            txtDanToc.Clear();
            txtTonGiao.Clear();
            dtpNgayVaoDoan.Value = DateTime.Now;
            dtpNgayVaoDang.Value = DateTime.Now;
            txtDienChinhSach.Clear();
            cboPhongBan.SelectedIndex = -1;
            cboChucDanh.SelectedIndex = -1;
            cboLoaiNhanVien.SelectedIndex = -1;
            dtpNgayVaoCongTy.Value = DateTime.Now;
            txtTrangThai.Clear();
            picHinhAnh.Image = null;
            currentImagePath = "";
            currentImageBytes = null;
            currentMaNV = 0;
            isAdding = false;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra các trường bắt buộc
                if (string.IsNullOrEmpty(txtHoTen.Text) || string.IsNullOrEmpty(txtSoCMND.Text) ||
                    cboPhongBan.SelectedValue == null || cboChucDanh.SelectedValue == null)
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin bắt buộc!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Tạo đối tượng nhân viên mới
                NhanVien nv = new NhanVien
                {
                    HoTen = txtHoTen.Text,
                    NgaySinh = dtpNgaySinh.Value,
                    GioiTinh = cboGioiTinh.SelectedItem.ToString(),
                    SoCMND = txtSoCMND.Text,
                    TrinhDoChuyenMon = txtTrinhDoChuyenMon.Text,
                    TrinhDoNgoaiNgu = txtTrinhDoNgoaiNgu.Text,
                    HoKhauThuongTru = txtHoKhauThuongTru.Text,
                    DiaChi = txtDiaChi.Text,
                    DanToc = txtDanToc.Text,
                    TonGiao = txtTonGiao.Text,
                    NgayVaoDoan = dtpNgayVaoDoan.Value,
                    NgayVaoDang = dtpNgayVaoDang.Value,
                    DienChinhSach = txtDienChinhSach.Text,
                    MaPhong = Convert.ToInt32(cboPhongBan.SelectedValue),
                    MaChucDanh = Convert.ToInt32(cboChucDanh.SelectedValue),
                    LoaiNhanVien = cboLoaiNhanVien.SelectedItem.ToString(),
                    NgayVaoCongTy = dtpNgayVaoCongTy.Value,
                    TrangThai = "Đang làm việc",
                    HinhAnh = currentImageBytes
                };

                // Thêm nhân viên vào database
                int maNV = NhanVienDAL.Add(nv);
                if (maNV > 0)
                {
                    MessageBox.Show("Thêm nhân viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                    ClearInputs();
                }
                else
                {
                    MessageBox.Show("Thêm nhân viên thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm nhân viên: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                if (currentMaNV <= 0)
                {
                    MessageBox.Show("Vui lòng chọn nhân viên cần sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Kiểm tra các trường bắt buộc
                if (string.IsNullOrEmpty(txtHoTen.Text) || string.IsNullOrEmpty(txtSoCMND.Text) ||
                    cboPhongBan.SelectedValue == null || cboChucDanh.SelectedValue == null)
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin bắt buộc!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Tạo đối tượng nhân viên để cập nhật
                NhanVien nv = new NhanVien
                {
                    MaNV = currentMaNV,
                    HoTen = txtHoTen.Text,
                    NgaySinh = dtpNgaySinh.Value,
                    GioiTinh = cboGioiTinh.SelectedItem.ToString(),
                    SoCMND = txtSoCMND.Text,
                    TrinhDoChuyenMon = txtTrinhDoChuyenMon.Text,
                    TrinhDoNgoaiNgu = txtTrinhDoNgoaiNgu.Text,
                    HoKhauThuongTru = txtHoKhauThuongTru.Text,
                    DiaChi = txtDiaChi.Text,
                    DanToc = txtDanToc.Text,
                    TonGiao = txtTonGiao.Text,
                    NgayVaoDoan = dtpNgayVaoDoan.Value,
                    NgayVaoDang = dtpNgayVaoDang.Value,
                    DienChinhSach = txtDienChinhSach.Text,
                    MaPhong = Convert.ToInt32(cboPhongBan.SelectedValue),
                    MaChucDanh = Convert.ToInt32(cboChucDanh.SelectedValue),
                    LoaiNhanVien = cboLoaiNhanVien.SelectedItem.ToString(),
                    NgayVaoCongTy = dtpNgayVaoCongTy.Value,
                    TrangThai = "Đang làm việc",
                    HinhAnh = currentImageBytes
                };

                // Cập nhật thông tin nhân viên
                if (NhanVienDAL.Update(nv))
                {
                    MessageBox.Show("Cập nhật thông tin thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                }
                else
                {
                    MessageBox.Show("Cập nhật thông tin thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật thông tin: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                if (currentMaNV <= 0)
                {
                    MessageBox.Show("Vui lòng chọn nhân viên cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (MessageBox.Show("Bạn có chắc chắn muốn xóa nhân viên này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (NhanVienBLL.Delete(currentMaNV))
                    {
                        MessageBox.Show("Xóa nhân viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadData();
                        ClearInputs();
                    }
                    else
                    {
                        MessageBox.Show("Xóa nhân viên thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    NhanVien nv = NhanVienBLL.GetByID(maNV);
                    if (nv != null)
                    {
                        DataTable dt = new DataTable();
                        dt.Columns.Add("MaNV", typeof(int));
                        dt.Columns.Add("HoTen", typeof(string));
                        dt.Columns.Add("NgaySinh", typeof(DateTime));
                        dt.Columns.Add("GioiTinh", typeof(string));
                        dt.Columns.Add("SoCMND", typeof(string));
                        dt.Columns.Add("TrinhDoChuyenMon", typeof(string));
                        dt.Columns.Add("TrinhDoNgoaiNgu", typeof(string));
                        dt.Columns.Add("HoKhauThuongTru", typeof(string));
                        dt.Columns.Add("DiaChi", typeof(string));
                        dt.Columns.Add("DanToc", typeof(string));
                        dt.Columns.Add("TonGiao", typeof(string));
                        dt.Columns.Add("NgayVaoDoan", typeof(DateTime));
                        dt.Columns.Add("NgayVaoDang", typeof(DateTime));
                        dt.Columns.Add("DienChinhSach", typeof(string));
                        dt.Columns.Add("MaPhong", typeof(int));
                        dt.Columns.Add("MaChucDanh", typeof(int));
                        dt.Columns.Add("LoaiNhanVien", typeof(string));
                        dt.Columns.Add("NgayVaoCongTy", typeof(DateTime));
                        dt.Columns.Add("TrangThai", typeof(string));
                        dt.Columns.Add("HinhAnh", typeof(byte[]));

                        dt.Rows.Add(
                            nv.MaNV,
                            nv.HoTen,
                            nv.NgaySinh,
                            nv.GioiTinh,
                            nv.SoCMND,
                            nv.TrinhDoChuyenMon,
                            nv.TrinhDoNgoaiNgu,
                            nv.HoKhauThuongTru,
                            nv.DiaChi,
                            nv.DanToc,
                            nv.TonGiao,
                            nv.NgayVaoDoan,
                            nv.NgayVaoDang,
                            nv.DienChinhSach,
                            nv.MaPhong,
                            nv.MaChucDanh,
                            nv.LoaiNhanVien,
                            nv.NgayVaoCongTy,
                            nv.TrangThai,
                            nv.HinhAnh
                        );

                        dgvNhanVien.DataSource = dt;
                        lblTongSo.Text = $"Tổng số: 1";
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy nhân viên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void btnChonHinh_Click(object sender, EventArgs e)
        {
            try
            {
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    // Load ảnh vào PictureBox
                    picHinhAnh.Image = Image.FromFile(openFileDialog1.FileName);

                    // Chuyển ảnh từ PictureBox thành mảng byte
                    byte[] imageBytes;
                    using (MemoryStream ms = new MemoryStream())
                    {
                        picHinhAnh.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                        imageBytes = ms.ToArray();
                    }

                    // Lưu đường dẫn và mảng byte
                    currentImagePath = openFileDialog1.FileName;
                    currentImageBytes = imageBytes;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi chọn ảnh: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvNhanVien_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvNhanVien.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dgvNhanVien.SelectedRows[0];
                currentMaNV = Convert.ToInt32(row.Cells["MaNV"].Value);

                // Cập nhật các control với dữ liệu từ dòng được chọn
                txtHoTen.Text = row.Cells["HoTen"].Value.ToString();
                dtpNgaySinh.Value = Convert.ToDateTime(row.Cells["NgaySinh"].Value);
                cboGioiTinh.SelectedItem = row.Cells["GioiTinh"].Value.ToString();
                txtSoCMND.Text = row.Cells["SoCMND"].Value.ToString();
                txtTrinhDoChuyenMon.Text = row.Cells["TrinhDoChuyenMon"].Value?.ToString() ?? "";
                txtTrinhDoNgoaiNgu.Text = row.Cells["TrinhDoNgoaiNgu"].Value?.ToString() ?? "";
                txtHoKhauThuongTru.Text = row.Cells["HoKhauThuongTru"].Value?.ToString() ?? "";
                txtDiaChi.Text = row.Cells["DiaChi"].Value?.ToString() ?? "";
                txtDanToc.Text = row.Cells["DanToc"].Value?.ToString() ?? "";
                txtTonGiao.Text = row.Cells["TonGiao"].Value?.ToString() ?? "";

                if (row.Cells["NgayVaoDoan"].Value != DBNull.Value)
                    dtpNgayVaoDoan.Value = Convert.ToDateTime(row.Cells["NgayVaoDoan"].Value);
                if (row.Cells["NgayVaoDang"].Value != DBNull.Value)
                    dtpNgayVaoDang.Value = Convert.ToDateTime(row.Cells["NgayVaoDang"].Value);

                txtDienChinhSach.Text = row.Cells["DienChinhSach"].Value?.ToString() ?? "";
                cboPhongBan.SelectedValue = Convert.ToInt32(row.Cells["MaPhong"].Value);
                cboChucDanh.SelectedValue = Convert.ToInt32(row.Cells["MaChucDanh"].Value);
                cboLoaiNhanVien.SelectedItem = row.Cells["LoaiNhanVien"].Value.ToString();
                dtpNgayVaoCongTy.Value = Convert.ToDateTime(row.Cells["NgayVaoCongTy"].Value);

                // Xử lý ảnh
                if (row.Cells["HinhAnh"].Value != DBNull.Value)
                {
                    byte[] imageBytes = (byte[])row.Cells["HinhAnh"].Value;
                    using (MemoryStream ms = new MemoryStream(imageBytes))
                    {
                        picHinhAnh.Image = Image.FromStream(ms);
                    }
                    currentImageBytes = imageBytes;
                }
                else
                {
                    picHinhAnh.Image = null;
                    currentImageBytes = null;
                }
            }
        }

        private void frmHoSoCaNhan_Load(object sender, EventArgs e)
        {
            LoadData();
            LoadComboBoxes();
        }
    }
}