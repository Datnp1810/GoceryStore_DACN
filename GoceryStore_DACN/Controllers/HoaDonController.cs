using GoceryStore_DACN.DTOs;
using GoceryStore_DACN.Entities;
using GoceryStore_DACN.Helpers;
using GoceryStore_DACN.Services;
using GoceryStore_DACN.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GoceryStore_DACN.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HoaDonController : ControllerBase
    {
        private readonly IHoaDonService _hoaDonService;
        public HoaDonController(IHoaDonService hoaDonService)
        {
            _hoaDonService = hoaDonService;
        }
        //Get all hóa đơn
        [HttpGet]
        public async Task<IActionResult> GetAllHoaDons()
        {
            try
            {
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(new
                {
                    status = false,
                    message = e.Message
                }); 
            }
        }
        //GET: api/HoaDon/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetInvoiceById(int id)
        {
            try
            {
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(new
                {
                    status = false,
                    error = e.Message
                });
            }
        }


        [Authorize(Roles = ApplicationRoles.User)]
        [HttpPost("add")]
        public async Task<IActionResult> CreateHoaDon([FromBody] HoaDonDTO hoaDonDTO)
        {
            try
            {
                //get user id from jwt 

                var result = await _hoaDonService.CreateHoaDon(hoaDonDTO);
                if (result == null)
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
            catch (Exception e)
            {
                return BadRequest(new
                {
                    status = false,
                    message = e.Message
                });
            }
        }

        [Authorize(Roles = ApplicationRoles.User)]
        [HttpGet("layGioHang")]
        public async Task<IActionResult> GetGioHang()
        {
            try
            {
                //get user id from jwt 

                var result = await _hoaDonService.GetGioHang();
                if ( result == null )
                {
                    return BadRequest(result);
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
        [HttpPost("create")]
        public async Task<IActionResult> CreateInvoiceTask([FromBody] CreateHoaDonDto createHoaDonDto)
        {
            try
            {
                //get user id from jwt 
                
                var result = await _hoaDonService.CreateInvoiceAsync(createHoaDonDto);
                if(result == null)
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
            catch (Exception e)
            {
                return BadRequest(new
                {
                    status = false,
                    message = e.Message
                });
            }
        }
              
        [Authorize(Roles = ApplicationRoles.Admin)]
        [HttpDelete("/delete/{id}")]
        public async Task<IActionResult> DeleteInvoice(int id)
        {
            try
            {
                bool isDelete = await _hoaDonService.DeleteInvoiceAsync(id);
                if (!isDelete)
                {
                    return BadRequest(new
                    {
                        status = false,
                        message = "Remove invoice failed"
                    });
                }
                return Ok(new
                {
                    status = true,
                    message = "Remove invoice successfully"
                });
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error deleting invoice: {e.Message}");
                return BadRequest(new
                {
                    status = false,
                    message = e.Message
                });
            }
        }

        [HttpPut("/update/{id}")]
        public async Task<IActionResult> UpdateInvoiceAsync(int id)
        {
            try
            {
                var result = await _hoaDonService.UpdateInvoiceAsync(id);
                if (result == null)
                {
                    return BadRequest(new
                    {
                        status = false,
                        message = "Update invoice failed"
                    });
                }
                return Ok(result);
            }
            catch (Exception e)
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
