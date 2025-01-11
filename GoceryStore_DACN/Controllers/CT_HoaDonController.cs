using GoceryStore_DACN.DTOs;
using GoceryStore_DACN.Entities;
using GoceryStore_DACN.Helpers;
using GoceryStore_DACN.Repositories.Interface;
using GoceryStore_DACN.Services;
using GoceryStore_DACN.Services.Interface;
using GroceryStore_DACN.Repositories.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GoceryStore_DACN.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CT_HoaDonController : ControllerBase
    {
        private readonly ICT_HoaDonServices _service;
        private readonly IUserContextService _userContextService;
        public CT_HoaDonController(ICT_HoaDonServices services, IUserContextService userContext)
        {
            _service = services;
            _userContextService = userContext;
        }
        [HttpPost("add")]
        public async Task<ActionResult<CT_HoaDon>> AddCT_HoaDon([FromBody] List<CT_HoaDonDTO> ct_HoaDon)
        {
            try
            {
                //get user id from jwt 
                var result = await _service.AddChiTietHoaDon(ct_HoaDon);
                if ( result == null )
                {
                    return BadRequest(result);
                }
                return Ok(new
                {
                    status = true,
                    message = "Tạo hóa đơn thành công",
                    result
                });
            }
            catch ( Exception e )
            {
                return BadRequest(new
                {
                    status = false,
                    message = e.Message
                });
            }
        }

        [Authorize(Roles = ApplicationRoles.User)]
        [HttpPost("CreateGioHang")]
        public async Task<ActionResult<CT_HoaDon>> GioHang([FromBody] List<CT_HoaDonDTO> ct_HoaDon)
        {
            try
            {
                var userID = _userContextService.GetCurrentUserId();

                var result = await _service.TaoGioHang(ct_HoaDon);
                if ( result == null )
                {
                    return BadRequest(result);
                }
                return Ok(new
                {
                    status = true,
                    message = "Tạo Giỏ Hàng thành công",
                    result
                });
            }
            catch ( Exception e )
            {
                return BadRequest(new
                {
                    status = false,
                    message = e.Message
                });
            }
        }

        [Authorize(Roles = ApplicationRoles.User)]
        [HttpGet("GetGioHang")]
        public async Task<IActionResult> GetGioHang()
        {
            try
            {
                //get user id from jwt 

                var result = await _service.GetGioHang();
                if ( result == null )
                {
                    return Ok(new
                    {
                        message = "Giỏ hàng trống"
                    }
                        );
                }
                return Ok(new
                {
                    status = true,
                    message = "Lấy giỏ hàng thành công",
                    result
                });
            }
            catch ( Exception e )
            {
                return BadRequest(new
                {
                    status = false,
                    message = e.Message
                });
            }
        }
        
        [Authorize(Roles = ApplicationRoles.User)]
        [HttpPut("UpdateSoLuong")]
        public async Task<IActionResult> UpdateSoLuong([FromQuery] int ID_ThucPham, [FromQuery] double soLuong)
        {
            try
            {
                //get user id from jwt 

                var result = await _service.updateSoLuong(ID_ThucPham, soLuong);
                if ( result == null )
                {
                    return Ok(new
                    {
                        message = "Không có thực phẩm trong giỏ hàng"
                    }
                        );
                }
                return Ok(new
                {
                    status = true,
                    message = "Lấy giỏ hàng thành công",
                    result
                });
            }
            catch ( Exception e )
            {
                return BadRequest(new
                {
                    status = false,
                    message = e.Message
                });
            }
        }

        [Authorize(Roles = ApplicationRoles.User)]
        [HttpPut("ThanhToan")]
        public async Task<IActionResult> ThanhToan([FromBody] ThanhToanDTO thanhToanDTO)
        {
            try
            {
                //get user id from jwt 

                var result = await _service.thanhToan(thanhToanDTO);
                if ( result == null )
                {
                    return Ok(new
                    {
                        message = "Thanh Toán Thất Bại"
                    });
                }
                return Ok(new
                {
                    status = true,
                    message = "Thanh Toán Thành Công",
                    result
                });
            }
            catch ( Exception e )
            {
                return BadRequest(new
                {
                    status = false,
                    message = e.Message
                });
            }
        }

        [Authorize(Roles = ApplicationRoles.User)]
        [HttpDelete("DeleteGioHang/{id}")]
        public async Task<IActionResult> UpdateSoLuong(int id)
        {
            try
            {
                //get user id from jwt 

                var result = await _service.removeID_ThucPham(id);
                if ( result == false )
                {
                    return Ok(new
                    {
                        message = "Không có thực phẩm trong giỏ hàng"
                    }
                        );
                }
                return Ok(new
                {
                    status = true,
                    message = "Xóa thành công",
                    result
                });
            }
            catch ( Exception e )
            {
                return BadRequest(new
                {
                    status = false,
                    message = e.Message
                });
            }
        }




    }
}
