using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos
{
    public class StopRequest
    {
        [Required]
        public required string Name { get; set; }
        public  string? Address { get; set; }
        [Required]
        public required decimal x { get; set; }
        [Required]
        public required decimal y { get; set; }
        public string? Status { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; } = null;
        public bool IsDeleted { get; set; } = false;
    }

    public class StopResponse
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Address { get; set; }
        public required decimal x { get; set; }
        public required decimal y { get; set; }
        public string? Status { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; } = null;
        public bool IsDeleted { get; set; } = false;
    }
}
