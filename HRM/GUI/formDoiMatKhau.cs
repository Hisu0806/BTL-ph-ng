using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HRM.BLL;
using HRM.DTO;

namespace HRM.GUI
{
    public partial class formDoiMatKhau : Form
    {
        public formDoiMatKhau()
        {
            InitializeComponent();
        }

        private void btnDongY_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtTenTaiKhoan.Text))
            {
                MessageBox.Show("Vui lòng nhập tên tài khoản!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenTaiKhoan.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtMatKhauCu.Text))
            {
                MessageBox.Show("Vui lòng nhập mật khẩu cũ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMatKhauCu.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtMatKhauMoi.Text))
            {
                MessageBox.Show("Vui lòng nhập mật khẩu mới!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMatKhauMoi.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtNhapLaiMatKhauMoi.Text))
            {
                MessageBox.Show("Vui lòng nhập lại mật khẩu mới!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNhapLaiMatKhauMoi.Focus();
                return;
            }

            // Đổi mật khẩu
            bool doiThanhCong = NguoiDungBLL.DoiMatKhau(
                txtTenTaiKhoan.Text.Trim(),
                txtMatKhauCu.Text.Trim(),
                txtMatKhauMoi.Text.Trim(),
                txtNhapLaiMatKhauMoi.Text.Trim()
            );

            if (!doiThanhCong)
            {
                MessageBox.Show("Đổi mật khẩu thất bại. Mật khẩu cũ không đúng hoặc mật khẩu mới không khớp!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MessageBox.Show("Đổi mật khẩu thành công. Vui lòng đăng nhập lại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

            this.Hide();
            formDangNhap frmdangnhap = new formDangNhap();
            frmdangnhap.FormClosed += (s, args) => this.Close();
            frmdangnhap.Show();
        }


        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Hide();
            formDangNhap frmdangnhap = new formDangNhap();
            frmdangnhap.FormClosed += (s, args) => this.Close();
            frmdangnhap.Show();
        }
    }
}
