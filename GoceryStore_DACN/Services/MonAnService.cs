using AutoMapper;
using GoceryStore_DACN.DTOs;
using GoceryStore_DACN.Entities;
using GroceryStore_DACN.Repositories.Interface;

namespace GoceryStore_DACN.Services
{
    public class MonAnService :IMonAnServices
    {
        private readonly IMonAnRepository _repository;
        private readonly IMapper _mapper;

        public MonAnService(IMonAnRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<MonAn> CreateMonAn(MonAnDTO monAnDTO)
        {
            var mapMonAn = _mapper.Map<MonAn>(monAnDTO);

            var addMonAn = await _repository.CreateMonAn(mapMonAn);
            return addMonAn;
        }

        public async Task<bool> DeleteMonAn(int id)
        {
            var delete = await _repository.DeleteMonAn(id);
            if (delete == false)
            {
                return false;
            }
            return true;
        }

        public async Task<IEnumerable<MonAn>> GetAllMonAn()
        {
            var monAns = await _repository.GetAllMonAn();
            return monAns.ToList();
        }

        public async Task<MonAn> GetAllMonAnById(int id)
        {
            var monAn = await _repository.GetAllMonAnById(id);
            return monAn;
        }

        public async Task<MonAn> UpdateMonAn(int id, MonAnDTO monAnDTO)
        {
            var timMonAn = await _repository.GetAllMonAnById(id);
            if (timMonAn != null)
            {
                _mapper.Map(monAnDTO, timMonAn);
                await _repository.UpdateMonAn(timMonAn);
                return timMonAn;
            }
            return null;
        }
    }
}
