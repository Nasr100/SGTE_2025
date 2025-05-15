using Gridify;
using Shared.Dtos;

namespace Route_Service.Services.Stop
{
    public interface IStopService
    {
        public Task<StopResponse> AddStop(StopRequest stopRequest);
        public Paging<StopResponse> GetStops(GridifyQuery gridifyQuery);
        public Task<StopResponse> GetStopById(int id);
        public Task<StopResponse> UpdateStop(int id, StopRequest stopRequest);
        public Task DeleteStop(int id);


    }
}
