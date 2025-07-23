using System;
using System.Collections.Generic;

namespace MyWebsite.EF;

public partial class RolePermission
{
    public int RolePermissionId { get; set; }

    public int? RoleId { get; set; }

    public string? Controller { get; set; }

    public bool? IsActive { get; set; }
}
