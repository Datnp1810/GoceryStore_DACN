using AutoMapper;
using GoceryStore_DACN.Data;
using GoceryStore_DACN.DTOs;
using GoceryStore_DACN.Entities;
using GoceryStore_DACN.Models.Respones;
using GoceryStore_DACN.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace GoceryStore_DACN.Repositories
{
    public class ChiTietBuoiAnRepository : IChiTietBuoiAnRepository
    {
        private readonly  ApplicationDbContext _context;
        private readonly  IMemoryCache _cache;

        public ChiTietBuoiAnRepository(ApplicationDbContext context, IMemoryCache cache)
        {
            _context = context;
            _cache = cache;
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

        public void AddToCache(string cachKey, IEnumerable<CT_BuoiAnDTO> ct_BuoiAnList)
        {
            // Lưu vào cache cho các lần truy vấn tiếp theo
            var cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromMinutes(30)) // Đặt thời gian hết hạn trượt
                .SetAbsoluteExpiration(TimeSpan.FromHours(1))   // Đặt thời gian hết hạn tuyệt đối
                .SetPriority(CacheItemPriority.Normal);         // Cài đặt mức độ ưu tiên
            _cache.Set("CT_BuoiAnTable", ct_BuoiAnList, cacheEntryOptions);
        }

        public IEnumerable<CT_BuoiAnDTO> GetAllCT_BuoiAnCache()
        {
            if (_cache.TryGetValue("CT_BuoiAnTable", out IEnumerable<CT_BuoiAnDTO> ct_BuoiAnList))
            {
                // Nếu có, trả về dữ liệu từ cache
              
                return ct_BuoiAnList;
            }
            // Nếu không có trong cache, lấy dữ liệu từ database
            ct_BuoiAnList = _context.CTBuoiAns!.Include(p => p.MonAn)
                .Include(q => q.ThucPham).Select(s => new CT_BuoiAnDTO
                {
                    ID_MonAn = s.ID_MonAn,
                    ID_ThucPham = s.ID_ThucPham,
                    Gram = s.Gram
                }).ToList();
            AddToCache("CT_BuoiAnTable", ct_BuoiAnList);
            return ct_BuoiAnList;
        }
        public IEnumerable<CT_BuoiAnDTO> GetAllCT_BuoiAnByIdMonAnThreadCache(int id)
        {
      
            //Nếu không có MonAn trong cacche
            if (!_cache.TryGetValue("CT_BuoiAnTable", out IEnumerable<CT_BuoiAnDTO> ct_BuoiAnList))
            {
                ct_BuoiAnList = GetAllCT_BuoiAnCache();
            }
   
            // Lọc danh sách MonAn theo ID_LoaiMonAn
            var locTheoID = ct_BuoiAnList.Where(m => m.ID_MonAn == id).ToList();
            return locTheoID;

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



        public Task<CT_BuoiAn> UpdateCT_BuoiAn(CT_BuoiAn ct_BuoiAn)
        {
            throw new NotImplementedException();
        }
        
    }
}
