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
        public string BusStatus { get; set; } = "active";
        [Required]
        public int Capacity { get; set; }
    }

    public class BusResponse
    {
        public int Id { get; set; }
        public required string Number { get; set; }
        public required string BusStatus { get; set; } 
        public int Capacity { get; set; }
    }
}
