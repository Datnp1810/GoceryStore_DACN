using GoceryStore_DACN.Data;
using GoceryStore_DACN.DTOs;
using GoceryStore_DACN.Entities;
using GoceryStore_DACN.Models.Respones;
using GoceryStore_DACN.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace GoceryStore_DACN.Repositories
{
    public class ChiTietBuoiAnRepository : IChiTietBuoiAnRepository
    {
        private readonly  ApplicationDbContext _context;

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

        public async Task<IEnumerable<CT_BuoiAnDTO>> GetAllCT_BuoiAn()
        {
            var ct_buoian = await _context.CTBuoiAns!.Include(p => p.MonAn)
                .Include(q => q.ThucPham).Select(s => new CT_BuoiAnDTO
                {
                    ID_MonAn = s.ID_MonAn,
                    ID_ThucPham = s.ID_ThucPham,
                    Gram = s.Gram
                }).ToListAsync();
            return ct_buoian;
        }

        public async Task<List<CT_BuoiAnResponse>> GetAllCT_BuoiAnByIdMonAn(int id)
        {
            var buoiAn = await _context.CTBuoiAns.Include(tp => tp.MonAn).Include(t=>t.ThucPham).Where(s => s.MonAn.ID_MonAn== id).
                Select(s => new CT_BuoiAnResponse
                {
                    ID_MonAn = s.ID_MonAn,
                    ID_ThucPham = s.ID_ThucPham,
                    Gram= s.Gram,
                }).ToListAsync();

            return buoiAn;
        }

        public async Task<List<CT_BuoiAnResponse>> GetAllCT_BuoiAnByIdSongSong(int id, ApplicationDbContext db)
        {
            var buoiAn = await db.CTBuoiAns.Include(tp => tp.MonAn).Include(t => t.ThucPham).Where(s => s.MonAn.ID_MonAn == id).
                Select(s => new CT_BuoiAnResponse
                {
                    ID_MonAn = s.ID_MonAn,
                    ID_ThucPham = s.ID_ThucPham,
                    Gram = s.Gram,
                }).ToListAsync();

            return buoiAn;
        }


        public Task<CT_BuoiAn> UpdateCT_BuoiAn(CT_BuoiAn ct_BuoiAn)
        {
            throw new NotImplementedException();
        }
    }
}
