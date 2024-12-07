using GoceryStore_DACN.DTOs;
using GoceryStore_DACN.Entities;
using GroceryStore_DACN.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace GoceryStore_DACN.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ThanhPhanDinhDuongController : ControllerBase
    {
        private readonly IThanhPhanDinhDuongServices _thanhPhanDDService;
        private readonly IThanhPhanDinhDuongRepository _repo;

        public ThanhPhanDinhDuongController(IThanhPhanDinhDuongServices services, IThanhPhanDinhDuongRepository repo)
        {
            _thanhPhanDDService = services;
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllThanhPhanDinhDuong()
        {
            try
            {
                var getAll = await _thanhPhanDDService.GetAllThanhPhanDinhDuong();
                return Ok(new
                {
                    status = true,
                    message = "Lấy Thành Phần Dinh Dưỡng thành công",
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

        [HttpGet("/cache/getAllThanhPhanDinhDuong")]
        public IActionResult GetAllThanhPhanDinhDuongCache()
        {
            try
            {
                var dem = new Stopwatch();
                dem.Start();
                var getAll =  _repo.GetAllThanhPhanDinhDuongCache();
                dem.Stop();
                Console.WriteLine("Thời gian thực thi của TPDD {0}", dem.ElapsedMilliseconds/1000);
                return Ok(new
                {
                    status = true,
                    message = "Lấy Thành Phần Dinh Dưỡng thành công",
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

        [HttpGet("/cache/getByIDThucPham/{id}")]
        public IActionResult GetThanhPhanDinhDuongByIdCache(int id)
        {
            try
            {
                var cda = _repo.GetAllThanhPhanDinhDuongByIdThreadCache(id);
                if (cda == null)
                {
                    return NotFound(new
                    {
                        status = true,
                        message = "Không tìm thấy Thành Phần Dinh Dưỡng"
                    });
                }
                return Ok(new
                {
                    status = true,
                    message = "Lấy Thành Phần Dinh Dưỡng thành công",
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


        [HttpGet("{id}")]
        public async Task<IActionResult> GetThanhPhanDinhDuongById(int id)
        {
            try
            {
                var cda = await _thanhPhanDDService.GetAllThanhPhanDinhDuongById(id);
                if (cda == null)
                {
                    return NotFound(new
                    {
                        status = true,
                        message = "Không tìm thấy Thành Phần Dinh Dưỡng"
                    });
                }
                return Ok(new
                {
                    status = true,
                    message = "Lấy Thành Phần Dinh Dưỡng thành công",
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
        public async Task<ActionResult<ThanhPhanDinhDuong>> CreateThucPham([FromForm] ThanhPhanDinhDuong thanhPhanDD)
        {
            try
            {
                if (thanhPhanDD.ID_ThucPham==null)
                {
                    return BadRequest(new
                    {
                        status = false,
                        message = "ID không được để trống"
                    });
                }

                var addthanhPhanDD = await _thanhPhanDDService.CreateThanhPhanDinhDuong(thanhPhanDD);
                if (addthanhPhanDD == null)
                {
                    return BadRequest(new
                    {
                        status = true,
                        message = "Thực phẩm chưa tồn tại"
                    });
                }
                return Ok(new
                {
                    status = true,
                    message = "Created Thành Phần Dinh Dưỡng successfully",
                    result = addthanhPhanDD
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
        public async Task<IActionResult> UpdateThanhPhanDinhDuong(int id, [FromBody] ThanhPhanDinhDuongDTO thanhPhanDinhDuongDTO)
        {
            try
            {
                var thucPham = await _thanhPhanDDService.UpdateThanhPhanDinhDuong(id, thanhPhanDinhDuongDTO);
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
        public async Task<IActionResult> DeleteThanhPhanDinhDuong(int id)
        {
            try
            {
                var delete = await _thanhPhanDDService.DeleteThanhPhanDinhDuong(id);
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
                    message = "Xóa Thực Phẩm thành công"
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
