using System;
using System.Collections.Generic;

namespace WarehouseDesktopApp.Models;

public partial class DistributionCenterType
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<DistributionCenter> DistributionCenters { get; set; } = new List<DistributionCenter>();
}
