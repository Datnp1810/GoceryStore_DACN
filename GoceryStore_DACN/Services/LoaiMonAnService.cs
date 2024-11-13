using AutoMapper;
using GoceryStore_DACN.DTOs;
using GoceryStore_DACN.Entities;
using GroceryStore_DACN.Repositories.Interface;

namespace GoceryStore_DACN.Services
{
    public class LoaiMonAnService : ILoaiMonAnServices
    {
        private readonly ILoaiMonAnRepository _repository;
        private readonly IMapper _mapper;

        public LoaiMonAnService(ILoaiMonAnRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<LoaiMonAn> CreateLoaiMonAn(LoaiMonAnDTO loaiMonAn)
        {
            var mapCDA = _mapper.Map<LoaiMonAn>(loaiMonAn);

            var addCDA = await _repository.CreateLoaiMonAn(mapCDA);
            return addCDA;
        }

        public async Task<bool> DeleteLoaiMonAn(int id)
        {
            var delete = await _repository.DeleteLoaiMonAn(id);
            if (delete == false)
            {
                return false;
            }
            return true;
        }

        public async Task<IEnumerable<LoaiMonAn>> GetAllLoaiMonAn()
        {
            var loaiMonAns = await _repository.GetAllLoaiMonAn();
            return loaiMonAns.ToList();

        }

        public async Task<LoaiMonAn> GetAllLoaiMonAnById(int id)
        {
            var loaiMonAn = await _repository.GetAllLoaiMonAnById(id);
            return loaiMonAn;
        }

        public async Task<LoaiMonAn> UpdateLoaiMonAn(int id, LoaiMonAnDTO loaiMonAn)
        {
            var timLoaiMonAn = await _repository.GetAllLoaiMonAnById(id);
            if (timLoaiMonAn != null)
            {
                _mapper.Map(loaiMonAn, timLoaiMonAn);
                await _repository.UpdateLoaiMonAn(timLoaiMonAn);
                return timLoaiMonAn;
            }
            return null;
        }
    }
}
