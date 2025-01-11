using GoceryStore_DACN.DTOs;
using GoceryStore_DACN.Entities;
using GoceryStore_DACN.Models.Respones;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GroceryStore_DACN.Repositories.Interface
{
    public interface IMonAnServices
    {
        public Task<List<MonAnResponse>> GetAllMonAn();
        public Task<IEnumerable<MonAnResponse>> GetAllMonAnByLoaiMonAn(string nameLoai);
        public Task<MonAn> GetAllMonAnById(int id);
        public Task<MonAn> CreateMonAn(MonAnDTO monAnDTO);
        public Task<MonAn> UpdateMonAn(int id, MonAnDTO monAnDTO);
        public Task<bool> DeleteMonAn(int id);
    }
}
