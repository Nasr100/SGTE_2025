using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos
{
    public  class MiniTripEmployeeAssignementRequest
    {
        //[Required]
        //public int SeatNumber { get; set; }
        [Required]
        public int MiniTripId { get; set; }
        [Required]
        public int WorkerId { get; set; }
    }
    public class MiniTripEmployeeAssignementResponse
    {
        public int Id { get; set; }
        public int SeatNumber { get; set; }
        //public int MiniTripId { get; set; }
        //public virtual MiniTrip MiniTrip { get; set; }
        public List<WorkerResponse> Workers { get; set; }
    }
}
