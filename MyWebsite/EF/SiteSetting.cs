﻿using System;
using System.Collections.Generic;

namespace MyWebsite.EF;

public partial class SiteSetting
{
    public string SettingKey { get; set; } = null!;

    public string? SettingValue { get; set; }
}
