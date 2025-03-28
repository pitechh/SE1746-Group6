using Core.Application.Interface;
using Core.Domain.Model;
using HotChocolate.Types;

namespace WebAPI.GraphQL
{
    public class NhanVienType : ObjectType<NhanVien>
    {
        protected override void Configure(IObjectTypeDescriptor<NhanVien> descriptor)
        {
            descriptor.Description("Thông tin nhân viên");

            descriptor.Field(x => x.MaNV)
                .Description("Mã nhân viên");

            descriptor.Field(x => x.HoTen)
                .Description("Họ và tên");

            descriptor.Field(x => x.ChucVu)
                .Description("Chức vụ");

            descriptor.Field("tenPhongBan")
                .Description("Tên phòng ban")
                .Type<StringType>()
                .Resolve(async context =>
                {
                    var nhanVien = context.Parent<NhanVien>();
                    var service = context.Service<IThietLapService>();
                    return await service.LayTenPhongBanTuMa(nhanVien.MaPhongBan);
                });
        }
    }
}