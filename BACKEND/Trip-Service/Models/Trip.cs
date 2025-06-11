using System.ComponentModel.DataAnnotations.Schema;
using Trip_Service.Enums;

namespace Trip_Service.Models
{
    public class Trip
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("name")]
        public string Name { get; set; }
        //[Column("date")]
        //public DateTime Date { get; set; } = DateTime.Now;
        [Column("direction")]
        public TripDirection Direction { get; set; }
        [Column("shift")]
        public ShiftTypes Shift {  get; set; } 
        [Column("type")]
        public TripType TripRole { get; set; }
        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        [Column("updated_at")]
        public DateTime? UpdatedAt { get; set; } = null;
        [Column("is_deleted")]
        public bool IsDeleted { get; set; } = false;
        public List<MiniTrip>? MiniTrips { get; set; } 
        
    }
}
