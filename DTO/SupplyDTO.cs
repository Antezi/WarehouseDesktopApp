using System;
using System.Collections.Generic;
using WarehouseDesktopApp.Models;

namespace WarehouseAPI.Models;

public partial class SupplyDTO
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
    
}