using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using User_Service.Enums;

namespace User_Service.Models
{
    public class Driver
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Required]
        [StringLength(16)]
        [Column("licence_number")]

        public required string LicenceNumber { get; set; }
        [Column("employee_id")]

        public int EmployeeId { get; set; }
        [ForeignKey("EmployeeId")]
        public virtual Employee? Employee { get; set; }
        [Required]
        [Column("permis_type")]
        public PermisTypeEnum PermisType { get; set; }
    }
}
