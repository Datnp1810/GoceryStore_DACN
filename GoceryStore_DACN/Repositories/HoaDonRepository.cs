using AutoMapper;
using GoceryStore_DACN.Data;
using GoceryStore_DACN.DTOs;
using GoceryStore_DACN.Entities;
using GoceryStore_DACN.Services.Interface;
using GroceryStore_DACN.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace GoceryStore_DACN.Repositories
{
    public class HoaDonRepository : IHoaDonRepository
    {
       private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public HoaDonRepository(ApplicationDbContext context, IMapper mapper)
        {
          _context = context;
            _mapper = mapper;
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

        public async Task<HoaDon> GetByUserID(string userID)
        {
            var hoaDonUserID = await _context.HoaDons.Include(ct=>ct.CTHoaDons).FirstOrDefaultAsync(x => x.UserId == userID && x.ID_TT == 1);
            if (hoaDonUserID == null)
            {
                return null;
            }
            return hoaDonUserID;
        }

        public Task<List<HoaDon>> GetAllAsync()
        {
            //lấy hết tất cả không phân trang 
            return _context.HoaDons.Include(x => x.CTHoaDons).
                ThenInclude(x => x.ThucPham).ToListAsync();
        }
        public async Task<HoaDon> TaoHoaDonAsync(HoaDon hoaDon)
        {
            // Bắt đầu giao dịch
            await using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // Kiểm tra xem hóa đơn với MaHoaDon và ID_TT = 1 đã tồn tại hay chưa
                var existingInvoice = await _context.HoaDons
                    .Include(x => x.CTHoaDons)
                    .FirstOrDefaultAsync(x => x.MAHD == hoaDon.MAHD && x.ID_TT == 1);

                if (existingInvoice != null)
                {
                    // Hóa đơn tồn tại - cập nhật chi tiết hóa đơn và tổng tiền
                    foreach (var item in hoaDon.CTHoaDons)
                    {
                        var existingDetail = existingInvoice.CTHoaDons?.FirstOrDefault(x => x.ID_ThucPham == item.ID_ThucPham);

                        if (existingDetail != null)
                        {
                            // Nếu chi tiết hóa đơn tồn tại, cập nhật số lượng và thành tiền
                            existingDetail.SoLuong += item.SoLuong;
                            existingDetail.ThanhTien += item.SoLuong * item.DonGia;
                            _context.CTHoaDons.Update(existingDetail);
                        }
                        else
                        {
                            // Nếu chi tiết hóa đơn chưa tồn tại, kiểm tra tồn kho và thêm mới
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

                    // Cập nhật tổng tiền của hóa đơn
                    existingInvoice.TongTien = existingInvoice.CTHoaDons.Sum(x => x.ThanhTien);
                    _context.HoaDons.Update(existingInvoice);
                }
                else
                {
                    // Hóa đơn chưa tồn tại - tạo mới hóa đơn
                    hoaDon.NgayLap = DateTime.Now;
                    hoaDon.ID_TT = 1; // trạng thái 1 là giỏ hàng
                    hoaDon.TongTien = hoaDon.CTHoaDons.Sum(x => x.SoLuong * x.DonGia);

                    await _context.HoaDons.AddAsync(hoaDon);
                    await _context.SaveChangesAsync();

                    // Duyệt qua các chi tiết hóa đơn và xử lý từng chi tiết
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
                        item.ID_HoaDon = hoaDon.MAHD; // Gán mã hóa đơn cho chi tiết hóa đơn

                        await _context.CTHoaDons.AddAsync(item);
                    }

                    // Cập nhật lại tổng tiền sau khi thêm chi tiết hóa đơn
                    hoaDon.TongTien = hoaDon.CTHoaDons.Sum(x => x.ThanhTien);
                    _context.HoaDons.Update(hoaDon);
                }

                // Lưu các thay đổi và commit giao dịch
                await _context.SaveChangesAsync();
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

        public async Task<List<HoaDon>> GetOverdueInvoiceAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<HoaDon> CreateHoaDon(HoaDonDTO hoaDon)
        {
            var HoaDon = new HoaDon
            {
                UserId = hoaDon.UserId,
                NgayLap = hoaDon.NgayLap,
                NoiNhan = hoaDon.NoiNhan,
                GhiChu = hoaDon.GhiChu,
                ID_TT = hoaDon.IdTinhTrang,
                ID_HinhThuc = hoaDon.IdHinhThucThanhToan,
                TongTien = hoaDon.TongTien,
            };
            await _context.HoaDons.AddAsync(HoaDon);
            //Save changes to database 
            await _context.SaveChangesAsync();        
            return HoaDon;
        }
    }
}

