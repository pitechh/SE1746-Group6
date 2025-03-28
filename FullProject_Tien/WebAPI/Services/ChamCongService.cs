using WebAPI.Data;
using Core.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Core.Application.Interface;
using static HotChocolate.ErrorCodes;
using Core.Application.DTOs;

namespace WebAPI.Services
{
    public class ChamCongService: IChamCongService
    {
        ChamCongDbContext dbContext = new ChamCongDbContext();
        private readonly ChamCongDbContext _dbContext;
        public ChamCongService()
        {
            _dbContext = dbContext;
        }
        public async Task<List<NhanVien>> GetAllNhanViens()
        {
            try
            {
                return await _dbContext.NhanViens
                    .Select(nv => new NhanVien
                    {
                        MaNV = nv.MaNV ?? string.Empty,
                        HoTen = nv.HoTen ?? string.Empty,
                        // Thêm các trường khác nếu cần, xử lý NULL tương ứng
                    })
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                // Ghi lại lỗi
                throw new Exception("Đã xảy ra lỗi khi lấy danh sách nhân viên.", ex);
            }
        }
        public async Task<List<ChamCong>> GetAllChamCongs()
        {
            return await dbContext.ChamCongs.AsNoTracking().ToListAsync();
        }
        public async Task<List<PhongBan>> GetAllPhongBan()
        {
            return await dbContext.PhongBans.AsNoTracking().ToListAsync();
        }
        public async Task<int> GetTotalRecords()
        {
            return await _dbContext.ChamCongs.CountAsync();
        }

        public async Task<List<ChamCong>> GetChamCongPaged(int skip, int take)
        {
            return await _dbContext.ChamCongs.Skip(skip).Take(take).ToListAsync();
        }

        public string PostChamCongs(List<ChamCongDTO> chamCongs)
        {
            if (chamCongs == null || !chamCongs.Any())
            {
                return "không có";
            }

            var result = new List<ChamCong>();

            foreach(var chamCong in chamCongs)
            {
                result.Add(new ChamCong
                {
                    MaNV = chamCong.MaNV,
                    NgayChamCong = chamCong.NgayChamCong,
                    GioRa = chamCong.GioRa,
                    GioVao = chamCong.GioVao
                });
            }


                _dbContext.ChamCongs.AddRange(result);
                _dbContext.SaveChanges();
                return "thành công";
            
          
        }
    }
}
