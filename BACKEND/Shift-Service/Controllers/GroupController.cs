using Gridify;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Dtos;
using Shift_Service.Repositories.Group;
using Shift_Service.Services.Group;

namespace Shift_Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupController : ControllerBase
    {
        private readonly IGroupService _groupService;

        public GroupController(IGroupService _groupService)
        {
            this._groupService = _groupService; 
        }

        [HttpGet]
        public ActionResult GetAllGroups([FromQuery]GridifyQuery gridify)
        {
            try
            {
                var shifts = _groupService.GetAllGroups(gridify);
                return Ok(shifts);
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{id}")]
        public async Task<ActionResult> GetGroupById(int id)
        {
            try
            {
                var shift =await _groupService.GetGroupById(id);
                return Ok(shift);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteGroup(int id)
        {
            try
            {
               await _groupService.DeleteGroup(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> AddGroup(GroupRequest groupRequest)
        {
            try
            {
                var group = await _groupService.AddGroup(groupRequest);
                return Ok(group);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateGroup(int id,[FromBody] GroupRequest groupReq)
        {
            try
            {
                var group = await _groupService.UpdateGroup(id,groupReq);
                return Ok(group);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("role")]
        public async Task<ActionResult> GetGroupsByRole(string role)
        {
            try
            {
                var groups = await _groupService.GetGroupsByRole(role);
                return Ok(groups);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
