using GoceryStore_DACN.DTOs;
using GoceryStore_DACN.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GroceryStore_DACN.Repositories.Interface
{
    public interface ICT_HoaDonServices
    {
        // Định nghĩa các phương thức của interface tại đây
        public Task<List<CT_HoaDon>> AddChiTietHoaDon(List<CT_HoaDonDTO> cT_HoaDonDTO);
        public Task<List<CT_HoaDon>> TaoGioHang(List<CT_HoaDonDTO> ct_HoaDonDTO);
        public Task<List<CT_HoaDonDTO>> GetGioHang();

        public Task<CT_HoaDonDTO> updateSoLuong(int ID_ThucPham, double soLuong);

        public Task<bool> removeID_ThucPham(int id);
        public Task<HoaDonDTO> thanhToan(ThanhToanDTO thanhToanDTO);

    }
}
