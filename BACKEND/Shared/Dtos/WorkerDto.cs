using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos
{
    public class WorkerRequest
    {
        public int? GroupId { get; set; } = null;
        [Required]
        public required  EmployeeRequest Employee { get; set; }
        public int? StopId { get; set; } = null;
    }

    public class WorkerResponse
    {
        public int Id { get; set; }
        public GroupResposne? GroupResponse { get; set; } = null;
        public required EmployeeResponse Employee { get; set; }
        public StopResponse? StopResponse { get; set; } = null;
    }
}
