using GoceryStore_DACN.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GroceryStore_DACN.Repositories.Interface
{
    public interface IMonAnRepository
    {
        public Task<IEnumerable<MonAn>> GetAllMonAn();
        public Task<MonAn> GetAllMonAnById(int id);
        public Task<MonAn> CreateMonAn(MonAn monAn);
        public Task<MonAn> UpdateMonAn(MonAn monAn);
        public Task<bool> DeleteMonAn(int id);
        public Task<bool> ExistsByIdAsync(int id);
    }
}
