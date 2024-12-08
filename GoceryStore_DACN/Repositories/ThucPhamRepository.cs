using AutoMapper;
using GoceryStore_DACN.Data;
using GoceryStore_DACN.Entities;
using GoceryStore_DACN.Models.Respones;
using GroceryStore_DACN.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System.Collections.Immutable;
using System.Net.WebSockets;

namespace GoceryStore_DACN.Repositories
{
    public class ThucPhamRepository : IThucPhamRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ThucPhamRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        public async Task<(IEnumerable<ThucPhamResponse> thucPham, int totalItems)> GetAllThucPhamPhanTrang(string search, int pageNumber, int pageSize, string sortColumn, string sortOrder)
        {
            var query = _context.ThucPhams.Include(p => p.LoaiThucPham).AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(p => p.TenThucPham.ToLower().Contains(search.ToLower()));
            }


            switch (sortColumn.ToLower())
            {
                case "TenSanPham":
                    query = sortOrder == "desc" ? query.OrderByDescending(p => p.TenThucPham) : query.OrderBy(p => p.TenThucPham);
                    break;
                default:
                    query = sortOrder == "desc" ? query.OrderByDescending(p => p.ID_ThucPham) : query.OrderBy(p => p.ID_ThucPham);
                    break;
            }

            var totalItems = await query.CountAsync();
            var pagedQuery = query.Skip((pageNumber - 1) * pageSize).Take(pageSize);
            var sanPhamEntities = await pagedQuery.ToListAsync();
            var thucPhamResponse = sanPhamEntities.Select(s => new ThucPhamResponse
            {
                ID_ThucPham = s.ID_ThucPham,
                TenThucPham = s.TenThucPham,
                GiaBan = s.GiaBan,
                SoLuong = s.SoLuong,
                Image = s.Image,
                ID_LoaiThucPham = s.LoaiThucPham.ID_LoaiThucPham,
                TenLoaiThucPham = s.LoaiThucPham.TenLoaiThucPham
            }).ToList();
            return (thucPhamResponse, totalItems);
        }
        public async Task<(List<ThucPhamResponse>, int totalItems)> GetThucPhamByLoaiThucPham(int id, string search, int pageNumber, int pageSize, string sortColumn, string sortOrder)
        {
            var query = _context.ThucPhams.Include(p => p.LoaiThucPham).Where(tp => tp.LoaiThucPham.ID_LoaiThucPham == id).AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(p => p.TenThucPham.ToLower().Contains(search.ToLower()));
            }


            switch (sortColumn.ToLower())
            {
                case "TenSanPham":
                    query = sortOrder == "desc" ? query.OrderByDescending(p => p.TenThucPham) : query.OrderBy(p => p.TenThucPham);
                    break;
                default:
                    query = sortOrder == "desc" ? query.OrderByDescending(p => p.ID_ThucPham) : query.OrderBy(p => p.ID_ThucPham);
                    break;
            }

            var totalItems = await query.CountAsync();
            var pagedQuery = query.Skip((pageNumber - 1) * pageSize).Take(pageSize);
            var sanPhamEntities = await pagedQuery.ToListAsync();
            var thucPhams = sanPhamEntities.Select(s => new ThucPhamResponse
            {
                ID_ThucPham = s.ID_ThucPham,
                TenThucPham = s.TenThucPham,
                GiaBan = s.GiaBan,
                SoLuong = s.SoLuong,
                Image = s.Image,
                ID_LoaiThucPham = s.LoaiThucPham.ID_LoaiThucPham,
                TenLoaiThucPham = s.LoaiThucPham.TenLoaiThucPham
            }).ToList();
            return (thucPhams, totalItems);
        }


        public async Task<ThucPham> CreateThucPham(ThucPham thucPham)
        {
            await _context.ThucPhams.AddAsync(thucPham);
            await _context.SaveChangesAsync();
            return thucPham;
        }

        public async Task<bool> DeleteThucPham(int id)
        {
            var thucPham = await _context.ThucPhams!.FindAsync(id);
            if (thucPham != null)
            {
                _context.ThucPhams.Remove(thucPham);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
        

        public async Task<bool> ExistsByIdAsync(int id)
        {
            return await _context.ThucPhams?.AnyAsync(tp => tp.ID_ThucPham == id);
        }

        public async Task<List<ThucPhamResponse>> GetAllThucPham()
        {
            var thucPhams = await _context.ThucPhams.Include(tp => tp.LoaiThucPham).Select(s => new ThucPhamResponse
            {
                ID_ThucPham = s.ID_ThucPham,
                TenThucPham = s.TenThucPham,
                GiaBan = s.GiaBan,
                SoLuong = s.SoLuong,
                Image = s.Image,
                ID_LoaiThucPham = s.LoaiThucPham.ID_LoaiThucPham,
                TenLoaiThucPham = s.LoaiThucPham.TenLoaiThucPham
            })
                .ToListAsync();

            return thucPhams;
        }

        public async Task<ThucPham> GetThucPhamById(int id)
        {
            return await _context.ThucPhams.Include(x => x.LoaiThucPham).FirstOrDefaultAsync(x => x.ID_ThucPham == id);
        }


        public async Task<ThucPham> UpdateThucPham(ThucPham thucPham)
        {
            _context.ThucPhams.Update(thucPham);
            await _context.SaveChangesAsync();
            return thucPham;
        }
    }
}
