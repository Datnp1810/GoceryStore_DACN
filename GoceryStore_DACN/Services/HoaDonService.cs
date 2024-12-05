using GoceryStore_DACN.DTOs;
using GoceryStore_DACN.Entities;
using GoceryStore_DACN.Services.Interface;
using GroceryStore_DACN.Repositories.Interface;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.IdentityModel.Tokens;

namespace GoceryStore_DACN.Services
{
    public class HoaDonService : IHoaDonService
    {
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IThucPhamRepository _thucPhamRepository;
        private readonly IUserContextService _userContextService;
        public HoaDonService(IInvoiceRepository invoiceRepository, 
            IThucPhamRepository thucPhamRepository,
            IUserContextService userContextService)
        {
            _invoiceRepository = invoiceRepository;
            _thucPhamRepository = thucPhamRepository;
            _userContextService = userContextService;
        }

        public async Task<InvoiceDto> CreateInvoiceAsync(CreateHoaDonDto createHoaDonDto)
        {
            foreach (var item in createHoaDonDto.InvoiceItems)
            {
                var idThucPham = item.ThucPhamId; 
                var thucPham = await _thucPhamRepository.GetThucPhamById(idThucPham);
                if (thucPham == null)
                {
                    throw new Exception($"Thực phẩm {item.ThucPhamId} không tồn tại trong database");
                }
                //Kiểm tra số lượng tồn kho
                if (thucPham.SoLuong < item.SoLuong)
                {
                    throw new Exception($"Thực phẩm {thucPham.TenThucPham} không đủ số lượng tồn");
                }
            }

            //Tạo hóa đơn trước rồi mới tạo chi tiết hóa đơn
            var userId = _userContextService.GetCurrentUserId();
            if (userId.IsNullOrEmpty())
            {
                throw new Exception("User does not exist in database");
            }
            var hoaDon = new HoaDon
            {
                UserId = userId, //assign user id 
                NgayLap = DateTime.Now,
                NoiNhan = createHoaDonDto.NoiNhan,
                GhiChu = createHoaDonDto.GhiChu,
                ID_TT = 1,
                ID_HinhThuc = createHoaDonDto.ID_HinhThuc,
                TongTien = createHoaDonDto.InvoiceItems.Sum(x => x.SoLuong * x.DonGia),
                CTHoaDons = createHoaDonDto.InvoiceItems.Select(x => new CT_HoaDon
                {
                    
                    ID_ThucPham = x.ThucPhamId,
                    SoLuong = x.SoLuong,
                    DonGia = x.DonGia,
                    ThanhTien = x.SoLuong * x.DonGia
                }).ToList()
            };
            try
            {
                //Tạo hóa đơn
                var result = await _invoiceRepository.TaoHoaDonAsync(hoaDon);
                if (result == null)
                {
                    throw new Exception("Lỗi lưu hóa đơn.");
                }
                //Tạo chi tiết hóa đơn
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
              
                Console.WriteLine($"Error saving invoice: {ex.Message}");
                throw;
            }
        }

        public Task<InvoiceDto> GetInvoiceByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<InvoiceDto> UpdateInvoiceAsync(int id)
        {
            var invoiceExist = await _invoiceRepository.GetByIdAsync(id);
            if(invoiceExist == null)
            {
                throw new Exception("Invoice does not exist");
            }
            try
            {
                var result = await _invoiceRepository.UpdateAsync(invoiceExist);
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
            catch (Exception e)
            {
                Console.WriteLine($"Error updating invoice: {e.Message}");
                throw;
            }

        }

        public async Task<bool> DeleteInvoiceAsync(int id)
        {
            var invoiceExist = await _invoiceRepository.ExistAsync(id);
            if (!invoiceExist)
            {
                throw new Exception("Invoice does not exist");
            }
            try
            {
                await _invoiceRepository.DeleteAsync(id);
                return true; 
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error deleting invoice: {e.Message}");
                return false;
            }
        }
    }
}
