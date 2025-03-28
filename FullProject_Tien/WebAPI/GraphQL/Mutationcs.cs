using Core.Application.Interface;
using Core.Domain.Model;
using HotChocolate;

namespace WebAPI.GraphQL
{
    public class Mutation
    {
        public async Task<NhanVien> CreateNhanVien(NhanVienInput input, [Service] IThietLapService thietLapService)
        {
            var nhanVien = new NhanVien
            {
                MaNV = input.MaNV,
                HoTen = input.HoTen,
                NgaySinh = input.NgaySinh,
                CCCD = input.CCCD,
                SoDienThoai = input.SoDienThoai,
                MailLamViec = input.MailLamViec,
                ChucVu = input.ChucVu,
                MaPhongBan = input.MaPhongBan,
                TrangThai = input.TrangThai,
                LoaiNhanVien = input.LoaiNhanVien
            };
            await thietLapService.ThemMoiNhanVienAsync(nhanVien);
            return nhanVien;
        }

        public async Task<bool> DeleteNhanVien(string maNV, [Service] IThietLapService thietLapService)
        {
            await thietLapService.DeleteNhanVien(maNV);
            return true;
        }
    }

    public class NhanVienInput
    {
        public string MaNV { get; set; }
        public string HoTen { get; set; }
        public DateTime NgaySinh { get; set; }
        public string CCCD { get; set; }
        public string SoDienThoai { get; set; }
        public string MailLamViec { get; set; }
        public string ChucVu { get; set; }
        public string MaPhongBan { get; set; }
        public string TrangThai { get; set; }
        public string LoaiNhanVien { get; set; }
    }
}