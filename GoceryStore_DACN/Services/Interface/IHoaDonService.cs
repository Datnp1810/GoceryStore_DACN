using GoceryStore_DACN.DTOs;
using GoceryStore_DACN.Entities;
using GroceryStore_DACN.Repositories.Interface;

namespace GoceryStore_DACN.Services.Interface
{
    public interface IHoaDonService
    {
        Task<InvoiceDto> CreateInvoiceAsync(CreateHoaDonDto createHoaDonDto);
        Task<InvoiceDto> GetInvoiceByIdAsync(int id);
        Task<InvoiceDto> UpdateInvoiceAsync(int id);
        Task<bool> DeleteInvoiceAsync(int id); 
    }
}
