using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Route_Service.Models
{
    public class RouteStops
    {
        [Required]
        [Column("stop_id")]
        public required int StopId { get; set; }
        [ForeignKey("StopId")]
        public virtual Stop? Stop { get; set; }
        [Required]
        [Column("route_id")]
        public required int RouteId { get; set; }
        [ForeignKey("RouteId")]
        public virtual Route? Route { get; set; }
        [Required]
        [Column("stop_order")]
        public required int StopOrder { get; set; }
        [Required]
        [Column("arrival_time")]
        public required TimeOnly ArrivalTime { get; set; }
        [Required]
        [Column("departure_time")]
        public required TimeOnly DepartureTime { get; set; }

    }
}
