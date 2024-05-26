using System;
using System.Collections.Generic;

namespace WarehouseDesktopApp.Models;

public partial class Product
{
    public string Name { get; set; } = null!;

    public int Type { get; set; }

    public string? Photopath { get; set; }

    public int Id { get; set; }

    public virtual ICollection<Supply> Supplies { get; set; } = new List<Supply>();

    public virtual ProductType TypeNavigation { get; set; } = null!;
}
