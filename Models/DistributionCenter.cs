using System;
using System.Collections.Generic;

namespace WarehouseDesktopApp.Models;

public partial class DistributionCenter
{
    public int Id { get; set; }

    public string Address { get; set; } = null!;

    public int Type { get; set; }

    public virtual DistributionCenterType TypeNavigation { get; set; } = null!;
}
