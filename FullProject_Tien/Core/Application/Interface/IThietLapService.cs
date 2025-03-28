using Core.Application.DTOs;
using Core.Domain.Model;
using System.Data;


namespace Core.Application.Interface
{
    public interface IThietLapService
    {
        // Các phương thức liên quan đến Thiết lập
        Task<List<string>> GetThangTinhCongValuesAsync();
        Task<List<ThietLap>> GetAllThietLap();
        Task<List<ThietLap>> GetThietLapByThangTinhCongAsync(string thangTinhCong);
        Task AddThietLap(ThietLap thietLap);
        Task UpdateThietLap(ThietLap updatedThietLap);
        Task DeleteThietLap(string thangTinhCong);
        Task<int> GetTotalRecords();
        Task<List<ThietLap>> GetThietLapPaged(int skip, int take);
        Task<int> GetTotalRecordsThangTinhCong();
        Task<List<ThangTinhCongDuocThiepLap>> GetThangTinhCongPaged(int skip, int take);
        Task<List<ThangTinhCongDuocThiepLap>> GetThangTinhCong();

        // Các phương thức liên quan đến Phạt đi muộn
        Task<List<BangPhatDiMuon>> GetAllPhatMuon();
        Task<List<BangPhatDiMuon>> GetPhatByThangTinhCong(string thangTinhCong);
        Task AddPhat(BangPhatDiMuon thietLap);
        Task UpdatePhat(BangPhatDiMuon updatedThietLap);
        Task DeletePhat(string thangTinhCong);
        Task<int> GetTotalRecordsPhatMuon();
        Task<List<BangPhatDiMuon>> GetPhatMuonPaged(int skip, int take);

        // Các phương thức liên quan đến Nhân viên
        Task<int> GetTotalEmployees();
        Task<int> GetTotalPhongBan();
        Task<int> GetTotalNhanVienChinhThuc();
        Task<int> GetTotalNhanVienHopDong();
        Task<Dictionary<int, int>> GetTotalEmployeesByYear();
        Task<int> GetResignedEmployeesCountByYear(int year);
        Task<int> GetNewEmployeesCountByYear(int year);
        Task<DepartmentEmployeeStats> CountDepartmentsAndEmployees();

        // Các phương thức liên quan đến Thưởng/Phạt
        Task<List<ThuongPhatWithNhanVienInfo>> GetThuongAsync();
        Task<List<ThuongPhatWithNhanVienInfo>> GetPhatAsync();
        Task AddThuongPhatAsync(string maNhanVien, DateTime ngay, string nguon, decimal soTien, string loai);
        Task XoaThuongPhatAsync(int maThuongPhat);
        Task CapNhatThuongPhatAsync(int maTP, string maNhanVien, DateTime ngay, string nguon, decimal soTien, string loai);
        Task<List<ThuongPhatWithNhanVienInfo>> GetThuongPagedAsync(int skip, int take);
        Task<int> GetTotalRecordsThuongAsync();
        Task<List<ThuongPhatWithNhanVienInfo>> GetPhatPagedAsync(int skip, int take);
        Task<int> GetTotalRecordsPhatAsync();

        // exportExcel
        byte[] ExportToExcel(DataTable dataTable);



        // Các phương thức liên quan đến Lương
        Task<List<LuongNhanVien>> GetLuong();
        Task AddLuong(string maNhanVien, string thangLinhLuong, decimal luongCung, decimal thue);
        Task CapNhatLuong(string maNhanVien, string thangLinhLuong, decimal luongCung, decimal thue);
        Task<List<LuongNhanVien>> GetLuongPaged(int skip, int take);
        Task<int> GetTotalRecordsLuong();

        // Các phương thức liên quan đến Nhân viên (2)
        Task<NhanVienInfo> GetEmployeeInfo(string maNV);
        Task<string> LayMaPhongBanTuTenAsync(string tenPhongBan);
        Task<string> LayTenPhongBanTuMa(string maPhongBan);
        Task ThemMoiNhanVienAsync(NhanVien nhanVien);
        Task<List<string>> LoadDanhSachPhongBan();
        Task<List<NhanVienInfo>> GetNhanVienPhongBanInfo();
        Task<int> GetTotalRecordsNhanVien();
        Task<List<NhanVienInfo>> GetNhanVienPaged(int skip, int take);
        Task<List<NhanVienInfo>> GetNhanVienByMaNV(string maNV);
        Task<List<NhanVienInfo>> GetNhanVienByHoTen(string hoTen);
        Task<List<NhanVienInfo>> GetNhanVienByChucVu(string chucVu);
        Task<List<NhanVienInfo>> GetNhanVienByPhongBan(string phongBan);
        Task<List<NhanVienInfo>> GetNhanVienByTrangThai(string trangThai);
        Task DeleteNhanVien(string MaNV);
        Task CapNhatThongTinNhanVienAsync(NhanVien nhanVien);


        // Các phương thức kiểm tra dữ liệu
        Task<bool> KiemTraChamCong(string maNhanVien, DateTime ngayChamCong);
        Task<bool> KiemTraMaNhanVienCoLuongThangCuThe(string maNhanVien, string thangLinhLuong);
        Task<bool> KiemTraMaNhanVienTonTai(string maNhanVien);
        Task<bool> KiemTraMaNhanVienCoLuong(string maNhanVien);

    }
}

