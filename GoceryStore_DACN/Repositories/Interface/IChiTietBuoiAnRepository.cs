using GoceryStore_DACN.Entities;

namespace GoceryStore_DACN.Repositories.Interface
{
    public interface IChiTietBuoiAnRepository
    {
        public Task<IEnumerable<CT_BuoiAn>> GetAllCT_BuoiAn();
        public Task<List<CT_BuoiAn>> GetAllCT_BuoiAnById(int id);
        public Task<List<CT_BuoiAn>> CreateCT_BuoiAn(List<CT_BuoiAn> ct_BuoiAn);
        public Task<CT_BuoiAn> UpdateCT_BuoiAn(CT_BuoiAn ct_BuoiAn);
        public Task<bool> DeleteCT_BuoiAn(int ID_ThucPham, int ID_MonAn);
    }
}
