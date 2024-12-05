using GoceryStore_DACN.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GoceryStore_DACN.Models.Respones;

namespace GroceryStore_DACN.Repositories.Interface
{
    public interface IThucPhamRepository
    {
        // Định nghĩa các phương thức của interface tại đây
        public Task<List<ThucPhamResponse>> GetAllThucPham();
        public Task<(IEnumerable<ThucPhamResponse> thucPham, int totalItems)> GetAllThucPhamPhanTrang(string search, int pageNumber, int pageSize, string sortColumn, string sortOrder);
        public Task<ThucPham> GetThucPhamById(int id);
        public Task<List<ThucPhamResponse>> GetThucPhamByLoaiThucPham(int id);
        public Task<ThucPham> CreateThucPham(ThucPham thucPham);
        public Task<ThucPham> UpdateThucPham(ThucPham thucPham);
        public Task<bool> DeleteThucPham(int id);
        public Task<bool> ExistsByIdAsync(int id);
    }
}
