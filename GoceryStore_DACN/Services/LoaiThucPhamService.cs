
using AutoMapper;
using GoceryStore_DACN.DTOs;
using GoceryStore_DACN.Entities;
using GroceryStore_DACN.Repositories.Interface;

namespace GoceryStore_DACN.Services
{
    public class LoaiThucPhamService : ILoaiThucPhamServices
    {
        private readonly ILoaiThucPhamRepository _repository;
        private readonly IMapper _mapper;

        public LoaiThucPhamService(ILoaiThucPhamRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<LoaiThucPham> CreateLoaiThucPham(LoaiThucPhamDTO loaiThucPham)
        {
            var mapCDA = _mapper.Map<LoaiThucPham>(loaiThucPham);

            var addCDA = await _repository.CreateLoaiThucPham(mapCDA);
            return addCDA;
        }

        public async Task<bool> DeleteLoaiThucPham(int id)
        {
            var delete = await _repository.DeleteLoaiThucPham(id);
            if (delete == false)
            {
                return false;
            }
            return true;
        }

        public async Task<IEnumerable<LoaiThucPham>> GetAllLoaiThucPham()
        {
            var loaiThucPhams = await _repository.GetAllLoaiThucPham();
            return loaiThucPhams.ToList();

        }

        public async Task<LoaiThucPham> GetAllLoaiThucPhamById(int id)
        {
            var loaiThucPham = await _repository.GetAllLoaiThucPhamById(id);
            return loaiThucPham;
        }

        public async Task<LoaiThucPham> UpdateLoaiThucPham(int id, LoaiThucPhamDTO loaiThucPham)
        {
            var timLoaiThucPham = await _repository.GetAllLoaiThucPhamById(id);
            if (timLoaiThucPham != null)
            {
                _mapper.Map(loaiThucPham, timLoaiThucPham);
                await _repository.UpdateLoaiThucPham(timLoaiThucPham);
                return timLoaiThucPham;
            }
            return null;
        }
    }
}
