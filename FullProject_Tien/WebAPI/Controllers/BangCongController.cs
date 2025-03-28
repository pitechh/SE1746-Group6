using Core.Application.Interface;
using Core.Domain.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BangCongController : ControllerBase
    {
        private readonly IBangCongService _bangCongService;

        public BangCongController(IBangCongService bangCongService)
        {
            _bangCongService = bangCongService;
        }

        [HttpGet("getAllNhanViens")]
        public async Task<ActionResult<List<NhanVien>>> GetAllNhanViens() 
        {
            var result = await _bangCongService.GetAllNhanViens();
            return Ok(result);
        }

        [HttpGet("getAllBangCongs")]
        public async Task<ActionResult<List<BangCong>>> GetAllBangCongs()
        {
            var result = await _bangCongService.GetAllBangCongs();
            return Ok(result);
        }

        [HttpGet("getAllChamCongs")]
        public async Task<ActionResult<List<ChamCong>>> GetAllChamCongs()
        {
            var result = await _bangCongService.GetAllChamCongs();
            return Ok(result);
        }

        [HttpGet("getAllPhongBans")]
        public async Task<ActionResult<List<PhongBan>>> GetAllPhongBans()
        {
            var result = await _bangCongService.GetAllPhongBans();
            return Ok(result);
        }

        [HttpGet("getAllThietLaps")]
        public async Task<ActionResult<List<ThietLap>>> GetAllThietLap()
        {
            var result = await _bangCongService.GetAllThietLaps();
            return Ok(result);
        }

        [HttpGet("getTotalRecords")]
        public async Task<ActionResult<int>> GetTotalRecords()
        {
            var result = await _bangCongService.GetTotalRecords();
            return Ok(result);
        }

        [HttpGet("getBangCongPaged/{skip}/{take}")]
        public async Task<ActionResult<List<NhanVien>>> GetBangCongPaged(int skip, int take)
        {
            var result = await _bangCongService.GetBangCongPaged(skip, take);
            return Ok(result);
        }
    }
}
