using Mapster;
using Microsoft.EntityFrameworkCore;
using Shared.Dtos;
using Trip_Service.Data;

namespace Trip_Service.Repositories.MiniTrip
{
    public class MiniTripRepo:IMinitripRepo
    {
        private readonly TripServiceContext _context;

        public MiniTripRepo(TripServiceContext context)
        {
            _context = context;
        }

        public async Task<MinitripResponse> AddMiniTrip(MinitripRequest minitripRequest)
        {
            var minitripmodel = minitripRequest.Adapt<Models.MiniTrip>();
            var x = await _context.minitrips.AddAsync(minitripmodel);
           await _context.SaveChangesAsync();
            return x.Entity.Adapt<MinitripResponse>();

        }

        public async  Task<List<MinitripResponse>> GetMiniTripByTrip(int TripId)
        {
            var miniTrips =await _context.minitrips.Where(mt => !mt.IsDeleted && mt.TripId == TripId).Include(mt=>mt.Trip).ToListAsync();
            return miniTrips.Adapt<List<MinitripResponse>>();
        }
        public async Task<Models.MiniTrip> GetMinitripById(int id)
        {
            var minitrip = await _context.minitrips.Where(mt => !mt.IsDeleted).FirstOrDefaultAsync(mt=> mt.Id == id) ?? throw new Exception("minitrip not found");
            return minitrip;
        }
        public async Task deleteMinitrip(int id)
        {
            var miniTrip =await GetMinitripById(id);
            miniTrip.IsDeleted = true; 
             _context.minitrips.Update(miniTrip);
            await _context.SaveChangesAsync();
        }

        public async Task<MinitripResponse> UpdateMinitrip(int id,MinitripRequest minitripRequest)
        {
            var minitrip =await GetMinitripById(id);
            minitripRequest.Adapt(minitrip);
            await _context.SaveChangesAsync();
            return minitrip.Adapt<MinitripResponse>();
        }
    }
}
