using GoceryStore_DACN.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GroceryStore_DACN.Repositories.Interface
{
    public interface IThucPhamRepository
    {
        // Định nghĩa các phương thức của interface tại đây
        public Task<IEnumerable<ThucPham>> GetAllThucPham();
        public Task<ThucPham> GetAllThucPhamById(int id);
        public Task<ThucPham> CreateThucPham(ThucPham thucPham);
        public Task<ThucPham> UpdateThucPham(ThucPham thucPham);
        public Task<bool> DeleteThucPham(int id);
        public Task<bool> ExistsByIdAsync(int id);
    }
}
