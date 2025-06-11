using System.Collections.Generic;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Shared.Dtos;
using Trip_Service.Data;

namespace Trip_Service.Repositories.AssignedEmpolyees
{
    public class AssignedEmployeesRepo
    {
        private readonly TripServiceContext _TripServiceContext;
        public AssignedEmployeesRepo(TripServiceContext TripServiceContext)
        {
            _TripServiceContext = TripServiceContext;
        }
        public async Task<List<MiniTripEmployeeAssignementResponse>> AssignEmployeeSeat(List<MiniTripEmployeeAssignementRequest> miniTripEmployees)
        {
            List<MiniTripEmployeeAssignementResponse> miniTripEmployeeAssignementResponses = new List<MiniTripEmployeeAssignementResponse>();
            foreach (var employee in miniTripEmployees)
            {
                int LastAssignedSeat = GetLastSeatNumber(employee.MiniTripId);
                if (LastAssignedSeat < 30)
                {
                    var minitripAssignementModel = miniTripEmployees.Adapt<Models.MiniTripEmployeeAssignement>();
                    minitripAssignementModel.SeatNumber = LastAssignedSeat++;
                    var x = await _TripServiceContext.miniTripEmployees.AddAsync(minitripAssignementModel);
                    await _TripServiceContext.SaveChangesAsync();
                    miniTripEmployeeAssignementResponses.Add(x.Entity.Adapt<MiniTripEmployeeAssignementResponse>());
                }
            }
            return miniTripEmployeeAssignementResponses;
        }

        public IQueryable<Models.MiniTripEmployeeAssignement> GetAssignementsByMinitripId(int MinitripId)
        {
            return _TripServiceContext.miniTripEmployees.Where(e => e.MiniTripId == MinitripId) ?? throw new Exception("minitrip not found");
        }

        public async Task<Models.MiniTripEmployeeAssignement> GetAssignementsById(int id)
        {
            var assignement = await _TripServiceContext.miniTripEmployees.FirstOrDefaultAsync(e => e.Id == id) ?? throw new Exception("Employee assignement not found");
            return assignement;
        }

        private int GetLastSeatNumber(int MinitripId)
        {
            var minitripAssignementModel = GetAssignementsByMinitripId(MinitripId).OrderByDescending(e => e.CreatedAt).Take(1).FirstOrDefault() ?? throw new Exception("minitrip not found");
            int lastAssignedSeat = minitripAssignementModel.SeatNumber;
            return lastAssignedSeat;

        }

        public async  Task UnassignEmployee(int id)
        {
            var assignement = await GetAssignementsById(id);
            _TripServiceContext.miniTripEmployees.Remove(assignement);
            await _TripServiceContext.SaveChangesAsync();
        }


    }
}
