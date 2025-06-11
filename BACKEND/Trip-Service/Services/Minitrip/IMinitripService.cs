using Shared.Dtos;

namespace Trip_Service.Services.Minitrip
{
    public interface IMinitripService
    {
        public Task<MinitripResponse> AddMiniTrip(MinitripRequest minitripRequest);
        public Task<List<MinitripResponse>> GetMiniTripByTrip(int TripId);
        public Task<MinitripResponse> GetMinitripById(int id);
        public Task deleteMinitrip(int id);
        public Task<MinitripResponse> UpdateMinitrip(int id, MinitripRequest minitripRequest);

    }
}
