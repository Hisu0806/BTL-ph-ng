using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using HRM.DAL;
using HRM.DTO;

namespace HRM.BLL
{
    public class NhanVienBLL
    {
        public static List<NhanVien> GetAll()
        {
            return NhanVienDAL.GetAll();
        }
        
        public static NhanVien GetByID(int maNV)
        {
            return NhanVienDAL.GetByID(maNV);
        }
        
        public static int Insert(NhanVien nv)
        {
            // Kiểm tra dữ liệu đầu vào
            if (string.IsNullOrEmpty(nv.HoTen))
                throw new Exception("Họ tên không được để trống");
                
            if (string.IsNullOrEmpty(nv.SoCMND))
                throw new Exception("Số CMND không được để trống");
                
            // Kiểm tra trùng CMND
            List<NhanVien> dsNhanVien = NhanVienDAL.GetAll();
            foreach (NhanVien item in dsNhanVien)
            {
                if (item.SoCMND.Equals(nv.SoCMND))
                    throw new Exception("Số CMND đã tồn tại");
            }
            
            // Mặc định trạng thái là đang làm việc nếu chưa có
            if (string.IsNullOrEmpty(nv.TrangThai))
                nv.TrangThai = "Đang làm việc";
                
            return NhanVienDAL.Insert(nv);
        }
        
        public static bool Update(NhanVien nv)
        {
            // Kiểm tra dữ liệu đầu vào
            if (string.IsNullOrEmpty(nv.HoTen))
                throw new Exception("Họ tên không được để trống");
                
            if (string.IsNullOrEmpty(nv.SoCMND))
                throw new Exception("Số CMND không được để trống");
                
            // Kiểm tra trùng CMND trừ chính bản thân nhân viên đó
            List<NhanVien> dsNhanVien = NhanVienDAL.GetAll();
            foreach (NhanVien item in dsNhanVien)
            {
                if (item.MaNV != nv.MaNV && item.SoCMND.Equals(nv.SoCMND))
                    throw new Exception("Số CMND đã tồn tại");
            }
            
            return NhanVienDAL.Update(nv);
        }
        
        public static bool Delete(int maNV)
        {
            return NhanVienDAL.Delete(maNV);
        }
        
        public static List<NhanVien> Search(string hoTen, int? tuoi, string diaChi, int? bacLuong, 
                                         string trinhDo, bool? isDangVien, int? maPhong, 
                                         string congViec, string dienChinhSach)
        {
            return NhanVienDAL.Search(hoTen, tuoi, diaChi, bacLuong, trinhDo, isDangVien, 
                                    maPhong, congViec, dienChinhSach);
        }
        
        public static List<NhanVien> GetBirthdayInMonth(int month)
        {
            return NhanVienDAL.GetBirthdayInMonth(month);
        }
        
        public static List<NhanVien> GetRetirementList()
        {
            return NhanVienDAL.GetRetirementList();
        }
        
        public static List<NhanVien> GetPaged(int pageNumber, int pageSize, out int totalRecords)
        {
            if (pageNumber < 1)
                throw new Exception("Số trang không hợp lệ!");
            
            if (pageSize < 1)
                throw new Exception("Kích thước trang không hợp lệ!");
            
            try
            {
                return NhanVienDAL.GetPaged(pageNumber, pageSize, out totalRecords);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public static List<NhanVien> GetPagedAndFiltered(int pageNumber, int pageSize, out int totalRecords,
                                                string hoTen = null, string phong = null, string chucDanh = null)
        {
            // Kiểm tra thông tin đầu vào
            if (pageNumber < 1)
                pageNumber = 1;
                
            if (pageSize < 1)
                pageSize = 10;
                
            return NhanVienDAL.GetPagedAndFiltered(pageNumber, pageSize, out totalRecords, hoTen, phong, chucDanh);
        }
    }
} 