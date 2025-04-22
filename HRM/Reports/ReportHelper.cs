using System;
using System.Data;
using System.IO;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Printing;
using System.Collections.Generic;
using HRM.DTO;

namespace HRM.Reports
{
    public class ReportHelper
    {
        private PrintDocument printDocument = new PrintDocument();
        private int currentPage = 0;
        private string reportTitle = "";
        private DataTable data;
        private string[] columnNames;
        private string[] columnHeaders;
        private int[] columnWidths;
        private Font titleFont = new Font("Arial", 16, FontStyle.Bold);
        private Font headerFont = new Font("Arial", 10, FontStyle.Bold);
        private Font normalFont = new Font("Arial", 10);
        private int pageWidth;
        private int pageHeight;
        private int margin = 50;
        private int currentY = 0;
        private int rowHeight = 25;
        private int columnPadding = 5;
        private string footerText = "HRM - Hệ thống quản lý nhân sự | " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        
        // Constructor
        public ReportHelper(string title, DataTable reportData, string[] colNames, string[] colHeaders, int[] colWidths)
        {
            reportTitle = title;
            data = reportData;
            columnNames = colNames;
            columnHeaders = colHeaders;
            columnWidths = colWidths;
            
            // Cấu hình sự kiện in
            printDocument.PrintPage += new PrintPageEventHandler(PrintPage);
            printDocument.BeginPrint += new PrintEventHandler(BeginPrint);
        }
        
        // Hiển thị hộp thoại xem trước khi in
        public void PrintPreview()
        {
            PrintPreviewDialog preview = new PrintPreviewDialog();
            preview.Document = printDocument;
            preview.WindowState = FormWindowState.Maximized;
            preview.ShowDialog();
        }
        
        // In báo cáo
        public void Print()
        {
            PrintDialog printDialog = new PrintDialog();
            printDialog.Document = printDocument;
            
            if (printDialog.ShowDialog() == DialogResult.OK)
            {
                printDocument.Print();
            }
        }
        
