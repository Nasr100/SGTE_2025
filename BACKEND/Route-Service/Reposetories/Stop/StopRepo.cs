using Mapster;
using Route_Service.Data;
using Shared.Dtos;

namespace Route_Service.Reposetories.Stop
{
    public class StopRepo : IStopRepo
    {
        private readonly RouteServiceContext _context;
        public StopRepo(RouteServiceContext context)
        {
            _context = context;
        }

        public async Task<StopResponse> AddStop(StopRequest stopRequest)
        {
            var stopModel = stopRequest.Adapt<Models.Stop>();
            await _context.Stops.AddAsync(stopModel);
            await _context.SaveChangesAsync();
            return stopModel.Adapt<StopResponse>();

        }

        public async Task<Models.Stop> GetStopById(int id)
        {
            var stop = await _context.Stops.FindAsync(id);
            if(stop == null)
            {
                throw new Exception("stop with the id :" + id + "not found");
            }
            return stop;
        }

        public  IQueryable<Models.Stop> GetAllStops()
        {
            var stops = _context.Stops.Where(s => s.IsDeleted == false);
            return stops;
        }

        public async Task DeleteStop(int id)
        {
            var stop = await GetStopById(id);
            stop.IsDeleted = true;
            _context.Stops.Update(stop);
            await _context.SaveChangesAsync();
        }

        public async Task<StopResponse> UpdateStop(int id,StopRequest stopReq)
        {
            var stop = await GetStopById(id);
            stopReq.Adapt(stop);
            await _context.SaveChangesAsync();
            return stop.Adapt<StopResponse>();
        }
    }
}
