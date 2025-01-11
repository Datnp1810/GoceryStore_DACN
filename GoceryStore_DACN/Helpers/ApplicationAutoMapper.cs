using AutoMapper;
using GoceryStore_DACN.DTOs;
using GoceryStore_DACN.Entities;
using GoceryStore_DACN.Models.Respones;

namespace GoceryStore_DACN.Helpers
{
    public class ApplicationAutoMapper : Profile
    {
        public ApplicationAutoMapper()
        {
            CreateMap<CheDoAn, CheDoAnDTO>().ReverseMap();
            CreateMap<CT_HoaDon, CT_HoaDonDTO>().ReverseMap();
            CreateMap<HinhThucThanhToan, HinhThucThanhToanDTO>().ReverseMap();
            CreateMap<HoaDon, CreateHoaDonDto>().ReverseMap();
            CreateMap<HoaDon, HoaDonDTO>().ReverseMap();
            CreateMap<LoaiMonAn, LoaiMonAnDTO>().ReverseMap();
            CreateMap<LoaiThucPham, LoaiThucPhamDTO>().ReverseMap();
            CreateMap<MonAn, MonAnDTO>().ReverseMap();
            CreateMap<ThanhPhanDinhDuong, ThanhPhanDinhDuongDTO>().ReverseMap();
            CreateMap<ThucPham, ThucPhamDTO>().ReverseMap();
            CreateMap<TinhTrang, TinhTrangDTO>().ReverseMap();
            CreateMap<ThucPham, ThucPhamResponse>().ReverseMap();
        }
    }
}
