using GoceryStore_DACN.Data;
using GoceryStore_DACN.Entities;
using GroceryStore_DACN.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace GoceryStore_DACN.Repositories
{
    public class MonAnRepository:IMonAnRepository
    {
        private readonly ApplicationDbContext _context;

        public MonAnRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<MonAn> CreateMonAn(MonAn monAn)
        {
            await _context.MonAns.AddAsync(monAn);
            await _context.SaveChangesAsync();
            return monAn;
        }

        public async Task<bool> DeleteMonAn(int id)
        {
            var monAn = await _context.MonAns!.FindAsync(id);
            if (monAn != null)
            {
                _context.MonAns.Remove(monAn);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> ExistsByIdAsync(int id)
        {
            return await _context.MonAns?.AnyAsync(tp => tp.ID_MonAn == id);
        }

        public async Task<IEnumerable<MonAn>> GetAllMonAn()
        {
            return await _context.MonAns!.Include(l => l.ID_LoaiMonAn).ToListAsync();
        }

        public async Task<MonAn> GetAllMonAnById(int id)
        {
            return await _context.MonAns!.Include(l => l.ID_LoaiMonAn).FirstOrDefaultAsync(tp => tp.ID_MonAn == id);
        }

        public async Task<MonAn> UpdateMonAn(MonAn monAn)
        {
            _context.MonAns.Update(monAn);
            await _context.SaveChangesAsync();
            return monAn;
        }
    }
}
