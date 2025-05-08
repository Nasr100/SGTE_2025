using Route_Service.Data;
using Shared.Dtos;
using Mapster;
using Route_Service.Models;
using Microsoft.EntityFrameworkCore;

namespace Route_Service.Reposetories.Bus
{
    public class BusRepo : IBusRepo
    {
        private readonly RouteServiceContext _context;

        public BusRepo(RouteServiceContext context)
        {
            _context = context; 
        }

        public async Task<BusResponse> AddBus(BusRequest bus)
        {
            var busModel = bus.Adapt<Models.Bus>();
            await _context.Buses.AddAsync(busModel);
            await _context.SaveChangesAsync();
            return busModel.Adapt<BusResponse>();
        }

        public async Task<Models.Bus> GetBusById(int id)
        {
            var bus = await _context.Buses.FindAsync(id);
            if (bus == null)
            {
                throw new Exception("bus with id "+id+"not found");
            }
            return bus;
        }

        public IQueryable<Models.Bus> GetAllBuses()
        {
            var buses = _context.Buses.Where(b=>b.IsDeleted == false);
            return buses;
        }

        public async Task DeleteBus(int id)
        {
            var bus = await GetBusById(id);
            bus.IsDeleted = true;
            _context.Buses.Update(bus);
            await _context.SaveChangesAsync();
        }

        public async Task<BusResponse> UpdateBus(int id,BusRequest BusReq)
        {
            var bus = await GetBusById(id);
            BusReq.Adapt(bus);
            await _context.SaveChangesAsync();
            return bus.Adapt<BusResponse>();
        }
    }
}
