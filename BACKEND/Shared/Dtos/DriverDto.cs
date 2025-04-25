using System.ComponentModel.DataAnnotations;


namespace Shared.Dtos
{
    public class DriverRequest
    {
    
        [Required]
        [StringLength(16)]
        public required string LicenceNumber { get; set; }
        [Required]
        public EmployeeRequest? Employee { get; set; }
        [Required]
        public string PermisType { get; set; }
    }

    public class DriverResponse
    {
        public int Id { get; set; }
        public required string LicenceNumber { get; set; }
        public EmployeeResponse? Employee { get; set; }
        public string PermisType { get; set; }
    }
}
