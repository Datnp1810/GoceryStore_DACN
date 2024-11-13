using GoceryStore_DACN.DTOs;
using GoceryStore_DACN.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GroceryStore_DACN.Repositories.Interface
{
    public interface IThanhPhanDinhDuongServices
    {
        public Task<IEnumerable<ThanhPhanDinhDuong>> GetAllThanhPhanDinhDuong();
        public Task<ThanhPhanDinhDuong> GetAllThanhPhanDinhDuongById(int id);
        public Task<ThanhPhanDinhDuong> CreateThanhPhanDinhDuong(ThanhPhanDinhDuong thanhPhanDD);
        public Task<ThanhPhanDinhDuong> UpdateThanhPhanDinhDuong(int id, ThanhPhanDinhDuongDTO thanhPhanDDDTO);
        public Task<bool> DeleteThanhPhanDinhDuong(int id);
    }
 }
