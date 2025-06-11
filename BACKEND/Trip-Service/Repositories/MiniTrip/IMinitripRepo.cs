using Shared.Dtos;

namespace Trip_Service.Repositories.MiniTrip
{
    public interface IMinitripRepo
    {
        public Task<MinitripResponse> AddMiniTrip(MinitripRequest minitripRequest);
        public  Task<List<MinitripResponse>> GetMiniTripByTrip(int TripId);
        public Task<Models.MiniTrip> GetMinitripById(int id);
        public  Task deleteMinitrip(int id);
        public Task<MinitripResponse> UpdateMinitrip(int id, MinitripRequest minitripRequest);


    }
}
