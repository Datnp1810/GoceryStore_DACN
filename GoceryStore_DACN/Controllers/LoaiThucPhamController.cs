
using Microsoft.AspNetCore.Mvc;

namespace GoceryStore_DACN.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoaiThucPhamController : ControllerBase
    {
        [HttpGet]
       public async Task<IActionResult> GetLoaiSanPham()
        {
            return Ok("ok");
        }
    }
}
