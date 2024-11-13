using GoceryStore_DACN.DTOs;
using GoceryStore_DACN.Entities;
using GoceryStore_DACN.Services.Interface;
using GroceryStore_DACN.Repositories.Interface;
using System.Security.Claims;

namespace GoceryStore_DACN.Services
{
    public class InvoiceService : IInvoiceService
    {
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IThucPhamRepository _thucPhamRepository;
        private readonly IUserContextService _userContextService;
        public InvoiceService(IInvoiceRepository invoiceRepository, 
            IThucPhamRepository thucPhamRepository,
            IUserContextService userContextService)
        {
            _invoiceRepository = invoiceRepository;
            _thucPhamRepository = thucPhamRepository;
            _userContextService = userContextService;
        }

        public async Task<InvoiceDto> CreateAsync(CreateInvoiceDto createInvoiceDto)
        {
            // Validation data 
            foreach (var item in createInvoiceDto.InvoiceItems)
            {
                var thucPham = await _thucPhamRepository.GetThucPhamById(item.ThucPhamId);
                if (thucPham == null)
                {
                    throw new Exception($"Thuc pham voi id {item.ThucPhamId} khong ton tai");
                }

                // Kiem tra so luong ton 
                if (thucPham.SoLuong < item.SoLuong)
                {
                    throw new Exception($"Thuc pham {thucPham.TenThucPham} khong du so luong ton");
                }
            }

            var hoaDon = new HoaDon
            {
                UserId = _userContextService.GetCurrentUserId(),
                NgayLap = DateTime.Now,
                NoiNhan = createInvoiceDto.NoiNhan,
                GhiChu = createInvoiceDto.GhiChu,
                ID_TT = 1,
                ID_HinhThuc = 1,
                TongTien = createInvoiceDto.InvoiceItems.Sum(x => x.SoLuong * x.DonGia),
            };

            try
            {
                var result = await _invoiceRepository.CreateAsync(hoaDon);
                if (result == null)
                {
                    throw new Exception("Failed to save invoice.");
                }

                return new InvoiceDto
                {
                    MaHoaDon = result.MAHD,
                    NgayLap = result.NgayLap,
                    TongTien = result.TongTien,
                    NoiNhan = result.NoiNhan,
                    GhiChu = result.GhiChu,
                    IdTinhTrang = result.ID_TT,
                    IdHinhThucThanhToan = result.ID_HinhThuc,
                    UserId = result.UserId,
                    ChiTietHoaDons = result.CTHoaDons.Select(x => new InvoiceDetailDto
                    {
                        ThucPhamId = x.ID_ThucPham,
                        SoLuong = x.SoLuong,
                        DonGia = (int)x.DonGia,
                    }).ToList()
                };
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Error saving invoice: {ex.Message}");
                throw;
            }
        }

        public Task<InvoiceDto> GetInvoiceByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
