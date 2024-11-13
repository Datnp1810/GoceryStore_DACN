using GoceryStore_DACN.DTOs;
using GroceryStore_DACN.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GoceryStore_DACN.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoaiMonAnController : ControllerBase
    {
        private readonly ILoaiMonAnServices _loaiMonAnService;

        public LoaiMonAnController(ILoaiMonAnServices services)
        {
            _loaiMonAnService = services;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCDA()
        {
            try
            {
                var getAll = await _loaiMonAnService.GetAllLoaiMonAn();
                return Ok(new
                {
                    status = true,
                    message = "Lấy Loại Món Ăn thành công",
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
        public async Task<IActionResult> GetLoaiMonAnById(int id)
        {
            try
            {
                var cda = await _loaiMonAnService.GetAllLoaiMonAnById(id);
                if (cda == null)
                {
                    return NotFound(new
                    {
                        status = true,
                        message = "Không tìm thấy Loại Món Ăn"
                    });
                }
                return Ok(new
                {
                    status = true,
                    message = "Lấy Loại Món Ăn thành công",
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
        public async Task<ActionResult<LoaiMonAnDTO>> CreateCDA([FromForm] LoaiMonAnDTO loaiMonAnDTO)
        {
            try
            {
                if (string.IsNullOrEmpty(loaiMonAnDTO.TenLoaiMonAn))
                {
                    return BadRequest(new
                    {
                        status = false,
                        message = "Tên không được để trống"
                    });
                }

                var addCDA = await _loaiMonAnService.CreateLoaiMonAn(loaiMonAnDTO);
                if (addCDA == null)
                {
                    return BadRequest(new
                    {
                        status = true,
                        message = "Do not create Loại Món Ăn"
                    });
                }
                return Ok(new
                {
                    status = true,
                    message = "Created manufacture successfully",
                    result = addCDA
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
        public async Task<IActionResult> UpdateCDA(int id, [FromBody] LoaiMonAnDTO loaiMonAnDTO)
        {
            try
            {
                var cda = await _loaiMonAnService.UpdateLoaiMonAn(id, loaiMonAnDTO);
                if (cda == null)
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSanPham(int id)
        {
            try
            {
                var delete = await _loaiMonAnService.DeleteLoaiMonAn(id);
                if (delete == false)
                {
                    return NotFound(new
                    {
                        status = 404,
                        message = "Loại Món Ăn không tồn tại"
                    });
                }

                return Ok(new
                {
                    status = 200,
                    message = "Xóa Loại Món Ăn thành công"
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
