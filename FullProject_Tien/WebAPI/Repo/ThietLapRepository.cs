//using FullProject.Data;
//using FullProject.Interface;
//using FullProject.Model;
//using FullProject.Services;
//using Microsoft.EntityFrameworkCore;

//namespace FullProject.Repo
//{
//    public class ThietLapRepository : IThietLapRepository
//    {
//        private readonly ThietLapContext _context;

//        public ThietLapRepository(ThietLapContext context)
//        {
//            _context = context;
//        }

//        public async Task<List<ThietLap>> GetAllThietLapAsync()
//        {
//            return await _context.ThietLaps.ToListAsync ();
//        }

//        public async Task<List<ThietLap>> GetThietLapByThangTinhCongAsync(string thangTinhCong)
//        {
//            return await _context.ThietLaps
//                                .Where (tl => tl.ThangTinhCong == thangTinhCong)
//                                .ToListAsync ();
//        }

//        public async Task AddThietLapAsync(ThietLap thietLap)
//        {
//            _context.ThietLaps.Add (thietLap);
//            await _context.SaveChangesAsync ();
//        }

//        public async Task UpdateThietLapAsync(ThietLap thietLap)
//        {
//            _context.ThietLaps.Update (thietLap);
//            await _context.SaveChangesAsync ();
//        }

//        public async Task DeleteThietLapAsync(string thangTinhCong)
//        {
//            var thietLap = await _context.ThietLaps.FirstOrDefaultAsync (tl => tl.ThangTinhCong == thangTinhCong);
//            if ( thietLap != null ) {
//                _context.ThietLaps.Remove (thietLap);
//                await _context.SaveChangesAsync ();
//            }
//        }

//        public async Task<List<BangPhatDiMuon>> GetAllPhatMuonAsync()
//        {
//            return await _context.PhatDiMuons.ToListAsync ();
//        }

//        public async Task<List<BangPhatDiMuon>> GetPhatByThangTinhCongAsync(string thangTinhCong)
//        {
//            return await _context.PhatDiMuons
//                                .Where (p => p.ThangTinhCong == thangTinhCong)
//                                .ToListAsync ();
//        }

//        public async Task AddPhatMuonAsync(BangPhatDiMuon phat)
//        {
//            _context.PhatDiMuons.Add (phat);
//            await _context.SaveChangesAsync ();
//        }

//        public async Task UpdatePhatMuonAsync(BangPhatDiMuon phat)
//        {
//            _context.PhatDiMuons.Update (phat);
//            await _context.SaveChangesAsync ();
//        }

//        public async Task DeletePhatMuonAsync(string thangTinhCong)
//        {
//            var phat = await _context.PhatDiMuons.FirstOrDefaultAsync (p => p.ThangTinhCong == thangTinhCong);
//            if ( phat != null ) {
//                _context.PhatDiMuons.Remove (phat);
//                await _context.SaveChangesAsync ();
//            }
//        }

//        public Task<int> GetTotalRecordsAsync()
//        {
//            throw new NotImplementedException ();
//        }

//        public Task<int> GetTotalRecordsPhatMuonAsync()
//        {
//            throw new NotImplementedException ();
//        }

//        public Task<int> GetTotalEmployeesAsync()
//        {
//            throw new NotImplementedException ();
//        }

//        public Task<int> GetTotalPhongBanAsync()
//        {
//            throw new NotImplementedException ();
//        }

//        public Task<int> GetTotalNhanVienChinhThucAsync()
//        {
//            throw new NotImplementedException ();
//        }

//        public Task<int> GetTotalNhanVienHopDongAsync()
//        {
//            throw new NotImplementedException ();
//        }

//        public Task<Dictionary<int, int>> GetTotalEmployeesByYearAsync()
//        {
//            throw new NotImplementedException ();
//        }

//        public Task<int> GetResignedEmployeesCountByYearAsync(int year)
//        {
//            throw new NotImplementedException ();
//        }

//        public Task<int> GetNewEmployeesCountByYearAsync(int year)
//        {
//            throw new NotImplementedException ();
//        }

//        public Task<(int, Dictionary<string, int>)> CountDepartmentsAndEmployeesAsync()
//        {
//            throw new NotImplementedException ();
//        }

//        public Task<List<ThietLapService.ThuongPhatWithNhanVienInfo>> GetThuongAsync()
//        {
//            throw new NotImplementedException ();
//        }

//        public Task<List<ThietLapService.ThuongPhatWithNhanVienInfo>> GetPhatAsync()
//        {
//            throw new NotImplementedException ();
//        }

//        public Task AddThuongPhatAsync(ThuongPhat thuongPhat)
//        {
//            throw new NotImplementedException ();
//        }

//        public Task UpdateThuongPhatAsync(ThuongPhat thuongPhat)
//        {
//            throw new NotImplementedException ();
//        }

//        public Task DeleteThuongPhatAsync(int maThuongPhat)
//        {
//            throw new NotImplementedException ();
//        }

//        public Task<List<ThietLapService.LuongNhanVien>> GetLuongAsync()
//        {
//            throw new NotImplementedException ();
//        }

//        public Task AddLuongAsync(Luong luong)
//        {
//            throw new NotImplementedException ();
//        }

//        public Task UpdateLuongAsync(Luong luong)
//        {
//            throw new NotImplementedException ();
//        }
//    }
//}
    
