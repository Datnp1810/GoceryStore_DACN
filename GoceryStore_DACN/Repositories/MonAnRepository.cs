using GoceryStore_DACN.Data;
using GoceryStore_DACN.Entities;
using GoceryStore_DACN.Models.Respones;
using GroceryStore_DACN.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System.Collections;

namespace GoceryStore_DACN.Repositories
{
    public class MonAnRepository:IMonAnRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMemoryCache _cache;

        public MonAnRepository(ApplicationDbContext context, IMemoryCache cache)
        {
            _context = context;
            _cache = cache;
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

        public async Task<List<MonAnResponse>> GetAllMonAn()
        {
            var monAn = await _context.MonAns.Include(tp => tp.LoaiMonAn).Select(s => new MonAnResponse
            {
                ID_TenMonAn = s.ID_MonAn,
                TenMonAn = s.TenMonAn,
                ID_LoaiMonAn = s.LoaiMonAn.ID_LoaiMonAn,
                TenLoaiMonAn = s.LoaiMonAn.TenLoaiMonAn
            }).ToListAsync();

            return monAn;
        }

        private void AddToCache(string cacheKey, IEnumerable<MonAnResponse> monAnList)
        {
            // Lưu vào cache cho các lần truy vấn tiếp theo
            var cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromMinutes(30)) // Đặt thời gian hết hạn trượt
                .SetAbsoluteExpiration(TimeSpan.FromHours(1))   // Đặt thời gian hết hạn tuyệt đối
                .SetPriority(CacheItemPriority.NeverRemove);         // Cài đặt mức độ ưu tiên
            _cache.Set("MonAnTable", monAnList, cacheEntryOptions);
            Console.WriteLine("Dữ liệu đã được lưu vào cache.");
        }

        public IEnumerable<MonAnResponse> GetAllMonAnCache()
        {
            if (_cache.TryGetValue("MonAnTable", out IEnumerable<MonAnResponse> monAnList))
            {
                // Nếu có, trả về dữ liệu từ cache
                Console.WriteLine("Dữ liệu lấy từ cache.");
                return monAnList;
            }
            // Nếu không có trong cache, lấy dữ liệu từ database
            Console.WriteLine("Dữ liệu không có trong cache, tải từ database...");
            monAnList = _context.MonAns.Include(tp => tp.LoaiMonAn).Select(s => new MonAnResponse
            {
                ID_TenMonAn = s.ID_MonAn,
                TenMonAn = s.TenMonAn,
                ID_LoaiMonAn = s.LoaiMonAn.ID_LoaiMonAn,
                TenLoaiMonAn = s.LoaiMonAn.TenLoaiMonAn
            }).ToList();
            AddToCache("MonAnTable", monAnList);
            return monAnList;
        }
        public IEnumerable<MonAnResponse> GetAllMonAnByLoaiMonAnThreadCache(int idLoaiMon)
        {
            IEnumerable<MonAnResponse> locTheoLoai = new List<MonAnResponse>();
            //Nếu không có MonAn trong cacche
            if (!_cache.TryGetValue("MonAnTable", out IEnumerable<MonAnResponse> monAnList))
            {
                monAnList = GetAllMonAnCache();
            }
            Console.WriteLine("lấy dữ liệu trong cache để lọc");
            // Lọc danh sách MonAn theo ID_LoaiMonAn
            locTheoLoai = monAnList.Where(m => m.ID_LoaiMonAn == idLoaiMon).ToList();
            return locTheoLoai;

        }

        public async Task<MonAn> GetAllMonAnById(int id)
        {
            
            return await _context.MonAns!.Include(l => l.LoaiMonAn).FirstOrDefaultAsync(tp => tp.ID_MonAn == id);
        }

        public async Task<IEnumerable<MonAnResponse>> GetAllMonAnByLoaiMonAn(string nameLoai)
        {
            var monAn = await _context.MonAns.Include(tp => tp.LoaiMonAn).Where(s => s.LoaiMonAn.TenLoaiMonAn==nameLoai).
                Select(s => new MonAnResponse
            {
                ID_TenMonAn = s.ID_MonAn,
                TenMonAn = s.TenMonAn,
                ID_LoaiMonAn = s.LoaiMonAn.ID_LoaiMonAn,
                TenLoaiMonAn = s.LoaiMonAn.TenLoaiMonAn
            }).ToListAsync();

            return monAn;
        }
       
        public async Task<MonAn> UpdateMonAn(MonAn monAn)
        {
            _context.MonAns.Update(monAn);
            await _context.SaveChangesAsync();
            return monAn;
        }
    }
}
