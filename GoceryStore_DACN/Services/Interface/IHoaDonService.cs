using GoceryStore_DACN.DTOs;
using GoceryStore_DACN.Entities;
using GroceryStore_DACN.Repositories.Interface;

namespace GoceryStore_DACN.Services.Interface
{
    public interface IHoaDonService
    {
        Task<HoaDonDTO> CreateInvoiceAsync(CreateHoaDonDto createHoaDonDto);
        public Task<HoaDon> CreateHoaDon(HoaDonDTO hoaDon);
        Task<HoaDon> GetGioHang();
        Task<HoaDonDTO> GetInvoiceByIdAsync(int id);
        Task<HoaDonDTO> UpdateInvoiceAsync(int id);
        Task<bool> DeleteInvoiceAsync(int id); 
    }
}
