using Shared.Dtos;

namespace Trip_Service.Repositories.Trip
{
    public interface ITripRepo
    {
        public Task<TripResponse> AddTrip(TripRequest tripRequest);
        public  Task<Models.Trip> GetTripById(int id);
        public  Task DeleteTrip(int id);
        public IQueryable<Models.Trip> GetAllTrips();
        public Task<TripResponse> UpdateTrip(int id, TripRequest tripRequest);

    }
}
