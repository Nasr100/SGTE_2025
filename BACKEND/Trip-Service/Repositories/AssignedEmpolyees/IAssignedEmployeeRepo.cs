using Shared.Dtos;

namespace Trip_Service.Repositories.AssignedEmpolyees
{
    public interface IAssignedEmployeeRepo
    {
        public Task<List<MiniTripEmployeeAssignementResponse>> AssignEmployeeSeat(List<MiniTripEmployeeAssignementRequest> miniTripEmployees);
        public IQueryable<Models.MiniTripEmployeeAssignement> GetAssignementsByMinitripId(int MinitripId);
        public Task<Models.MiniTripEmployeeAssignement> GetAssignementsById(int id);
        public Task UnassignEmployee(int id);




    }
}
