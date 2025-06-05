using System.ComponentModel.DataAnnotations.Schema;
using Shift_Service.Enums;

namespace Shift_Service.Models
{
    public class Group
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("name")]
        public required string Name { get; set; }
        public virtual Shift? Shift { get; set; }
        [Column("shift_id")]
        public int ShiftId { get; set; }
        [Column("role")]

        public Role Role { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        [Column("updated_at")]
        public DateTime? UpdatedAt { get; set; } = null;
        [Column("is_deleted")]
        public bool IsDeleted { get; set; } = false;
    }
}
