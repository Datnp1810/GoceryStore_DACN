using AutoMapper;
using GoceryStore_DACN.DTOs;
using GoceryStore_DACN.Entities;
using GroceryStore_DACN.Repositories.Interface;

namespace GoceryStore_DACN.Services
{
    public class ThanhPhanDinhDuongService : IThanhPhanDinhDuongServices
    {
        private readonly IThanhPhanDinhDuongRepository _repository;
        private readonly IThucPhamRepository _thucPhamrepository;
        private readonly IMapper _mapper;

        public ThanhPhanDinhDuongService(IThanhPhanDinhDuongRepository repository, IThucPhamRepository thucPhamrepository, IMapper mapper)
        {
            _repository = repository;
            _thucPhamrepository = thucPhamrepository;
            _mapper = mapper;
        }
        public async Task<ThanhPhanDinhDuong> CreateThanhPhanDinhDuong(ThanhPhanDinhDuong thanhPhanDD)
        {
            var checkTP = await _thucPhamrepository.ExistsByIdAsync(thanhPhanDD.ID_ThucPham);
            if (!checkTP) 
            {
                return null;
            }

            var addTPDD = await _repository.CreateThanhPhanDinhDuong(thanhPhanDD);
            return addTPDD;
        }

        public async Task<bool> DeleteThanhPhanDinhDuong(int id)
        {
            var delete = await _repository.DeleteThanhPhanDinhDuong(id);
            if (delete == false)
            {
                return false;
            }
            return true;
        }

        public async Task<IEnumerable<ThanhPhanDinhDuong>> GetAllThanhPhanDinhDuong()
        {
            var thanhPhanDDs = await _repository.GetAllThanhPhanDinhDuong();
            return thanhPhanDDs.ToList();
        }

        public async Task<ThanhPhanDinhDuong> GetAllThanhPhanDinhDuongById(int id)
        {
            var thanhPhanDD = await _repository.GetAllThanhPhanDinhDuongById(id);
            return thanhPhanDD;
        }

        public async Task<ThanhPhanDinhDuong> UpdateThanhPhanDinhDuong(int id, ThanhPhanDinhDuongDTO thanhPhanDDDTO)
        {
            var timThanhPhanDinhDuong = await _repository.GetAllThanhPhanDinhDuongById(id);
            if (timThanhPhanDinhDuong != null)
            {
                _mapper.Map(thanhPhanDDDTO, timThanhPhanDinhDuong);
                await _repository.UpdateThanhPhanDinhDuong(timThanhPhanDinhDuong);
                return timThanhPhanDinhDuong;
            }
            return null;
        }
    }
}
