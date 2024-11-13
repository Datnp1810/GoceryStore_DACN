using GoceryStore_DACN.Data;
using GoceryStore_DACN.Entities;
using GroceryStore_DACN.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace GoceryStore_DACN.Repositories
{
    public class LoaiMonAnRepository:ILoaiMonAnRepository
    {
        private readonly ApplicationDbContext _context;

        public LoaiMonAnRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<LoaiMonAn> CreateLoaiMonAn(LoaiMonAn loaiMonAn)
        {
            //Map chế độ ăn về Entity
            await _context.LoaiMonAns.AddAsync(loaiMonAn);
            await _context.SaveChangesAsync();
            return loaiMonAn;

        }

        public async Task<bool> DeleteLoaiMonAn(int id)
        {
            var loaiMonAn = await _context.LoaiMonAns!.FindAsync(id);
            if (loaiMonAn != null)
            {
                _context.LoaiMonAns.Remove(loaiMonAn);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<LoaiMonAn>> GetAllLoaiMonAn()
        {
            return await _context.LoaiMonAns!.ToListAsync();
        }

        public async Task<LoaiMonAn> GetAllLoaiMonAnById(int id)
        {
            return await _context.LoaiMonAns!.FindAsync(id);
        }

        public async Task<LoaiMonAn> UpdateLoaiMonAn(LoaiMonAn loaiMonAn)
        {
            _context.LoaiMonAns.Update(loaiMonAn);
            await _context.SaveChangesAsync();
            return loaiMonAn;
        }
    }
}
