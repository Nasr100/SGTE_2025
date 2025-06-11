using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Shared.Dtos;
using Trip_Service.Data;
using Trip_Service.Enums;
using Trip_Service.Models;

namespace Trip_Service.Services.BusAvailabilty
{
    public class BusAvailabilityService
    {
        private readonly TripServiceContext _context;
        private readonly ILogger<TripServiceContext> _logger;
        public BusAvailabilityService(TripServiceContext context, ILogger<TripServiceContext> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<List<Models.Bus>> GetAvailableBusesAsync(ShiftTypes currentShift, int? currentTripId = null)
        {
            try
            {
                // 1. Get all active buses
                var allActiveBuses = await _context.buses
                    .Where(b => b.BusStatus.ToString().Equals("active"))
                    .ToListAsync();

                // 2. Get buses already used in this trip's minitrips
                var busesInCurrentTrip = await _context.minitrips
                    .Where(mt => mt.TripId == currentTripId)
                    .Select(mt => mt.BusId)
                    .Distinct()
                    .ToListAsync();

                // 3. Get buses assigned to next shift
                var nextShift = GetNextShiftType(currentShift);
                var busesInNextShift = nextShift.HasValue
                    ? await _context.minitrips
                        .Where(mt => mt.Trip.Shift == nextShift.Value)
                        .Select(mt => mt.BusId)
                        .Distinct()
                        .ToListAsync()
                    : new List<int?>();

                // 4. Filter available buses
                return allActiveBuses
                    .Where(b => !busesInCurrentTrip.Contains(b.Id) &&
                               !busesInNextShift.Contains(b.Id))
                    .ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new Exception ("Failed to get available buses", ex);
            }
         
        }

        private async Task<Models.Bus?> GetAvailableBusForMiniTripAsync(
          TimeOnly miniTripStart,
          TimeOnly miniTripEnd,
          ShiftTypes tripShiftType,
          int? excludedBusId = null)
        {
            // 1. Get all buses that are unavailable due to:
            //    - Time overlap with other minitrips
            //    - Already assigned to the next shift
            var unavailableBusIds = await GetUnavailableBusIds(
                miniTripStart,
                miniTripEnd,
                tripShiftType,
                excludedBusId);

            // 2. Find first available bus that meets criteria
            return await _context.buses
                .Where(b => !unavailableBusIds.Contains(b.Id) &&
                             b.BusStatus.ToString().Equals("active")) // Assuming buses have an active flag
                // Example: prioritize buses needing maintenance
                .FirstOrDefaultAsync();
        }
        private async Task<HashSet<int>> GetUnavailableBusIds(
           TimeOnly miniTripStart,
           TimeOnly miniTripEnd,
           ShiftTypes tripShiftType,
           int? excludedBusId)
        {
            // Buses with overlapping minitrips (excluding current bus if specified)
            var overlappingBusIds = await _context.minitrips
                .Where(mt => miniTripStart < mt.EndTime &&
                             miniTripEnd > mt.StartTime &&
                             mt.BusId != excludedBusId)
                .Select(mt => mt.BusId)
                .Distinct()
                .ToListAsync();

            // Buses assigned to next shift
            var nextShiftType = GetNextShiftType(tripShiftType);
            var nextShiftBusIds = nextShiftType.HasValue
                ? await _context.minitrips
                    .Where(mt => mt.Trip.Shift == nextShiftType.Value)
                    .Select(mt => mt.BusId)
                    .Distinct()
                    .ToListAsync()
                : new List<int?>();

            // Combine and return as hashset for efficient lookup
            return overlappingBusIds
                .Union(nextShiftBusIds)
                .Where(id => id.HasValue)
                .Select(id => id.Value)
                .ToHashSet();
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
        public async Task<BusAvailabilityResponse> ValidateBusAvailabilityAsync(
    int busId,
    int tripId,
    TimeOnly miniTripStart,
    TimeOnly miniTripEnd,
    ShiftTypes tripShiftType, int? currentMiniTripId = null)
        {
            // 1. Check if bus exists and is active
            var bus = await _context.buses
                .FirstOrDefaultAsync(b => b.Id == busId && b.BusStatus.ToString().Equals("active"));

            if (bus == null)
            {
                return new BusAvailabilityResponse
                {
                    IsAvailable = false,
                    Reason = "Bus not found or inactive"
                };
            }
            var isBusUsedInSameTrip = await _context.minitrips.Include(b => b.Trip)
    .AnyAsync(mt => mt.TripId == tripId &&
                    mt.BusId == busId &&
                    mt.Id != currentMiniTripId); // For update scenarios

            if (isBusUsedInSameTrip)
            {
                return new BusAvailabilityResponse
                {
                    IsAvailable = false,
                    Reason = "Bus is already assigned to another minitrip in the same trip"
                };
            }
            // 2. Check for time conflicts
            var hasTimeConflict = await _context.minitrips
                .AnyAsync(mt => mt.BusId == busId &&
                                miniTripStart < mt.EndTime &&
                                miniTripEnd > mt.StartTime &&
                                mt.Id != currentMiniTripId); // For update scenarios

            if (hasTimeConflict)
            {
                return new BusAvailabilityResponse
                {
                    IsAvailable = false,
                    Reason = "Bus is already scheduled during this time"
                };
            }

            // 3. Check if bus is assigned to next shift
            var nextShiftType = GetNextShiftType(tripShiftType);
            if (nextShiftType.HasValue)
            {
                var isAssignedToNextShift = await _context.minitrips
                    .AnyAsync(mt => mt.BusId == busId &&
                                   mt.Trip.Shift == nextShiftType.Value);

                if (isAssignedToNextShift)
                {
                    return new BusAvailabilityResponse
                    {
                        IsAvailable = false,
                        Reason = "Bus is assigned to the next shift"
                    };
                }
            }

            return new BusAvailabilityResponse
            {
                IsAvailable = true,
                Reason = "Bus is available"
            };
        }
    }
}
