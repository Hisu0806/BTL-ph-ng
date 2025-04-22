using System;
using System.Drawing;
using System.Windows.Forms;
using HRM.BLL;
using HRM.DTO;
using HRM.Utility;

namespace HRM.GUI
{
    public partial class formDangNhap : Form
    {
        public formDangNhap()
        {
            InitializeComponent();
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            // Kiểm tra dữ liệu nhập
            if (string.IsNullOrEmpty(txtTaiKhoan.Text))
            {
                MessageBox.Show("Vui lòng nhập tài khoản!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTaiKhoan.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtMatKhau.Text))
            {
                MessageBox.Show("Vui lòng nhập mật khẩu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMatKhau.Focus();
                return;
            }

            // Kiểm tra đăng nhập
            NguoiDung nguoiDung = NguoiDungBLL.DangNhap(txtTaiKhoan.Text, txtMatKhau.Text);

            if (nguoiDung != null)
            {
                // Lưu thông tin người dùng đăng nhập
                GlobalVars.CurrentUser = nguoiDung;

                // Mở form chính
                this.Hide();
                formMain frmMain = new formMain();
                frmMain.FormClosed += (s, args) => this.Close();
                frmMain.Show();
            }
            else
            {
                MessageBox.Show("Sai tài khoản hoặc mật khẩu!", "Lỗi đăng nhập", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtMatKhau.Clear();
                txtMatKhau.Focus();
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void formDangNhap_Load(object sender, EventArgs e)
        {
            // Căn giữa form
            this.CenterToScreen();
            
            // Đặt focus vào ô tài khoản
            txtTaiKhoan.Focus();
        }
    }
}