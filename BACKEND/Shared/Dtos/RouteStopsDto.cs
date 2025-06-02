using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos
{
    public class RouteStopsRequest
    {
        [Required]
        public int StopId { get; set; }
        //[Required]
        //public  int RouteId { get; set; }

        [Required]
        public required int StopOrder { get; set; }
        [Required]
        public required TimeOnly ArrivalTime { get; set; }
        [Required]
        public required TimeOnly DepartureTime { get; set; }
    }

    public class RouteStopsResponse
    {
        public required StopResponse Stop { get; set; }
        //public  RouteResponse? RouteResponse  { get; set; }
        public required int StopOrder { get; set; }
        public required TimeOnly ArrivalTime { get; set; }
        public required TimeOnly DepartureTime { get; set; }
    }
}
