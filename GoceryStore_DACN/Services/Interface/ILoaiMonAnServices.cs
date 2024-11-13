using GoceryStore_DACN.DTOs;
using GoceryStore_DACN.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GroceryStore_DACN.Repositories.Interface
{
    public interface ILoaiMonAnServices
    {
        // Định nghĩa các phương thức của interface tại đây
        public Task<IEnumerable<LoaiMonAn>> GetAllLoaiMonAn();
        public Task<LoaiMonAn> GetAllLoaiMonAnById(int id);
        public Task<LoaiMonAn> CreateLoaiMonAn(LoaiMonAnDTO loaiMonAn);
        public Task<LoaiMonAn> UpdateLoaiMonAn(int id, LoaiMonAnDTO loaiMonAn);
        public Task<bool> DeleteLoaiMonAn(int id);
    }
}
