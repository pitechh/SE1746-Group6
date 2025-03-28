using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Domain.Model;

namespace Core.Application.Interface
{
    public interface IBangCongService
    {
        Task<List<NhanVien>> GetAllNhanViens();
        Task<List<BangCong>> GetAllBangCongs();
        Task<List<ChamCong>> GetAllChamCongs();
        Task<List<PhongBan>> GetAllPhongBans();
        Task<List<ThietLap>> GetAllThietLaps();
        Task<int> GetTotalRecords();
        Task<List<NhanVien>> GetBangCongPaged(int skip, int take);       
        
    }
}
