using GoceryStore_DACN.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GroceryStore_DACN.Repositories.Interface
{
    public interface ILoaiThucPhamRepository
    {
        // Định nghĩa các phương thức của interface tại đây
        public Task<IEnumerable<LoaiThucPham>> GetAllLoaiThucPham();
        public Task<LoaiThucPham> GetAllLoaiThucPhamById(int id);
        public Task<LoaiThucPham> CreateLoaiThucPham(LoaiThucPham loaiThucPham);
        public Task<LoaiThucPham> UpdateLoaiThucPham(LoaiThucPham loaiThucPham);
        public Task<bool> DeleteLoaiThucPham(int id);
    }
}
