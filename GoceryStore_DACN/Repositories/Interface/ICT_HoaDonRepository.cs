using GoceryStore_DACN.DTOs;
using GoceryStore_DACN.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GroceryStore_DACN.Repositories.Interface
{
    public interface ICT_HoaDonRepository
    {
        // Định nghĩa các phương thức của interface tại đây
        public Task<List<CT_HoaDon>> AddChiTietHoaDon(List<CT_HoaDonDTO> cT_HoaDonDTO);
        public Task<List<CT_HoaDon>> TimHoaDon(int ID_HoaDon);
    }
}
