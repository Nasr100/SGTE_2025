using Shared.Dtos;

namespace Route_Service.Reposetories.Stop
{
    public interface IStopRepo
    {
        public Task<StopResponse> AddStop(StopRequest stopRequest);
        public Task<Models.Stop> GetStopById(int id);
        public IQueryable<Models.Stop> GetAllStops();
        public Task DeleteStop(int id);
        public Task<StopResponse> UpdateStop(int id, StopRequest stopReq);


    }
}
