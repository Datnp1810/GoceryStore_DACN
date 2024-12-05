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

        public CT_BuoiAnController(ICT_BuoiAnServices services)
        {
            _ct_BuoiAnService = services;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllCT_BuoiAn()
        {
            try
            {
                var getAll = await _ct_BuoiAnService.GetAllCT_BuoiAnDTO();
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
