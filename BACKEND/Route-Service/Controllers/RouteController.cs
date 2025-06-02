using Gridify;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Route_Service.Services.Route;
using Shared.Dtos;

namespace Route_Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RouteController : ControllerBase
    {
        private readonly IRouteService _routeService;

        public RouteController(IRouteService routeService) 
        {
            _routeService = routeService;
        }

        [HttpGet]
        public ActionResult GetAllRoutes([FromQuery] GridifyQuery gridify)
        {
            try
            {
                var routes = _routeService.GetRoutes(gridify);
                return Ok(routes);
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> AddRoute([FromBody] ComplexRouteStopsRequest routeReq)
        {
            try
            {
                var RouteStop = await _routeService.AddRoute(routeReq);
                return Ok(RouteStop);
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetRouteById(int id)
        {
            try
            {
                var route = await _routeService.GetRouteById(id);
                return Ok(route);
            }
            catch (Exception ex)
            {
                return NotFound();
            }
            
        }

        [HttpDelete("{id}")]

        public async  Task<ActionResult> DeleteRoute(int id) 
        {
            try
            {
                await _routeService.DeleteRoute(id);
                return Ok();
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> EditRoute(int id,[FromBody]RouteRequest routeReq)
        {
            try
            {
                var route = await _routeService.UpdateRoute(id, routeReq);
                return Ok(route);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("stop")]
        public async Task<ActionResult> AssignStop(int routeId,RouteStopsRequest routeStopsRequest)
        {
            try
            {
                var routeStop = await _routeService.AssignStop(routeId, routeStopsRequest);
                return Ok(routeStop);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


        [HttpDelete("stop")]
        public async Task<ActionResult> DeleteAssignStop( int stopId,  int routeId)
        {
            try
            {
                await _routeService.DeleteAssignedStop(routeId, stopId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }



        [HttpPut("stop")]
        public async Task<ActionResult> UpdateAssignedStop([FromBody]RouteStopsRequest routeStopReq, int routeId)
        {
            try
            {
                var routeStop = await _routeService.UpdateAssignedstop(routeStopReq, routeId, routeStopReq.StopId);
                return Ok(routeStop);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
