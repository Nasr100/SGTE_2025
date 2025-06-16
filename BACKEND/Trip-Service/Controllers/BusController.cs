using Gridify;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Shared.Dtos;
using Trip_Service.Services.Bus;

namespace Trip_Service.Controllers
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
        public async Task<ActionResult> AddBus(BusRequest busRequest)
        {
            try
            {
                var bus =await _busService.AddBus(busRequest);
                return Ok(bus);
            }catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet]
        public ActionResult GetAllBuses([FromQuery]GridifyQuery gridify)
        {
            try
            {
                var buses =  _busService.GetAllBuses(gridify);
                return Ok(buses);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetBusById(int id)
        {
            try
            {
                var bus = await _busService.GetBusById(id);
                return Ok(bus);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteBus(int id)
        {
            try
            {
                await _busService.DeleteBus(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut]
        public async Task<ActionResult> UpdateBus(int id, BusRequest busRequest)
        {

            try
            {
                var bus = await _busService.UpdateBus(id, busRequest);
                return Ok(bus);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("AvailableBuses")]
        public async Task<ActionResult> GetAvailableBuses([FromQuery]string currentShift,[FromRoute] int? currentTripId = null)
        {

            try
            {
                var buses = await _busService.GetAvailableBuses(currentShift, currentTripId);
                return Ok(buses);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }



    }
}
