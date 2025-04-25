using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos
{
     public class AdministrationRequest
    {
        [Required]
        public required string Departement { get; set; }

        public EmployeeRequest? Employee { get; set; }

        public int? StopId { get; set; } = null;
    }

    public class AdministrationResponse
    {
        public int  Id { get; set; }
        public required string Departement { get; set; }

        public EmployeeRequest? Employee { get; set; }

        public int? StopId { get; set; } = null;
    }
}
