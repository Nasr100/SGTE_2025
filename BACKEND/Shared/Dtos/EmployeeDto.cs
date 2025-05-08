using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Shared.Dtos
{
     public class EmployeeRequest
    {
        [Required]
        public required string FirstName { get; set; }
        [Required]
        public required string LastName { get; set; }
        [Required]
        public required string Address { get; set; }

        [Required]
        [Phone]
        public required string PhoneNumber { get; set; }
        [Required]
        public required string BadgeNumber { get; set; }
        public bool IsAdmin { get; set; } = false;
        [Required]
        [EmailAddress]
        public required string Email { get; set; }
        [Required]
        [MinLength(8)]
        public required string Password { get; set; }
    }


    public  class EmployeeResponse
    {
        public int Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string PhoneNumber { get; set; }
        public required string Address { get; set; }
        public bool IsAdmin { get; set; }

        public required string BadgeNumber { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
}
