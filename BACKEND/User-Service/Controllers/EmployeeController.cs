using Gridify;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Dtos;
using User_Service.Services.Employee;

namespace User_Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        public EmployeeController(IEmployeeService employeeService)
        { 
            _employeeService = employeeService;
        }

        [HttpPost]
        public async Task<ActionResult> AddEmployee([FromBody] EmployeeRequest employeeRequest)
        {
            try
            {
                var employee = await _employeeService.AddEmployee(employeeRequest);
                return  Ok(employee);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message); 
            }
        }

        [HttpGet]
        public ActionResult GetEmployee([FromQuery] GridifyQuery gridify)
        {
            try
            {
                var employees = _employeeService.GetAll(gridify);
                return Ok(employees);
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetEmployeeById(int id)
        {
            try
            {
                var employees =await _employeeService.GetById(id);
                return Ok(employees);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteEmployee(int id)
        {
            try
            {
                await _employeeService.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateEmployee(int id,[FromBody]EmployeeRequest EmployeeRequest)
        {
            try
            {
                var employee = await _employeeService.Update(id,EmployeeRequest);
                return Ok(employee);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("group/{groupId}")]
        public async Task<ActionResult> GetDriversByGroupId(int groupId)
        {
            try
            {
                var employees = await _employeeService.GetDriversByGroupId(groupId);
                return Ok(employees);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("/Trip")]
        public async Task<ActionResult> GetEmployeesByTripShiftRole([FromQuery] string shift, [FromQuery] string Role)
        {
            try
            {
                var employees = await _employeeService.GetEmployeesByShift(shift,Role);
                return Ok(employees);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult> GetEmployeeByRole([FromQuery]string role)
        {
            try
            {
                var employees = await _employeeService.GetEmployeeByRole(role);
                return Ok(employees);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
