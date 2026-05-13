using System.ComponentModel.DataAnnotations;

namespace MyPortfolio.Models
{
    public class UserModel
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        public string Gender { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;

        public string Role { get; set; } = "User";

        public string Remark { get; set; } = string.Empty;
    }
}
