using Gridify;
using Shared.Dtos;

namespace User_Service.Services.Employee
{
    public interface IEmployeeService
    {
        public Task<EmployeeResponse> AddEmployee(EmployeeRequest employeeRequest);
        public Paging<EmployeeResponse> GetAll(GridifyQuery gridifyQuery);
        public Task<EmployeeResponse> GetById(int id, CancellationToken cancellationToken = default);
        public  Task<EmployeeResponse> Update(int id, EmployeeRequest employeeRequest);
        public  Task Delete(int id);
        public Task<Models.Employee> GetEmployeeByEmail(string email);
        public Task<Models.Employee> GetByRefreshToken(string RefreshToken);
        public  Task<EmployeeResponse> GetDriversByGroupId(int groupId);





    }
}
