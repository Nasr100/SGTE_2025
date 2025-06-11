using Mapster;
using Shared.Dtos;
using Trip_Service.Repositories.MiniTrip;

namespace Trip_Service.Services.Minitrip
{
    public class MinitripService : IMinitripService
    {
        private readonly IMinitripRepo _miniTripRepo;

        public MinitripService(IMinitripRepo miniTripRepo)
        {
            _miniTripRepo = miniTripRepo;
        }

        public async Task<MinitripResponse> AddMiniTrip(MinitripRequest minitripRequest)
        {
            var minitrip =await _miniTripRepo.AddMiniTrip(minitripRequest);
            return minitrip;
        }
        public async Task<List<MinitripResponse>> GetMiniTripByTrip(int TripId)
        {
            var minitrips =await _miniTripRepo.GetMiniTripByTrip(TripId); 
            return minitrips;
            
        }
        public async Task<MinitripResponse> GetMinitripById(int id)
        {
            var minitrip = await _miniTripRepo.GetMinitripById(id);
            return minitrip.Adapt<MinitripResponse>();
        }
        public async Task deleteMinitrip(int id)
        {
            await _miniTripRepo.deleteMinitrip(id);
        }
        public  Task<MinitripResponse> UpdateMinitrip(int id, MinitripRequest minitripRequest)
        {
            var minitrp = _miniTripRepo.UpdateMinitrip(id, minitripRequest);    
            return minitrp;
        }

       //private async Task<bool> CheckBusMaxPassenger(Models.MiniTrip miniTrip)
       // {
       //     if (miniTrip.AssignedEmployeesCount <= miniTrip.Bus.Capacity)
       //     {

       //     }
       // }

    }
}
