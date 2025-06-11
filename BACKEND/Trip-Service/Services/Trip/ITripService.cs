using Gridify;
using Shared.Dtos;

namespace Trip_Service.Services.Trip
{
    public interface ITripService
    {
        public Task<TripResponse> AddTrip(TripRequest tripRequest);
        public Task<TripResponse> GetTripById(int id);
        public Task DeleteTrip(int id);
        public Paging<TripResponseWoMinitrip> GetAllTrips(GridifyQuery gridify);
        public Task<TripResponse> UpdateTrip(int id, TripRequest tripRequest);

    }
}
