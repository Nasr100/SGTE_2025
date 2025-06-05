using Gridify;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Dtos;
using Shift_Service.Services.Shift;

namespace Shift_Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShiftController : ControllerBase
    {
        private readonly IShiftService _shiftService;
        public ShiftController(IShiftService shiftService)
        {
            this._shiftService = shiftService;
        }

        [HttpGet]
        public ActionResult GetAllShifts(GridifyQuery gridify)
        {
            try
            {
                var shifts = _shiftService.GetAllShifts(gridify);
                return Ok(shifts);
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);  
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetShiftById(int id)
        {
            try
            {
                var shift = await _shiftService.GetShifteById(id);
                return Ok(shift);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteShiftById(int id)
        {
            try
            {
                await _shiftService.DeleteShift(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> AddShift([FromBody] ShiftRequest shiftRequest)
        {
            try
            {
                var shift = await _shiftService.AddShift(shiftRequest);
                return Ok(shift);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateShift([FromBody]ShiftRequest shiftRequest,int id)
        {
            try
            {
                var shift = await _shiftService.UpdateShift(id,shiftRequest);
                return Ok(shift);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
