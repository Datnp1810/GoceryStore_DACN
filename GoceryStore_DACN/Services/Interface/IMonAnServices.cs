using GoceryStore_DACN.DTOs;
using GoceryStore_DACN.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GroceryStore_DACN.Repositories.Interface
{
    public interface IMonAnServices
    {
        public Task<IEnumerable<MonAn>> GetAllMonAn();
        public Task<MonAn> GetAllMonAnById(int id);
        public Task<MonAn> CreateMonAn(MonAnDTO monAnDTO);
        public Task<MonAn> UpdateMonAn(int id, MonAnDTO monAnDTO);
        public Task<bool> DeleteMonAn(int id);
    }
}
