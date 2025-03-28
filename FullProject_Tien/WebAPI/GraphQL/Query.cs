using Core.Application.DTOs;
using Core.Application.Interface;
using Core.Domain.Model;
using HotChocolate;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Services;

namespace WebAPI.GraphQL
{
    public class Query
    {
        // Nhân viên
        public Task<NhanVienInfo> GetNhanVien(string maNV, [Service] IThietLapService thietLapService)
        {
            return thietLapService.GetEmployeeInfo(maNV);
        }

        public Task<List<NhanVienInfo>> GetNhanViens([Service] IThietLapService thietLapService)
        {
            return thietLapService.GetNhanVienPhongBanInfo();
        }

        public Task<List<NhanVien>> GetAllNhanViens([Service] IBangCongService bangCongService)
        {
            return bangCongService.GetAllNhanViens();
        }

        // Thiết lập
        public Task<List<ThietLap>> GetAllThietLap([Service] IThietLapService thietLapService)
        {
            return thietLapService.GetAllThietLap();
        }

        public Task<List<ThietLap>> GetThietLapByThangTinhCong(string thangTinhCong, [Service] IThietLapService thietLapService)
        {
            return thietLapService.GetThietLapByThangTinhCongAsync(thangTinhCong);
        }

        public Task<List<string>> GetThangTinhCongValues([Service] IThietLapService thietLapService)
        {
            return thietLapService.GetThangTinhCongValuesAsync();
        }

        // Phòng ban
        public Task<List<string>> LoadDanhSachPhongBan([Service] IThietLapService thietLapService)
        {
            return thietLapService.LoadDanhSachPhongBan();
        }

        public Task<string> LayMaPhongBanTuTen(string tenPhongBan, [Service] IThietLapService thietLapService)
        {
            return thietLapService.LayMaPhongBanTuTenAsync(tenPhongBan);
        }

        public Task<string> LayTenPhongBanTuMa(string maPhongBan, [Service] IThietLapService thietLapService)
        {
            return thietLapService.LayTenPhongBanTuMa(maPhongBan);
        }

        // Bảng công
        public Task<List<BangCong>> GetAllBangCongs([Service] IBangCongService bangCongService)
        {
            return bangCongService.GetAllBangCongs();
        }

        public Task<List<ChamCong>> GetAllChamCongs([Service] IBangCongService bangCongService)
        {
            return bangCongService.GetAllChamCongs();
        }

        // Phạt muộn
        public Task<List<BangPhatDiMuon>> GetAllPhatMuon([Service] IThietLapService thietLapService)
        {
            return thietLapService.GetAllPhatMuon();
        }

        public Task<List<BangPhatDiMuon>> GetPhatByThangTinhCong(string thangTinhCong, [Service] IThietLapService thietLapService)
        {
            return thietLapService.GetPhatByThangTinhCong(thangTinhCong);
        }

        // Thưởng/phạt
        public Task<List<ThuongPhatWithNhanVienInfo>> GetThuong([Service] IThietLapService thietLapService)
        {
            return thietLapService.GetThuongAsync();
        }

        public Task<List<ThuongPhatWithNhanVienInfo>> GetPhat([Service] IThietLapService thietLapService)
        {
            return thietLapService.GetPhatAsync();
        }

        // Lương
        public Task<List<LuongNhanVien>> GetLuong([Service] IThietLapService thietLapService)
        {
            return thietLapService.GetLuong();
        }

        // Thống kê
        public Task<int> GetTotalEmployees([Service] IThietLapService thietLapService)
        {
            return thietLapService.GetTotalEmployees();
        }

        public Task<int> GetTotalPhongBan([Service] IThietLapService thietLapService)
        {
            return thietLapService.GetTotalPhongBan();
        }

        public Task<DepartmentEmployeeStats> CountDepartmentsAndEmployees([Service] IThietLapService thietLapService)
        {
            return thietLapService.CountDepartmentsAndEmployees();
        }

        // Phân trang
        public Task<List<NhanVienInfo>> GetNhanVienPaged(int skip, int take, [Service] IThietLapService thietLapService)
        {
            return thietLapService.GetNhanVienPaged(skip, take);
        }

        public Task<List<ThietLap>> GetThietLapPaged(int skip, int take, [Service] IThietLapService thietLapService)
        {
            return thietLapService.GetThietLapPaged(skip, take);
        }

        public Task<List<BangPhatDiMuon>> GetPhatMuonPaged(int skip, int take, [Service] IThietLapService thietLapService)
        {
            return thietLapService.GetPhatMuonPaged(skip, take);
        }
    }
}