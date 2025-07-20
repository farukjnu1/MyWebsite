using System;
using System.Collections.Generic;

namespace MyWebsite.EF;

public partial class ContentGroup
{
    public int ContentGroupId { get; set; }

    public string? SettingKey { get; set; }

    public string? ContentSource { get; set; }

    public int? ContentId { get; set; }
}
