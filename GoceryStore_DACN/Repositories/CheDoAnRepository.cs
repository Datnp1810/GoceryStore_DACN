using AutoMapper;
using GoceryStore_DACN.Data;
using GoceryStore_DACN.DTOs;
using GoceryStore_DACN.Entities;
using GroceryStore_DACN.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace GoceryStore_DACN.Repositories
{
    public class CheDoAnRepository : ICheDoAnRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CheDoAnRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public Task<CheDoAn> CreateCheDoAn(CheDoAnDTO cheDoAn)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteCheDoAn(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<CheDoAn>> GetAllCheDoAn()
        {
            throw new NotImplementedException();
        }

        public Task<CheDoAn> GetAllCheDoAnById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<CheDoAn> UpdateCheDoAn(int id, CheDoAnDTO cheDoAn)
        {
            throw new NotImplementedException();
        }
    }
}
