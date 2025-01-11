using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GoceryStore_DACN.DTOs;
using GoceryStore_DACN.Entities;

namespace GroceryStore_DACN.Repositories.Interface
{
    public interface IHoaDonRepository
    {
        Task<HoaDon> GetByIdAsync(int id);
        Task<HoaDon> GetByUserID(string userID);
        Task<List<HoaDon>> GetAllAsync();
        Task<HoaDon> TaoHoaDonAsync(HoaDon hoaDon);
        public Task<HoaDon> CreateHoaDon(HoaDonDTO hoaDon);
        Task<HoaDon> UpdateAsync(HoaDon hoaDon);
        Task DeleteAsync(int id); 
        Task<bool> ExistAsync(int id);
        Task<List<HoaDon>> GetOverdueInvoiceAsync();
    }
}
