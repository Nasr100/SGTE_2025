using Gridify;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Dtos;
using Trip_Service.Services.Trip;

namespace Trip_Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TripController : ControllerBase
    {
        private readonly ITripService _tripService;
        public TripController(ITripService tripService)
        {
            _tripService = tripService; 
        }

        [HttpGet]
        public ActionResult GetAllTrips([FromQuery]GridifyQuery gridify)
        {
            try
            {
                var trips = _tripService.GetAllTrips(gridify);
                return Ok(trips);
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);  
            }
        }


        [HttpGet("{id}")]
        public async Task<ActionResult> GetTripById(int id)
        {
            try
            {
                var trip =await _tripService.GetTripById(id);
                return Ok(trip);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateTrip(int id, TripRequest tripRequest)
        {
            try
            {
                var trip =await _tripService.UpdateTrip(id, tripRequest);
                return Ok(trip);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTrip(int id)
        {
            try
            {
                await  _tripService.DeleteTrip(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> AddTrip(TripRequest tripRequest)
        {
            try
            {
                var trip = await _tripService.AddTrip(tripRequest);
                return Ok(trip);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
