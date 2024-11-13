using AutoMapper;
using GoceryStore_DACN.DTOs;
using GoceryStore_DACN.Entities;
using GroceryStore_DACN.Repositories.Interface;

namespace GoceryStore_DACN.Services
{
    public class ThucPhamsService : IThucPhamServices
    {
        private readonly IThucPhamRepository _repository;
        private readonly IMapper _mapper;

        public ThucPhamsService(IThucPhamRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<ThucPham> CreateThucPham(ThucPhamDTO thucPhamDTO)
        {
            var mapThucPham = _mapper.Map<ThucPham>(thucPhamDTO);

            var addThucPham = await _repository.CreateThucPham(mapThucPham);
            return addThucPham;
        }

        public async Task<bool> DeleteThucPham(int id)
        {
            var delete = await _repository.DeleteThucPham(id);
            if (delete == false)
            {
                return false;
            }
            return true;
        }

        public async Task<IEnumerable<ThucPham>> GetAllThucPham()
        {
           var listThucPham = await _repository.GetAllThucPham();
            return listThucPham;
        }

        public async Task<ThucPham> GetAllThucPhamById(int id)
        {
            return await _repository.GetThucPhamById(id);
        }

        public async Task<ThucPham> UpdateThucPham(int id, ThucPhamDTO thucPhamDTO)
        {
            var timThucPham = await _repository.GetThucPhamById(id);
            if (timThucPham != null)
            {
                _mapper.Map(thucPhamDTO, timThucPham);
                await _repository.UpdateThucPham(timThucPham);
                return timThucPham;
            }
            return null;
        }
    }
}
