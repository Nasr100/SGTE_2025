using Mapster;
using Shared.Dtos;
using Trip_Service.Models;
using Trip_Service.Repositories.AssignedEmpolyees;

namespace Trip_Service.Services.AssignEmployees
{
    public class AssignEmployeeService : IAssignEmployeeService
    {
        private readonly IAssignedEmployeeRepo _assignedEmployeeRepo;
        public AssignEmployeeService(IAssignedEmployeeRepo assignedEmployeeRepo)
        {
            _assignedEmployeeRepo = assignedEmployeeRepo;
        }

        public async  Task<List<MiniTripEmployeeAssignementResponse>> AssignEmployeeSeat(List<MiniTripEmployeeAssignementRequest> miniTripEmployees)
        {
            var assignement = await _assignedEmployeeRepo.AssignEmployeeSeat(miniTripEmployees);
            return assignement;
        }
        public  List<MiniTripEmployeeAssignementResponse> GetAssignementsByMinitripId(int MinitripId)
        {
            var assignements =  _assignedEmployeeRepo.GetAssignementsByMinitripId(MinitripId);
            return assignements.Adapt<List<MiniTripEmployeeAssignementResponse>>();
        }

        public async Task<List<MiniTripEmployeeAssignementResponse>> GetAssignementsById(int id)
        {
            var assignements =await _assignedEmployeeRepo.GetAssignementsById(id);
            return assignements.Adapt<List<MiniTripEmployeeAssignementResponse>>();
        }
        public async Task UnassignEmployee(int id)
        {
             await _assignedEmployeeRepo.UnassignEmployee(id);

        }

    }
}
