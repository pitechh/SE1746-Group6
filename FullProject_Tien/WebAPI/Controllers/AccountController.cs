using Core.Application.Interface;
using Core.Domain.Model;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebAPI.Services;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        // GET: api/account
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaiKhoan>>> GetAllAccounts()
        {
            var accounts = await _accountService.GetAllTaiKhoanAsync();
            return Ok(accounts);
        }

        // GET: api/account/check-username/{username}
        [HttpGet("check-username/{username}")]
        public async Task<ActionResult<bool>> CheckUsernameExists(string username)
        {
            var exists = await _accountService.TenTaiKhoanExistsAsync(username);
            return Ok(exists);
        }

        // GET: api/account/username/{username}
        [HttpGet("username/{username}")]
        public async Task<ActionResult<TaiKhoan>> GetAccountByUsername(string username)
        {
            var account = await _accountService.GetAccountByUsernameAsync(username);
            if (account == null)
            {
                return NotFound();
            }
            return Ok(account);
        }

        // GET: api/account/check-manv/{manv}
        [HttpGet("check-manv/{manv}")]
        public async Task<ActionResult<bool>> CheckMaNVExists(string manv)
        {
            var exists = await _accountService.ManvExistsAsync(manv);
            return Ok(exists);
        }

        // POST: api/account
        [HttpPost]
        public async Task<ActionResult<TaiKhoan>> CreateAccount([FromBody] TaiKhoan account)
        {
            // Check if username already exists
            if (await _accountService.TenTaiKhoanExistsAsync(account.TenTaiKhoan))
            {
                return Conflict("Tên tài khoản đã tồn tại");
            }

            // Check if MaNV exists
            if (!await _accountService.ManvExistsAsync(account.MaNV))
            {
                return BadRequest("Mã nhân viên không tồn tại");
            }

            await _accountService.RegisterAccountAsync(account);
            return CreatedAtAction(nameof(GetAccountByUsername), new { username = account.TenTaiKhoan }, account);
        }

        // PUT: api/account
        [HttpPut]
        public async Task<IActionResult> UpdateAccount([FromBody] TaiKhoan account)
        {
            await _accountService.UpdateAccountAsync(account);
            return Ok();
        }

        // DELETE: api/account/{manv}
        [HttpDelete("{manv}")]
        public async Task<IActionResult> DeleteAccount(string manv)
        {
            var account = await _accountService.GetAccountByMaNVAsync(manv);
            if (account == null)
            {
                return NotFound();
            }

            await _accountService.DeleteAccountAsync(manv);
            return NoContent();
        }
    }
}
