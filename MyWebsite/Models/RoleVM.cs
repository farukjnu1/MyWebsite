namespace MyWebsite.Models
{
    public class RoleVM
    {
        public int RoleId { get; set; }

        public string RoleName { get; set; } = null!;

        public string? Description { get; set; }
    }
}
