using System;
using System.Collections.Generic;

namespace WarehouseDesktopApp.Models;

public partial class TruckStatus
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Truck> Trucks { get; set; } = new List<Truck>();
}
