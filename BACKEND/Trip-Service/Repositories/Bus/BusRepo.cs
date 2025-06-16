using Mapster;
using Microsoft.EntityFrameworkCore;
using Shared.Dtos;
using Shared.Helpers;
using Trip_Service.Data;
using Trip_Service.Enums;

namespace Trip_Service.Repositories.Bus
{
    public class BusRepo : IBusRepo
    {
        private readonly TripServiceContext _tripContext;
        private readonly ILogger<BusRepo> _logger;

        public BusRepo(TripServiceContext tripContext, ILogger<BusRepo> logger)
        {
            _tripContext = tripContext;
            _logger = logger;
        }

        public async Task<BusResponse> AddBus(BusRequest busRequest)
        {
            var busModel = busRequest.Adapt<Models.Bus>();
            await _tripContext.buses.AddAsync(busModel);
            await _tripContext.SaveChangesAsync();
            return busModel.Adapt<BusResponse>();
        }

        public IQueryable<Models.Bus> GetAllBuses()
        {
            var buses = _tripContext.buses.Where(b => !b.IsDeleted);
            return buses;
        }

        public async Task<Models.Bus> GetBusById(int id)
        {
            var bus = await _tripContext.buses.Where(b => !b.IsDeleted).FirstOrDefaultAsync(b => b.Id == id) ?? throw new Exception("bus with id " + id + "not found"); ;
            return bus;
        }

        public async Task DeleteBus(int id)
        {
            var bus = await GetBusById(id);
            bus.IsDeleted = true;   
            _tripContext.buses.Update(bus);
            await _tripContext.SaveChangesAsync();
        }

        public async Task<BusResponse> UpdateBus(int id,BusRequest busRequest)
        {
            var bus = await GetBusById(id);
            busRequest.Adapt(bus);
            await _tripContext.SaveChangesAsync();
            return bus.Adapt<BusResponse>();
        }

        public async Task<List<Models.Bus>> GetAvailableBusesAsync(string currentShift, int? currentTripId = null)
        {
            try
            {
                // 1. Get all active buses
                var allActiveBuses = await _tripContext.buses
                    .Where(b => b.BusStatus.ToString().Equals("active"))
                    .ToListAsync();

                // 2. Get buses already used in this trip's minitrips
                var busesInCurrentTrip = await _tripContext.minitrips
                    .Where(mt => mt.TripId == currentTripId)
                    .Select(mt => mt.BusId)
                    .Distinct()
                    .ToListAsync();

                // 3. Get buses assigned to next shift
                ShiftTypes shift = StringToEnum.ParseEnum<ShiftTypes>(currentShift);
                var nextShift = GetNextShiftType(shift);
                var busesInNextShift = nextShift.HasValue
                    ? await _tripContext.minitrips
                        .Where(mt => mt.Trip.Shift == nextShift.Value)
                        .Select(mt => mt.BusId)
                        .Distinct()
                        .ToListAsync()
                    : new List<int>();

                // 4. Filter available buses
                return allActiveBuses
                    .Where(b => !busesInCurrentTrip.Contains(b.Id) &&
                               !busesInNextShift.Contains(b.Id))
                    .ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new Exception("Failed to get available buses", ex);
            }

        }

        private ShiftTypes? GetNextShiftType(ShiftTypes current)
        {
            return current switch
            {
                ShiftTypes.Matin => ShiftTypes.Soir,
                ShiftTypes.Soir => ShiftTypes.Nuit,
                ShiftTypes.Nuit => ShiftTypes.Matin,
                _ => null
            };
        }
    }
}
