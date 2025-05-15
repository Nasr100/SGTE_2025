using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos
{
    public class BusRequest
    {
        [Required]
        public required string Number { get; set; }
        [Required]
        public required string Plate { get; set; }
        public int? RouteId { get; set; }
        public short? StartYear { get; set; }
        public string? Status { get; set; }
    }

    public class BusResponse
    {
        public int Id { get; set; }
        public required string Number { get; set; }
        public required string Plate { get; set; }
        public RouteStopsResponse? RouteStops { get; set; }
        public short? StartYear { get; set; }
        public string? Status { get; set; }
    }
}
