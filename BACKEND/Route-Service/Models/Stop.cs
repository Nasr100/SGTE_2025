using System.ComponentModel.DataAnnotations.Schema;
using Route_Service.Enums;

namespace Route_Service.Models
{
    public class Stop
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("name")]
        public required string Name { get; set; }
        [Column("description")]
        public  string? Description { get; set; }
        [Column("address")]
        public required string Address { get; set; }
        [Column("longitude")]
        public decimal x { get; set; }
        [Column("latitude")]
        public decimal y { get; set; }
        [Column("status")]
        public StopStatusEnum Status { get; set; } = StopStatusEnum.active;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        [Column("updated_at")]
        public DateTime? UpdatedAt { get; set; } = null;
        [Column("is_deleted")]
        public bool IsDeleted { get; set; } = false;
        public  virtual List<Route> Routes { get; set; } = [];
        public virtual List<RouteStops> Stops { get; set; } = [];


    }
}
