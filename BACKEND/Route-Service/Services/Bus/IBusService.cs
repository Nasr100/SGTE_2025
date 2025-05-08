using Gridify;
using Shared.Dtos;

namespace Route_Service.Services.Bus
{
    public interface IBusService
    {
        public Task<BusResponse> AddBus(BusRequest busreq);
        public Paging<BusResponse> GetBuses(GridifyQuery gridifyQuery);
        public Task<BusResponse> GetBusById(int id);
        public Task DeleteBus(int id);
        public Task<BusResponse> UpdateBus(int id, BusRequest busreq);


    }
}
