using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace User_Service.Models
{
    public class Role
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("role")]
        public required string RoleName {  get; set; }
        public List<Employee> Employees { get;} = [];

    }
}
