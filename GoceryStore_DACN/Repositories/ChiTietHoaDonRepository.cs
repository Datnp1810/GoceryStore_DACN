using AutoMapper;
using GoceryStore_DACN.Data;
using GoceryStore_DACN.DTOs;
using GoceryStore_DACN.Entities;
using GroceryStore_DACN.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System.Net.WebSockets;

namespace GoceryStore_DACN.Repositories
{
    public class ChiTietHoaDonRepository : ICT_HoaDonRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ChiTietHoaDonRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<CT_HoaDon>> AddChiTietHoaDon(List<CT_HoaDonDTO> ct_HoaDonDTO) 
        {
            var ct_HoaDon = new List<CT_HoaDon>();
            foreach (var detail in ct_HoaDonDTO)
            {
                var mapCT = _mapper.Map<CT_HoaDon>(detail);
                await _context.CTHoaDons.AddAsync(mapCT);
                ct_HoaDon.Add(mapCT);
            }
            await _context.SaveChangesAsync();
           
            return ct_HoaDon;
        }

        public async Task<List<CT_HoaDon>> TimHoaDon(int ID_HoaDon)
        {
            var findHoaDon = await _context.CTHoaDons.Include(hd =>hd.HoaDon).Include(tp => tp.ThucPham).Where(x => x.ID_HoaDon == ID_HoaDon).ToListAsync();
            return findHoaDon;
        }
    }
}
