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
        private readonly IHoaDonRepository _hoaDonRepository;
        private readonly IThucPhamRepository _thucPhamRepository;
        private readonly IUserContextService _userContextService;
        public HoaDonService(IHoaDonRepository invoiceRepository, 
            IThucPhamRepository thucPhamRepository,
            IUserContextService userContextService)
        {
            _hoaDonRepository = invoiceRepository;
            _thucPhamRepository = thucPhamRepository;
            _userContextService = userContextService;
        }

       public async Task<HoaDonDTO> CreateInvoiceAsync(CreateHoaDonDto createHoaDonDto)
{
    // Kiểm tra danh sách mặt hàng
            foreach (var item in createHoaDonDto.InvoiceItems)
            {
                var thucPham = await _thucPhamRepository.GetThucPhamById(item.ThucPhamId);
                if (thucPham == null)
                {
                    throw new Exception($"Thực phẩm {item.ThucPhamId} không tồn tại trong database");
                }
                // Kiểm tra số lượng tồn kho
                if (thucPham.SoLuong < item.SoLuong)
                {
                    throw new Exception($"Thực phẩm {thucPham.TenThucPham} không đủ số lượng tồn");
                }
            }

            // Lấy thông tin người dùng
            var userId = _userContextService.GetCurrentUserId();
            if (string.IsNullOrEmpty(userId))
            {
                throw new Exception("User does not exist in database");
            }

            // Chuẩn bị đối tượng hóa đơn
            var hoaDon = new HoaDon
            {
                UserId = userId,
                NgayLap = DateTime.Now,
                NoiNhan = createHoaDonDto.NoiNhan,
                GhiChu = createHoaDonDto.GhiChu,
                ID_TT = 1, // Trạng thái hóa đơn: giỏ hàng
                ID_HinhThuc = createHoaDonDto.ID_HinhThuc,
                TongTien = createHoaDonDto.InvoiceItems.Sum(x => x.SoLuong * x.DonGia),
                CTHoaDons = createHoaDonDto.InvoiceItems.Select(item => new CT_HoaDon
                {
                    ID_ThucPham = item.ThucPhamId,
                    SoLuong = item.SoLuong,
                    DonGia = item.DonGia,
                    ThanhTien = item.SoLuong * item.DonGia
                }).ToList()
            };

            try
            {
                // Gọi repository để tạo hóa đơn (hoặc cập nhật nếu đã tồn tại)
                var createdInvoice = await _hoaDonRepository.TaoHoaDonAsync(hoaDon);

                if (createdInvoice == null)
                {
                    throw new Exception("Lỗi lưu hóa đơn.");
                }

                // Trả về DTO
                return new HoaDonDTO
                {
                    NgayLap = createdInvoice.NgayLap,
                    TongTien = createdInvoice.TongTien,
                    NoiNhan = createdInvoice.NoiNhan,
                    GhiChu = createdInvoice.GhiChu,
                    IdTinhTrang = createdInvoice.ID_TT,
                    IdHinhThucThanhToan = createdInvoice.ID_HinhThuc,
                    UserId = createdInvoice.UserId,
                    
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving invoice: {ex.Message}");
                throw;
            }
        }


        public Task<HoaDonDTO> GetInvoiceByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<HoaDonDTO> UpdateInvoiceAsync(int id)
        {
            var invoiceExist = await _hoaDonRepository.GetByIdAsync(id);
            if(invoiceExist == null)
            {
                throw new Exception("Invoice does not exist");
            }
            try
            {
                var result = await  _hoaDonRepository.UpdateAsync(invoiceExist);
                return new HoaDonDTO
                {
                    NgayLap = result.NgayLap,
                    TongTien = result.TongTien,
                    NoiNhan = result.NoiNhan,
                    GhiChu = result.GhiChu,
                    IdTinhTrang = result.ID_TT,
                    IdHinhThucThanhToan = result.ID_HinhThuc,
                    UserId = result.UserId,
                   
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
            var invoiceExist = await _hoaDonRepository.ExistAsync(id);
            if (!invoiceExist)
            {
                throw new Exception("Invoice does not exist");
            }
            try
            {
                await _hoaDonRepository.DeleteAsync(id);
                return true; 
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error deleting invoice: {e.Message}");
                return false;
            }
        }

        public async Task<HoaDon> CreateHoaDon(HoaDonDTO hoaDon)
        {
            var userId = _userContextService.GetCurrentUserId();

            if (string.IsNullOrEmpty(userId))
            {
                throw new Exception("Chưa Đăng Nhập.");
            }

            // Gán userID vào hoaDon
            hoaDon.UserId = userId;

            var taoHoaDon = await _hoaDonRepository.CreateHoaDon(hoaDon);
            return taoHoaDon;
        }

        public Task<HoaDon> GetGioHang()
        {
            var UserID = _userContextService.GetCurrentUserId();
            var layGioHang = _hoaDonRepository.GetByUserID(UserID);
            return layGioHang;
        }
    }
}
