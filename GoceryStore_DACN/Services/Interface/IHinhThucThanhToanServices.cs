using GoceryStore_DACN.DTOs;
using GoceryStore_DACN.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GroceryStore_DACN.Repositories.Interface
{
    public interface IHinhThucThanhToanServices
    {
        public Task<IEnumerable<HinhThucThanhToanDTO>> GetAllHinhThucThanhToan();
        public Task<HinhThucThanhToan> GetAllHinhThucThanhToanById(int id);
        public Task<HinhThucThanhToan> CreateHinhThucThanhToan(HinhThucThanhToanDTO hinhThucTT);
        public Task<HinhThucThanhToan> UpdateHinhThucThanhToan(int id, HinhThucThanhToanDTO hinhThucTT);
        public Task<bool> DeleteHinhThucThanhToan(int id);
    }
}
