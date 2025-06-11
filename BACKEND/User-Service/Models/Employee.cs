using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using User_Service.Enums;

namespace User_Service.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Column("first_name")]
        public required string FirstName { get; set; }
        [Column("address")]
        public required string Address {  get; set; }
        [Column("role")]
        [Required]
        public RolesEnum Role { get; set; }
        [Required]
        [Column("last_name")]
        public required string LastName { get; set; }
        [Required]
        [Phone]
        [Column("phone_number")]
        public required string PhoneNumber { get; set; }
        [Required]
        [Column("badge_number")]
        public required string BadgeNumber { get; set; }
        [Column("access_token")]
        public string? AccessToken { get; set; }
        [Column("refresh_token")]
        public string? RefreshToken {  get; set; }
        [Column("access_token_expiry")]
        public DateTime? AccessTokenExpiry { get; set; }
        [Column("refresh_token_expiry")]
        public DateTime? RefreshTokenExpiry { get; set; }
        [Required]
        [EmailAddress]
        [Column("email")]
        public required string  Email { get; set; }
        [Required]
        [MinLength(8)]
        [Column("password")]
        public required string Password { get; set; }
        [Column("status")]
        public StatusEnum Status { get; set; } = StatusEnum.active;
        [Column("is_deleted")]
        public bool IsDeleted { get; set; } = false;
        [Column("created_at")]
        public DateTime Created_at { get; set; } = DateTime.UtcNow;
        [Column("updated_at")]
        public DateTime? Updated_at { get; set; } = null;

        public int? StopId {  get; set; }
        public int? GroupId { get; set; }
        
        




    }
}
