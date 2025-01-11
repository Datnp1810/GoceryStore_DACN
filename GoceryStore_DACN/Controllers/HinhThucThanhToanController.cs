using GoceryStore_DACN.DTOs;
using GroceryStore_DACN.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GoceryStore_DACN.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HinhThucThanhToanController : ControllerBase
    {
        private readonly IHinhThucThanhToanServices _hinhThucTTService;

        public HinhThucThanhToanController(IHinhThucThanhToanServices services)
        {
            _hinhThucTTService = services;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllCDA()
        {
            try
            {
                var getAll = await _hinhThucTTService.GetAllHinhThucThanhToan();
                return Ok(new
                {
                    status = true,
                    message = "Lấy Hình Thức Thanh Toán thành công",
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
        public async Task<IActionResult> GetHinhThucThanhToanById(int id)
        {
            try
            {
                var cda = await _hinhThucTTService.GetAllHinhThucThanhToanById(id);
                if (cda == null)
                {
                    return NotFound(new
                    {
                        status = true,
                        message = "Không tìm thấy Hình Thức Thanh Toán"
                    });
                }
                return Ok(new
                {
                    status = true,
                    message = "Lấy Hình Thức Thanh Toán thành công",
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
        public async Task<ActionResult<HinhThucThanhToanDTO>> CreateCDA([FromForm] HinhThucThanhToanDTO hinhThucTTDTO)
        {
            try
            {
                if (string.IsNullOrEmpty(hinhThucTTDTO.HTThanhToan))
                {
                    return BadRequest(new
                    {
                        status = false,
                        message = "Tên không được để trống"
                    });
                }

                var addCDA = await _hinhThucTTService.CreateHinhThucThanhToan(hinhThucTTDTO);
                if (addCDA == null)
                {
                    return BadRequest(new
                    {
                        status = true,
                        message = "Do not create Hình Thức Thanh Toán"
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
        public async Task<IActionResult> UpdateCDA(int id, [FromBody] HinhThucThanhToanDTO hinhThucTTDTO)
        {
            try
            {
                var cda = await _hinhThucTTService.UpdateHinhThucThanhToan(id, hinhThucTTDTO);
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
            catch(Exception ex)
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
                var delete = await _hinhThucTTService.DeleteHinhThucThanhToan(id);
                if (delete == false)
                {
                    return NotFound(new
                    {
                        status = 404,
                        message = "Hình Thức Thanh Toán không tồn tại"
                    });
                }

                return Ok(new
                {
                    status = 200,
                    message = "Xóa Hình Thức Thanh Toán thành công"
                });
            }
            catch(Exception ex)
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
