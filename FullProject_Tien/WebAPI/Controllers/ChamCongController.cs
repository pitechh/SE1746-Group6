using Core.Application.DTOs;
using Core.Application.Interface;
using Core.Domain.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static HotChocolate.ErrorCodes;

namespace WebAPI.Controllers
{


    [Route("api/[controller]")]
    [ApiController]
    public class ChamCongController : ControllerBase
    {
        public readonly IChamCongService _chamCongService;
        public ChamCongController(IChamCongService chamCongService) 
        {
            _chamCongService = chamCongService;
        }

        //lấy ra tất cả nhân viên
        [HttpGet("getNhanViens")]
        public async Task<ActionResult<List<NhanVien>>> GetAllNhanViens()
        {
            var result = await _chamCongService.GetAllNhanViens();
            return Ok(result);
        }

        //lấy ra tất cả chấm công
        [HttpGet("getChamCongs")]
        public async Task<ActionResult<List<ChamCong>>> GetAllChamCongs()
        {
            var result = await _chamCongService.GetAllChamCongs();
            return Ok(result);
        }

        //lấy ra tất cả phòng ban
        [HttpGet("getPhongBans")]
        public async Task<ActionResult<List<PhongBan>>> GetAllPhongBan()
        {
            var result = await _chamCongService.GetAllPhongBan();
            return Ok(result);
        }

        // lấy ra tổng record
        [HttpGet("getTotalRecords")]
        public async Task<ActionResult<int>> GetTotalRecords()
        {
            var result = await _chamCongService.GetTotalRecords();
            return Ok(result);
        }

        //lấy ra chấm công theo phân trang
        [HttpGet("getChamCongPaged/{skip}/{take}")]
        public async Task<ActionResult<List<ChamCong>>> GetChamCongPaged(int skip, int take)
        {
            var result = await _chamCongService.GetChamCongPaged(skip, take); 
            return Ok(result);
        }

        [HttpPost("postChamCongs")]
        public IActionResult AddChamCongs(List<ChamCongDTO> chamCongs)
        {
            if (chamCongs == null || !chamCongs.Any())
            {
                return BadRequest("Danh sách chấm công không hợp lệ.");
            }

            try
            {
                _chamCongService.PostChamCongs(chamCongs);
                return Ok("Dữ liệu đã được thêm thành công.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi trong quá trình thêm dữ liệu: {ex.Message}");
            }
        }

    }
}
