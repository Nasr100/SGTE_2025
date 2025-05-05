using Mapster;
using Microsoft.EntityFrameworkCore;
using Shared.Dtos;
using User_Service.Data;
using User_Service.Repositories.Administration;

namespace User_Service.Repositories.Employee
{
    public class EmployeeRepo : IEmployeeRepo
    {
        private readonly UserServiceContext _context;
        public EmployeeRepo(UserServiceContext context)
        {
            _context = context;
        }

        public async Task<Models.Employee> GetByEmail(string email)
        {

            var administration = await _context.Employees.FirstOrDefaultAsync(e => e.Email == email);
            if (administration == null)
            {
                throw new KeyNotFoundException("Email not found");
            }
            return administration;

        }
        public async Task<Models.Employee> GetByRefreshToken(string RefreshToken)
        {
            var administration = await _context.Employees.FirstOrDefaultAsync(e => e.RefreshToken == RefreshToken);
            if (administration == null)
            {
                throw new KeyNotFoundException("Refresh Token not found");
            }
            return administration;
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
