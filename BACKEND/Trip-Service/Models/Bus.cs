using System.ComponentModel.DataAnnotations.Schema;
using Route_Service.Enums;

namespace Trip_Service.Models
{
    public class Bus
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("number")]
        public required string Number { get; set; }
        public BusStatusEnum BusStatus { get; set; }
        [Column("capacity")]
        public int Capacity { get; set; }
        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        [Column("updated_at")]
        public DateTime? UpdatedAt { get; set; } = null;
        [Column("is_deleted")]
        public bool IsDeleted { get; set; } = false;
    }
}
