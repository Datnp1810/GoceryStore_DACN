using GoceryStore_DACN.Data;
using GoceryStore_DACN.Entities;
using GroceryStore_DACN.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace GoceryStore_DACN.Repositories
{
    public class HinhThucThanhToanRepository :IHinhThucThanhToanRepository
    {
        private readonly ApplicationDbContext _context;

        public HinhThucThanhToanRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<HinhThucThanhToan> CreateHinhThucThanhToan(HinhThucThanhToan hinhThucTT)
        {
            //Map chế độ ăn về Entity
            await _context.HinhThucThanhToans.AddAsync(hinhThucTT);
            await _context.SaveChangesAsync();
            return hinhThucTT;

        }

        public async Task<bool> DeleteHinhThucThanhToan(int id)
        {
            var hinhThucTT = await _context.HinhThucThanhToans!.FindAsync(id);
            if (hinhThucTT != null)
            {
                _context.HinhThucThanhToans.Remove(hinhThucTT);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<HinhThucThanhToan>> GetAllHinhThucThanhToan()
        {
            return await _context.HinhThucThanhToans!.ToListAsync();
        }

        public async Task<HinhThucThanhToan> GetAllHinhThucThanhToanById(int id)
        {
            return await _context.HinhThucThanhToans!.FindAsync(id);
        }

        public async Task<HinhThucThanhToan> UpdateHinhThucThanhToan(HinhThucThanhToan hinhThucTT)
        {
            _context.HinhThucThanhToans.Update(hinhThucTT);
            await _context.SaveChangesAsync();
            return hinhThucTT;
        }
    }
}
