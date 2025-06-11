using System.ComponentModel.DataAnnotations.Schema;

namespace Trip_Service.Models
{
    public class MiniTripEmployeeAssignement
    {
        public int Id { get; set; }
        public int SeatNumber { get; set; }
        public int MiniTripId { get; set; }
        public virtual MiniTrip? MiniTrip { get; set; }
        public int EmployeeId { get; set; }
        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        [Column("updated_at")]
        public DateTime? UpdatedAt { get; set; } = null;
        

    }
}
