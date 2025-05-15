using System.ComponentModel.DataAnnotations.Schema;

namespace Route_Service.Models
{
    public class Route
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("name")]
        public required string Name { get; set; }
        [Column("description")]
        public string? Description { get; set; }
        [Column("is_activate")]
        public bool IsActive { get; set; } = true;
        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        [Column("updated_at")]
        public DateTime? UpdatedAt { get; set; } = null;
        [Column("is_deleted")]
        public bool IsDeleted { get; set; } = false;

        public List<RouteStops> RouteStops { get; set; } = [];
        public List<Stop> Stops { get; set; } = [];

    }
}
