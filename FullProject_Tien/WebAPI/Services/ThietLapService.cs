using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Data;
using Core.Application.Interface;
using Core.Domain.Model;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using Core.Application.DTOs;

namespace WebAPI.Services
{
    public class ThietLapService : IThietLapService
    {
        private readonly ThietLapContext _dbContext;

        public ThietLapService(ThietLapContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Các phương thức liên quan đến Thiết lập
        public async Task<List<string>> GetThangTinhCongValuesAsync()
        {
            return await _dbContext.ThietLaps.Select(t => t.ThangTinhCong).ToListAsync();
        }

        public async Task<List<ThietLap>> GetAllThietLap()
        {
            return await _dbContext.ThietLaps.AsNoTracking().ToListAsync();
        }

        public async Task<List<ThietLap>> GetThietLapByThangTinhCongAsync(string thangTinhCong)
        {
            return await _dbContext.ThietLaps
                        .Where(tl => tl.ThangTinhCong == thangTinhCong)
                        .AsNoTracking()
                        .ToListAsync();
        }

        public async Task AddThietLap(ThietLap thietLap)
        {
            try
            {
                _dbContext.ThietLaps.Add(thietLap);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding ThietLap: " + ex.Message);
            }
        }

        public async Task UpdateThietLap(ThietLap updatedThietLap)
        {
            var existingThietLap = await _dbContext.ThietLaps.FirstOrDefaultAsync(tl => tl.ThangTinhCong == updatedThietLap.ThangTinhCong);
            if (existingThietLap != null)
            {
                existingThietLap.ThangTinhCong = updatedThietLap.ThangTinhCong;
                existingThietLap.NgayBatDau = updatedThietLap.NgayBatDau;
                existingThietLap.NgayKetThuc = updatedThietLap.NgayKetThuc;
                existingThietLap.NgayCongBatBuoc = updatedThietLap.NgayCongBatBuoc;
                existingThietLap.NgayPhepToiDa = updatedThietLap.NgayPhepToiDa;

                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task DeleteThietLap(string thangTinhCong)
        {
            try
            {
                var thietLapToDelete = await _dbContext.ThietLaps.FirstOrDefaultAsync(tl => tl.ThangTinhCong == thangTinhCong);
                if (thietLapToDelete != null)
                {
                    _dbContext.ThietLaps.Remove(thietLapToDelete);
                    await _dbContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi xóa thietLap: " + ex.Message);
            }
        }

        public async Task<int> GetTotalRecords()
        {
            return await _dbContext.ThietLaps.CountAsync();
        }

        public async Task<List<ThietLap>> GetThietLapPagedAsync(int skip, int take)
        {
            return await _dbContext.ThietLaps.Skip(skip).Take(take).ToListAsync();
        }

        // Xuất Excel
        public byte[] ExportToExcel(DataTable dataTable)
        {
            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("LuongNhanVien");
                worksheet.Cells["A1"].LoadFromDataTable(dataTable, true);

                // Tùy chỉnh định dạng nếu cần thiết
                worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

                return package.GetAsByteArray();
            }
        }


        public async Task<bool> KiemTraChamCong(string maNhanVien, DateTime ngayChamCong)
        {
            // Kiểm tra nhân viên đã được nhập lương chưa
            var chamCong = await _dbContext.ChamCongs.FirstOrDefaultAsync(cc => cc.MaNV == maNhanVien && cc.NgayChamCong == ngayChamCong);
            return chamCong != null;
        }

        public async Task<bool> KiemTraMaNhanVienCoLuongThangCuThe(string maNhanVien, string thangLinhLuong)
        {
            // Kiểm tra nhân viên đã được nhập lương chưa
            var nhanVienCoLuong = await _dbContext.Luongs.FirstOrDefaultAsync(l => l.MaNV == maNhanVien && l.ThangLinhLuong == thangLinhLuong);
            return nhanVienCoLuong != null;
        }

        public async Task<bool> KiemTraMaNhanVienTonTai(string maNhanVien)
        {
            // Kiểm tra xem mã nhân viên có tồn tại trong cơ sở dữ liệu không
            var nhanVien = await _dbContext.NhanViens.FirstOrDefaultAsync(nv => nv.MaNV == maNhanVien);
            return nhanVien != null;
        }

        public async Task<bool> KiemTraMaNhanVienCoLuong(string maNhanVien)
        {
            // Kiểm tra nhân viên đã được nhập lương chưa
            var nhanVienCoLuong = await _dbContext.Luongs.FirstOrDefaultAsync(l => l.MaNV == maNhanVien);
            return nhanVienCoLuong != null;
        }

        // THÁNG TÍNH CÔNG: START

        public async Task<List<ThangTinhCongDuocThiepLap>> GetThangTinhCong()
        {
            // Thực hiện truy vấn LINQ để lấy danh sách ThangTinhCong từ cơ sở dữ liệu
            var thangTinhCongThietLap = await _dbContext.ThietLaps
                                                .Select(tl => new ThangTinhCongDuocThiepLap { ThangTinhCongThietLap = tl.ThangTinhCong })
                                                .ToListAsync();

            return thangTinhCongThietLap;
        }

        public async Task<List<ThangTinhCongDuocThiepLap>> GetThangTinhCongPaged(int skip, int take)
        {
            // Thực hiện truy vấn LINQ để lấy danh sách ThangTinhCong từ cơ sở dữ liệu
            var thangTinhCongThietLap = await _dbContext.ThietLaps
                                                .Select(tl => new ThangTinhCongDuocThiepLap { ThangTinhCongThietLap = tl.ThangTinhCong })
                                                .Skip(skip).Take(take).ToListAsync();

            return thangTinhCongThietLap;
        }
        public async Task<int> GetTotalRecordsThangTinhCong()
        {
            return await _dbContext.ThietLaps.Select(tl => new ThangTinhCongDuocThiepLap { ThangTinhCongThietLap = tl.ThangTinhCong }).CountAsync();
        }



        // Các phương thức liên quan đến Phạt đi muộn
        public async Task<List<BangPhatDiMuon>> GetAllPhatMuon()
        {
            return await _dbContext.PhatDiMuons.AsNoTracking().ToListAsync();
        }

        public async Task<List<BangPhatDiMuon>> GetPhatByThangTinhCong(string thangTinhCong)
        {
            return await _dbContext.PhatDiMuons
                .Where(tl => tl.ThangTinhCong == thangTinhCong)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task AddPhat(BangPhatDiMuon thietLap)
        {
            try
            {
                _dbContext.PhatDiMuons.Add(thietLap);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding ThietLap: " + ex.Message);
            }
        }

        public async Task UpdatePhat(BangPhatDiMuon updatedThietLap)
        {
            var existingThietLap = await _dbContext.PhatDiMuons.FirstOrDefaultAsync(tl => tl.ThangTinhCong == updatedThietLap.ThangTinhCong);
            if (existingThietLap != null)
            {
                existingThietLap.ThangTinhCong = updatedThietLap.ThangTinhCong;
                existingThietLap.SoGioTinhMuon = updatedThietLap.SoGioTinhMuon;
                existingThietLap.SoTienPhatMuon = updatedThietLap.SoTienPhatMuon;

                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task DeletePhat(string thangTinhCong)
        {
            try
            {
                var thietLapToDelete = await _dbContext.PhatDiMuons.FirstOrDefaultAsync(tl => tl.ThangTinhCong == thangTinhCong);
                if (thietLapToDelete != null)
                {
                    _dbContext.PhatDiMuons.Remove(thietLapToDelete);
                    await _dbContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi xóa thietLap: " + ex.Message);
            }
        }

        public async Task<int> GetTotalRecordsPhatMuon()
        {
            return await _dbContext.PhatDiMuons.CountAsync();
        }

        public async Task<List<BangPhatDiMuon>> GetPhatMuonPaged(int skip, int take)
        {
            if (skip < 0)
            {
                skip = 0;
            }

            return await _dbContext.PhatDiMuons
                .OrderBy(p => p.ThangTinhCong) // Ensure ordering for consistent pagination
                .Skip(skip)
                .Take(take)
                .ToListAsync();
        }

        // Các phương thức liên quan đến Nhân viên
        public async Task<int> GetTotalEmployees()
        {
            return await _dbContext.NhanViens.CountAsync(nv => nv.TrangThai == "Đang làm việc");
        }

        public async Task<int> GetTotalPhongBan()
        {
            return await _dbContext.PhongBans.CountAsync();
        }

        public async Task<int> GetTotalNhanVienChinhThuc()
        {
            return await _dbContext.NhanViens.CountAsync(nv => nv.TrangThai == "Đang làm việc" && nv.LoaiNhanVien == "Chính thức");
        }

        public async Task<int> GetTotalNhanVienHopDong()
        {
            return await _dbContext.NhanViens.CountAsync(nv => nv.TrangThai == "Đang làm việc" && nv.LoaiNhanVien == "Hợp đồng");
        }

        public async Task<Dictionary<int, int>> GetTotalEmployeesByYear()
        {
            var startYears = await _dbContext.NhanViens
                .Where(nv => nv.TrangThai == "Đang làm việc")
                .Select(nv => nv.BatDauLamViec.Year)
                .ToListAsync();

            var employeeCountsByYear = new Dictionary<int, int>();
            var allYears = Enumerable.Range(2017, 9);

            int totalEmployees = 0;
            foreach (int year in allYears)
            {
                int employeesInYear = startYears.Count(y => y == year);
                totalEmployees += employeesInYear;
                employeeCountsByYear[year] = totalEmployees;
            }

            return employeeCountsByYear;
        }

        public async Task<int> GetResignedEmployeesCountByYear(int year)
        {
            return await _dbContext.NhanViens
                .Where(nv => nv.TrangThai == "Đã nghỉ việc" && nv.NgayNghiViec.HasValue && nv.NgayNghiViec.Value.Year == year)
                .CountAsync();
        }

        public async Task<int> GetNewEmployeesCountByYear(int year)
        {
            return await _dbContext.NhanViens
                .Where(nv => nv.BatDauLamViec.Year == year && nv.TrangThai == "Đang làm việc")
                .CountAsync();
        }




        public async Task<DepartmentEmployeeStats> CountDepartmentsAndEmployees()
        {
            int departmentCount = await _dbContext.PhongBans.CountAsync();
            var departmentEmployeesCount = await _dbContext.PhongBans
                .Select(pb => new
                {
                    pb.MaPhongBan,
                    EmployeeCount = pb.NhanViens.Count()
                })
                .ToDictionaryAsync(pb => pb.MaPhongBan, pb => pb.EmployeeCount);

            return new DepartmentEmployeeStats
            {
                TotalDepartments = departmentCount,
                EmployeesPerDepartment = departmentEmployeesCount
            };
        }

        // Các phương thức liên quan đến Thưởng/Phạt
        public async Task<List<ThuongPhatWithNhanVienInfo>> GetThuongAsync()
        {
            return await _dbContext.ThuongPhats
                .Join(_dbContext.NhanViens,
                    tp => tp.MaNV,
                    nv => nv.MaNV,
                    (tp, nv) => new ThuongPhatWithNhanVienInfo
                    {
                        MaTP = tp.MaThuongPhat,
                        MaNV = tp.MaNV,
                        HoTen = nv.HoTen,
                        Ngay = tp.Ngay,
                        NguonThuongPhat = tp.NguonThuongPhat,
                        SoTien = tp.SoTien,
                        Loai = tp.Loai
                    })
                .Where(tp => tp.Loai == "Thưởng")
                .ToListAsync();
        }

        public async Task<List<ThuongPhatWithNhanVienInfo>> GetPhatAsync()
        {
            return await _dbContext.ThuongPhats
                .Where(tp => tp.Loai == "Phạt")
                .Join(_dbContext.NhanViens,
                    tp => tp.MaNV,
                    nv => nv.MaNV,
                    (tp, nv) => new ThuongPhatWithNhanVienInfo
                    {
                        MaTP = tp.MaThuongPhat,
                        MaNV = tp.MaNV,
                        HoTen = nv.HoTen,
                        Ngay = tp.Ngay,
                        NguonThuongPhat = tp.NguonThuongPhat,
                        SoTien = tp.SoTien
                    })
                .ToListAsync();
        }

        public async Task AddThuongPhatAsync(string maNhanVien, DateTime ngay, string nguon, decimal soTien, string loai)
        {
            var newThuongPhat = new ThuongPhat
            {
                MaNV = maNhanVien,
                Ngay = ngay,
                NguonThuongPhat = nguon,
                SoTien = soTien,
                Loai = loai
            };

            _dbContext.ThuongPhats.Add(newThuongPhat);
            await _dbContext.SaveChangesAsync();
        }

        public async Task XoaThuongPhatAsync(int maThuongPhat)
        {
            var thuongPhat = await _dbContext.ThuongPhats.FindAsync(maThuongPhat);
            if (thuongPhat != null)
            {
                _dbContext.ThuongPhats.Remove(thuongPhat);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task CapNhatThuongPhatAsync(int maTP, string maNhanVien, DateTime ngay, string nguon, decimal soTien, string loai)
        {
            var thuongphat = await _dbContext.ThuongPhats.FirstOrDefaultAsync(t => t.MaThuongPhat == maTP);
            if (thuongphat != null)
            {
                thuongphat.MaNV = maNhanVien;
                thuongphat.Ngay = ngay;
                thuongphat.NguonThuongPhat = nguon;
                thuongphat.SoTien = soTien;
                thuongphat.Loai = loai;
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<List<ThuongPhatWithNhanVienInfo>> GetThuongPagedAsync(int skip, int take)
        {
            return await _dbContext.ThuongPhats
                .Join(_dbContext.NhanViens,
                    tp => tp.MaNV,
                    nv => nv.MaNV,
                    (tp, nv) => new ThuongPhatWithNhanVienInfo
                    {
                        MaTP = tp.MaThuongPhat,
                        MaNV = tp.MaNV,
                        HoTen = nv.HoTen,
                        Ngay = tp.Ngay,
                        NguonThuongPhat = tp.NguonThuongPhat,
                        SoTien = tp.SoTien,
                        Loai = tp.Loai
                    })
                .Where(tp => tp.Loai == "Thưởng")
                .Skip(skip).Take(take).ToListAsync();
        }

        public async Task<int> GetTotalRecordsThuongAsync()
        {
            return await _dbContext.ThuongPhats.Where(tp => tp.Loai == "Thưởng").CountAsync();
        }

        public async Task<List<ThuongPhatWithNhanVienInfo>> GetPhatPagedAsync(int skip, int take)
        {
            return await _dbContext.ThuongPhats
                .Join(_dbContext.NhanViens,
                    tp => tp.MaNV,
                    nv => nv.MaNV,
                    (tp, nv) => new ThuongPhatWithNhanVienInfo
                    {
                        MaTP = tp.MaThuongPhat,
                        MaNV = tp.MaNV,
                        HoTen = nv.HoTen,
                        Ngay = tp.Ngay,
                        NguonThuongPhat = tp.NguonThuongPhat,
                        SoTien = tp.SoTien,
                        Loai = tp.Loai
                    })
                .Where(tp => tp.Loai == "Phạt")
                .Skip(skip).Take(take).ToListAsync();
        }

        public async Task<int> GetTotalRecordsPhatAsync()
        {
            return await _dbContext.ThuongPhats.Where(tp => tp.Loai == "Phạt").CountAsync();
        }

        // Các phương thức liên quan đến Lương
        public async Task<List<LuongNhanVien>> GetLuong()
        {
            try
            {
                var query = from nv in _dbContext.NhanViens
                            join pb in _dbContext.PhongBans on nv.MaPhongBan equals pb.MaPhongBan
                            join l in _dbContext.Luongs on nv.MaNV equals l.MaNV into luongGroup
                            from l in luongGroup.DefaultIfEmpty()
                            join tp in _dbContext.ThuongPhats on nv.MaNV equals tp.MaNV into thuongPhatGroup
                            from tp in thuongPhatGroup.DefaultIfEmpty()
                            where _dbContext.Luongs.Select(x => x.MaNV).Contains(nv.MaNV)
                            group new { nv, pb, l, tp } by new
                            {
                                nv.MaNV,
                                nv.HoTen,
                                nv.ChucVu,
                                pb.TenPhongBan,
                                l.ThangLinhLuong,
                                l.LuongCung,
                                l.Thue
                            } into g
                            select new LuongNhanVien
                            {
                                MaNV = g.Key.MaNV,
                                HoTen = g.Key.HoTen,
                                ChucVu = g.Key.ChucVu,
                                TenPhongBan = g.Key.TenPhongBan,
                                ThangLinhLuong = g.Key.ThangLinhLuong,
                                LuongCung = g.Key.LuongCung,
                                TongTienThuong = g.Sum(x => x.tp != null && x.tp.Loai == "Thưởng" ? x.tp.SoTien : 0),
                                TongTienPhat = g.Sum(x => x.tp != null && x.tp.Loai == "Phạt" ? x.tp.SoTien : 0),
                                Thue = g.Key.Thue,
                                ThucNhan = (g.Key.LuongCung) +
                                           g.Sum(x => x.tp != null && x.tp.Loai == "Thưởng" ? x.tp.SoTien : 0) -
                                           g.Sum(x => x.tp != null && x.tp.Loai == "Phạt" ? x.tp.SoTien : 0) -
                                           (g.Key.Thue)
                            };

                return await query.ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task AddLuong(string maNhanVien, string thangLinhLuong, decimal luongCung, decimal thue)
        {
            var newLuong = new Luong
            {
                MaNV = maNhanVien,
                ThangLinhLuong = thangLinhLuong,
                LuongCung = luongCung,
                Thue = thue
            };

            _dbContext.Luongs.Add(newLuong);
            await _dbContext.SaveChangesAsync();
        }

        public async Task CapNhatLuong(string maNhanVien, string thangLinhLuong, decimal luongCung, decimal thue)
        {
            var luog = await _dbContext.Luongs.FirstOrDefaultAsync(l => l.MaNV == maNhanVien && l.ThangLinhLuong == thangLinhLuong);
            if (luog != null)
            {
                luog.MaNV = maNhanVien;
                luog.ThangLinhLuong = thangLinhLuong;
                luog.LuongCung = luongCung;
                luog.Thue = thue;

                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<List<LuongNhanVien>> GetLuongPaged(int skip, int take)
        {
            var query = from nv in _dbContext.NhanViens
                        join pb in _dbContext.PhongBans on nv.MaPhongBan equals pb.MaPhongBan
                        join l in _dbContext.Luongs on nv.MaNV equals l.MaNV
                        into luongGroup
                        from l in luongGroup.DefaultIfEmpty()
                        join tp in _dbContext.ThuongPhats on nv.MaNV equals tp.MaNV
                        into thuongPhatGroup
                        from tp in thuongPhatGroup.DefaultIfEmpty()
                        where _dbContext.Luongs.Select(x => x.MaNV).Contains(nv.MaNV)
                        group new { nv, pb, l, tp } by new { nv.MaNV, nv.HoTen, nv.ChucVu, pb.TenPhongBan, l.ThangLinhLuong, l.LuongCung, l.Thue } into g
                        select new LuongNhanVien
                        {
                            MaNV = g.Key.MaNV,
                            HoTen = g.Key.HoTen,
                            ChucVu = g.Key.ChucVu,
                            TenPhongBan = g.Key.TenPhongBan,
                            ThangLinhLuong = g.Key.ThangLinhLuong,
                            LuongCung = g.Key.LuongCung,
                            TongTienThuong = g.Sum(x => x.tp != null && x.tp.Loai == "Thưởng" ? x.tp.SoTien : 0),
                            TongTienPhat = g.Sum(x => x.tp != null && x.tp.Loai == "Phạt" ? x.tp.SoTien : 0),
                            Thue = g.Key.Thue,
                            ThucNhan = (g.Key.LuongCung) + g.Sum(x => x.tp != null && x.tp.Loai == "Thưởng" ? x.tp.SoTien : 0)
                                             - g.Sum(x => x.tp != null && x.tp.Loai == "Phạt" ? x.tp.SoTien : 0)
                                             - (g.Key.Thue)
                        };
            return await query.Skip(skip).Take(take).ToListAsync();
        }

        public async Task<int> GetTotalRecordsLuong()
        {
            return await _dbContext.Luongs.CountAsync();
        }

        // Các phương thức liên quan đến Nhân viên (Minh)
        public async Task<NhanVienInfo> GetEmployeeInfo(string maNV)
        {
            var employeeInfo = from nv in _dbContext.NhanViens
                               join pb in _dbContext.PhongBans on nv.MaPhongBan equals pb.MaPhongBan
                               where nv.MaNV == maNV
                               select new NhanVienInfo
                               {
                                   MaNV = nv.MaNV,
                                   HoTen = nv.HoTen,
                                   CCCD = nv.CCCD,
                                   NgaySinh = nv.NgaySinh,
                                   GioiTinh = nv.GioiTinh,
                                   SoDienThoai = nv.SoDienThoai,
                                   MaBaoHiem = nv.MaBaoHiem,
                                   DiaChiThuongChu = nv.DiaChiThuongChu,
                                   DiaChiTamChu = nv.DiaChiTamChu,
                                   TrinhDoHocVan = nv.TrinhDoHocVan,
                                   TenNganHang = nv.TenNganHang,
                                   STKNganHang = nv.STKNganHang,
                                   MaSoThue = nv.MaSoThue,
                                   LoaiNhanVien = nv.LoaiNhanVien,
                                   ChucVu = nv.ChucVu,
                                   YeuCauChamCong = nv.YeuCauChamCong,
                                   NgayBatDauLam = nv.BatDauLamViec,
                                   NgayChinhThucLam = nv.ChinhThucLamViec,
                                   NgayNghiViec = nv.NgayNghiViec,
                                   MailLamViec = nv.MailLamViec,
                                   TrangThai = nv.TrangThai,
                                   PhongBan = pb.TenPhongBan,
                               };

            return await employeeInfo.FirstOrDefaultAsync();
        }

        public async Task<string> LayMaPhongBanTuTenAsync(string tenPhongBan)
        {
            try
            {
                var maPhongBan = await _dbContext.PhongBans
                    .Where(pb => pb.TenPhongBan == tenPhongBan)
                    .Select(pb => pb.MaPhongBan)
                    .FirstOrDefaultAsync();

                if (maPhongBan == null)
                {
                    throw new Exception("Không tìm thấy mã phòng ban cho tên phòng ban đã nhập.");
                }

                return maPhongBan;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Đã xảy ra một ngoại lệ: " + ex.Message);
                return null;
            }
        }

        public async Task<string> LayTenPhongBanTuMa(string maPhongBan)
        {
            var tenPhongBan = await _dbContext.PhongBans
                .Where(pb => pb.MaPhongBan == maPhongBan)
                .Select(pb => pb.TenPhongBan)
                .FirstOrDefaultAsync();

            return tenPhongBan;
        }

        public async Task ThemMoiNhanVienAsync(NhanVien nhanVien)
        {
            _dbContext.NhanViens.Add(nhanVien);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<string>> LoadDanhSachPhongBan()
        {
            return await _dbContext.PhongBans.Select(pb => pb.TenPhongBan).ToListAsync();
        }

        public async Task<List<NhanVienInfo>> GetNhanVienPhongBanInfo()
        {
            try
            {
                var nhanVienPhongBanInfo = await _dbContext.NhanViens
                    .Join(_dbContext.PhongBans,
                        nv => nv.MaPhongBan,
                        pb => pb.MaPhongBan,
                        (nv, pb) => new
                        {
                            NhanVien = nv,
                            PhongBan = pb
                        })
                    .Select(item => new NhanVienInfo
                    {
                        MaNV = item.NhanVien.MaNV,
                        HoTen = item.NhanVien.HoTen,
                        ChucVu = item.NhanVien.ChucVu,
                        PhongBan = item.PhongBan.TenPhongBan,
                        MailLamViec = item.NhanVien.MailLamViec,
                        TrangThai = item.NhanVien.TrangThai
                    })
                    .ToListAsync();

                return nhanVienPhongBanInfo;
            }
            catch (Exception ex)
            {
                throw new Exception("Error fetching NhanVienPhongBanInfo: " + ex.Message);
            }
        }

        public async Task<int> GetTotalRecordsNhanVien()
        {
            return await _dbContext.NhanViens.CountAsync();
        }

        public async Task<List<NhanVienInfo>> GetNhanVienPaged(int skip, int take)
        {
            return await _dbContext.NhanViens
                .Join(_dbContext.PhongBans,
                    nv => nv.MaPhongBan,
                    pb => pb.MaPhongBan,
                        (nv, pb) => new
                        {
                            NhanVien = nv,
                            PhongBan = pb
                        })
                .Select(item => new NhanVienInfo
                {
                    MaNV = item.NhanVien.MaNV,
                    HoTen = item.NhanVien.HoTen,
                    ChucVu = item.NhanVien.ChucVu,
                    PhongBan = item.PhongBan.TenPhongBan,
                    MailLamViec = item.NhanVien.MailLamViec,
                    TrangThai = item.NhanVien.TrangThai
                })
                .OrderBy(nv => nv.MaNV)
                .Skip(skip)
                .Take(take)
                .ToListAsync();
        }

        public async Task<List<NhanVienInfo>> GetNhanVienByMaNV(string maNV)
        {
            return await _dbContext.NhanViens
                .Where(nv => nv.MaNV == maNV)
                .Join(_dbContext.PhongBans,
                        nv => nv.MaPhongBan,
                        pb => pb.MaPhongBan,
                            (nv, pb) => new
                            {
                                NhanVien = nv,
                                PhongBan = pb
                            })
                .Select(item => new NhanVienInfo
                {
                    MaNV = item.NhanVien.MaNV,
                    HoTen = item.NhanVien.HoTen,
                    ChucVu = item.NhanVien.ChucVu,
                    PhongBan = item.PhongBan.TenPhongBan,
                    MailLamViec = item.NhanVien.MailLamViec,
                    TrangThai = item.NhanVien.TrangThai
                })
                .ToListAsync();
        }

        public async Task<List<NhanVienInfo>> GetNhanVienByHoTen(string hoTen)
        {
            return await _dbContext.NhanViens
                .Where(nv => nv.HoTen.Contains(hoTen))
                .Join(_dbContext.PhongBans,
                    nv => nv.MaPhongBan,
                    pb => pb.MaPhongBan,
                    (nv, pb) => new NhanVienInfo
                    {
                        MaNV = nv.MaNV,
                        HoTen = nv.HoTen,
                        ChucVu = nv.ChucVu,
                        PhongBan = pb.TenPhongBan,
                        MailLamViec = nv.MailLamViec,
                        TrangThai = nv.TrangThai
                    })
                .ToListAsync();
        }

        public async Task<List<NhanVienInfo>> GetNhanVienByChucVu(string chucVu)
        {
            return await _dbContext.NhanViens
                .Where(nv => nv.ChucVu.Contains(chucVu))
                .Join(_dbContext.PhongBans,
                    nv => nv.MaPhongBan,
                    pb => pb.MaPhongBan,
                    (nv, pb) => new NhanVienInfo
                    {
                        MaNV = nv.MaNV,
                        HoTen = nv.HoTen,
                        ChucVu = nv.ChucVu,
                        PhongBan = pb.TenPhongBan,
                        MailLamViec = nv.MailLamViec,
                        TrangThai = nv.TrangThai
                    })
                .ToListAsync();
        }

        public async Task<List<NhanVienInfo>> GetNhanVienByPhongBan(string phongBan)
        {
            return await _dbContext.NhanViens
                .Join(_dbContext.PhongBans,
                    nv => nv.MaPhongBan,
                    pb => pb.MaPhongBan,
                    (nv, pb) => new { NhanVien = nv, PhongBan = pb })
                .Where(item => item.PhongBan.TenPhongBan.Contains(phongBan))
                .Select(item => new NhanVienInfo
                {
                    MaNV = item.NhanVien.MaNV,
                    HoTen = item.NhanVien.HoTen,
                    ChucVu = item.NhanVien.ChucVu,
                    PhongBan = item.PhongBan.TenPhongBan,
                    MailLamViec = item.NhanVien.MailLamViec,
                    TrangThai = item.NhanVien.TrangThai
                })
                .ToListAsync();
        }

        public async Task<List<NhanVienInfo>> GetNhanVienByTrangThai(string trangThai)
        {
            return await _dbContext.NhanViens
                .Where(nv => nv.TrangThai.Contains(trangThai))
                .Join(_dbContext.PhongBans,
                    nv => nv.MaPhongBan,
                    pb => pb.MaPhongBan,
                    (nv, pb) => new NhanVienInfo
                    {
                        MaNV = nv.MaNV,
                        HoTen = nv.HoTen,
                        ChucVu = nv.ChucVu,
                        PhongBan = pb.TenPhongBan,
                        MailLamViec = nv.MailLamViec,
                        TrangThai = nv.TrangThai
                    })
                .ToListAsync();
        }

        public async Task DeleteNhanVien(string MaNV)
        {
            try
            {
                var nhanvienToDelete = await _dbContext.NhanViens.FirstOrDefaultAsync(tl => tl.MaNV == MaNV);
                if (nhanvienToDelete != null)
                {
                    _dbContext.NhanViens.Remove(nhanvienToDelete);
                    await _dbContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi xóa nhan vien: " + ex.Message);
            }
        }

        public async Task CapNhatThongTinNhanVienAsync(NhanVien nhanVien)
        {
            try
            {
                Console.WriteLine($"Đang tìm kiếm nhân viên với mã: {nhanVien.MaNV}");
                var existingEmployee = await _dbContext.NhanViens.FirstOrDefaultAsync(e => e.MaNV == nhanVien.MaNV);

                if (existingEmployee != null)
                {
                    Console.WriteLine($"Đang cập nhật thông tin cho nhân viên: {existingEmployee.MaNV}");

                    existingEmployee.HoTen = nhanVien.HoTen;
                    existingEmployee.CCCD = nhanVien.CCCD;
                    existingEmployee.NgaySinh = nhanVien.NgaySinh;
                    existingEmployee.GioiTinh = nhanVien.GioiTinh;
                    existingEmployee.SoDienThoai = nhanVien.SoDienThoai;
                    existingEmployee.MaBaoHiem = nhanVien.MaBaoHiem;
                    existingEmployee.DiaChiThuongChu = nhanVien.DiaChiThuongChu;
                    existingEmployee.DiaChiTamChu = nhanVien.DiaChiTamChu;
                    existingEmployee.TrinhDoHocVan = nhanVien.TrinhDoHocVan;
                    existingEmployee.TenNganHang = nhanVien.TenNganHang;
                    existingEmployee.STKNganHang = nhanVien.STKNganHang;
                    existingEmployee.MaSoThue = nhanVien.MaSoThue;
                    existingEmployee.LoaiNhanVien = nhanVien.LoaiNhanVien;
                    existingEmployee.ChucVu = nhanVien.ChucVu;
                    existingEmployee.MaPhongBan = nhanVien.MaPhongBan;
                    existingEmployee.YeuCauChamCong = nhanVien.YeuCauChamCong;
                    existingEmployee.BatDauLamViec = nhanVien.BatDauLamViec;
                    existingEmployee.ChinhThucLamViec = nhanVien.ChinhThucLamViec;
                    existingEmployee.NgayNghiViec = nhanVien.NgayNghiViec;
                    existingEmployee.MailLamViec = nhanVien.MailLamViec;
                    existingEmployee.TrangThai = nhanVien.TrangThai;

                    _dbContext.NhanViens.Update(existingEmployee);
                    await _dbContext.SaveChangesAsync();

                    Console.WriteLine($"Cập nhật thông tin nhân viên thành công: {existingEmployee.MaNV}");
                }
                else
                {
                    Console.WriteLine($"Không tìm thấy nhân viên với mã: {nhanVien.MaNV}");
                    throw new Exception($"Không tìm thấy nhân viên với mã: {nhanVien.MaNV}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi cập nhật thông tin nhân viên: {ex.Message}");
                throw new Exception("Lỗi khi cập nhật thông tin nhân viên: " + ex.Message);
            }
        }

        public Task<List<ThietLap>> GetThietLapPaged(int skip, int take)
        {
            throw new NotImplementedException();
        }
    }
}
