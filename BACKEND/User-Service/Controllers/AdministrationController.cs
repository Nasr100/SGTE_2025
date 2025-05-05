using Gridify;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Shared.Dtos;
using User_Service.Models;
using User_Service.Services.Administration;

namespace User_Service.Controllers
{
    [Route("api/user/[controller]")]
    [ApiController]
    public class AdministrationController : ControllerBase
    {
        private readonly IAdministrationService _administrationService;
        private readonly ILogger<AdministrationController> _logger;
        public AdministrationController(IAdministrationService administrationService, ILogger<AdministrationController> logger)
        {
            _administrationService = administrationService;
            _logger = logger;
        }   

        [HttpPost]
        public async Task<ActionResult> addAdministration(AdministrationRequest administration)
        {
            await _administrationService.Add(administration);
            return Ok("Administration added succefully");
        }
        //[Authorize(Roles ="Admin")]
        [HttpGet]
        public  ActionResult GetAll([FromQuery] GridifyQuery gridifyQuery)
        {
            try
            {
                var response = _administrationService.GetAll(gridifyQuery);
                return Ok(response);
            }
            catch ( Exception ex)
            {

                return BadRequest(ex.Message);
            }
           
            
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            try
            {
                var response = await _administrationService.GetById(id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _administrationService.Delete(id);
                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);  
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] AdministrationRequest administration)
        {
            try
            {
                var response = await _administrationService.Update(id, administration);
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
                _logger.LogInformation("dictionary" + JsonConvert.SerializeObject(updates));

                var response = await _administrationService.PartialUpdate(id, updates);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
