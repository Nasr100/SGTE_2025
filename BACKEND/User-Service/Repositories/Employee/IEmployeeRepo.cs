using Shared.Dtos;

namespace User_Service.Repositories.Employee
{
    public interface IEmployeeRepo
    {
        public  Task<Models.Employee> GetByEmail(string email);
        public Task<Models.Employee> GetByRefreshToken(string RefreshToken);




    }
}
