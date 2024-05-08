using System;
using System.Collections.Generic;

namespace WarehouseDesktopApp.Models;

public partial class WarehousesClass
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Warehouse> Warehouses { get; set; } = new List<Warehouse>();
}
