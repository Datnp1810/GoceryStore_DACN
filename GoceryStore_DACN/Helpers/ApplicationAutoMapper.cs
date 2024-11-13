using AutoMapper;
using GoceryStore_DACN.DTOs;
using GoceryStore_DACN.Entities;

namespace GoceryStore_DACN.Helpers
{
    public class ApplicationAutoMapper : Profile
    {
        public ApplicationAutoMapper()
        {
            CreateMap<CheDoAn, CheDoAnDTO>().ReverseMap();
            CreateMap<CT_HoaDon, CT_HoaDonDTO>().ReverseMap();
            CreateMap<HinhThucThanhToan, HinhThucThanhToanDTO>().ReverseMap();
            CreateMap<HoaDon, CreateInvoiceDto>().ReverseMap();
            CreateMap<LoaiMonAn, LoaiMonAnDTO>().ReverseMap();
            CreateMap<LoaiThucPham, LoaiThucPhamDTO>().ReverseMap();
            CreateMap<MonAn, MonAnDTO>().ReverseMap();
            CreateMap<ThanhPhanDinhDuong, ThanhPhanDinhDuongDTO>().ReverseMap();
            CreateMap<ThucPham, ThucPhamDTO>().ReverseMap();
            CreateMap<TinhTrang, TinhTrangDTO>().ReverseMap();
        }
    }
}
