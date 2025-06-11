using Mapster;
using Microsoft.EntityFrameworkCore;
using Shared.Dtos;
using Trip_Service.Data;

namespace Trip_Service.Repositories.Trip
{
    public class TripRepo : ITripRepo
    {
        private readonly TripServiceContext _context;

        public TripRepo(TripServiceContext context)
        {
            _context = context;
        }

        public async Task<TripResponse> AddTrip(TripRequest tripRequest)
        {
            var TripModel = tripRequest.Adapt<Models.Trip>();
            var x = await _context.trips.AddAsync(TripModel);
            await _context.SaveChangesAsync();
            return x.Entity.Adapt<TripResponse>();
        }
        public async Task<Models.Trip> GetTripById(int id)
        {
            var trip = await _context.trips.Where(t => !t.IsDeleted).Include(t => t.MiniTrips).FirstOrDefaultAsync(t => t.Id == id) ?? throw new Exception("minitrip not found");
            return trip;
        }
        public async Task DeleteTrip(int id)
        {
            var trip = await GetTripById(id);
            trip.IsDeleted = true;
            _context.trips.Update(trip);
            await _context.SaveChangesAsync();
        }

        public IQueryable<Models.Trip> GetAllTrips()
        {
            var trips = _context.trips.Where(t => !t.IsDeleted);
            return trips;
        }

        public async Task<TripResponse> UpdateTrip(int id, TripRequest tripRequest)
        {
            var trip = await GetTripById(id);
            tripRequest.Adapt(trip);
            await _context.SaveChangesAsync();
            return trip.Adapt<TripResponse>();
        }
    }
}
