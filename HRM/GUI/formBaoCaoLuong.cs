using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Windows.Forms;
using HRM.BLL;

namespace HRM.GUI
{
    public partial class formBaoCaoLuong : Form
    {
        private readonly PhongBanBLL _phongBanBLL = new PhongBanBLL();

        public formBaoCaoLuong()
        {
        }

        private void formBaoCaoLuong_Load(object sender, EventArgs e)
        {
            // Khởi tạo dữ liệu cho combobox tháng
            for (int i = 1; i <= 12; i++)
            {
                cboThang.Items.Add(i);
            }

            // Khởi tạo dữ liệu cho combobox năm (từ năm 2020 đến năm hiện tại + 5)
            int yearNow = DateTime.Now.Year;
            for (int i = 2020; i <= yearNow + 5; i++)
            {
                cboNam.Items.Add(i);
            }

            // Khởi tạo dữ liệu cho combobox loại báo cáo
            cboLoaiBaoCao.Items.Add("Báo cáo lương tất cả nhân viên");
            cboLoaiBaoCao.Items.Add("Báo cáo lương theo phòng ban");

            // Khởi tạo dữ liệu cho combobox phòng
            cboPhong.DisplayMember = "TenPB";
            cboPhong.ValueMember = "MaPB";

            // Thiết lập giá trị mặc định
            cboThang.SelectedIndex = DateTime.Now.Month - 1;
            cboNam.SelectedItem = yearNow;
            cboLoaiBaoCao.SelectedIndex = 0;
        }

        private void cboLoaiBaoCao_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Hiển thị/ẩn combo box phòng ban dựa trên loại báo cáo
            if (cboLoaiBaoCao.SelectedIndex == 1) // Báo cáo lương theo phòng ban
            {
                cboPhong.Visible = true;
                lblPhong.Visible = true;
            }
            else
            {
                cboPhong.Visible = false;
                lblPhong.Visible = false;
            }
        }

        private void btnXemBaoCao_Click(object sender, EventArgs e)
        {
            try
            {
                int thang = Convert.ToInt32(cboThang.SelectedItem);
                int nam = Convert.ToInt32(cboNam.SelectedItem);
                DataTable dtBangLuong;

                // Xác định loại báo cáo
                if (cboLoaiBaoCao.SelectedIndex == 0) // Báo cáo lương tất cả nhân viên
                {
                   
                }
                else if (cboLoaiBaoCao.SelectedIndex == 1) // Báo cáo lương theo phòng ban
                {
                    if (cboPhong.SelectedValue == null)
                    {
                        MessageBox.Show("Vui lòng chọn phòng ban", "Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    string maPhong = cboPhong.SelectedValue.ToString();
                    string tenPhong = cboPhong.Text;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnIn_Click(object sender, EventArgs e)
        {
            try
            {
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXuatExcel_Click(object sender, EventArgs e)
        {
            try
            {


                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "Excel Files|*.xls",
                    Title = "Lưu báo cáo",
                    FileName = "BaoCaoLuong_" + cboThang.SelectedItem + "_" + cboNam.SelectedItem
                };

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    if (File.Exists(saveFileDialog.FileName))
                    {
                        MessageBox.Show("Xuất báo cáo thành công!", "Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}