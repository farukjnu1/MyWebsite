using System;
using System.Collections.Generic;

namespace MyWebsite.EF;

public partial class User
{
    public int UserId { get; set; }

    public string Username { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<Medium> Media { get; set; } = new List<Medium>();

    public virtual ICollection<Page> Pages { get; set; } = new List<Page>();

    public virtual ICollection<Role> Roles { get; set; } = new List<Role>();
}
