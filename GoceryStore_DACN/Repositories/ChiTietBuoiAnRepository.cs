using GoceryStore_DACN.Data;
using GoceryStore_DACN.Entities;
using GoceryStore_DACN.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace GoceryStore_DACN.Repositories
{
    public class ChiTietBuoiAnRepository : IChiTietBuoiAnRepository
    {
        private readonly ApplicationDbContext _context;

        public ChiTietBuoiAnRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<CT_BuoiAn>> CreateCT_BuoiAn(List<CT_BuoiAn> ct_BuoiAn)
        {
            await _context.CTBuoiAns.AddRangeAsync(ct_BuoiAn);
            await _context.SaveChangesAsync();
            return ct_BuoiAn;
        }

        public async Task<bool> DeleteCT_BuoiAn(int ID_ThucPham, int ID_MonAn)
        {
            var timCT = await _context.CTBuoiAns.FirstOrDefaultAsync(p => p.ID_MonAn == ID_MonAn && p.ID_ThucPham == ID_ThucPham);
            if (timCT != null) 
            {
                _context.CTBuoiAns.Remove(timCT);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
                  
        }

        public async Task<IEnumerable<CT_BuoiAn>> GetAllCT_BuoiAn()
        {
            return await _context.CTBuoiAns!.Include(p=>p.ID_MonAn)
                .Include(l => l.ID_CDA)
                .Include(q=>q.ID_MonAn).ToListAsync();
        }

        public Task<List<CT_BuoiAn>> GetAllCT_BuoiAnById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<CT_BuoiAn> UpdateCT_BuoiAn(CT_BuoiAn ct_BuoiAn)
        {
            throw new NotImplementedException();
        }
    }
}