        // Xuất báo cáo dạng PDF
        public void ExportToPdf(string filePath)
        {
            // Thực hiện xuất PDF (cần thêm thư viện hỗ trợ như iTextSharp)
            MessageBox.Show("Chức năng xuất PDF sẽ được phát triển sau.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        
        // Xuất báo cáo dạng Excel
        public void ExportToExcel(string filePath)
        {
            try
            {
                // Tạo file Excel
                using (StreamWriter sw = new StreamWriter(filePath, false, System.Text.Encoding.UTF8))
                {
                    // Tiêu đề
                    sw.WriteLine(reportTitle);
                    sw.WriteLine();
                    
                    // Header
                    string headerLine = "";
                    foreach (string header in columnHeaders)
                    {
                        headerLine += "\"" + header + "\",";
                    }
                    sw.WriteLine(headerLine);
                    
                    // Data
                    foreach (DataRow row in data.Rows)
                    {
                        string dataLine = "";
                        foreach (string col in columnNames)
                        {
                            dataLine += "\"" + row[col].ToString() + "\",";
                        }
                        sw.WriteLine(dataLine);
                    }
                }
                
                MessageBox.Show("Xuất Excel thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xuất Excel: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        // Sự kiện bắt đầu in
        private void BeginPrint(object sender, PrintEventArgs e)
        {
            currentPage = 0;
        }
        
        // Sự kiện in trang
        private void PrintPage(object sender, PrintPageEventArgs e)
        {
            pageWidth = e.PageBounds.Width;
            pageHeight = e.PageBounds.Height;
            currentY = margin;
            
            // In tiêu đề báo cáo
            e.Graphics.DrawString(reportTitle, titleFont, Brushes.Black, pageWidth / 2 - e.Graphics.MeasureString(reportTitle, titleFont).Width / 2, currentY);
            currentY += (int)e.Graphics.MeasureString(reportTitle, titleFont).Height + 20;
            
            // In ngày tạo báo cáo
            string dateText = "Ngày tạo: " + DateTime.Now.ToString("dd/MM/yyyy");
            e.Graphics.DrawString(dateText, normalFont, Brushes.Black, pageWidth - margin - e.Graphics.MeasureString(dateText, normalFont).Width, currentY);
            currentY += (int)e.Graphics.MeasureString(dateText, normalFont).Height + 20;
            
            // Vẽ header cho bảng
            int x = margin;
            for (int i = 0; i < columnHeaders.Length; i++)
            {
                Rectangle rect = new Rectangle(x, currentY, columnWidths[i], rowHeight);
                e.Graphics.FillRectangle(Brushes.LightGray, rect);
                e.Graphics.DrawRectangle(Pens.Black, rect);
                e.Graphics.DrawString(columnHeaders[i], headerFont, Brushes.Black, x + columnPadding, currentY + columnPadding);
                x += columnWidths[i];
            }
            currentY += rowHeight;
            
            // Vẽ dữ liệu
            int recordsPerPage = 0;
            int startRecord = currentPage * 25; // 25 bản ghi mỗi trang
            
            for (int i = startRecord; i < data.Rows.Count && i < startRecord + 25; i++)
            {
                x = margin;
                DataRow row = data.Rows[i];
                
                for (int j = 0; j < columnNames.Length; j++)
                {
                    Rectangle rect = new Rectangle(x, currentY, columnWidths[j], rowHeight);
                    e.Graphics.DrawRectangle(Pens.Black, rect);
                    e.Graphics.DrawString(row[columnNames[j]].ToString(), normalFont, Brushes.Black, x + columnPadding, currentY + columnPadding);
                    x += columnWidths[j];
                }
                
                currentY += rowHeight;
                recordsPerPage++;
            }
            
            // In footer
            e.Graphics.DrawString(footerText, normalFont, Brushes.Black, pageWidth / 2 - e.Graphics.MeasureString(footerText, normalFont).Width / 2, pageHeight - margin);
            e.Graphics.DrawString("Trang " + (currentPage + 1), normalFont, Brushes.Black, pageWidth - margin - e.Graphics.MeasureString("Trang " + (currentPage + 1), normalFont).Width, pageHeight - margin);
            
            // Kiểm tra còn trang nữa không
            if (startRecord + recordsPerPage < data.Rows.Count)
            {
                currentPage++;
                e.HasMorePages = true;
            }
            else
            {
                currentPage = 0;
                e.HasMorePages = false;
            }
        }
        
        #region Các báo cáo mẫu
        
        // Báo cáo danh sách nhân viên
        public static ReportHelper CreateEmployeeListReport(List<NhanVien> dsNhanVien)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("MaNV", typeof(int));
            dt.Columns.Add("HoTen", typeof(string));
            dt.Columns.Add("NgaySinh", typeof(string));
            dt.Columns.Add("GioiTinh", typeof(string));
            dt.Columns.Add("SoCMND", typeof(string));
            dt.Columns.Add("PhongBan", typeof(string));
            dt.Columns.Add("ChucDanh", typeof(string));
            dt.Columns.Add("LoaiNhanVien", typeof(string));
            
            foreach (NhanVien nv in dsNhanVien)
            {
                DataRow row = dt.NewRow();
                row["MaNV"] = nv.MaNV;
                row["HoTen"] = nv.HoTen;
                row["NgaySinh"] = nv.NgaySinh.ToString("dd/MM/yyyy");
                row["GioiTinh"] = nv.GioiTinh;
                row["SoCMND"] = nv.SoCMND;
                row["PhongBan"] = nv.TenPhong;
                row["ChucDanh"] = nv.TenChucDanh;
                row["LoaiNhanVien"] = nv.LoaiNhanVien;
                dt.Rows.Add(row);
            }
            
            string[] columnNames = {"MaNV", "HoTen", "NgaySinh", "GioiTinh", "SoCMND", "PhongBan", "ChucDanh", "LoaiNhanVien"};
            string[] columnHeaders = {"Mã NV", "Họ tên", "Ngày sinh", "Giới tính", "Số CMND", "Phòng ban", "Chức danh", "Loại NV"};
            int[] columnWidths = {60, 150, 100, 80, 100, 150, 120, 100};
            
            return new ReportHelper("DANH SÁCH NHÂN VIÊN", dt, columnNames, columnHeaders, columnWidths);
        }
        
        // Báo cáo bảng lương
        public static ReportHelper CreateSalaryReport(List<Luong> dsLuong, int thang, int nam)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("MaNV", typeof(int));
            dt.Columns.Add("HoTen", typeof(string));
            dt.Columns.Add("PhongBan", typeof(string));
            dt.Columns.Add("ChucDanh", typeof(string));
            dt.Columns.Add("LuongCoBan", typeof(string));
            dt.Columns.Add("TongPhuCap", typeof(string));
            dt.Columns.Add("BaoHiem", typeof(string));
            dt.Columns.Add("TongNgayCong", typeof(int));
            dt.Columns.Add("TongLuong", typeof(string));
            
            foreach (Luong luong in dsLuong)
            {
                DataRow row = dt.NewRow();
                row["MaNV"] = luong.MaNV;
                row["HoTen"] = luong.HoTenNV;
                row["PhongBan"] = luong.PhongBan;
                row["ChucDanh"] = luong.ChucDanh;
                row["LuongCoBan"] = string.Format("{0:#,##0}", luong.LuongCoBan);
                row["TongPhuCap"] = string.Format("{0:#,##0}", luong.TongPhuCap);
                row["BaoHiem"] = string.Format("{0:#,##0}", luong.BaoHiem);
                row["TongNgayCong"] = luong.TongNgayCong;
                row["TongLuong"] = string.Format("{0:#,##0}", luong.TongLuong);
                dt.Rows.Add(row);
            }
            
            string[] columnNames = {"MaNV", "HoTen", "PhongBan", "ChucDanh", "LuongCoBan", "TongPhuCap", "BaoHiem", "TongNgayCong", "TongLuong"};
            string[] columnHeaders = {"Mã NV", "Họ tên", "Phòng ban", "Chức danh", "Lương cơ bản", "Tổng phụ cấp", "Bảo hiểm", "Ngày công", "Tổng lương"};
            int[] columnWidths = {60, 150, 150, 120, 100, 100, 100, 80, 120};
            
            return new ReportHelper("BẢNG LƯƠNG THÁNG " + thang + "/" + nam, dt, columnNames, columnHeaders, columnWidths);
        }
        
        // Báo cáo danh sách tăng lương
        public static ReportHelper CreateSalaryIncreaseReport(List<TangLuong> dsTangLuong)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("MaNV", typeof(int));
            dt.Columns.Add("HoTen", typeof(string));
            dt.Columns.Add("NgayTangLuong", typeof(string));
            dt.Columns.Add("BacLuongCu", typeof(string));
            dt.Columns.Add("HeSoCu", typeof(float));
            dt.Columns.Add("BacLuongMoi", typeof(string));
            dt.Columns.Add("HeSoMoi", typeof(float));
            dt.Columns.Add("LyDo", typeof(string));
            
            foreach (TangLuong tangLuong in dsTangLuong)
            {
                DataRow row = dt.NewRow();
                row["MaNV"] = tangLuong.MaNV;
                row["HoTen"] = tangLuong.HoTenNV;
                row["NgayTangLuong"] = tangLuong.NgayTangLuong.ToString("dd/MM/yyyy");
                row["BacLuongCu"] = tangLuong.TenBacLuongCu;
                row["HeSoCu"] = tangLuong.HeSoCu;
                row["BacLuongMoi"] = tangLuong.TenBacLuongMoi;
                row["HeSoMoi"] = tangLuong.HeSoMoi;
                row["LyDo"] = tangLuong.LyDo;
                dt.Rows.Add(row);
            }
            
            string[] columnNames = {"MaNV", "HoTen", "NgayTangLuong", "BacLuongCu", "HeSoCu", "BacLuongMoi", "HeSoMoi", "LyDo"};
            string[] columnHeaders = {"Mã NV", "Họ tên", "Ngày tăng lương", "Bậc lương cũ", "Hệ số cũ", "Bậc lương mới", "Hệ số mới", "Lý do"};
            int[] columnWidths = {60, 150, 100, 100, 80, 100, 80, 200};
            
            return new ReportHelper("DANH SÁCH TĂNG LƯƠNG", dt, columnNames, columnHeaders, columnWidths);
        }
        
        // Báo cáo danh sách nghỉ hưu
        public static ReportHelper CreateRetirementReport(List<NhanVien> dsNghiHuu)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("MaNV", typeof(int));
            dt.Columns.Add("HoTen", typeof(string));
            dt.Columns.Add("NgaySinh", typeof(string));
            dt.Columns.Add("GioiTinh", typeof(string));
            dt.Columns.Add("Tuoi", typeof(int));
            dt.Columns.Add("PhongBan", typeof(string));
            dt.Columns.Add("ChucDanh", typeof(string));
            dt.Columns.Add("NgayVaoCongTy", typeof(string));
            dt.Columns.Add("SoNamCongTac", typeof(int));
            
            foreach (NhanVien nv in dsNghiHuu)
            {
                DataRow row = dt.NewRow();
                row["MaNV"] = nv.MaNV;
                row["HoTen"] = nv.HoTen;
                row["NgaySinh"] = nv.NgaySinh.ToString("dd/MM/yyyy");
                row["GioiTinh"] = nv.GioiTinh;
                row["Tuoi"] = nv.Tuoi;
                row["PhongBan"] = nv.TenPhong;
                row["ChucDanh"] = nv.TenChucDanh;
                row["NgayVaoCongTy"] = nv.NgayVaoCongTy.ToString("dd/MM/yyyy");
                
                int soNamCongTac = DateTime.Now.Year - nv.NgayVaoCongTy.Year;
                if (DateTime.Now.DayOfYear < nv.NgayVaoCongTy.DayOfYear)
                    soNamCongTac--;
                    
                row["SoNamCongTac"] = soNamCongTac;
                dt.Rows.Add(row);
            }
            
            string[] columnNames = {"MaNV", "HoTen", "NgaySinh", "GioiTinh", "Tuoi", "PhongBan", "ChucDanh", "NgayVaoCongTy", "SoNamCongTac"};
            string[] columnHeaders = {"Mã NV", "Họ tên", "Ngày sinh", "Giới tính", "Tuổi", "Phòng ban", "Chức danh", "Ngày vào công ty", "Số năm công tác"};
            int[] columnWidths = {60, 150, 100, 80, 60, 150, 120, 100, 100};
            
            return new ReportHelper("DANH SÁCH NHÂN VIÊN NGHỈ HƯU", dt, columnNames, columnHeaders, columnWidths);
        }
        
        // Báo cáo danh sách sinh nhật
        public static ReportHelper CreateBirthdayReport(List<NhanVien> dsSinhNhat, int thang)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("MaNV", typeof(int));
            dt.Columns.Add("HoTen", typeof(string));
            dt.Columns.Add("NgaySinh", typeof(string));
            dt.Columns.Add("Tuoi", typeof(int));
            dt.Columns.Add("GioiTinh", typeof(string));
            dt.Columns.Add("PhongBan", typeof(string));
            dt.Columns.Add("ChucDanh", typeof(string));
            
            foreach (NhanVien nv in dsSinhNhat)
            {
                DataRow row = dt.NewRow();
                row["MaNV"] = nv.MaNV;
                row["HoTen"] = nv.HoTen;
                row["NgaySinh"] = nv.NgaySinh.ToString("dd/MM/yyyy");
                row["Tuoi"] = nv.Tuoi;
                row["GioiTinh"] = nv.GioiTinh;
                row["PhongBan"] = nv.TenPhong;
                row["ChucDanh"] = nv.TenChucDanh;
                dt.Rows.Add(row);
            }
            
            string[] columnNames = {"MaNV", "HoTen", "NgaySinh", "Tuoi", "GioiTinh", "PhongBan", "ChucDanh"};
            string[] columnHeaders = {"Mã NV", "Họ tên", "Ngày sinh", "Tuổi", "Giới tính", "Phòng ban", "Chức danh"};
            int[] columnWidths = {60, 150, 100, 60, 80, 150, 120};
            
            string[] thangNames = {"Một", "Hai", "Ba", "Tư", "Năm", "Sáu", "Bảy", "Tám", "Chín", "Mười", "Mười Một", "Mười Hai"};
            
            return new ReportHelper("DANH SÁCH SINH NHẬT THÁNG " + thangNames[thang - 1], dt, columnNames, columnHeaders, columnWidths);
        }
        
        #endregion
    }
} 