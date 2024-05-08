using System;
using System.Collections.Generic;

namespace WarehouseDesktopApp.Models;

public partial class Supply
{
    public int Id { get; set; }

    public int Product { get; set; }

    public int Size { get; set; }

    public int Status { get; set; }

    public int DepartWarehouseId { get; set; }

    public int DestinationWarehouseId { get; set; }

    public DateTime DeliveryStart { get; set; }

    public DateTime? DeliveryEnd { get; set; }

    public virtual ICollection<Cell> Cells { get; set; } = new List<Cell>();

    public virtual Warehouse DepartWarehouse { get; set; } = null!;

    public virtual Warehouse DestinationWarehouse { get; set; } = null!;

    public virtual Product ProductNavigation { get; set; } = null!;

    public virtual SizeType SizeNavigation { get; set; } = null!;

    public virtual StatusType StatusNavigation { get; set; } = null!;
}
