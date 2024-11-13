using GoceryStore_DACN.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GroceryStore_DACN.Repositories.Interface
{
    public interface IHinhThucThanhToanRepository
    {
        public Task<IEnumerable<HinhThucThanhToan>> GetAllHinhThucThanhToan();
        public Task<HinhThucThanhToan> GetAllHinhThucThanhToanById(int id);
        public Task<HinhThucThanhToan> CreateHinhThucThanhToan(HinhThucThanhToan hinhThucTT);
        public Task<HinhThucThanhToan> UpdateHinhThucThanhToan(HinhThucThanhToan hinhThucTT);
        public Task<bool> DeleteHinhThucThanhToan(int id);

    }
}
