using AutoMapper;
using GoceryStore_DACN.DTOs;
using GoceryStore_DACN.Entities;
using GroceryStore_DACN.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace GoceryStore_DACN.Services
{
    public class CheDoAnService : ICheDoAnServices
    {
        private readonly ICheDoAnRepository _repository;
        private readonly IMapper _mapper;

        public CheDoAnService(ICheDoAnRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<CheDoAn> CreateCheDoAn(CheDoAnDTO cheDoAn)
        {
            var mapCDA = _mapper.Map<CheDoAn>(cheDoAn);

            var addCDA = await _repository.CreateCheDoAn(mapCDA);
            return addCDA;
        }

        public async Task<bool> DeleteCheDoAn(int id)
        {
            var delete = await _repository.DeleteCheDoAn(id);
            if (delete == false)
            {
                return false;
            }
            return true;
        }

        public async Task<IEnumerable<CheDoAn>> GetAllCheDoAn()
        {
            var cheDoAns = await _repository.GetAllCheDoAn();
            return cheDoAns.ToList();

        }

        public async Task<CheDoAn> GetAllCheDoAnById(int id)
        {
            var cheDoAn = await _repository.GetAllCheDoAnById(id);
            return cheDoAn;
        }

        public async Task<CheDoAn> UpdateCheDoAn(int id, CheDoAnDTO cheDoAn)
        {
            var timCheDoAn = await _repository.GetAllCheDoAnById(id);
            if (timCheDoAn != null)
            {
                _mapper.Map(cheDoAn, timCheDoAn);
                await _repository.UpdateCheDoAn(timCheDoAn);
                return timCheDoAn;
            }
            return null;
        }
    }
}
