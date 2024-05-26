using System;
using System.Collections.Generic;

namespace WarehouseDesktopApp.Models;

public partial class WarehouseType
{
    public string Name { get; set; } = null!;

    public int Id { get; set; }

    public virtual ICollection<Warehouse> Warehouses { get; set; } = new List<Warehouse>();
}
