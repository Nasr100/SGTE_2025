using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Security.Cryptography;
using System.Linq;
using Shared.Dtos;
using User_Service.Repositories.Employee;
using User_Service.Data;
using User_Service.Services.Employee;


namespace User_Service.Services.Auth
{
    public class AuthService(IConfiguration configuration, IEmployeeService _employeeService, UserServiceContext _context,ILogger<AuthService> _logger) : IAuthService
    {
       
        public async Task<object> Login(AuthRequest AuthReq)
        {

            var employee = await _employeeService.GetEmployeeByEmail(AuthReq.Email);
            if (employee != null && employee.Password == AuthReq.Password)
            {
                string accessToken = GenerateAccessToken(employee);
                var accessTokenExpiry = DateTime.UtcNow.AddHours(1);


                string refreshToken = GenerateRefreshToken();
                var refreshTokenExpiry = DateTime.UtcNow.AddDays(7);

                employee.AccessToken = accessToken; 
                employee.RefreshToken = refreshToken;
                employee.RefreshTokenExpiry = refreshTokenExpiry;
                employee.AccessTokenExpiry = accessTokenExpiry;
                 _context.Employees.Update(employee);
                await _context.SaveChangesAsync();
                return new
                {
                    AccessToken = accessToken,
                    AccessTokenExpiry = accessTokenExpiry,
                    RefreshToken = refreshToken,
                    RefreshTokenExpiry = refreshTokenExpiry
                };
            }
            return new { Message = "Invalid email or password" };
        }


        private string GenerateAccessToken(Models.Employee employee)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, employee.Id.ToString()),
                new Claim(ClaimTypes.Email, employee.Email),
                new Claim(ClaimTypes.Role, employee.Role.ToString())

            };
            
 
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:SecretKey"]!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var tokenExpiry = DateTime.UtcNow.AddHours(1);

            var token = new JwtSecurityToken(
                issuer: configuration["JwtSettings:Issuer"],
                audience: configuration["JwtSettings:Audience"],
                claims: claims,
                expires: tokenExpiry,
                signingCredentials: creds
            );

            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);
        }
        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);

                return HashRefreshToken(Convert.ToBase64String(randomNumber));
            }
        }
        private string HashRefreshToken(string refreshToken)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(refreshToken));
                return Convert.ToBase64String(hashedBytes);
            }
        }

        public async Task<string> checkRefreshToken(string RefreshToken)
        {
            var employee = await _employeeService.GetByRefreshToken(RefreshToken);
            if (employee.RefreshTokenExpiry < DateTime.UtcNow)
            {
                throw new Exception("refresh token expired");
            }
            return GenerateAccessToken(employee);

        }
    }
}
