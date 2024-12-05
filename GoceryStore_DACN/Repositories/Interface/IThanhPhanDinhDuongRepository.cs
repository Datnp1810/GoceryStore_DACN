using GoceryStore_DACN.Data;
using GoceryStore_DACN.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GroceryStore_DACN.Repositories.Interface
{
    public interface IThanhPhanDinhDuongRepository
    {
        // Định nghĩa các phương thức của interface tại đây
        public Task<IEnumerable<ThanhPhanDinhDuong>> GetAllThanhPhanDinhDuong();
        public Task<ThanhPhanDinhDuong> GetAllThanhPhanDinhDuongById(int id);
        public Task<ThanhPhanDinhDuong> GetAllThanhPhanDinhDuongByIdSongSong(int id, ApplicationDbContext db);
        public Task<ThanhPhanDinhDuong> CreateThanhPhanDinhDuong(ThanhPhanDinhDuong thanhPhanDD);
        public Task<ThanhPhanDinhDuong> UpdateThanhPhanDinhDuong(ThanhPhanDinhDuong thanhPhanDD);
        public Task<bool> DeleteThanhPhanDinhDuong(int id);
    }
}
