using GoceryStore_DACN.DTOs;
using GroceryStore_DACN.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GoceryStore_DACN.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ThucPhamController : ControllerBase
    {
        private readonly IThucPhamServices _thucPhamService;

        public ThucPhamController(IThucPhamServices services)
        {
            _thucPhamService = services;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllThucPham()
        {
            try
            {
                var getAll = await _thucPhamService.GetAllThucPham();
                return Ok(new
                {
                    status = true,
                    message = "Lấy Thực Phẩm thành công",
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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetThucPhamById(int id)
        {
            try
            {
                var cda = await _thucPhamService.GetAllThucPhamById(id);
                if (cda == null)
                {
                    return NotFound(new
                    {
                        status = true,
                        message = "Không tìm thấy Thực Phẩm"
                    });
                }
                return Ok(new
                {
                    status = true,
                    message = "Lấy Thực Phẩm thành công",
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

        [HttpPost()]
        public async Task<ActionResult<ThucPhamDTO>> CreateThucPham([FromForm] ThucPhamDTO thucPhamDTO)
        {
            try
            {
                if (string.IsNullOrEmpty(thucPhamDTO.TenThucPham))
                {
                    return BadRequest(new
                    {
                        status = false,
                        message = "Tên không được để trống"
                    });
                }

                var addThucPham = await _thucPhamService.CreateThucPham(thucPhamDTO);
                if (addThucPham == null)
                {
                    return BadRequest(new
                    {
                        status = true,
                        message = "Do not create Thực Phẩm"
                    });
                }
                return Ok(new
                {
                    status = true,
                    message = "Created Thực Phẩm successfully",
                    result = addThucPham
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

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateThucPham(int id, [FromBody] ThucPhamDTO thucPhamDTO)
        {
            var thucPham = await _thucPhamService.UpdateThucPham(id, thucPhamDTO);
            if (thucPham == null)
            {
                return NotFound(new
                {
                    status = 200,
                    message = "Không Có Thực Phẩm",
                });
            }
            return Ok(new
            {
                status = 200,
                message = "Cập nhật sản phẩm thành công",
                result = thucPham
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteThucPham(int id)
        {
            var delete = await _thucPhamService.DeleteThucPham(id);
            if (delete == false)
            {
                return NotFound(new
                {
                    status = 404,
                    message = "Thực Phẩm không tồn tại"
                });
            }

            return Ok(new
            {
                status = 200,
                message = "Xóa Thực Phẩm Ăn thành công"
            });
        }
    }
}
