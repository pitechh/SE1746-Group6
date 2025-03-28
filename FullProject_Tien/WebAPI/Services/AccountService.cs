using Core.Application.Interface;
using Core.Domain.Model;
using WebAPI.Repo;

namespace WebAPI.Services
{
    public class AccountService : IAccountService
    {
        private readonly AccountRepository _accountRepository;

        public AccountService(AccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<bool> TenTaiKhoanExistsAsync(string tenTaiKhoan)
        {
            return await _accountRepository.TenTaiKhoanExistsAsync(tenTaiKhoan);
        }

        public async Task<TaiKhoan> GetAccountByUsernameAsync(string username)
        {
            return await _accountRepository.GetAccountByUsernameAsync(username);
        }

        public async Task RegisterAccountAsync(TaiKhoan account)
        {
            // You can add business logic here before saving
            // For example, validate the account, hash passwords, etc.

            await _accountRepository.RegisterAccountAsync(account);
        }

        public async Task<bool> ManvExistsAsync(string manv)
        {
            return await _accountRepository.ManvExistsAsync(manv);
        }

        public async Task<List<TaiKhoan>> GetAllTaiKhoanAsync()
        {
            return await _accountRepository.GetAllTaiKhoanAsync();
        }

        public async Task DeleteAccountAsync(string maNV)
        {
            // You could add validation logic here before deleting
            await _accountRepository.DeleteAccountAsync(maNV);
        }

        public async Task UpdateAccountAsync(TaiKhoan updatedAccount)
        {
            // Add any business logic for updating accounts here
            await _accountRepository.UpdateAccountAsync(updatedAccount);
        }

        public async Task<TaiKhoan> GetAccountByMaNVAsync(string maNV)
        {
            return await _accountRepository.GetAccountByMaNVAsync(maNV);
        }
    }
}

