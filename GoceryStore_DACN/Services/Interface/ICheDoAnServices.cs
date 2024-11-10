using GoceryStore_DACN.DTOs;
using GoceryStore_DACN.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GroceryStore_DACN.Repositories.Interface
{
    public interface ICheDoAnServices
    {
        public Task<IEnumerable<CheDoAn>> GetAllCheDoAn();
        public Task<CheDoAn> GetAllCheDoAnById(int id);
        public Task<CheDoAn> CreateCheDoAn(CheDoAnDTO cheDoAn);
        public Task<CheDoAn> UpdateCheDoAn(int id, CheDoAnDTO cheDoAn);
        public Task<bool> DeleteCheDoAn(int id);
    }
}
