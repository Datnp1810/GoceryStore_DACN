using AutoMapper;
using GoceryStore_DACN.Data;
using GoceryStore_DACN.DTOs;
using GoceryStore_DACN.Entities;
using GoceryStore_DACN.Services.Interface;
using GroceryStore_DACN.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System.Net.WebSockets;

namespace GoceryStore_DACN.Services
{

    public class CT_HoaDonService : ICT_HoaDonServices
    {
        private readonly ICT_HoaDonRepository _repository;
        private readonly IHoaDonRepository _hoaDonrepository;
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IUserContextService _userContextService;

        public CT_HoaDonService(ICT_HoaDonRepository repository, IMapper mapper,
            IUserContextService userContextService,
            IHoaDonRepository hoaDonRepository,
            ApplicationDbContext context)
        {
            _repository = repository;
            _context = context;
            _mapper = mapper;
            _hoaDonrepository = hoaDonRepository;
            _userContextService = userContextService;
        }
        public async Task<List<CT_HoaDon>> AddChiTietHoaDon(List<CT_HoaDonDTO> cT_HoaDonDTO)
        {
            var ct_hoadon = await _repository.AddChiTietHoaDon(cT_HoaDonDTO);
            return ct_hoadon;
        }

        public async Task<List<CT_HoaDonDTO>> GetGioHang()
        {
            var userId = _userContextService.GetCurrentUserId();
            var timHoaDon = await _hoaDonrepository.GetByUserID(userId); //==1
            if(timHoaDon== null)
            {
                return null;
            }    
            var getListCT_HoaDon = await _repository.TimHoaDon(timHoaDon.MAHD);
            var cthoaDonDTOs = getListCT_HoaDon.Select(entity => new CT_HoaDonDTO
            {
                
                ID_HoaDon = entity.ID_HoaDon,
                ID_ThucPham = entity.ID_ThucPham,
                TenThucPham = entity.ThucPham.TenThucPham,
                SoLuong = entity.SoLuong,
                DonGia = entity.DonGia,
                ThanhTien = entity.ThanhTien
                // Gán các thuộc tính khác tương tự
            }).ToList();
            return cthoaDonDTOs;
        }

        public async Task<bool> removeID_ThucPham(int ID_ThucPham)
        {
            var userID = _userContextService.GetCurrentUserId();

            // Tìm hóa đơn có trạng thái tt == 1
            var timHoaDon = await _hoaDonrepository.GetByUserID(userID);
            if ( timHoaDon == null )
            {
                return false;
            }

            // Lấy danh sách chi tiết hóa đơn
            var getHoaDon = await _repository.TimHoaDon(timHoaDon.MAHD);

            // Kiểm tra sản phẩm trong hóa đơn
            var checkThucPham = getHoaDon.FirstOrDefault(x => x.ID_ThucPham == ID_ThucPham);
            if ( checkThucPham != null )
            {
                // Cập nhật thông tin
                _context.CTHoaDons.Remove(checkThucPham);
                await _context.SaveChangesAsync();

                timHoaDon.TongTien = timHoaDon.CTHoaDons.Sum(x => x.ThanhTien);
                _context.HoaDons.Update(timHoaDon);
                await _context.SaveChangesAsync();
                // Chuyển đổi sang DTO
                return true;
            }
            return false;
        }

