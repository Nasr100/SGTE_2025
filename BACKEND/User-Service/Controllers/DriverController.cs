using Gridify;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Shared.Dtos;
using User_Service.Services.Administration;
using User_Service.Services.Driver;

namespace User_Service.Controllers
{
    [Route("api/user/[controller]")]
    [ApiController]
    public class DriverController : ControllerBase
    {
        private readonly IDriverService _driverService;
        public DriverController(IDriverService driverService)
        {
            _driverService = driverService;
        }

        [HttpPost]
        public async Task<ActionResult> addDrievr(DriverRequest driverRequest)
        {
            await _driverService.Add(driverRequest);
            return Ok("Driver added succefully");
        }

        [HttpGet]
        public ActionResult GetAll([FromQuery] GridifyQuery gridifyQuery)
        {
            try
            {
                var response = _driverService.GetAll(gridifyQuery);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            var response = await _driverService.GetById(id);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _driverService.Delete(id);
                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] DriverRequest driverRequest)
        {
            try
            {
                var response = await _driverService.Update(id, driverRequest);
                return Ok(response);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> Patch(int id, [FromBody] Dictionary<string, object> updates)
        {
            try
            {

                var response = await _driverService.PartialUpdate(id, updates);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
