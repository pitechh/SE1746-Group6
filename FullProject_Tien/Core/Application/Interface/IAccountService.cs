using Core.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Interface
{
    public interface IAccountService
    {
        Task<bool> TenTaiKhoanExistsAsync(string tenTaiKhoan);
        Task<TaiKhoan> GetAccountByUsernameAsync(string username);
        Task RegisterAccountAsync(TaiKhoan account);
        Task<bool> ManvExistsAsync(string manv);
        Task<List<TaiKhoan>> GetAllTaiKhoanAsync();
        Task DeleteAccountAsync(string maNV);
        Task UpdateAccountAsync(TaiKhoan updatedAccount);
        Task<TaiKhoan> GetAccountByMaNVAsync(string maNV);
    }
}

