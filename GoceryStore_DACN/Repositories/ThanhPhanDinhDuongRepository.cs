using GoceryStore_DACN.Data;
using GoceryStore_DACN.Entities;
using GroceryStore_DACN.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace GoceryStore_DACN.Repositories
{
    public class ThanhPhanDinhDuongRepository : IThanhPhanDinhDuongRepository
    {
        private readonly ApplicationDbContext _context;

        public ThanhPhanDinhDuongRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ThanhPhanDinhDuong> CreateThanhPhanDinhDuong(ThanhPhanDinhDuong thanhPhanDD)
        {
            await _context.ThanhPhanDinhDuongs.AddAsync(thanhPhanDD);
            await _context.SaveChangesAsync();
            return thanhPhanDD;
        }

        public async Task<bool> DeleteThanhPhanDinhDuong(int id)
        {
            var thanhPhanDD = await _context.ThanhPhanDinhDuongs!.FindAsync(id);
            if (thanhPhanDD != null)
            {
                _context.ThanhPhanDinhDuongs.Remove(thanhPhanDD);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<ThanhPhanDinhDuong>> GetAllThanhPhanDinhDuong()
        {
            return await _context.ThanhPhanDinhDuongs!.Include(l => l.ThucPham).ToListAsync();
        }

        public async Task<ThanhPhanDinhDuong> GetAllThanhPhanDinhDuongById(int id)
        {
            return await _context.ThanhPhanDinhDuongs!.Include(l => l.ThucPham).FirstOrDefaultAsync(tp => tp.ID_ThucPham == id);
        }

        public async Task<ThanhPhanDinhDuong> GetAllThanhPhanDinhDuongByIdSongSong(int id, ApplicationDbContext db)
        {
            return await db.ThanhPhanDinhDuongs!.Include(l => l.ThucPham).FirstOrDefaultAsync(tp => tp.ID_ThucPham == id);
        }

        public async Task<ThanhPhanDinhDuong> UpdateThanhPhanDinhDuong(ThanhPhanDinhDuong thanhPhanDD)
        {
            _context.ThanhPhanDinhDuongs.Update(thanhPhanDD);
            await _context.SaveChangesAsync();
            return thanhPhanDD;
        }
    }
}
