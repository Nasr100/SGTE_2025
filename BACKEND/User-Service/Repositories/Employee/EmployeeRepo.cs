using Mapster;
using Microsoft.EntityFrameworkCore;
using Shared.Dtos;
using User_Service.Data;

namespace User_Service.Repositories.Employee
{
    public class EmployeeRepo : IEmployeeRepo
    {
        private readonly UserServiceContext _context;
        public EmployeeRepo(UserServiceContext context)
        {
            _context = context;
        }
        public async Task<EmployeeResponse> AddEmployee(EmployeeRequest employee)
        {
            var Employeemodel = employee.Adapt<Models.Employee>();
            await _context.Employees.AddAsync(Employeemodel);
            await _context.SaveChangesAsync();
            return Employeemodel.Adapt<EmployeeResponse>();
        }
        public async Task<Models.Employee> GetEmployeeById(int id)
        {
            var employee = await _context.Employees.Where(e=>!e.IsDeleted).FirstOrDefaultAsync(e=>e.Id==id);
            if (employee == null)
            {
                throw new InvalidOperationException("Employee with the id " + id + "not found");
            }
            return employee;
        }

        public IQueryable<Models.Employee> GetAllEmployees()
        {
            var employee = _context.Employees.Where(e => !e.IsDeleted).AsQueryable();
            return employee;
        }

        public async Task DeleteEmployee(int id)
        {
            var employee = await GetEmployeeById(id);
            employee.IsDeleted = true;
            _context.Employees.Update(employee);
            await _context.SaveChangesAsync();
        }
        public async Task<EmployeeResponse> UpdateEmployeer(int id, EmployeeRequest employeeRequest)
        {
            var employee = await GetEmployeeById(id);
            employeeRequest.Adapt(employee);
            await _context.SaveChangesAsync();
            return employee.Adapt<EmployeeResponse>();

        }
        public async Task<Models.Employee> GetByEmail(string email)
        {

            var employee = await _context.Employees.FirstOrDefaultAsync(e => e.Email == email);
            if (employee == null)
            {
                throw new KeyNotFoundException("Email not found");
            }
            return employee;

        }
        public async Task<Models.Employee> GetByRefreshToken(string RefreshToken)
        {
            var employee = await _context.Employees.FirstOrDefaultAsync(e => e.RefreshToken == RefreshToken);
            if (employee == null)
            {
                throw new KeyNotFoundException("Refresh Token not found");
            }
            return employee;
        }

        public IQueryable<Models.Employee> GetEmployeeByRole(string Role)
        {
            var emloyees = GetAllEmployees().Where(e => e.Role.ToString().Equals(Role));
            return emloyees;
        }

        

        //public async Task<AdministrationResponse> UpdateEmployee(int id, AdministrationRequest administration)
        //{
        //    var member = await GetAdministrationById(id);
        //    member = administration.Adapt<Models.Administration>();
        //    _context.Administrations.Update(member);
        //    await _context.SaveChangesAsync();
        //    return member.Adapt<AdministrationResponse>();

        //}
    }
}
