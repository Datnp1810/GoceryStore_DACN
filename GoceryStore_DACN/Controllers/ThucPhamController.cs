using AutoMapper;
using GoceryStore_DACN.DTOs;
using GoceryStore_DACN.Models.Requests;
using GoceryStore_DACN.Models.Respones;
using GroceryStore_DACN.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.WebSockets;

namespace GoceryStore_DACN.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ThucPhamController : ControllerBase
    {
        private readonly IThucPhamServices _thucPhamService;
        private readonly IMapper _mapper; 

        public ThucPhamController(IThucPhamServices services, IMapper mapper)
        {
            _thucPhamService = services;
            _mapper = mapper;
        }


        [HttpGet("phantrang")]
        public async Task<IActionResult> GetAllThucPhamPhanTrang([FromQuery] string search = "", [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 5, [FromQuery] string sortColumn = "Id", [FromQuery] string sortOrder = "asc")
        {
            try
            {
                var (products, totalItems) = await _thucPhamService.GetAllThucPhamPhanTrang(search, pageNumber, pageSize, sortColumn, sortOrder);
                var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);
                return Ok(new
                {
                    status = true,
                    message = "Lấy Thực Phẩm thành công",
                    result = products,
                    pagination = new
                    {
                        currentPage = pageNumber,
                        pageSize,
                        totalItems,
                        totalPages
                    }
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
        public IActionResult GetAllThucPham()
        {
            try
            {
                var getAll = _thucPhamService.GetAllThucPhamCache();
                return Ok(new
                {
                    results = getAll,
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    status = false,
                    error = ex.Message
                });
            }
        }

        [HttpGet("group")]
        public IActionResult GroupThucPham()
        {
            try
            {
                var getAll = _thucPhamService.GroupThucPhamByLTP();
                return Ok(new
                {
                    results = getAll,
                });
            }
            catch ( Exception ex )
            {
                return BadRequest(new
                {
                    status = false,
                    error = ex.Message
                });
            }
        }

        [HttpGet("cache/getByID/{id}")]
        public IActionResult GetAllThucPhamID(int id)
        {
            try
            {
                var getAll = _thucPhamService.GetAllThucPhamByIDCache(id);
                return Ok(new
                {
                    results = getAll,
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    status = false,
                    error = ex.Message
                });
            }
        }


        [HttpPost("listBuy")]
        public IActionResult DanhSachCanMuaNgay([FromBody] ThucDonNgayResponse thucDonNgay)
        {
            //if (buas == null || !buas.Any())
            //{
            //    return BadRequest("Danh sách bữa ăn không hợp lệ");
            //}

            //// Chuyển Buas thành ThucDonNgayResponse nếu cần (ví dụ để tái sử dụng logic)
            //var thucDonNgay = new ThucDonNgayResponse
            //{
            //    Buas = buas
            //};

            // Gọi hàm để lấy danh sách thực phẩm cần mua
            var danhSachCanMua = _thucPhamService.GetDanhSachMuaNgay(thucDonNgay);
            return Ok(danhSachCanMua);
        }


        [HttpPost("listBuyTuan")]
        public IActionResult DanhSachCanMuaTuan([FromBody] ThucDonTuanResponse ThucDonTuan)
        {
            if (ThucDonTuan == null)
            {
                return BadRequest(ModelState);
            }
            
            // Gọi hàm để lấy danh sách thực phẩm cần mua
            var danhSachCanMua = _thucPhamService.GetDanhSachMuaTuan(ThucDonTuan);
            return Ok(danhSachCanMua);
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

        [HttpGet("findltp/{id}")]
        public async Task<IActionResult> GetThucPhamByLoaiThucPham([FromRoute] int id, [FromQuery] string search = "", [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 5, [FromQuery] string sortColumn = "Id", [FromQuery] string sortOrder = "asc")
        {
            try
            {
                var (cda, totalItems) = await _thucPhamService.GetAllThucPhamByLoaiThucPham(id,search, pageNumber, pageSize, sortColumn, sortOrder);
                var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);
                if (cda == null)
                {
                    return NotFound(new
                    {
                        status = true,
                        message = "Không tìm thấy Loại Sản Phẩm"
                        
                    });
                }
                return Ok(new
                {
                    status = true,
                    message = "Lấy Thực Phẩm thành công",
                    result = cda,
                    pagination = new
                    {
                        currentPage = pageNumber,
                        pageSize,
                        totalItems,
                        totalPages
                    }
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

        [HttpGet("idltp/{id}")]
        public async Task<IActionResult> LocTheoLoaiThucPham([FromRoute] int id)
        {
            try
            {
                var cda = await _thucPhamService.FillterByIDLoaiThucPham(id);
             
                if ( cda == null )
                {
                    return NotFound(new
                    {
                        status = true,
                        message = "Không tìm thấy Loại Sản Phẩm"

                    });
                }
                return Ok(new
                {
                    status = true,
                    message = "Lấy Thực Phẩm thành công",
                    result = cda

                });
            }
            catch ( Exception ex )
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
            try
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
        public async Task<IActionResult> DeleteThucPham(int id)
        {
            try
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
