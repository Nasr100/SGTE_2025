using System.ComponentModel.DataAnnotations.Schema;
using Route_Service.Enums;

namespace Route_Service.Models
{
    public class Bus
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("number")]
        public required string Number { get; set; }
        [Column("plate")]
        public required string Plate { get; set; }

        [Column(name:"start_year",TypeName = "YEAR")]
        public short? StartYear { get; set; }
        [Column("status")]
        public BusStatusEnum Status { get; set; } = BusStatusEnum.Active;
        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        [Column("updated_at")]
        public DateTime? UpdatedAt { get; set; } = null;
        [Column("is_deleted")]
        public bool IsDeleted { get; set; } = false;

    }
}
