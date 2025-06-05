using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos
{
    public class ShiftRequest
    {

        [Required]
        public required string shift { get; set;}
        [Required]
        public required string Role { get; set;}
        [Required]
        public TimeOnly StartShift { get; set;}
        [Required]
        public TimeOnly EndShift { get; set;}

    }

    public class ShiftResponse
    {
        public int Id { get; set; }
        public required string shift { get; set;}
        public required string Role { get; set;}
        public TimeOnly StartShift { get; set; }
        public TimeOnly EndShift { get; set; }

    }
}
