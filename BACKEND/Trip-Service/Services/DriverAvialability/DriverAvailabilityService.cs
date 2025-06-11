using System.Reflection.Metadata.Ecma335;
using MassTransit.Transports;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using Shared.Dtos;
using Trip_Service.Data;
using Trip_Service.Enums;

namespace Trip_Service.Services.DriverAvialability
{
    public class DriverAvailabilityService
    {
        private readonly TripServiceContext _context;
        private HttpClient _httpClient;
        string shiftServiceUrl = "https://localhost:7009/api/";
        string EmployeeServiceUrl = "https://localhost:7115/api/";

        public DriverAvailabilityService(TripServiceContext context, HttpClient httpClient)
        {
            _context = context;
            _httpClient = httpClient;
        }

        public async Task<List<EmployeeResponse>> GetAvailableDriversAsync(ShiftTypes shift, DateTime tripDate)
        {
            var group = await _httpClient.GetFromJsonAsync<GroupResposne>($"{shiftServiceUrl}Group/Group/shift?shift={shift.ToString()}") ;
            var drivers = await _httpClient.GetFromJsonAsync<List<EmployeeResponse>>($"{EmployeeServiceUrl}Employee/group/{group.Id}");

            var busyDriverIds = await _context.minitrips
      .Where(mt => mt.Trip.Date.Date == tripDate.Date)
      .Select(mt => mt.DriverId)
      .Distinct().ToListAsync();
     
            // Step 4: Filter out busy drivers
            var availableDrivers = drivers
                .Where(d => !busyDriverIds.Contains(d.Id))
                .ToList();

            return availableDrivers;
        }

    }
}
