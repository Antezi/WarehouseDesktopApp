using System;
using System.Collections.Generic;

namespace WarehouseDesktopApp.Models;

public partial class WarehousesAccessToUser
{
    public int Id { get; set; }

    public int WarehouseId { get; set; }

    public int UserId { get; set; }

    public virtual User User { get; set; } = null!;

    public virtual Warehouse Warehouse { get; set; } = null!;
}
