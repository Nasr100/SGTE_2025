using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Dtos;
using Trip_Service.Services.Minitrip;

namespace Trip_Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MinitripController : ControllerBase
    {
        private readonly IMinitripService _minitripService;
        public MinitripController(IMinitripService minitripService)
        {
            _minitripService = minitripService;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult> GetMinitripById(int id)
        {
            try
            {
                var minitrip =await _minitripService.GetMinitripById(id);
                return Ok(minitrip);
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("trip/{Tripid}")]
        public async Task<ActionResult> GetMiniTripByTrip(int Tripid)
        {
            try
            {
                var minitrips = await _minitripService.GetMiniTripByTrip(Tripid);
                return Ok(minitrips);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateMinitrip(int id, [FromBody] MinitripRequest minitripRequest)
        {
            try
            {
                var minitrip = await _minitripService.UpdateMinitrip(id, minitripRequest);
                return Ok(minitrip);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> deleteMinitrip(int id)
        {
            try
            {
                await _minitripService.deleteMinitrip(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]

        public async Task<ActionResult> AddMiniTrip(MinitripRequest minitripRequest)
        {
            try
            {
                var minitrip = await _minitripService.AddMiniTrip( minitripRequest);
                return Ok(minitrip);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
