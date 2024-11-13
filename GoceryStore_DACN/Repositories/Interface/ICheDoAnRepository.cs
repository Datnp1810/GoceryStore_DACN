using GoceryStore_DACN.DTOs;
using GoceryStore_DACN.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GroceryStore_DACN.Repositories.Interface
{
    public interface ICheDoAnRepository
    {
        // Định nghĩa các phương thức của interface tại đây
        public Task<IEnumerable<CheDoAn>> GetAllCheDoAn();
        public Task<CheDoAn> GetAllCheDoAnById(int id);
        public Task<CheDoAn> CreateCheDoAn(CheDoAn cheDoAn);
        public Task<CheDoAn> UpdateCheDoAn(CheDoAn cheDoAn);
        public Task<bool> DeleteCheDoAn(int id);
    }
}
