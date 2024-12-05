using GoceryStore_DACN.Services;
using GoceryStore_DACN.Services.Interface;
using GroceryStore_DACN.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace GoceryStore_DACN.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ThucDonController : ControllerBase
    {
        private readonly IThucDonTuanService _thucDonTuanService;

        public ThucDonController(IThucDonTuanService services)
        {
            _thucDonTuanService = services;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult> ThuatToanGA(int id)
        {
            var stopWath = new Stopwatch();
            try
            {
                stopWath.Start();
                var thuatToan = await _thucDonTuanService.ThuatToanGA(id);
                stopWath.Stop();   
                return Ok(new
                {
                    results = thuatToan,
                    thoiGianThucThi = stopWath.ElapsedMilliseconds / 60000.0
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


        [HttpGet]
        public async Task<IActionResult> GenerateThucDon()
        {
            try
            {
                var getAll = await _thucDonTuanService.GenerateThucDonTuan();
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

        [HttpGet("/generateThucDonNgay")]
        public async Task<IActionResult> GenerateThucDonNgay()
        {
            try
            {
                var history = new Dictionary<string, Queue<int>>();
                var getAll = await _thucDonTuanService.GenerateThucDonNgay(2, history);
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
    }
}
