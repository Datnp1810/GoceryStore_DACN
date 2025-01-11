using AutoMapper;
using GoceryStore_DACN.DTOs;
using GoceryStore_DACN.Models.Respones;
using GoceryStore_DACN.Repositories.Interface;
using GroceryStore_DACN.Repositories.Interface;

namespace GoceryStore_DACN.Services
{
    public class CT_BuoiAnService : ICT_BuoiAnServices
    {
        private readonly IChiTietBuoiAnRepository _repository;
        private readonly IMapper _mapper;

        public CT_BuoiAnService(IChiTietBuoiAnRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public Task<List<CT_BuoiAnResponse>> GetAllCT_BuoiAnByIDMonAn(int id)
        {
            return _repository.GetAllCT_BuoiAnByIdMonAn(id);
        }

        public Task<IEnumerable<CT_BuoiAnDTO>> GetAllCT_BuoiAnDTO()
        {
            return _repository.GetAllCT_BuoiAn();
        }
    }
}
