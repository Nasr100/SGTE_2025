using Shared.Dtos;

namespace Trip_Service.Services.AssignEmployees
{
    public interface IAssignEmployeeService
    {
        public Task<List<MiniTripEmployeeAssignementResponse>> AssignEmployeeSeat(List<MiniTripEmployeeAssignementRequest> miniTripEmployees);
        public List<MiniTripEmployeeAssignementResponse> GetAssignementsByMinitripId(int MinitripId);
        public Task<List<MiniTripEmployeeAssignementResponse>> GetAssignementsById(int id);
        public Task UnassignEmployee(int id);

    }
}
