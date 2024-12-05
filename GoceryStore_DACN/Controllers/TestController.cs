using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GoceryStore_DACN.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpGet("UnAuthentication")]
        
        public async Task<IActionResult> TestUnAuthentication()
        {
            return Ok("UnAuthentication");
        }
        [HttpGet("Authentication")]
        [Authorize]
        public async Task<IActionResult> TestAuthentication()
        {
         
            var claims = User.Claims;
            claims.ToList().ForEach(x =>
            {
                Console.WriteLine(x.Type + " : " + x.Value);
            });
            return Ok("ok");
        }
    }
}
