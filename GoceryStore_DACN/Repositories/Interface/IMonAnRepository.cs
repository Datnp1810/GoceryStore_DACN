using GoceryStore_DACN.Data;
using GoceryStore_DACN.Entities;
using GoceryStore_DACN.Models.Respones;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GroceryStore_DACN.Repositories.Interface
{
    public interface IMonAnRepository
    {
        public Task<List<MonAnResponse>> GetAllMonAn();
        public IEnumerable<MonAnResponse> GetAllMonAnCache();
        public IEnumerable<MonAnResponse> GetAllMonAnByLoaiMonAnThreadCache(int idLoaiMon);
        public Task<IEnumerable<MonAnResponse>> GetAllMonAnByLoaiMonAn(string nameLoai);
        public Task<MonAn> GetAllMonAnById(int id);
        public Task<MonAn> CreateMonAn(MonAn monAn);
        public Task<MonAn> UpdateMonAn(MonAn monAn);
        public Task<bool> DeleteMonAn(int id);
        public Task<bool> ExistsByIdAsync(int id);
    }
}
