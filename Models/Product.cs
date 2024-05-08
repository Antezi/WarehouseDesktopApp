using System;
using System.Collections.Generic;

namespace WarehouseDesktopApp.Models;

public partial class Product
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int Type { get; set; }

    public virtual ICollection<Supply> Supplies { get; set; } = new List<Supply>();

    public virtual ProductType TypeNavigation { get; set; } = null!;
}
