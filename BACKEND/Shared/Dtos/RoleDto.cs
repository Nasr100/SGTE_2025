using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos
{
    public class RoleRequest
    {
        [Required]
        public string RoleName {  get; set; }
    }

    public class RoleResponse
    {
        public int Id { get; set; }
        public string RoleName { get; set; }
    }
}
