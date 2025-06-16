using System.Collections.Generic;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Shared.Dtos;
using Trip_Service.Data;
using Trip_Service.Models;

namespace Trip_Service.Repositories.MiniTrip
{
    public class MiniTripRepo:IMinitripRepo
    {
        private readonly TripServiceContext _context;
        private readonly HttpClient _httpClient;
        string userServieBaseUrl = "https://localhost:7115";
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
           
            var miniTripsModel =await _context.minitrips.Where(mt => !mt.IsDeleted && mt.TripId == TripId).Include(mt=>mt.Bus).ToListAsync();
            var minitrips = miniTripsModel.Adapt<List<MinitripResponse>>();
            foreach (var minitrip in minitrips)
            {
                var driver =await GetDriverById(minitrip.Driver.Id);
                if (driver != null)
                {
                    minitrip.Driver = driver;
                }
            }
           
            return minitrips;
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

        private async Task<EmployeeResponse?> GetDriverById(int id)
        {
            var employee =await _httpClient.GetFromJsonAsync<EmployeeResponse>($"{userServieBaseUrl}/{id}");
            if (employee == null)
            {
                throw new Exception($"employee with id {id} not found");
            }
            return employee;
        }
    }
}