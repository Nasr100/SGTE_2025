using Gridify;
using Mapster;
using Shared.Dtos;
using Trip_Service.Repositories.Trip;

namespace Trip_Service.Services.Trip
{
    public class TripService : ITripService
    {
        private readonly ITripRepo _tripRepo;
        public TripService(ITripRepo tripRepo)
        {
            _tripRepo = tripRepo;
        }

        public async Task<TripResponse> AddTrip(TripRequest tripRequest)
        {
           //bool x =  checkTripShiftAlreadyExist(tripRequest);
            
                var trip = await _tripRepo.AddTrip(tripRequest);
                return trip;
            
            //throw new Exception("Trip already exist in this shift");

        }

        public async Task<TripResponse> GetTripById(int id)
        {
            var trip = await _tripRepo.GetTripById(id);
            return trip.Adapt<TripResponse>();
        }

        public async Task DeleteTrip(int id)
        {
            await _tripRepo.DeleteTrip(id);
        }

        public Paging<TripResponseWoMinitrip> GetAllTrips(GridifyQuery gridify)
        {
            var trips = _tripRepo.GetAllTrips().Gridify(gridify);
            return trips.Adapt<Paging<TripResponseWoMinitrip>>();
        }
        public async Task<TripResponse> UpdateTrip(int id, TripRequest tripRequest)
        {
            var trip = await _tripRepo.UpdateTrip(id, tripRequest);
            return trip;
        }

        //private bool checkTripShiftRouteAlreadyExist(TripRequest tripRequest)
        //{
        //    var trips = _tripRepo.GetAllTrips().Where(t=>t.Shift.ToString().Equals(tripRequest.Shift));
        //    if (trips.Any()) 
        //    {
        //        return true;
        //    }
        //    return false;
        //}

    }
}
