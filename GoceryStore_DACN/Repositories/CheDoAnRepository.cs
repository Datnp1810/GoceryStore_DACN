using AutoMapper;
using GoceryStore_DACN.Data;
using GoceryStore_DACN.DTOs;
using GoceryStore_DACN.Entities;
using GroceryStore_DACN.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System.Net.WebSockets;

namespace GoceryStore_DACN.Repositories
{
    public class CheDoAnRepository : ICheDoAnRepository
    {
        private readonly ApplicationDbContext _context;

        public CheDoAnRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<CheDoAn> CreateCheDoAn(CheDoAn cheDoAn)
        {
            //Map chế độ ăn về Entity
            await _context.CheDoAns.AddAsync(cheDoAn);
            await _context.SaveChangesAsync();
            return cheDoAn;
            
        }

        public async Task<bool> DeleteCheDoAn(int id)
        {
            var cheDoAn = await _context.CheDoAns!.FindAsync(id);
            if (cheDoAn != null)
            {
                _context.CheDoAns.Remove(cheDoAn);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<CheDoAn>> GetAllCheDoAn()
        {
            return await _context.CheDoAns!.ToListAsync();
        }

        public async Task<CheDoAn> GetAllCheDoAnById(int id)
        {
            return await _context.CheDoAns!.FindAsync(id);
        }

        public async Task<CheDoAn> UpdateCheDoAn(CheDoAn cheDoAn)
        {
            _context.CheDoAns.Update(cheDoAn);
            await _context.SaveChangesAsync();
            return cheDoAn;
        }   
    }
}
