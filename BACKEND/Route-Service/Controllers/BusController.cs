using Gridify;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Route_Service.Services.Bus;
using Shared.Dtos;

namespace Route_Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BusController : ControllerBase
    {
        private readonly IBusService _busService;
        public BusController(IBusService busService)
        {
            _busService = busService;
        }
        [HttpPost]
        public async Task<ActionResult> AddBus(BusRequest busReq)
        {
            try
            {
                var bus = await _busService.AddBus(busReq);
                return Ok(bus);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        public  ActionResult GetAllBuses(GridifyQuery gridify)
        {
            try
            {
                var bus =  _busService.GetBuses(gridify);
                return Ok(bus);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{id}")]
        public async Task<ActionResult> GetBusById(int id)
        {
            try
            {
                var bus =await _busService.GetBusById(id);
                return Ok(bus);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public  ActionResult DeleteBus(int id)
        {
            try
            {
                var bus = _busService.DeleteBus(id);
                return Ok(bus);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPut("{id}")]
        public ActionResult DeleteBus(int id, [FromBody] BusRequest busReq)
        {
            try
            {
                var bus = _busService.UpdateBus(id, busReq);
                return Ok(bus);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
