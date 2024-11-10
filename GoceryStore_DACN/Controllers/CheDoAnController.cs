using GoceryStore_DACN.DTOs;
using GroceryStore_DACN.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GoceryStore_DACN.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CheDoAnController : ControllerBase
    {
        private readonly ICheDoAnServices _cheDoAnService;

        public CheDoAnController(ICheDoAnServices services) 
        {
            _cheDoAnService = services;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCDA()
        {
            try
            {
                var getAll = await _cheDoAnService.GetAllCheDoAn();
                return Ok(new
                {
                    status = true,
                    message = "Lấy Chế Độ Ăn thành công",
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
        public async Task<IActionResult> GetCheDoAnById(int id)
        {
            try
            {
                var cda = await _cheDoAnService.GetAllCheDoAnById(id);
                if (cda == null)
                {
                    return NotFound(new
                    {
                        status = true,
                        message = "Không tìm thấy Chế Độ Ăn"
                    });
                }
                return Ok(new
                {
                    status = true,
                    message = "Lấy Chế Độ Ăn thành công",
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
        public async Task<ActionResult<CheDoAnDTO>> CreateCDA([FromForm]CheDoAnDTO cheDoAnDTO)
        {
            try
            {
                if (string.IsNullOrEmpty(cheDoAnDTO.TenCheDoAn))
                {
                    return BadRequest(new
                    {
                        status = false,
                        message = "Tên không được để trống"
                    });
                }
                
                var addCDA = await _cheDoAnService.CreateCheDoAn(cheDoAnDTO);
                if (addCDA == null)
                {
                    return BadRequest(new
                    {
                        status = true,
                        message = "Do not create Chế Độ Ăn"
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
        public async Task<IActionResult> UpdateCDA(int id, [FromBody] CheDoAnDTO cheDoAnDTO)
        {
            var cda = await _cheDoAnService.UpdateCheDoAn(id, cheDoAnDTO);
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSanPham(int id)
        {
            var delete = await _cheDoAnService.DeleteCheDoAn(id);
            if (delete == false)
            {
                return NotFound(new
                {
                    status = 404,
                    message = "Chế Độ Ăn không tồn tại"
                });
            }

            return Ok(new
            {
                status = 200,
                message = "Xóa Chế Độ Ăn thành công"
            });
        }

    }
}
