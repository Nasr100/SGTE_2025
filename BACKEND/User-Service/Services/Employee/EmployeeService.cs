using System.Buffers.Text;
using System.Collections.Generic;
using System.Threading;
using Gridify;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Shared.Dtos;
using User_Service.Enums;
using User_Service.Models;
using User_Service.Repositories.Employee;

namespace User_Service.Services.Employee
{
    public class EmployeeService : IEmployeeService
    {
        public readonly IEmployeeRepo _EmployeeRepo;
        public readonly ILogger<EmployeeService> _logger;
        private string StopServiceBaseUrl = "https://localhost:7025/api";
        private string GroupServiceBaseUrl = "https://localhost:7009/api";
        private HttpClient _httpClient;
        public EmployeeService(IEmployeeRepo EmployeeRepo, ILogger<EmployeeService> logger)
        {
            _EmployeeRepo = EmployeeRepo;
            _httpClient = new HttpClient();
            _logger = logger;
        }

        public async Task<EmployeeResponse> AddEmployee(EmployeeRequest employeeRequest)
        {
            var responseModel = await _EmployeeRepo.AddEmployee(employeeRequest);

            return responseModel;
        }

        public Paging<EmployeeResponse> GetAll(GridifyQuery gridifyQuery)
        {
            var response = _EmployeeRepo.GetAllEmployees().Gridify(gridifyQuery);
            return response.Adapt<Paging<EmployeeResponse>>();
        }

        public async Task<EmployeeResponse> GetById(int id, CancellationToken cancellationToken = default)
        {
            try
            {
                var employee = await _EmployeeRepo.GetEmployeeById(id);
                if (employee == null)
                {
                    _logger.LogWarning("Employee with ID {EmployeeId} not found", id);
                    return null;
                }

                var stopTask = _httpClient.GetFromJsonAsync<StopResponse>(
                    $"{StopServiceBaseUrl}/route/Stop/{employee.StopId}",
                    cancellationToken);

                var groupTask = _httpClient.GetFromJsonAsync<GroupResposne>(
                    $"{GroupServiceBaseUrl}/Group/{employee.GroupId}",
                    cancellationToken);

                await Task.WhenAll(stopTask, groupTask);

                var employeeResponse = employee.Adapt<EmployeeResponse>();
                employeeResponse.Stop = await stopTask;
                employeeResponse.Group = await groupTask;

                return employeeResponse;
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error calling external service for employee ID {EmployeeId}", id);
                throw; 
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error getting employee ID {EmployeeId}", id);
                throw;
            }


        }

        public async Task<EmployeeResponse> Update(int id, EmployeeRequest employeeRequest)
        {
            var response = await _EmployeeRepo.UpdateEmployeer(id, employeeRequest);
            return response;
        }
        public async Task Delete(int id)
        {
            await _EmployeeRepo.DeleteEmployee(id);
        }

        public async Task<Models.Employee> GetEmployeeByEmail(string email)
        {
            var employee = await _EmployeeRepo.GetByEmail(email);
            return employee;
        }

        public async Task<Models.Employee> GetByRefreshToken(string RefreshToken)
        {
            var employee = await _EmployeeRepo.GetByRefreshToken(RefreshToken);
            return employee;
        }
        public int WorkerNumberPerStop(int stopId)
        {
            int workersCount = _EmployeeRepo.GetAllEmployees().Where(s => s.StopId == stopId && s.Role.Equals(RolesEnum.worker)).Count();
            return workersCount;

        }
        public int AdminNumberPerStop(int stopId)
        {
            int AdminCount = _EmployeeRepo.GetAllEmployees().Where(s => s.StopId == stopId && s.Role.Equals(RolesEnum.admin)).Count();
            return AdminCount;

        }

        public async Task<EmployeeResponse> GetDriversByGroupId(int groupId)
        {
            var drivers =await _EmployeeRepo.GetAllEmployees().Where(e => e.GroupId == groupId).ToListAsync();
            return drivers.Adapt<EmployeeResponse>();
        }

        //public async Task<List<Models.Employee>>  GetEmployeeByRole(string Role)
        //{
        //    var emloyee =  await _EmployeeRepo.GetEmployeeByRole(Role).ToListAsync();
        //    return emloyee;
        //}

        public async Task<List<EmployeeResponse>> GetEmployeesByShift(string Tripshift,string TripRole)
        {
            var Employees =  await _EmployeeRepo.GetEmployeeByRole(TripRole).ToListAsync();
            List< EmployeeResponse > employeeResponses = new List< EmployeeResponse >();
            
                var groupTask = await _httpClient.GetFromJsonAsync<GroupResposne>(
                $"{GroupServiceBaseUrl}/Group/{Tripshift}");
            if (groupTask != null)
            {
                foreach (var employee in Employees)
                {
                    if (employee.GroupId == groupTask.Id)
                    {
                        var employeeResponse = employee.Adapt<EmployeeResponse>();
                        employeeResponses.Add(employeeResponse);
                    }
                }
                return employeeResponses;
            }
            else
            {
                throw new Exception("Group shift not found");
            } 
        }
    }
}
