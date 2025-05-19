using Gridify;
using Mapster;
using Route_Service.Reposetories.Stop;
using Shared.Dtos;

namespace Route_Service.Services.Stop
{
    public class StopService : IStopService
    {
        private readonly IStopRepo _stopRepo;
        private readonly ILogger<StopService> _logger;

        public StopService(IStopRepo stopRepo ,ILogger<StopService> logger)
        {
            _stopRepo = stopRepo;
            _logger = logger;
        }

        public async Task<StopResponse> AddStop(StopRequest stopRequest)
        {
            var stop = await _stopRepo.AddStop(stopRequest);
            return stop;
        }

        public Paging<StopResponse> GetStops(GridifyQuery gridifyQuery)
        {
            var stops = _stopRepo.GetAllStops().Gridify(gridifyQuery);
            return stops.Adapt<Paging<StopResponse>>(); 
        }

        public async Task<StopResponse> GetStopById(int id)
        {
            var stop = await _stopRepo.GetStopById(id);
            return stop.Adapt<StopResponse>();  
        }

        public async Task<StopResponse> UpdateStop(int id,StopRequest stopRequest)
        {
            var stop = await _stopRepo.UpdateStop(id, stopRequest);
            return stop;
        }

        public async Task DeleteStop(int id)
        {
            await _stopRepo.DeleteStop(id);
        }
    }
}
