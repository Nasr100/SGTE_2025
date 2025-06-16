using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Dtos;
using Trip_Service.Models;
using Trip_Service.Services.AssignEmployees;

namespace Trip_Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssignController : ControllerBase
    {
        private readonly IAssignEmployeeService _service;

        public AssignController(IAssignEmployeeService service)
        {
            _service = service;
        }

        [HttpGet("minitrip/{MinitripId}")]
        public ActionResult GetAssignementsByMinitripId(int MinitripId)
        {
            try
            {
                var assignement = _service.GetAssignementsByMinitripId(MinitripId);
                return Ok(assignement);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> AssignEmployeeSeat(List<MiniTripEmployeeAssignementRequest> miniTripEmployees)
        {
            try
            {
                var assignement =await _service.AssignEmployeeSeat(miniTripEmployees);
                return Ok(assignement);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{id}")]
        public async Task<ActionResult> GetAssignementsById(int id)
        {
            try
            {
                var assignement =await _service.GetAssignementsById(id);
                return Ok(assignement);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> UnassignEmployee(int id)
        {
            try
            {
              await _service.UnassignEmployee(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
