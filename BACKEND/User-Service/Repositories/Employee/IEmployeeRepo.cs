using Shared.Dtos;

namespace User_Service.Repositories.Employee
{
    public interface IEmployeeRepo
    {
        public  Task<Models.Employee> GetByEmail(string email);
        public Task<Models.Employee> GetByRefreshToken(string RefreshToken);
        public Task<EmployeeResponse> AddEmployee(EmployeeRequest employee);
        public  Task<Models.Employee> GetEmployeeById(int id);
        public IQueryable<Models.Employee> GetAllEmployees();
        public  Task DeleteEmployee(int id);
        public Task<EmployeeResponse> UpdateEmployeer(int id, EmployeeRequest employeeRequest);
        public IQueryable<Models.Employee> GetEmployeeByRole(string Role);





    }
}
