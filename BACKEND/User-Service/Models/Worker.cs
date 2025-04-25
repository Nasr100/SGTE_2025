using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace User_Service.Models
{
    public class Worker
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("group_id")]
        public int? GroupId { get; set; } = null;
        [Column("employee_id")]
        [Required]
        public int EmployeeId { get; set; }

        [ForeignKey("EmployeeId")]
        [Required]
        public required virtual Employee Employee { get; set; }
        [Column("stop_id")]
        public int? StopId { get; set; } = null;
    }
}
