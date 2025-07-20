using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MyWebsite.Models
{
    public class UserVM
    {
        [Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserID { get; set; }

        [Required]
        [StringLength(50)]

        public string Username { get; set; }

        [Required]
        [StringLength(100)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(255)]
        public string PasswordHash { get; set; }

        [Required]
        [StringLength(255)]
        [Compare("PasswordHash")]
        public string ConfirmPassword { get; set; }

        [Required]
        [StringLength(20)]
        public string? Role { get; set; } = "viewer"; // Matches SQL default

        [Required]
        public DateTime? CreatedAt { get; set; } = DateTime.Now; // Matches SQL default
        public IEnumerable<SelectListItem> RoleOptions { get; set; }
    }
}
