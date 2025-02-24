using GoceryStore_DACN.Data;
using GoceryStore_DACN.Entities;
using GoceryStore_DACN.Models.Respones;
using GroceryStore_DACN.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace GoceryStore_DACN.Repositories
{
    public class ThanhPhanDinhDuongRepository : IThanhPhanDinhDuongRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMemoryCache _cache;

        public ThanhPhanDinhDuongRepository(ApplicationDbContext context, IMemoryCache cache)
        {
            _context = context;
            _cache = cache;
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



        public void AddToCache(string cacheKey, IEnumerable<ThanhPhanDinhDuong> thanhPhanDinhDuonngList)
        {
            var cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromMinutes(30)) // Đặt thời gian hết hạn trượt
                .SetAbsoluteExpiration(TimeSpan.FromHours(1))   // Đặt thời gian hết hạn tuyệt đối
                .SetPriority(CacheItemPriority.NeverRemove);         // Cài đặt mức độ ưu tiên
            _cache.Set("ThanhPhanDinhDuongTable", thanhPhanDinhDuonngList, cacheEntryOptions);
            Console.WriteLine("Dữ liệu đã được lưu vào cache.");

        }
        
        private static readonly object _cacheLock = new object();
        public IEnumerable<ThanhPhanDinhDuong> GetAllThanhPhanDinhDuongCache()
        {
            if (_cache.TryGetValue("ThanhPhanDinhDuongTable", out IEnumerable<ThanhPhanDinhDuong> thanhPhanDinhDuonngList))
            {
                return thanhPhanDinhDuonngList;
            }
            lock (_cacheLock)
            {
                if (!_cache.TryGetValue("ThanhPhanDinhDuongTable", out thanhPhanDinhDuonngList) )
                {
                    thanhPhanDinhDuonngList = _context.ThanhPhanDinhDuongs!.Include(l => l.ThucPham).ToList();
                    AddToCache("ThanhPhanDinhDuongTable", thanhPhanDinhDuonngList);
                }
            }
           
            return thanhPhanDinhDuonngList;

        }

        public ThanhPhanDinhDuong GetAllThanhPhanDinhDuongByIdThreadCache(int id)
        {   
            //Nếu không có MonAn trong cacche
            if (!_cache.TryGetValue("ThanhPhanDinhDuongTable", out IEnumerable<ThanhPhanDinhDuong> tpDDList))
            {
                tpDDList = GetAllThanhPhanDinhDuongCache();
            }
            Console.WriteLine("lấy dữ liệu trong cache để lọc");
            // Lọc danh sách MonAn theo ID_LoaiMonAn
            var locTheoTen = tpDDList.Where(m => m.ID_ThucPham == id).FirstOrDefault();
            return locTheoTen;
        }

        

        public async Task<ThanhPhanDinhDuong> UpdateThanhPhanDinhDuong(ThanhPhanDinhDuong thanhPhanDD)
        {
            _context.ThanhPhanDinhDuongs.Update(thanhPhanDD);
            await _context.SaveChangesAsync();
            return thanhPhanDD;
        }
    }
}
