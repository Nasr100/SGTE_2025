using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos
{
    public  class GroupRequest
    {
        [Required]
        public required string Name { get; set; }
        [Required]
        public int ShiftId { get; set; }
        [Required]
        public required string Role { get; set; }
    }

    public class GroupResposne
    {
        public int Id { get; set;}
        public required string Name { get; set; }
        public required string Role { get; set; }

        public ShiftResponse? Shift { get; set; }
    }
}
