using GoceryStore_DACN.DTOs;
using GoceryStore_DACN.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GoceryStore_DACN.Models.Respones;
using GoceryStore_DACN.Models.Requests;

namespace GroceryStore_DACN.Repositories.Interface
{
    public interface IThucPhamServices
    {
        public IEnumerable<ThucPhamResponse> GetAllThucPhamCache();
        public List<ThucPhamResponse> GetDanhSachMuaNgay(ThucDonNgayResponse thucDonNgay);
        public List<ThucPhamResponse> GetDanhSachMuaTuan(ThucDonTuanResponse thucDonTuan);
        public ThucPhamResponse GetAllThucPhamByIDCache(int id);
        public Task<(IEnumerable<ThucPhamResponse> thucPham, int totalItems)> GetAllThucPhamPhanTrang(string search, int pageNumber, int pageSize, string sortColumn, string sortOrder);

        public Task<List<ThucPhamResponse>> FillterByIDLoaiThucPham(int id);
        public Task<List<ThucPhamGroupDTO>> GroupThucPhamByLTP();

        public Task<ThucPham> GetAllThucPhamById(int id);
        public Task<(List<ThucPhamResponse>, int totalItems)> GetAllThucPhamByLoaiThucPham(int id, string search, int pageNumber, int pageSize, string sortColumn, string sortOrder);
        public Task<ThucPham> CreateThucPham(ThucPhamDTO thucPhamDTO);
        public Task<ThucPham> UpdateThucPham(int id, ThucPhamDTO thucPhamDTO);
        public Task<bool> DeleteThucPham(int id);
    }
}
