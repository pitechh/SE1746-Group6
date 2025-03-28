using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Application.DTOs;
using Core.Domain.Model;

namespace Core.Application.Interface
{
    public interface IChamCongService
    {
        Task<List<NhanVien>> GetAllNhanViens();
        Task<List<ChamCong>> GetAllChamCongs();
        Task<List<PhongBan>> GetAllPhongBan();
        Task<int> GetTotalRecords();

        Task<List<ChamCong>> GetChamCongPaged(int skip, int take);

        string PostChamCongs(List<ChamCongDTO> chamCongs);
    }
}
