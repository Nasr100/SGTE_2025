using Gridify;
using Mapster;
using Route_Service.Reposetories.Bus;
using Shared.Dtos;

namespace Route_Service.Services.Bus
{
    public class BusService : IBusService
    {
        private readonly IBusRepo _busRepo;

        public BusService(IBusRepo busRepo)
        {
            _busRepo = busRepo;
        }

        public async Task<BusResponse> AddBus(BusRequest busreq)
        {
            var bus = await _busRepo.AddBus(busreq);
            return bus; 
        }

        public Paging<BusResponse> GetBuses(GridifyQuery gridifyQuery)
        {
            var buses = _busRepo.GetAllBuses().Gridify(gridifyQuery);
            return buses.Adapt<Paging<BusResponse>>();
        }

        public async Task<BusResponse> GetBusById(int id)
        {
            var bus = await _busRepo.GetBusById(id);
            return bus.Adapt<BusResponse>();
        }

        public async Task DeleteBus(int id)
        {
            await _busRepo.DeleteBus(id);
        }

        public async Task<BusResponse> UpdateBus(int id, BusRequest busreq)
        {
            var bus = await _busRepo.UpdateBus(id, busreq); 
            return bus;
        }
    }
}
