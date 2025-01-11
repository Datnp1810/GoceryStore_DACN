using GoceryStore_DACN.Data;
using GoceryStore_DACN.DTOs;
using GoceryStore_DACN.Entities;
using GoceryStore_DACN.Models.Respones;

namespace GoceryStore_DACN.Repositories.Interface
{
    public interface IChiTietBuoiAnRepository
    {
        public Task<IEnumerable<CT_BuoiAnDTO>> GetAllCT_BuoiAn();
        public IEnumerable<CT_BuoiAnDTO> GetAllCT_BuoiAnCache();
        public Task<List<CT_BuoiAnResponse>> GetAllCT_BuoiAnByIdMonAn(int id);
        public IEnumerable<CT_BuoiAnDTO> GetAllCT_BuoiAnByIdMonAnThreadCache(int id);
        public Task<List<CT_BuoiAn>> CreateCT_BuoiAn(List<CT_BuoiAn> ct_BuoiAn);
        public Task<CT_BuoiAn> UpdateCT_BuoiAn(CT_BuoiAn ct_BuoiAn);
        public Task<bool> DeleteCT_BuoiAn(int ID_ThucPham, int ID_MonAn);
     
    }
}
