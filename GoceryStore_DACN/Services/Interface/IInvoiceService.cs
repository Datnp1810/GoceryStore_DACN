using GoceryStore_DACN.DTOs;
using GoceryStore_DACN.Entities;
using GroceryStore_DACN.Repositories.Interface;

namespace GoceryStore_DACN.Services.Interface
{
    public interface IInvoiceService
    {
        Task<InvoiceDto> CreateAsync(CreateInvoiceDto createInvoiceDto);
        Task<InvoiceDto> GetInvoiceByIdAsync(int id);

    }
}
