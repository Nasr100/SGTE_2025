using Shared.Dtos;
using Trip_Service.Enums;

namespace Trip_Service.Repositories.Bus
{
    public interface IBusRepo
    {
        public Task<BusResponse> AddBus(BusRequest busRequest);
        public IQueryable<Models.Bus> GetAllBuses();
        public Task<Models.Bus> GetBusById(int id);
        public Task DeleteBus(int id);
        public Task<BusResponse> UpdateBus(int id, BusRequest busRequest);
        public Task<List<Models.Bus>> GetAvailableBusesAsync(string currentShift, int? currentTripId = null);

    }
}
