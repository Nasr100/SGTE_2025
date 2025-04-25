using Shared.Dtos;
using User_Service.Models;

namespace User_Service.Services.Auth
{
    public interface IAuthService
    {
        public Task<object> Login(AuthRequest AuthReq);
        public Task<string> checkRefreshToken(string RefreshToken);


    }
}
