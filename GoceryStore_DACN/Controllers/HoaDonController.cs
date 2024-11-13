using GoceryStore_DACN.DTOs;
using GoceryStore_DACN.Services;
using GoceryStore_DACN.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GoceryStore_DACN.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HoaDonController : ControllerBase
    {
        private readonly IInvoiceService _invoiceService;
        public HoaDonController(IInvoiceService invoiceService)
        {
            _invoiceService = invoiceService;
        }
       
        [HttpGet]
        public async Task<IActionResult> CreateInvoice()
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

        [HttpPost("create")]
        public async Task<IActionResult> CreateInvoice([FromBody] CreateInvoiceDto createInvoiceDto)
        {
            try
            {
                var result = await _invoiceService.CreateAsync(createInvoiceDto);
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
    }
}
