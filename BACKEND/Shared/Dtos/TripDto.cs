using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos
{
    public class TripRequest
    {
        
        //public string Date { get; set; } = DateTime.Now.ToString("dd/MM/yyyy");
        [Required]
        public string Direction { get; set; }
        [Required]
        public string Shift { get; set; }
        [Required]
        public string TripRole { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int RouteId { get; set; }

    }
    public class TripResponse
    {
        public int Id { get; set; }
        //public string Date { get; set; } 
        public string Direction { get; set; }
        public string Shift { get; set; }
        public string Name { get; set; }
        public string TripRole { get; set; }
        public RouteResponse Route { get; set; }


        public List<MinitripResponse> Minitrips { get; set; }
    }

    public class TripResponseWoMinitrip
    {
        public int Id { get; set; }
        //public string Date { get; set; }
        public string Direction { get; set; }
        public string Name { get; set; }
        public string Shift { get; set; }
        public string TripRole { get; set; }
        public RouteResponse Route { get; set; }



        //public int MinitripId { get; set; }
    }
}
