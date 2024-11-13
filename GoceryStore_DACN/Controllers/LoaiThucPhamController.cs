
using GoceryStore_DACN.DTOs;
using GroceryStore_DACN.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;

namespace GoceryStore_DACN.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoaiThucPhamController : ControllerBase
    {
        private readonly ILoaiThucPhamServices _loaiThucPhamService;

        public LoaiThucPhamController(ILoaiThucPhamServices services)
        {
            _loaiThucPhamService = services;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCDA()
        {
            try
            {
                var getAll = await _loaiThucPhamService.GetAllLoaiThucPham();
                return Ok(new
                {
                    status = true,
                    message = "Lấy Loại Thực Phẩm thành công",
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
        public async Task<IActionResult> GetLoaiThucPhamById(int id)
        {
            try
            {
                var ltp = await _loaiThucPhamService.GetAllLoaiThucPhamById(id);
                if (ltp == null)
                {
                    return NotFound(new
                    {
                        status = true,
                        message = "Không tìm thấy Loại Thực Phẩm"
                    });
                }
                return Ok(new
                {
                    status = true,
                    message = "Lấy Loại Thực Phẩm thành công",
                    result = ltp
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
        public async Task<ActionResult<LoaiThucPhamDTO>> CreateLTP([FromForm] LoaiThucPhamDTO loaiThucPhamDTO)
        {
            try
            {
                if (string.IsNullOrEmpty(loaiThucPhamDTO.TenLoaiThucPham))
                {
                    return BadRequest(new
                    {
                        status = false,
                        message = "Tên không được để trống"
                    });
                }

                var addltp = await _loaiThucPhamService.CreateLoaiThucPham(loaiThucPhamDTO);
                if (addltp == null)
                {
                    return BadRequest(new
                    {
                        status = true,
                        message = "Do not create Loại Thực Phẩm"
                    });
                }
                return Ok(new
                {
                    status = true,
                    message = "Created manufacture successfully",
                    result = addltp
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
        public async Task<IActionResult> UpdateLTP(int id, [FromBody] LoaiThucPhamDTO loaiThucPhamDTO)
        {
            var ltp = await _loaiThucPhamService.UpdateLoaiThucPham(id, loaiThucPhamDTO);
            if (ltp == null)
            {
                return NotFound(new
                {
                    status = 200,
                    message = "Không Có chế độ ăn",
                });
            }
            return Ok(new
            {
                status = 200,
                message = "Cập nhật sản phẩm thành công",
                result = ltp
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLTP(int id)
        {
            var delete = await _loaiThucPhamService.DeleteLoaiThucPham(id);
            if (delete == false)
            {
                return NotFound(new
                {
                    status = 404,
                    message = "Loại Thực Phẩm không tồn tại"
                });
            }

            return Ok(new
            {
                status = 200,
                message = "Xóa Loại Thực Phẩm thành công"
            });
        }
    }
}
