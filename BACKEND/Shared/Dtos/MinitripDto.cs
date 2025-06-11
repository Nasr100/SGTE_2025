using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos
{
    public class MinitripRequest
    {
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public int TripId { get; set; }
    }

    public class MinitripResponse
    {
        public int Id { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public int AssignedEmployeesCount { get; set; }
        public BusResponse? Bus { get; set; }
        public DriverResponse? Driver { get; set; }

    }

    public class MinitripResponseWithTrip
    {
        public int Id { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public int AssignedEmployeesCount { get; set; }
        public BusResponse? Bus { get; set; }
        public DriverResponse? Driver { get; set; }
        public TripResponseWoMinitrip? Trip { get; set; }


    }
}
