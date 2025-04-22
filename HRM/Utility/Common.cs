using System;
using System.Drawing;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace HRM.Utility
{
    public static class Common
    {
        // Mã hóa mật khẩu
        public static string MaHoaMD5(string input)
        {
            using (MD5 md5Hash = MD5.Create())
            {
                byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
                StringBuilder sBuilder = new StringBuilder();
                
                for (int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("x2"));
                }
                
                return sBuilder.ToString();
            }
        }
        
        // Chuyển đổi từ byte[] sang Image
        public static Image ByteArrayToImage(byte[] byteArrayIn)
        {
            if (byteArrayIn == null)
                return null;
                
            try
            {
                using (MemoryStream ms = new MemoryStream(byteArrayIn))
                {
                    return Image.FromStream(ms);
                }
            }
            catch
            {
                return null;
            }
        }
        
        // Chuyển đổi từ Image sang byte[]
        public static byte[] ImageToByteArray(Image imageIn)
        {
            if (imageIn == null)
                return null;
                
            try
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                    return ms.ToArray();
                }
            }
            catch
            {
                return null;
            }
        }
        
        // Định dạng số tiền
        public static string FormatMoney(decimal money)
        {
            return string.Format("{0:#,##0}", money);
        }
        
        // Tạo mã ngẫu nhiên
        public static string GenerateRandomCode(int length)
        {
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            Random random = new Random();
            StringBuilder result = new StringBuilder(length);
            
            for (int i = 0; i < length; i++)
            {
                result.Append(chars[random.Next(chars.Length)]);
            }
            
            return result.ToString();
        }
        
        // Lấy tuổi từ ngày sinh
        public static int GetAge(DateTime birthDate)
        {
            DateTime today = DateTime.Today;
            int age = today.Year - birthDate.Year;
            
            if (birthDate.Date > today.AddYears(-age))
                age--;
                
            return age;
        }
        
        // Validate email
        public static bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
        
        // Lấy số ngày trong tháng
        public static int GetDaysInMonth(int month, int year)
        {
            return DateTime.DaysInMonth(year, month);
        }
        
        // Lấy tên ngày trong tuần
        public static string GetDayOfWeekName(DateTime date)
        {
            string[] days = { "Chủ nhật", "Thứ hai", "Thứ ba", "Thứ tư", "Thứ năm", "Thứ sáu", "Thứ bảy" };
            return days[(int)date.DayOfWeek];
        }
        
        // Lấy tên tháng
        public static string GetMonthName(int month)
        {
            string[] months = { "Tháng 1", "Tháng 2", "Tháng 3", "Tháng 4", "Tháng 5", "Tháng 6", "Tháng 7", "Tháng 8", "Tháng 9", "Tháng 10", "Tháng 11", "Tháng 12" };
            return months[month - 1];
        }
        
        // Lấy danh sách năm từ năm bắt đầu đến năm hiện tại
        public static int[] GetYearList(int startYear)
        {
            int currentYear = DateTime.Now.Year;
            int count = currentYear - startYear + 1;
            int[] years = new int[count];
            
            for (int i = 0; i < count; i++)
            {
                years[i] = startYear + i;
            }
            
            return years;
        }
    }
} 