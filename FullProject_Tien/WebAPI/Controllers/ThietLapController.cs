using WebAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Core.Domain.Model;
using WebAPI.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Application.Interface;
using System.Data;
using Core.Application.DTOs;
using System.Web;



namespace APIController.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ThietLapController : ControllerBase
    {
        private readonly IThietLapService _thietLapService;

        public ThietLapController(IThietLapService thietLapService)
        {
            _thietLapService = thietLapService;
        }

        // Lấy tất cả thiết lập
        [HttpGet("thietlap")]
        public async Task<ActionResult<List<ThietLap>>> GetAllThietLap()
        {
            var result = await _thietLapService.GetAllThietLap();
            return Ok(result);
        }

        // Lấy thiết lập theo tháng tính công
        [HttpGet("thietlap/{thangTinhCong}")]
        public async Task<ActionResult<List<ThietLap>>> GetThietLapByThangTinhCong(string thangTinhCong)
        {
            var result = await _thietLapService.GetThietLapByThangTinhCongAsync(thangTinhCong);
            return Ok(result);
        }


        [HttpGet("thangtinhcong")]
        public async Task<ActionResult<List<string>>> GetThangTinhCongValues()
        {
            var result = await _thietLapService.GetThangTinhCongValuesAsync();
            return Ok(result);
        }


        // Lấy danh sách tháng tính công được thiết lập
        [HttpGet("thangtinhcongthietlap")]
        public async Task<ActionResult<List<ThangTinhCongDuocThiepLap>>> GetThangTinhCong()
        {
            var result = await _thietLapService.GetThangTinhCong();
            return Ok(result);
        }

        // Lấy danh sách tháng tính công được thiết lập (phân trang)
        [HttpGet("thangtinhcongpaged/{skip}/{take}")]
        public async Task<ActionResult<List<ThangTinhCongDuocThiepLap>>> GetThangTinhCongPaged(int skip, int take)
        {
            var result = await _thietLapService.GetThangTinhCongPaged(skip, take);
            return Ok(result);
        }

        // Lấy tổng số bản ghi tháng tính công
        [HttpGet("totalrecordsthangtinhcong")]
        public async Task<ActionResult<int>> GetTotalRecordsThangTinhCong()
        {
            var result = await _thietLapService.GetTotalRecordsThangTinhCong();
            return Ok(result);
        }


        // Lấy thông tin nhân viên
        [HttpGet("nhanvien/{maNV}")]
        public async Task<ActionResult<NhanVienInfo>> GetEmployeeInfo(string maNV)
        {
            var result = await _thietLapService.GetEmployeeInfo(maNV);
            return Ok(result);
        }

        // Lấy mã phòng ban từ tên
        [HttpGet("laymaphongban/{tenPhongBan}")]
        public async Task<ActionResult<string>> LayMaPhongBanTuTen(string tenPhongBan)
        {
            var result = await _thietLapService.LayMaPhongBanTuTenAsync(tenPhongBan);
            return Ok(result);
        }

        // Lấy tên phòng ban từ mã
        [HttpGet("laytenphongban/{maPhongBan}")]
        public async Task<ActionResult<string>> LayTenPhongBanTuMa(string maPhongBan)
        {
            var result = await _thietLapService.LayTenPhongBanTuMa(maPhongBan);
            return Ok(result);
        }


        public class NhanVienDTO
        {
            public string HoTen { get; set; }
            public DateTime NgaySinh { get; set; }
            public string GioiTinh { get; set; }
            public string CCCD { get; set; }
            public string SoDienThoai { get; set; }
            public string MaBaoHiem { get; set; }
            public string DiaChiThuongChu { get; set; }
            public string DiaChiTamChu { get; set; }
            public string TrinhDoHocVan { get; set; }
            public string TenNganHang { get; set; }
            public string STKNganHang { get; set; }
            public string MaSoThue { get; set; }
            public string MaNV { get; set; }
            public string MailLamViec { get; set; }
            public string LoaiNhanVien { get; set; }
            public string ChucVu { get; set; }
            public string MaPhongBan { get; set; }
            public string YeuCauChamCong { get; set; }
            public DateTime BatDauLamViec { get; set; }
            public DateTime ChinhThucLamViec { get; set; }
            public DateTime? NgayNghiViec { get; set; }
            public string TrangThai { get; set; }
        }


        // Thêm mới nhân viên
        [HttpPost("nhanvien")]
        public async Task<ActionResult> ThemMoiNhanVien([FromBody] NhanVienDTO nhanVienDTO)
        {
            if (nhanVienDTO == null)
            {
                return BadRequest("Dữ liệu nhân viên không hợp lệ.");
            }

            NhanVien newNhanVien = new NhanVien
            {
                HoTen = nhanVienDTO.HoTen,
                NgaySinh = nhanVienDTO.NgaySinh,
                GioiTinh = nhanVienDTO.GioiTinh,
                CCCD = nhanVienDTO.CCCD,
                SoDienThoai = nhanVienDTO.SoDienThoai,
                MaBaoHiem = nhanVienDTO.MaBaoHiem,
                DiaChiThuongChu = nhanVienDTO.DiaChiThuongChu,
                DiaChiTamChu = nhanVienDTO.DiaChiTamChu,
                TrinhDoHocVan = nhanVienDTO.TrinhDoHocVan,
                TenNganHang = nhanVienDTO.TenNganHang,
                STKNganHang = nhanVienDTO.STKNganHang,
                MaSoThue = nhanVienDTO.MaSoThue,
                MaNV = nhanVienDTO.MaNV,
                MailLamViec = nhanVienDTO.MailLamViec,
                LoaiNhanVien = nhanVienDTO.LoaiNhanVien,
                ChucVu = nhanVienDTO.ChucVu,
                MaPhongBan = nhanVienDTO.MaPhongBan,
                YeuCauChamCong = nhanVienDTO.YeuCauChamCong,
                BatDauLamViec = nhanVienDTO.BatDauLamViec,
                ChinhThucLamViec = nhanVienDTO.ChinhThucLamViec,
                NgayNghiViec = nhanVienDTO.NgayNghiViec,
                TrangThai = nhanVienDTO.TrangThai
            };

            await _thietLapService.ThemMoiNhanVienAsync(newNhanVien);

            return Ok("Nhân viên được thêm mới thành công.");
        }


        // Lấy danh sách phòng ban
        [HttpGet("danhsachphongban")]
        public async Task<ActionResult<List<string>>> LoadDanhSachPhongBan()
        {
            var result = await _thietLapService.LoadDanhSachPhongBan();
            return Ok(result);
        }

        // Lấy thông tin nhân viên và phòng ban
        [HttpGet("nhanvienphongbaninfo")]
        public async Task<ActionResult<List<NhanVienInfo>>> GetNhanVienPhongBanInfo()
        {
            var result = await _thietLapService.GetNhanVienPhongBanInfo();
            return Ok(result);
        }

        // Lấy tổng số bản ghi nhân viên
        [HttpGet("totalrecordsnhanvien")]
        public async Task<ActionResult<int>> GetTotalRecordsNhanVien()
        {
            var result = await _thietLapService.GetTotalRecordsNhanVien();
            return Ok(result);
        }

        // Lấy danh sách nhân viên (phân trang)
        [HttpGet("nhanvienpaged/{skip}/{take}")]
        public async Task<ActionResult<List<NhanVienInfo>>> GetNhanVienPaged(int skip, int take)
        {
            var result = await _thietLapService.GetNhanVienPaged(skip, take);
            return Ok(result);
        }

        // Tìm kiếm nhân viên theo mã
        [HttpGet("nhanvienbymanv/{maNV}")]
        public async Task<ActionResult<List<NhanVienInfo>>> GetNhanVienByMaNV(string maNV)
        {
            var result = await _thietLapService.GetNhanVienByMaNV(maNV);
            return Ok(result);
        }

        // Tìm kiếm nhân viên theo họ tên
        [HttpGet("nhanvienbyhoten/{hoTen}")]
        public async Task<ActionResult<List<NhanVienInfo>>> GetNhanVienByHoTen(string hoTen)
        {
            var result = await _thietLapService.GetNhanVienByHoTen(hoTen);
            return Ok(result);
        }

        // Tìm kiếm nhân viên theo chức vụ
        [HttpGet("nhanvienbychucvu/{chucVu}")]
        public async Task<ActionResult<List<NhanVienInfo>>> GetNhanVienByChucVu(string chucVu)
        {
            var result = await _thietLapService.GetNhanVienByChucVu(chucVu);
            return Ok(result);
        }

        // Tìm kiếm nhân viên theo phòng ban
        [HttpGet("nhanvienbyphongban/{phongBan}")]
        public async Task<ActionResult<List<NhanVienInfo>>> GetNhanVienByPhongBan(string phongBan)
        {
            var result = await _thietLapService.GetNhanVienByPhongBan(phongBan);
            return Ok(result);
        }

        // Tìm kiếm nhân viên theo trạng thái
        [HttpGet("nhanvienbytrangthai/{trangThai}")]
        public async Task<ActionResult<List<NhanVienInfo>>> GetNhanVienByTrangThai(string trangThai)
        {
            var result = await _thietLapService.GetNhanVienByTrangThai(trangThai);
            return Ok(result);
        }

        // Xóa nhân viên
        [HttpDelete("nhanvien/{maNV}")]
        public async Task<ActionResult> DeleteNhanVien(string maNV)
        {
            await _thietLapService.DeleteNhanVien(maNV);
            return Ok();
        }

        public class EmployeeUpdateDto
        {
            public string MaNV { get; set; }
            public string HoTen { get; set; }
            public string CCCD { get; set; }
            public DateTime NgaySinh { get; set; }
            public string GioiTinh { get; set; }
            public string SoDienThoai { get; set; }
            public string MaBaoHiem { get; set; }
            public string DiaChiThuongChu { get; set; }
            public string DiaChiTamChu { get; set; }
            public string TrinhDoHocVan { get; set; }
            public string TenNganHang { get; set; }
            public string STKNganHang { get; set; }
            public string MaSoThue { get; set; }
            public string LoaiNhanVien { get; set; }
            public string ChucVu { get; set; }
            public string MaPhongBan { get; set; }
            public string YeuCauChamCong { get; set; }
            public DateTime BatDauLamViec { get; set; }
            public DateTime ChinhThucLamViec { get; set; }
            public DateTime? NgayNghiViec { get; set; }
            public string MailLamViec { get; set; }
            public string TrangThai { get; set; }
        }


        // Cập nhật thông tin nhân viên
        [HttpPut("nhanvien")]
        public async Task<IActionResult> CapNhatThongTinNhanVien([FromBody] EmployeeUpdateDto nhanVienDto)
        {
            if (nhanVienDto == null)
            {
                return BadRequest(new { message = "Dữ liệu nhân viên không hợp lệ" });
            }

            // Tạo đối tượng NhanVien từ EmployeeUpdateDto
            var nhanVien = new NhanVien
            {
                MaNV = nhanVienDto.MaNV,
                HoTen = nhanVienDto.HoTen,
                CCCD = nhanVienDto.CCCD,
                NgaySinh = nhanVienDto.NgaySinh,
                GioiTinh = nhanVienDto.GioiTinh,
                SoDienThoai = nhanVienDto.SoDienThoai,
                MaBaoHiem = nhanVienDto.MaBaoHiem,
                DiaChiThuongChu = nhanVienDto.DiaChiThuongChu,
                DiaChiTamChu = nhanVienDto.DiaChiTamChu,
                TrinhDoHocVan = nhanVienDto.TrinhDoHocVan,
                TenNganHang = nhanVienDto.TenNganHang,
                STKNganHang = nhanVienDto.STKNganHang,
                MaSoThue = nhanVienDto.MaSoThue,
                LoaiNhanVien = nhanVienDto.LoaiNhanVien,
                ChucVu = nhanVienDto.ChucVu,
                MaPhongBan = nhanVienDto.MaPhongBan,
                YeuCauChamCong = nhanVienDto.YeuCauChamCong,
                BatDauLamViec = nhanVienDto.BatDauLamViec,
                ChinhThucLamViec = nhanVienDto.ChinhThucLamViec,
                NgayNghiViec = nhanVienDto.NgayNghiViec,
                MailLamViec = nhanVienDto.MailLamViec,
                TrangThai = nhanVienDto.TrangThai
            };

            try
            {
                await _thietLapService.CapNhatThongTinNhanVienAsync(nhanVien);
                return Ok(new { message = "Cập nhật thông tin nhân viên thành công" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Lỗi khi cập nhật nhân viên: {ex.Message}" });
            }
        }



        // Thêm mới thiết lập
        [HttpPost("thietlap")]
        public async Task<ActionResult> AddThietLap([FromBody] ThietLap thietLap)
        {
            await _thietLapService.AddThietLap(thietLap);
            return Ok();
        }


        // Cập nhật thiết lập
        [HttpPut("thietlap")]
        public async Task<ActionResult> UpdateThietLap([FromBody] ThietLap thietLap)
        {
            await _thietLapService.UpdateThietLap(thietLap);
            return Ok();
        }

        // Xóa thiết lập
        [HttpDelete("thietlap/{thangTinhCong}")]
        public async Task<ActionResult> DeleteThietLap(string thangTinhCong)
        {
            await _thietLapService.DeleteThietLap(thangTinhCong);
            return Ok();
        }

        //Tong record
        [HttpGet("totalrecords")]
        public async Task<ActionResult<int>> GetTotalRecords()
        {
            var result = await _thietLapService.GetTotalRecords();
            return Ok(result);
        }

        // Lấy tất cả phạt muộn
        [HttpGet("phatmuon")]
        public async Task<ActionResult<List<BangPhatDiMuon>>> GetAllPhatMuon()
        {
            var result = await _thietLapService.GetAllPhatMuon();
            return Ok(result);
        }

        [HttpGet("thietlappaged/{skip}/{take}")]
        public async Task<ActionResult<List<ThietLap>>> GetThietLapPaged(int skip, int take)
        {
            var result = await _thietLapService.GetThietLapPaged(skip, take);
            return Ok(result);
        }

        // Lấy phạt muộn theo tháng tính công
        [HttpGet("phatmuon/{thangTinhCong}")]
        public async Task<ActionResult<List<BangPhatDiMuon>>> GetPhatByThangTinhCong(string thangTinhCong)
        {

            var result = await _thietLapService.GetPhatByThangTinhCong(thangTinhCong);
            return Ok(result);
        }

        // Thêm mới phạt muộn
        [HttpPost("phatmuon")]
        public async Task<ActionResult> AddPhat([FromBody] PhatMuonDto phatDto)
        {
            var phat = new BangPhatDiMuon
            {
                ThangTinhCong = phatDto.ThangTinhCong,
                SoGioTinhMuon = phatDto.SoGioTinhMuon,
                SoTienPhatMuon = phatDto.SoTienPhatMuon,
                SoTienPhatNghiLam = phatDto.SoTienPhatNghiLam
            };


            await _thietLapService.AddPhat(phat);
            return Ok();
        }

        public class PhatMuonDto
        {
            public string ThangTinhCong { get; set; }
            public int SoGioTinhMuon { get; set; }
            public decimal SoTienPhatMuon { get; set; }
            public decimal SoTienPhatNghiLam { get; set; }
        }

        // Cập nhật phạt muộn
        [HttpPut("phatmuon")]
        public async Task<ActionResult> UpdatePhat([FromBody] PhatMuonDto phatDto)
        {
            var phat = new BangPhatDiMuon
            {
                ThangTinhCong = phatDto.ThangTinhCong,
                SoGioTinhMuon = phatDto.SoGioTinhMuon,
                SoTienPhatMuon = phatDto.SoTienPhatMuon,
                SoTienPhatNghiLam = phatDto.SoTienPhatNghiLam
            };

            await _thietLapService.UpdatePhat(phat);
            return Ok();
        }

        // Xóa phạt muộn
        [HttpDelete("phatmuon/{thangTinhCong}")]
        public async Task<ActionResult> DeletePhat(string thangTinhCong)
        {
            await _thietLapService.DeletePhat(thangTinhCong);
            return Ok();
        }

        [HttpGet("totalrecordsphatmuon")]
        public async Task<ActionResult<int>> GetTotalRecordsPhatMuon()
        {
            var result = await _thietLapService.GetTotalRecordsPhatMuon();
            return Ok(result);
        }

        [HttpGet("phatmuonpaged/{skip}/{take}")]
        public async Task<ActionResult<List<BangPhatDiMuon>>> GetPhatMuonPaged(int skip, int take)
        {
            var result = await _thietLapService.GetPhatMuonPaged(skip, take);
            return Ok(result);
        }

        // Lấy tổng số nhân viên
        [HttpGet("totalemployees")]
        public async Task<ActionResult<int>> GetTotalEmployees()
        {
            var result = await _thietLapService.GetTotalEmployees();
            return Ok(result);
        }

        // Lấy tổng số phòng ban
        [HttpGet("totalphongban")]
        public async Task<ActionResult<int>> GetTotalPhongBan()
        {
            var result = await _thietLapService.GetTotalPhongBan();
            return Ok(result);
        }

        // Lấy tổng số nhân viên chính thức
        [HttpGet("totalnhanvienchinhthuc")]
        public async Task<ActionResult<int>> GetTotalNhanVienChinhThuc()
        {
            var result = await _thietLapService.GetTotalNhanVienChinhThuc();
            return Ok(result);
        }

        // Lấy tổng số nhân viên hợp đồng
        [HttpGet("totalnhanvienhopdong")]
        public async Task<ActionResult<int>> GetTotalNhanVienHopDong()
        {
            var result = await _thietLapService.GetTotalNhanVienHopDong();
            return Ok(result);
        }

        // Lấy số lượng nhân viên theo năm
        [HttpGet("totalemployeesbyyear")]
        public async Task<ActionResult<Dictionary<int, int>>> GetTotalEmployeesByYear()
        {
            var result = await _thietLapService.GetTotalEmployeesByYear();
            return Ok(result);
        }

        // Lấy số lượng nhân viên nghỉ việc theo năm
        [HttpGet("resignedemployeesbyyear/{year}")]
        public async Task<ActionResult<int>> GetResignedEmployeesCountByYear(int year)
        {
            var result = await _thietLapService.GetResignedEmployeesCountByYear(year);
            return Ok(result);
        }

        // Lấy số lượng nhân viên mới theo năm
        [HttpGet("newemployeesbyyear/{year}")]
        public async Task<ActionResult<int>> GetNewEmployeesCountByYear(int year)
        {
            var result = await _thietLapService.GetNewEmployeesCountByYear(year);
            return Ok(result);
        }

        // Lấy số lượng phòng ban và nhân viên
        [HttpGet("countdepartmentsandemployees")]
        public async Task<ActionResult<DepartmentEmployeeStats>> CountDepartmentsAndEmployees()
        {
            var result = await _thietLapService.CountDepartmentsAndEmployees();
            return Ok(result);
        }


        // Lấy danh sách thưởng
        [HttpGet("thuong")]
        public async Task<ActionResult<List<ThuongPhatWithNhanVienInfo>>> GetThuong()
        {
            var result = await _thietLapService.GetThuongAsync();
            return Ok(result);
        }

        // Lấy danh sách phạt
        [HttpGet("phat")]
        public async Task<ActionResult<List<ThuongPhatWithNhanVienInfo>>> GetPhat()
        {
            var result = await _thietLapService.GetPhatAsync();
            return Ok(result);
        }

        // Thêm thưởng/phạt
        [HttpPost("thuongphat")]
        public async Task<ActionResult> AddThuongPhat([FromBody] ThuongPhat thuongPhat)
        {
            await _thietLapService.AddThuongPhatAsync(thuongPhat.MaNV, thuongPhat.Ngay, thuongPhat.NguonThuongPhat, thuongPhat.SoTien, thuongPhat.Loai);
            return Ok();
        }

        // Cập nhật thưởng/phạt
        [HttpPut("thuongphat")]
        public async Task<ActionResult> UpdateThuongPhat([FromBody] ThuongPhat thuongPhat)
        {
            await _thietLapService.CapNhatThuongPhatAsync(thuongPhat.MaThuongPhat, thuongPhat.MaNV, thuongPhat.Ngay, thuongPhat.NguonThuongPhat, thuongPhat.SoTien, thuongPhat.Loai);
            return Ok();
        }

        // Xóa thưởng/phạt
        [HttpDelete("thuongphat/{maThuongPhat}")]
        public async Task<ActionResult> DeleteThuongPhat(int maThuongPhat)
        {
            await _thietLapService.XoaThuongPhatAsync(maThuongPhat);
            return Ok();
        }

        [HttpGet("totalrecordsthuong")]
        public async Task<ActionResult<int>> GetTotalRecordsThuong()
        {
            var result = await _thietLapService.GetTotalRecordsThuongAsync();
            return Ok(result);
        }

        [HttpGet("thuongpaged/{skip}/{take}")]
        public async Task<ActionResult<List<ThuongPhatWithNhanVienInfo>>> GetThuongPaged(int skip, int take)
        {
            var result = await _thietLapService.GetThuongPagedAsync(skip, take);
            return Ok(result);
        }

        [HttpGet("phatpaged/{skip}/{take}")]
        public async Task<ActionResult<List<ThuongPhatWithNhanVienInfo>>> GetPhatPaged(int skip, int take)
        {
            var result = await _thietLapService.GetPhatPagedAsync(skip, take);
            return Ok(result);
        }

        [HttpGet("totalrecordsphat")]
        public async Task<ActionResult<int>> GetTotalRecordsPhat()
        {
            var result = await _thietLapService.GetTotalRecordsPhatAsync();
            return Ok(result);
        }

        // Lấy danh sách lương
        [HttpGet("luong")]
        public async Task<ActionResult<List<LuongNhanVien>>> GetLuong()
        {
            var result = await _thietLapService.GetLuong();
            return Ok(result);
        }

        // Thêm lương
        [HttpPost("luong")]
        public async Task<ActionResult> AddLuong([FromBody] LuongUpdateDto luong)
        {
            await _thietLapService.AddLuong(luong.MaNV, luong.ThangLinhLuong, luong.LuongCung, luong.Thue);
            return Ok();
        }

        public class LuongUpdateDto
        {
            public string MaNV { get; set; }
            public string ThangLinhLuong { get; set; }
            public decimal LuongCung { get; set; }
            public decimal Thue { get; set; }
        }

        // Cập nhật lương
        [HttpPut("luong")]
        public async Task<ActionResult> UpdateLuong([FromBody] LuongUpdateDto luong)
        {
            await _thietLapService.CapNhatLuong(luong.MaNV, luong.ThangLinhLuong, luong.LuongCung, luong.Thue);
            return Ok();
        }

        [HttpGet("luongpaged/{skip}/{take}")]
        public async Task<ActionResult<List<LuongNhanVien>>> GetLuongPaged(int skip, int take)
        {
            var result = await _thietLapService.GetLuongPaged(skip, take);
            return Ok(result);
        }

        [HttpGet("totalrecordsluong")]
        public async Task<ActionResult<int>> GetTotalRecordsLuong()
        {
            var result = await _thietLapService.GetTotalRecordsLuong();
            return Ok(result);
        }


        // Xuất Excel
        [HttpPost("export-excel")]
        public IActionResult ExportToExcel([FromBody] DataTable dataTable)
        {
            var fileContent = _thietLapService.ExportToExcel(dataTable);
            return File(fileContent, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "LuongNhanVien.xlsx");
        }

        // Kiểm tra chấm công
        [HttpGet("kiemtrachamcong/{maNhanVien}/{ngayChamCong}")]
        public async Task<ActionResult<bool>> KiemTraChamCong(string maNhanVien, DateTime ngayChamCong)
        {
            var result = await _thietLapService.KiemTraChamCong(maNhanVien, ngayChamCong);
            return Ok(result);
        }

        // Kiểm tra nhân viên có lương trong tháng cụ thể
        [HttpGet("kiemtraluongthang/{maNhanVien}/{thangLinhLuong}")]
        public async Task<ActionResult<bool>> KiemTraMaNhanVienCoLuongThangCuThe(string maNhanVien, string thangLinhLuong)
        {
            var result = await _thietLapService.KiemTraMaNhanVienCoLuongThangCuThe(maNhanVien, thangLinhLuong);
            return Ok(result);
        }

        // Kiểm tra mã nhân viên tồn tại
        [HttpGet("kiemtramanhanvien/{maNhanVien}")]
        public async Task<ActionResult<bool>> KiemTraMaNhanVienTonTai(string maNhanVien)
        {
            var result = await _thietLapService.KiemTraMaNhanVienTonTai(maNhanVien);
            return Ok(result);
        }

        // Kiểm tra nhân viên có lương
        [HttpGet("kiemtramanhanviencoluong/{maNhanVien}")]
        public async Task<ActionResult<bool>> KiemTraMaNhanVienCoLuong(string maNhanVien)
        {
            var result = await _thietLapService.KiemTraMaNhanVienCoLuong(maNhanVien);
            return Ok(result);
        }
    }

}

