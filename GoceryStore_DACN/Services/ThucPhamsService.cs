using AutoMapper;
using GoceryStore_DACN.DTOs;
using GoceryStore_DACN.Entities;
using GoceryStore_DACN.Models.Respones;
using GoceryStore_DACN.Services.Interface;
using GroceryStore_DACN.Repositories.Interface;

namespace GoceryStore_DACN.Services
{
    public class ThucPhamsService : IThucPhamServices
    {
        private readonly IThucPhamRepository _repository;
        private readonly ILoaiThucPhamRepository _repoLoai;
        private readonly IMapper _mapper;
        private readonly IUploadService _uploadService;

        public ThucPhamsService(IThucPhamRepository repository, ILoaiThucPhamRepository repoLoai, IMapper mapper, IUploadService uploadService)
        {
            _repository = repository;
            _mapper = mapper;
            _uploadService = uploadService;
            _repoLoai = repoLoai;
        }
        public async Task<ThucPham> CreateThucPham(ThucPhamDTO thucPhamDTO)
        {
            var mapThucPham = _mapper.Map<ThucPham>(thucPhamDTO);
            var linkAnh =  _uploadService.UploadAsync(thucPhamDTO.ImageFile).Result.SecureUrl;
            mapThucPham.Image = linkAnh ?? ""; 
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

        public async Task<List<ThucPhamResponse>> GetAllThucPham()
        {
            var thucPhams =  await _repository.GetAllThucPham();
            return thucPhams;

        }

        public async Task<ThucPham> GetAllThucPhamById(int id)
        {
              
            return await _repository.GetThucPhamById(id);
        }

        public async Task<(List<ThucPhamResponse>, int totalItems)> GetAllThucPhamByLoaiThucPham(int id, string search, int pageNumber, int pageSize, string sortColumn, string sortOrder)
        {
            
            return await _repository.GetThucPhamByLoaiThucPham(id, search, pageNumber, pageSize, sortColumn, sortOrder);
        }

        public async Task<(IEnumerable<ThucPhamResponse> thucPham, int totalItems)> GetAllThucPhamPhanTrang(string search, int pageNumber, int pageSize, string sortColumn, string sortOrder)
        {
            var thucPhamRespo = await _repository.GetAllThucPhamPhanTrang(search, pageNumber, pageSize, sortColumn, sortOrder);
            return thucPhamRespo;
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
