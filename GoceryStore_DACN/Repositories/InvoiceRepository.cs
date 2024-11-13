using GoceryStore_DACN.Data;
using GoceryStore_DACN.Entities;
using GoceryStore_DACN.Services.Interface;
using GroceryStore_DACN.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace GoceryStore_DACN.Repositories
{
    public class InvoiceRepository : IInvoiceRepository
    {
       private readonly ApplicationDbContext _context;
        public InvoiceRepository(ApplicationDbContext context)
        {
          _context = context;
        }
        public async Task<HoaDon> GetByIdAsync(int id)
        {
            return await _context.HoaDons.Include(x => x.CTHoaDons).
                ThenInclude(x => x.ThucPham).
                FirstOrDefaultAsync(x => x.MAHD == id);
        }

        public Task<HoaDon> GetByNumberAsync(string number)
        {
            throw new NotImplementedException();
        }

        public Task<List<HoaDon>> GetAllAsync()
        {
            //lấy hết tất cả không phân trang 
            return _context.HoaDons.Include(x => x.CTHoaDons).
                ThenInclude(x => x.ThucPham).ToListAsync();
        }

        public async Task<HoaDon> CreateAsync(HoaDon hoaDon)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                await _context.HoaDons.AddAsync(hoaDon);
                var maHD = hoaDon.MAHD;
                if(hoaDon.CTHoaDons != null && hoaDon.CTHoaDons.Any())
                {
                    foreach (var item in hoaDon.CTHoaDons)
                    {
                        item.ID_HoaDon = maHD;
                        await _context.CTHoaDons.AddAsync(item);
                    }
                    await _context.SaveChangesAsync();
                }
                await transaction.CommitAsync();
                return hoaDon;
            }
            catch (Exception e)
            {
                await transaction.RollbackAsync();
                throw new Exception(e.Message);
            }
        }

        public async Task<HoaDon> UploadAsync(HoaDon hoaDon)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(int id)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var details = await _context.CTHoaDons.Where(x => x.ID_HoaDon == id).ToListAsync();
                _context.CTHoaDons.RemoveRange(details);

                //Delete invoice 
                var invoice = await _context.HoaDons.FindAsync(id);
                if (invoice != null)
                {
                   _context.HoaDons.Remove(invoice);
                }
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

            }
            catch (Exception e)
            {
                await transaction.RollbackAsync();
                throw new Exception(e.Message);
            }
        }

        public Task<bool> ExistAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<HoaDon>> GetOverdueInvoiceAsync()
        {
            throw new NotImplementedException();
        }
    }
}
