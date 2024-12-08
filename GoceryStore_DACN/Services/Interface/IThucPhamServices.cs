using GoceryStore_DACN.DTOs;
using GoceryStore_DACN.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GoceryStore_DACN.Models.Respones;

namespace GroceryStore_DACN.Repositories.Interface
{
    public interface IThucPhamServices
    {
        public Task<List<ThucPhamResponse>> GetAllThucPham();
        public Task<(IEnumerable<ThucPhamResponse> thucPham, int totalItems)> GetAllThucPhamPhanTrang(string search, int pageNumber, int pageSize, string sortColumn, string sortOrder);

        public Task<ThucPham> GetAllThucPhamById(int id);
        public Task<(List<ThucPhamResponse>, int totalItems)> GetAllThucPhamByLoaiThucPham(int id, string search, int pageNumber, int pageSize, string sortColumn, string sortOrder);
        public Task<ThucPham> CreateThucPham(ThucPhamDTO thucPhamDTO);
        public Task<ThucPham> UpdateThucPham(int id, ThucPhamDTO thucPhamDTO);
        public Task<bool> DeleteThucPham(int id);
    }
}
