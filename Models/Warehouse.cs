using System;
using System.Collections.Generic;

namespace WarehouseDesktopApp.Models;

public partial class Warehouse
{
    public int Id { get; set; }

    public string Address { get; set; } = null!;

    public int Type { get; set; }

    public int Class { get; set; }

    public virtual ICollection<Cell> Cells { get; set; } = new List<Cell>();

    public virtual WarehousesClass ClassNavigation { get; set; } = null!;

    public virtual ICollection<Supply> SupplyDepartWarehouses { get; set; } = new List<Supply>();

    public virtual ICollection<Supply> SupplyDestinationWarehouses { get; set; } = new List<Supply>();

    public virtual WarehouseType TypeNavigation { get; set; } = null!;

    public virtual ICollection<WarehousesAccessToUser> WarehousesAccessToUsers { get; set; } = new List<WarehousesAccessToUser>();
}