        public async Task<List<CT_HoaDon>> TaoGioHang(List<CT_HoaDonDTO> ct_HoaDonDTO)
        {
            var userId = _userContextService.GetCurrentUserId();

            //tìm hóa đơn của USERID cùng với ID_Tình Trạng ==1 là giỏ hàng
            var timHoaDon = await _hoaDonrepository.GetByUserID(userId);

            //Chưa có giỏ hàng, tạo hóa đơn mới
            if ( timHoaDon == null )
            {
                var tongTien = ct_HoaDonDTO.Sum(item => item.DonGia * item.SoLuong *0.001);
                timHoaDon = new HoaDon
                {
                    UserId = userId,
                    NgayLap = DateTime.Now,
                    TongTien = tongTien,
                    HoTen = " ",
                    SoDienThoai = " ",
                    NoiNhan = "",
                    GhiChu = "",
                    ID_HinhThuc = 1,
                    ID_TT = 1
                };

                //Thêm hóa đơn vào cơ sở dữ liệu
                _context.Add(timHoaDon);
                await _context.SaveChangesAsync();
            }


            //Có giỏ hàng
            //Tìm danh sách hóa đơn trong CT_HoaDon
            var getHoaDon = await _repository.TimHoaDon(timHoaDon.MAHD);
            foreach ( var item in ct_HoaDonDTO )
            {
                var checkThucPham = getHoaDon.FirstOrDefault(x => x.ID_ThucPham == item.ID_ThucPham);
                if ( checkThucPham != null )
                {
                    //Nếu có thì cập nhật số lượng
                    checkThucPham.SoLuong += item.SoLuong;
                    checkThucPham.ThanhTien += item.SoLuong * item.DonGia * 0.001;
                    //Cập nhật vào database của CT_HoaDon
                    _context.Update(checkThucPham);
                }
                else
                {
                    var thucPham = await _context.ThucPhams.FindAsync(item.ID_ThucPham);
                    if ( thucPham == null )
                    {
                        throw new Exception($"Thực phẩm {item.ID_ThucPham} không tồn tại.");
                    }
                    if ( thucPham.SoLuong < item.SoLuong )
                    {
                        throw new Exception($"Thực phẩm {thucPham.TenThucPham} không đủ số lượng tồn.");
                    }

                    thucPham.SoLuong -= item.SoLuong;
                    _context.ThucPhams.Update(thucPham);
                    //Nếu không có thì tạo mới
                    var newDongMoi = new CT_HoaDon
                    {
                        ID_HoaDon = timHoaDon.MAHD,
                        ID_ThucPham = item.ID_ThucPham,
                        SoLuong = item.SoLuong,
                        DonGia = item.DonGia,
                        ThanhTien = item.SoLuong * item.DonGia *0.001,
                    };
                    //Add vào database
                    await _context.AddAsync(newDongMoi);
                }
            }
            timHoaDon.TongTien = timHoaDon.CTHoaDons.Sum(x => x.ThanhTien);
            _context.HoaDons.Update(timHoaDon);
            await _context.SaveChangesAsync();
            return await _repository.TimHoaDon(timHoaDon.MAHD);
        }

        public async Task<HoaDonDTO> thanhToan(ThanhToanDTO thanhToanDTO)
        {
            var userID = _userContextService.GetCurrentUserId();

            // Tìm hóa đơn có trạng thái tt == 1
            var timHoaDon = await _hoaDonrepository.GetByUserID(userID);
            if ( timHoaDon == null )
            {
                return null;
            }

            timHoaDon.HoTen = thanhToanDTO.HoTen;
            timHoaDon.SoDienThoai = thanhToanDTO.SoDienThoai;
            timHoaDon.NoiNhan = thanhToanDTO.NoiNhan;
            timHoaDon.NgayLap = DateTime.Now;
            timHoaDon.GhiChu = thanhToanDTO.GhiChu;
            timHoaDon.ID_HinhThuc = thanhToanDTO.ID_HinhThuc;
            timHoaDon.ID_TT = thanhToanDTO.ID_TinhTrang;
            _context.HoaDons.Update(timHoaDon);
            await _context.SaveChangesAsync();

            return new HoaDonDTO
            {
                IdTinhTrang = timHoaDon.ID_TT,
                IdHinhThucThanhToan = timHoaDon.ID_HinhThuc,
                HoTen = timHoaDon.HoTen,
                SoDienThoai = timHoaDon.SoDienThoai,
                UserId = timHoaDon.UserId,
                NgayLap = timHoaDon.NgayLap,
                TongTien = timHoaDon.TongTien,
                NoiNhan = timHoaDon.NoiNhan,
                GhiChu = timHoaDon.GhiChu
            };
        }

        public async Task<CT_HoaDonDTO> updateSoLuong(int ID_ThucPham, double soLuong)
        {
            var userID = _userContextService.GetCurrentUserId();

            // Tìm hóa đơn có trạng thái tt == 1
            var timHoaDon = await _hoaDonrepository.GetByUserID(userID);
            if ( timHoaDon == null )
            {
                return null;
            }

            // Lấy danh sách chi tiết hóa đơn
            var getHoaDon = await _repository.TimHoaDon(timHoaDon.MAHD);

            // Kiểm tra sản phẩm trong hóa đơn
            var checkThucPham = getHoaDon.FirstOrDefault(x => x.ID_ThucPham == ID_ThucPham);
            if ( checkThucPham != null )
            {
                // Cập nhật thông tin
                checkThucPham.SoLuong = soLuong;
                checkThucPham.ThanhTien = soLuong * checkThucPham.DonGia*0.001;
                _context.Update(checkThucPham);

                timHoaDon.TongTien = timHoaDon.CTHoaDons.Sum(x => x.ThanhTien);
                _context.HoaDons.Update(timHoaDon);

                await _context.SaveChangesAsync();
                // Chuyển đổi sang DTO
                return new CT_HoaDonDTO
                {
                    ID_HoaDon = checkThucPham.ID_HoaDon,
                    ID_ThucPham = checkThucPham.ID_ThucPham,
                    TenThucPham = checkThucPham.ThucPham?.TenThucPham ?? "Không xác định",
                    SoLuong = checkThucPham.SoLuong,
                    DonGia = checkThucPham.DonGia,
                    ThanhTien = checkThucPham.ThanhTien
                };
            }
            return null;
        }



    }
}
