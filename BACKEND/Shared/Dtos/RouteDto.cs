using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos
{
    public class RouteRequest
    {
        [Required]
        public required string Name { get; set; }
        [Required]
        public string? Description { get; set; }
        public bool? IsActive { get; set; } = true;
    }


    public class RouteResponse
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public bool? IsActive { get; set; } = true;
        public List<StopResponse> Stop { get; set; } = [];
    }
}
