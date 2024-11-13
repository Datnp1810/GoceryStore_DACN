using GoceryStore_DACN.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GroceryStore_DACN.Repositories.Interface
{
    public interface ILoaiMonAnRepository
    {
        public Task<IEnumerable<LoaiMonAn>> GetAllLoaiMonAn();
        public Task<LoaiMonAn> GetAllLoaiMonAnById(int id);
        public Task<LoaiMonAn> CreateLoaiMonAn(LoaiMonAn loaiMonAn);
        public Task<LoaiMonAn> UpdateLoaiMonAn(LoaiMonAn loaiMonAn);
        public Task<bool> DeleteLoaiMonAn(int id);
    }
}
