using System.Reflection.Metadata.Ecma335;
using MassTransit.Transports;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using Shared.Dtos;


namespace Trip_Service.Services.DriverAvialability
{
    public class DriverAvailabilityService
    {
        private readonly TripServiceContext _context;
        private HttpClient _httpClient;
        string shiftServiceUrl = "https://localhost:7009/api/";
        string EmployeeServiceUrl = "https://localhost:7115/api/";

        public DriverAvailabilityService( context, HttpClient httpClient)
        {
            _context = context;
            _httpClient = httpClient;
        }

        

    }
}
