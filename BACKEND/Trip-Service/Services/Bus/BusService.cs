using Gridify;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Shared.Dtos;
using Trip_Service.Enums;
using Trip_Service.Repositories.Bus;

namespace Trip_Service.Services.Bus
{
    public class BusService : IBusService
    {
        private readonly IBusRepo _busRepo;

        public BusService(IBusRepo busRepo)
        {
            _busRepo = busRepo;
        }

        public async Task<BusResponse> AddBus(BusRequest busRequest)
        {
            var bus =await _busRepo.AddBus(busRequest);
            return bus;
        }

        public Paging<BusResponse> GetAllBuses(GridifyQuery gridify)
        {
            var buses = _busRepo.GetAllBuses().Gridify(gridify);
            return buses.Adapt<Paging<BusResponse>>();
        }

        public async Task<BusResponse> GetBusById(int id)
        {
            var bus =await _busRepo.GetBusById(id);
            return bus.Adapt<BusResponse>();
        }

        public async Task DeleteBus(int id)
        {
           await _busRepo.DeleteBus(id);
        }

        public async Task<BusResponse> UpdateBus(int id, BusRequest busRequest)
        {
            var bus =await _busRepo.UpdateBus(id, busRequest);
            return bus;
        }

        public async Task<List<BusResponse>> GetAvailableBuses(string currentShift, int? currentTripId = null)
        {
            List<Models.Bus> buses =await _busRepo.GetAvailableBusesAsync(currentShift, currentTripId);
            return buses.Adapt<List<BusResponse>>();
        }


    }
}
