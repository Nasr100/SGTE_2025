using System.ComponentModel.DataAnnotations.Schema;

namespace Trip_Service.Models
{
    public class MiniTrip
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("name")]
        public required string Name { get; set; }
        //[Column("assigned_employee_count")]
        //public int AssignedEmployeesCount { get; set; }
        //[Column("start_time")]
        //public TimeOnly StartTime { get; set; }
        //[Column("end_time")]
        //public TimeOnly EndTime { get; set; }
        [Column("trip_id")]
        public virtual Trip? Trip { get; set; }
        [Column("trip_id")]
        public int TripId { get; set; }

        public virtual Bus? Bus { get; set; }
        [Column("bus_id")]
        public int BusId {  get; set; } 
        [Column("driver_id")]
        public int DriverId { get; set; }
        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        [Column("updated_at")]
        public DateTime? UpdatedAt { get; set; } = null;
        [Column("is_deleted")]
        public bool IsDeleted { get; set; } = false;


    }
}
