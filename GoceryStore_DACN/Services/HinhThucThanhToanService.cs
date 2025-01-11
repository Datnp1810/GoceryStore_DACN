using AutoMapper;
using GoceryStore_DACN.DTOs;
using GoceryStore_DACN.Entities;
using GroceryStore_DACN.Repositories.Interface;

namespace GoceryStore_DACN.Services
{
    public class HinhThucThanhToanService :IHinhThucThanhToanServices
    {
        private readonly IHinhThucThanhToanRepository _repository;
        private readonly IMapper _mapper;

        public HinhThucThanhToanService(IHinhThucThanhToanRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<HinhThucThanhToan> CreateHinhThucThanhToan(HinhThucThanhToanDTO hinhThucTT)
        {
            var mapCDA = _mapper.Map<HinhThucThanhToan>(hinhThucTT);

            var addCDA = await _repository.CreateHinhThucThanhToan(mapCDA);
            return addCDA;
        }

        public async Task<bool> DeleteHinhThucThanhToan(int id)
        {
            var delete = await _repository.DeleteHinhThucThanhToan(id);
            if (delete == false)
            {
                return false;
            }
            return true;
        }

        public async Task<IEnumerable<HinhThucThanhToanDTO>> GetAllHinhThucThanhToan()
        {
            var hinhThucTTs = await _repository.GetAllHinhThucThanhToan();
            return hinhThucTTs.ToList();

        }

        public async Task<HinhThucThanhToan> GetAllHinhThucThanhToanById(int id)
        {
            var hinhThucTT = await _repository.GetAllHinhThucThanhToanById(id);
            return hinhThucTT;
        }

        public async Task<HinhThucThanhToan> UpdateHinhThucThanhToan(int id, HinhThucThanhToanDTO hinhThucTT)
        {
            var timHinhThucThanhToan = await _repository.GetAllHinhThucThanhToanById(id);
            if (timHinhThucThanhToan != null)
            {
                _mapper.Map(hinhThucTT, timHinhThucThanhToan);
                await _repository.UpdateHinhThucThanhToan(timHinhThucThanhToan);
                return timHinhThucThanhToan;
            }
            return null;
        }
    }
}
