using GoceryStore_DACN.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GoceryStore_DACN.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        public readonly IUploadService _uploadService;
        public UploadController(IUploadService uploadService)
        {
            _uploadService = uploadService;
        }
        [HttpPost()]
        public async Task<IActionResult> UploadAsync([FromForm] IFormFile file)
        {
            try
            {
                var result = await _uploadService.UploadAsync(file);
                if (result.Success)
                {
                    return Ok(new
                    {
                        status = true,
                        result.SecureUrl     //extract link ra đây 
                    });
                }
                return BadRequest(new
                {
                    status = false,
                    error = result.Error
                });
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
    }
}
