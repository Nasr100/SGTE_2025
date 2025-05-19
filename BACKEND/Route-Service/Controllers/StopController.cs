using Gridify;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Route_Service.Services.Stop;
using Shared.Dtos;

namespace Route_Service.Controllers
{
    [Route("api/route/[controller]")]
    [ApiController]
    public class StopController : ControllerBase
    {
        private readonly IStopService _stopService;

        public StopController(IStopService stopService)
        {
            _stopService = stopService;
        }
        [HttpPost]
        public async Task<ActionResult> AddStop(StopRequest stopreq)
        {
            try
            {
                var stop = await _stopService.AddStop(stopreq);
                return Ok(stop);
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public  ActionResult GetAllStops([FromQuery]GridifyQuery gridify)
        {
            try
            {
                var stops =  _stopService.GetStops(gridify);
                return Ok(stops);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetStopById(int id)
        {
            try
            {
                var stops = await _stopService.GetStopById(id);
                return Ok(stops);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPut("{id}")]
        public ActionResult UpdateStop(int id, StopRequest stopreq)
        {
            try
            {
                var stops = _stopService.UpdateStop(id, stopreq);
                return Ok(stops);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteStop(int id)
        {
            try
            {
                var stops = _stopService.DeleteStop(id);
                return Ok(stops);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
