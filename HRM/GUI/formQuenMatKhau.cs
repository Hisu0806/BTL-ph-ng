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

namespace HRM.GUI
{
    public partial class formQuenMatKhau : Form
    {
        public formQuenMatKhau()
        {
            InitializeComponent();
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtTenTaiKhoan.Text))
            {
                MessageBox.Show("Vui lòng nhập tên tài khoản!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenTaiKhoan.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtMaNhanVien.Text))
            {
                MessageBox.Show("Vui lòng nhập mật khẩu cũ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaNhanVien.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtSoCCCD.Text))
            {
                MessageBox.Show("Vui lòng nhập mật khẩu mới!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSoCCCD.Focus();
                return;
            }

            // Quên mật khẩu
            bool doiThanhCong = NguoiDungBLL.QuenMatKhau(
                txtTenTaiKhoan.Text.Trim(),
               txtMaNhanVien.Text.Trim(),
                txtSoCCCD.Text.Trim()
            );

            if (!doiThanhCong)
            {
                MessageBox.Show("Đổi mật khẩu thất bại. Mật khẩu cũ không đúng hoặc mật khẩu mới không khớp!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MessageBox.Show("Đổi mật khẩu thành công. Mật khẩu mới là mã nhân viên của bạn. Vui lòng đăng nhập lại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

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
