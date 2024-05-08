using System;
using System.Collections.Generic;

namespace WarehouseDesktopApp.Models;

public partial class StatusType
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Supply> Supplies { get; set; } = new List<Supply>();
}
