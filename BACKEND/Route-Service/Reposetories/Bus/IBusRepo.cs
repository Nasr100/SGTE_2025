using Shared.Dtos;

namespace Route_Service.Reposetories.Bus
{
    public interface IBusRepo
    {
        public Task<BusResponse> AddBus(BusRequest bus);
        public Task<Models.Bus> GetBusById(int id);
        public IQueryable<Models.Bus> GetAllBuses();
        public  Task DeleteBus(int id);
        public Task<BusResponse> UpdateBus(int id, BusRequest BusReq);

    }
}
