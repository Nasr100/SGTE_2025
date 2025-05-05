using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Dtos;
using User_Service.Repositories.Employee;
using User_Service.Services.Auth;
namespace User_Service.Controllers
{
    [Route("api/user/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        //private readonly IEmployeeRepo _employeeRepo;
        public AuthController(IAuthService authService)
        {
            //_employeeRepo = employeeRepo;
            _authService = authService;
        }
        
        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<ActionResult> Login([FromBody]AuthRequest  AuthReq)
        {
            try
            {
                var response = await _authService.Login(AuthReq);
                return Ok(response);
            }
            catch (Exception ex)
            {
            return BadRequest(ex.Message);
            
            } 
        }

        [HttpPost("Refresh-token")]
        public async Task<ActionResult> RefreshToken([FromBody]string refreshToken)
        {
            try
            {
                var token = await _authService.checkRefreshToken(refreshToken);
                return Ok(token); 
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message}");
            } 
        }

        //[HttpPost("Logout")]
        //public async Task<ActionResult> Logout(int id)
        //{
        //    try
        //    {

        //    }
        //}
      
    }
}
