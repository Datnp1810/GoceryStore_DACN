using AutoMapper;
using GoceryStore_DACN.DTOs;
using GoceryStore_DACN.Entities;
using GoceryStore_DACN.Models.Respones;
using GoceryStore_DACN.Repositories.Interface;
using GoceryStore_DACN.Services.Interface;
using GroceryStore_DACN.Repositories.Interface;

namespace GoceryStore_DACN.Services
{
    public class ThucPhamsService : IThucPhamServices
    {
        private readonly IThucPhamRepository _repository;
        private readonly IChiTietBuoiAnRepository _chiTietBuoiAnRepository;
        private readonly ILoaiThucPhamRepository _repoLoai;
        private readonly IMapper _mapper;
        private readonly IUploadService _uploadService;

        public ThucPhamsService(IThucPhamRepository repository,IChiTietBuoiAnRepository chiTietBuoiAnRepository ,ILoaiThucPhamRepository repoLoai, IMapper mapper, IUploadService uploadService)
        {
            _repository = repository;
            _mapper = mapper;
            _uploadService = uploadService;
            _repoLoai = repoLoai;
            _chiTietBuoiAnRepository = chiTietBuoiAnRepository;
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

        public IEnumerable<ThucPhamResponse> GetAllThucPhamCache()
        {
            var thucPhams =  _repository.GetAllThucPhamCache();
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

        public ThucPhamResponse GetAllThucPhamByIDCache(int id)
        {
            return _repository.ThucPhamByIdCache(id);
        }
    
        public List<ThucPhamResponse> GetDanhSachMuaNgay(ThucDonNgayResponse thucDonNgay)
        {
            var danhSachCanMua = new Dictionary<int, ThucPhamResponse>();
            foreach(var buaAn in thucDonNgay.Buas)
            {
                foreach(var monAn in buaAn.MonAn.Values)
                {
                    //Lấy danh sách thực phẩm thông qua chi tiết buổi ăn
                    var danhSachThucPham = _chiTietBuoiAnRepository.GetAllCT_BuoiAnByIdMonAnThreadCache(monAn.ID_TenMonAn);
                    foreach(var nguyenLieu in danhSachThucPham)
                    {
                        //Lấy số lượng,... thông qua bảng Thực Phẩm
                        var thucPham = _repository.ThucPhamByIdCache(nguyenLieu.ID_ThucPham);
                        if (thucPham != null)
                        {
                            //Lấy số lượng cần mua thông qua buoi ăn
                            double soLuongMua = nguyenLieu.Gram;
                            if (danhSachCanMua.ContainsKey(thucPham.ID_ThucPham))
                            {
                                danhSachCanMua[thucPham.ID_ThucPham].SoLuong += soLuongMua;
                            }
                            else
                            {
                                danhSachCanMua[thucPham.ID_ThucPham] = new ThucPhamResponse
                                {
                                    ID_ThucPham = thucPham.ID_ThucPham,
                                    TenThucPham = thucPham.TenThucPham,
                                    DVT = thucPham.DVT,
                                    SoLuong = soLuongMua,
                                    GiaBan = thucPham.GiaBan,
                                    Image = thucPham.Image,
                                    ID_LoaiThucPham = thucPham.ID_LoaiThucPham,
                                    TenLoaiThucPham = thucPham.TenLoaiThucPham
                                };
                            }    
                        }    

                    }    
                }    
            }
            return danhSachCanMua.Values.ToList();
        }

        public List<ThucPhamResponse> GetDanhSachMuaTuan(ThucDonTuanResponse thucDonTuan)
        {
            var danhSachCanMua = new Dictionary<int, ThucPhamResponse>();

            //Duyệt qua từng ngày trong tuần
            foreach(var thucDonNgay in thucDonTuan.listThucDonNgay)
            {
                //Gọi hàm lấy danh sách thực phẩm cần mua trong ngày
                var danhSachCanMuaNgay = GetDanhSachMuaNgay(thucDonNgay);

                //Duyệt qua từng thực phẩm trong ngày đó để cho lần lặp 2 xem có thực phẩm nào trùng hay chưa
                foreach(var thucPhamCanMua in danhSachCanMuaNgay)
                {
                    if (danhSachCanMua.ContainsKey(thucPhamCanMua.ID_ThucPham))
                    {
                        danhSachCanMua[thucPhamCanMua.ID_ThucPham].SoLuong += thucPhamCanMua.SoLuong;
                    }    
                    else
                    {
                        danhSachCanMua[thucPhamCanMua.ID_ThucPham] = new ThucPhamResponse
                        {
                            ID_ThucPham = thucPhamCanMua.ID_ThucPham,
                            TenThucPham = thucPhamCanMua.TenThucPham,
                            DVT = thucPhamCanMua.DVT,
                            SoLuong = thucPhamCanMua.SoLuong,
                            GiaBan = thucPhamCanMua.GiaBan,
                            Image = thucPhamCanMua.Image,
                            ID_LoaiThucPham = thucPhamCanMua.ID_LoaiThucPham,
                            TenLoaiThucPham = thucPhamCanMua.TenLoaiThucPham
                        };
                    }    
                }    
            }
            return danhSachCanMua.Values.ToList();

        }
    }
}
