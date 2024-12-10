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
                var fitness = await _thucDonTuanService.Fitness(thuatToan, id);
                stopWath.Stop();   
                return Ok(new
                {
                    results = thuatToan,
                    thoiGianThucThi = stopWath.ElapsedMilliseconds / 1000,
                    soFitness = fitness
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
        public IActionResult GenerateThucDon()
        {
            try
            {
                var getAll =  _thucDonTuanService.GenerateThucDonTuan();
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

        [HttpGet("/khoiTaoQuanThe")]
        public IActionResult KhoiTaoQuanThe()
        {
            try
            {
                Stopwatch stopWath = new Stopwatch();
                stopWath.Start();
                var getAll = _thucDonTuanService.KhoiTaoQuanThe();
                int numberOfCores = Environment.ProcessorCount;
                //Console.WriteLine($"Số lõi CPU: {numberOfCores}");
                stopWath.Stop();
                return Ok(new
                {
                    //results = getAll,
                    loi = numberOfCores,
                    thoiGianThucThi = stopWath.ElapsedMilliseconds / 1000
                }) ;
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
        public IActionResult GenerateThucDonNgay()
        {
            try
            {
                var history = new Dictionary<int, Queue<int>>();
                Random random = new Random();
                var ngay = random.Next(1, 8);
                var getAll = _thucDonTuanService.GenerateThucDonNgay(ngay, history);
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
