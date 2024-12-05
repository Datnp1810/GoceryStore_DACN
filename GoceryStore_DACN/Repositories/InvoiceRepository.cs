using GoceryStore_DACN.Data;
using GoceryStore_DACN.Entities;
using GoceryStore_DACN.Services.Interface;
using GroceryStore_DACN.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

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
            var invoice = await _context.HoaDons.FirstOrDefaultAsync(x => x.MAHD == id);
            if (invoice == null)
            {
                throw new Exception("Invoice not found");
            }
            return invoice;
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
        public async Task<HoaDon> TaoHoaDonAsync(HoaDon hoaDon)
        {
            await using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // Kiểm tra hóa đơn đã tồn tại
                var existingInvoice = await _context.HoaDons
                    .Include(x => x.CTHoaDons)
                    .FirstOrDefaultAsync(x => x.MAHD == hoaDon.MAHD && x.ID_TT == 1);

                if (existingInvoice != null)
                {
                    foreach (var item in hoaDon.CTHoaDons)
                    {
                        var existingDetail = existingInvoice.CTHoaDons?.FirstOrDefault(x => x.ID_ThucPham == item.ID_ThucPham);
                        if (existingDetail != null)
                        {
                            existingDetail.SoLuong += item.SoLuong;
                            existingDetail.ThanhTien += item.SoLuong * item.DonGia;
                            _context.CTHoaDons.Update(existingDetail);
                        }
                        else
                        {
                            // Kiểm tra tồn kho
                            var thucPham = await _context.ThucPhams.FindAsync(item.ID_ThucPham);
                            if (thucPham == null)
                            {
                                throw new Exception($"Thực phẩm {item.ID_ThucPham} không tồn tại.");
                            }
                            if (thucPham.SoLuong < item.SoLuong)
                            {
                                throw new Exception($"Thực phẩm {thucPham.TenThucPham} không đủ số lượng tồn.");
                            }

                            thucPham.SoLuong -= item.SoLuong;
                            _context.ThucPhams.Update(thucPham);

                            item.ThanhTien = item.SoLuong * item.DonGia;
                            await _context.CTHoaDons.AddAsync(item);
                        }
                    }

                    await _context.SaveChangesAsync();
                }
                else
                {
                    // Tạo hóa đơn mới
                    await _context.HoaDons.AddAsync(hoaDon);
                    await _context.SaveChangesAsync();

                    foreach (var item in hoaDon.CTHoaDons)
                    {
                        // Kiểm tra tồn kho
                        var thucPham = await _context.ThucPhams.FindAsync(item.ID_ThucPham);
                        if (thucPham == null)
                        {
                            throw new Exception($"Thực phẩm {item.ID_ThucPham} không tồn tại.");
                        }
                        if (thucPham.SoLuong < item.SoLuong)
                        {
                            throw new Exception($"Thực phẩm {thucPham.TenThucPham} không đủ số lượng tồn.");
                        }

                        thucPham.SoLuong -= item.SoLuong;
                        _context.ThucPhams.Update(thucPham);

                        item.ThanhTien = item.SoLuong * item.DonGia;
                        await _context.CTHoaDons.AddAsync(item);
                    }

                    await _context.SaveChangesAsync();
                }

                await transaction.CommitAsync();
                return hoaDon;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<HoaDon> UpdateAsync(HoaDon hoaDon)
        {
            await using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var updateInvoice = new HoaDon
                {
                    MAHD = hoaDon.MAHD,
                    NgayLap = hoaDon.NgayLap,
                    TongTien = hoaDon.TongTien,
                    NoiNhan = hoaDon.NoiNhan,
                    GhiChu = hoaDon.GhiChu,
                    UserId = hoaDon.UserId,
                    ID_TT = hoaDon.ID_TT,
                    ID_HinhThuc = hoaDon.ID_HinhThuc
                };
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return updateInvoice;

            }
            catch (Exception e)
            {
                await transaction.RollbackAsync();
                throw new Exception(e.Message);
            }
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

        public async Task<bool> ExistAsync(int id)
        {
            var invoice = await _context.HoaDons.FindAsync(id);
            if (invoice != null)
            {
                return true; 
            }

            return false; 
        }

        public Task<List<HoaDon>> GetOverdueInvoiceAsync()
        {
            throw new NotImplementedException();
        }
    }
}

