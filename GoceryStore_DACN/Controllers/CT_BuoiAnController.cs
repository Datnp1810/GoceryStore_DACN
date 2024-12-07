using GoceryStore_DACN.Repositories.Interface;
using GoceryStore_DACN.Services;
using GroceryStore_DACN.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GoceryStore_DACN.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CT_BuoiAnController : ControllerBase
    {
        private readonly ICT_BuoiAnServices _ct_BuoiAnService;
        private readonly IChiTietBuoiAnRepository _repo;

        public CT_BuoiAnController(ICT_BuoiAnServices services, IChiTietBuoiAnRepository repo)
        {
            _ct_BuoiAnService = services;
            _repo = repo;
        }
        [HttpGet("/cache/getAll")]
        public IActionResult GetAllCT_BuoiAnCache()
        {
            try
            {
                var getAll = _repo.GetAllCT_BuoiAnCache();
                return Ok(new
                {
                    status = true,
                    message = "Lấy Chi tiết thành công",
                    results = getAll
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    status = false,
                    message = ex.Message
                });
            }
        }

        [HttpGet("/cache/getByIDMonAn/{id}")]
        public IActionResult GetAllCT_BuoiAnCacheByIDLoai(int id)
        {
            try
            {
                var getAll = _repo.GetAllCT_BuoiAnByIdMonAnThreadCache(id);
                return Ok(new
                {
                    status = true,
                    message = "Lấy Chi tiết thành công",
                    results = getAll
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    status = false,
                    message = ex.Message
                });
            }
        }


        [HttpGet]
        public IActionResult GetAllCT_BuoiAn()
        {
            try
            {
                var getAll =  _repo.GetAllCT_BuoiAnCache();
                return Ok(new
                {
                    status = true,
                    message = "Lấy Chi tiết thành công",
                    results = getAll
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    status = false,
                    message = ex.Message
                });
            }
        }

        [HttpGet("{id_MonAn}")]
        public async Task<IActionResult> GetCT_BuoiAnByMonAn(int id_MonAn)
        {
            try
            {
                var cda = await _ct_BuoiAnService.GetAllCT_BuoiAnByIDMonAn(id_MonAn);
                if (cda == null)
                {
                    return NotFound(new
                    {
                        status = true,
                        message = "Không tìm thấy Chi Tiết"
                    });
                } 
                return Ok(new
                {
                    status = true,
                    message = "Lấy Chi Tiết thành công",
                    result = cda
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    status = false,
                    message = ex.Message
                });
            }
        }
    }
}
