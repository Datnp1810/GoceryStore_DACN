using GoceryStore_DACN.DTOs;
using GroceryStore_DACN.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GoceryStore_DACN.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MonAnController : ControllerBase
    {
        private readonly IMonAnServices _monAnService;
        private readonly IMonAnRepository _repo;

        public MonAnController(IMonAnServices services, IMonAnRepository repo)
        {
            _monAnService = services;
            _repo = repo;
        }

        [HttpGet("/byLoaiMonAn/{tenLoai}")]
        public async Task<IActionResult> GetAllMonAnByTenLoai(string tenLoai)
        {
            try
            {
                var getAll = await _monAnService.GetAllMonAnByLoaiMonAn(tenLoai);
                return Ok(new
                {
                    status = true,
                    message = "Lấy Món Ăn thành công",
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

        [HttpGet("/idLoaiMonAn/{idLoai}")]
        public IActionResult GetAllMonAnById(int idLoai)
        {
            try
            {
                var getAll = _repo.GetAllMonAnByLoaiMonAnThreadCache(idLoai);
                return Ok(new
                {
                    status = true,
                    message = "Lấy Món Ăn thành công",
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
        public async Task<IActionResult> GetAllMonAn()
        {
            try
            {
                var getAll = await _monAnService.GetAllMonAn();
                return Ok(new
                {
                    status = true,
                    message = "Lấy Món Ăn thành công",
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

        [HttpGet("cache")]
        public IActionResult GetAllMonAnCache()
        {
            try
            {
                var getAll = _repo.GetAllMonAnCache();
                return Ok(new
                {
                    status = true,
                    message = "Lấy Món Ăn thành công",
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
        public async Task<IActionResult> GetMonAnById(int id)
        {
            try
            {
                var cda = await _monAnService.GetAllMonAnById(id);
                if (cda == null)
                {
                    return NotFound(new
                    {
                        status = true,
                        message = "Không tìm thấy Món Ăn"
                    });
                }
                return Ok(new
                {
                    status = true,
                    message = "Lấy Món Ăn thành công",
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
        public async Task<ActionResult<MonAnDTO>> CreateMonAn([FromForm] MonAnDTO monAnDTO)
        {
            try
            {
                if (string.IsNullOrEmpty(monAnDTO.TenMonAn))
                {
                    return BadRequest(new
                    {
                        status = false,
                        message = "Tên không được để trống"
                    });
                }

                var addMonAn = await _monAnService.CreateMonAn(monAnDTO);
                if (addMonAn == null)
                {
                    return BadRequest(new
                    {
                        status = true,
                        message = "Do not create Món Ăn"
                    });
                }
                return Ok(new
                {
                    status = true,
                    message = "Created Món Ăn successfully",
                    result = addMonAn
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
        public async Task<IActionResult> UpdateMonAn(int id, [FromBody] MonAnDTO monAnDTO)
        {
            try
            {
                var monAn = await _monAnService.UpdateMonAn(id, monAnDTO);
                if (monAn == null)
                {
                    return NotFound(new
                    {
                        status = 200,
                        message = "Không Có Món Ăn",
                    });
                }
                return Ok(new
                {
                    status = 200,
                    message = "Cập nhật sản phẩm thành công",
                    result = monAn
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
        public async Task<IActionResult> DeleteMonAn(int id)
        {
            try
            {
                var delete = await _monAnService.DeleteMonAn(id);
                if (delete == false)
                {
                    return NotFound(new
                    {
                        status = 404,
                        message = "Món Ăn không tồn tại"
                    });
                }

                return Ok(new
                {
                    status = 200,
                    message = "Xóa Món Ăn Ăn thành công"
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
