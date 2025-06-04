using System.ComponentModel.DataAnnotations.Schema;
using Shift_Service.Enums;

namespace Shift_Service.Models
{
    public class Shift
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("shift_type")]
        public ShiftTypes shift { get; set; }
        [Column("role")]
        public Role Role { get; set; }
        [Column("start_shift")]
        public TimeOnly StartShift { get; set; }
        [Column("end_shift")]
        public TimeOnly EndShift { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        [Column("updated_at")]
        public DateTime? UpdatedAt { get; set; } = null;
        [Column("is_deleted")]
        public bool IsDeleted { get; set; } = false;
    }
}

