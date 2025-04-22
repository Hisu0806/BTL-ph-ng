using System;
using System.Windows.Forms;
using HRM.GUI;
using HRM.Utility;

namespace HRM
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            try
            {
                // Tạo và kết nối cơ sở dữ liệu
                bool isNewDatabase = SqlHelper.CreateDatabaseIfNotExists();
                
                // Nếu là cơ sở dữ liệu mới thì thêm dữ liệu mẫu
                if (isNewDatabase)
                {
                    SqlHelper.InsertSampleData();
                    MessageBox.Show("Đã tạo cơ sở dữ liệu và thêm dữ liệu mẫu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                
                // Khởi chạy form đăng nhập
                Application.Run(new formDangNhap());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khởi động ứng dụng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
