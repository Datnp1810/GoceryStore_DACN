using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GoceryStore_DACN.Entities;

namespace GroceryStore_DACN.Repositories.Interface
{
    public interface IInvoiceRepository
    {
        Task<HoaDon> GetByIdAsync(int id);
        Task<HoaDon> GetByNumberAsync(string number);
        Task<List<HoaDon>> GetAllAsync();
        Task<HoaDon> TaoHoaDonAsync(HoaDon hoaDon);
        Task<HoaDon> UpdateAsync(HoaDon hoaDon);
        Task DeleteAsync(int id); 
        Task<bool> ExistAsync(int id);
        
        Task<List<HoaDon>> GetOverdueInvoiceAsync();
    }
}
