using Gridify;
using Shared.Dtos;

namespace Trip_Service.Services.Bus
{
    public interface IBusService
    {
        public  Task<BusResponse> AddBus(BusRequest busRequest);
        public Paging<BusResponse> GetAllBuses(GridifyQuery gridify);
        public Task<BusResponse> GetBusById(int id);
        public  Task DeleteBus(int id);
        public Task<BusResponse> UpdateBus(int id, BusRequest busRequest);
        public Task<List<BusResponse>> GetAvailableBuses(string currentShift, int? currentTripId = null);




    }
}
