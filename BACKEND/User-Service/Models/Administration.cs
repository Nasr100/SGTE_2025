using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace User_Service.Models
{
    public class Administration
    {
        [Key]
        [Column("id")]

        public int Id { get; set; }
        [Column("departement")]
        public required string Departement { get; set; }
        [Column("employee_id")]

        public int EmployeeId { get; set; }
        [ForeignKey("EmployeeId")]
        public Employee? Employee { get; set; }

        [Column("stop_id")]
        public int? StopId { get; set; }

    }
}
