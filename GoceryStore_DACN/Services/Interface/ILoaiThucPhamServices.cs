using GoceryStore_DACN.DTOs;
using GoceryStore_DACN.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GroceryStore_DACN.Repositories.Interface
{
    public interface ILoaiThucPhamServices
    {
        public Task<IEnumerable<LoaiThucPham>> GetAllLoaiThucPham();
        public Task<LoaiThucPham> GetAllLoaiThucPhamById(int id);
        public Task<LoaiThucPham> CreateLoaiThucPham(LoaiThucPhamDTO loaiThucPham);
        public Task<LoaiThucPham> UpdateLoaiThucPham(int id, LoaiThucPhamDTO loaiThucPham);
        public Task<bool> DeleteLoaiThucPham(int id);
    }
}
