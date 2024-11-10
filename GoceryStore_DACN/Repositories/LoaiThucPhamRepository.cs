using GoceryStore_DACN.Data;
using GoceryStore_DACN.Entities;
using GroceryStore_DACN.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace GoceryStore_DACN.Repositories
{
    public class LoaiThucPhamRepository : ILoaiThucPhamRepository
    {
        private readonly ApplicationDbContext _context;

        public LoaiThucPhamRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<LoaiThucPham> CreateLoaiThucPham(LoaiThucPham loaiThucPham)
        {
            //Map chế độ ăn về Entity
            await _context.LoaiThucPhams.AddAsync(loaiThucPham);
            await _context.SaveChangesAsync();
            return loaiThucPham;

        }

        public async Task<bool> DeleteLoaiThucPham(int id)
        {
            var loaiThucPham = await _context.LoaiThucPhams!.FindAsync(id);
            if (loaiThucPham != null)
            {
                _context.LoaiThucPhams.Remove(loaiThucPham);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<LoaiThucPham>> GetAllLoaiThucPham()
        {
            return await _context.LoaiThucPhams!.ToListAsync();
        }

        public async Task<LoaiThucPham> GetAllLoaiThucPhamById(int id)
        {
            return await _context.LoaiThucPhams!.FindAsync(id);
        }

        public async Task<LoaiThucPham> UpdateLoaiThucPham(LoaiThucPham loaiThucPham)
        {
            _context.LoaiThucPhams.Update(loaiThucPham);
            await _context.SaveChangesAsync();
            return loaiThucPham;
        }
    }
}
