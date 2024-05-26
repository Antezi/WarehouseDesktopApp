using System;
using System.Collections.Generic;

namespace WarehouseDesktopApp.Models;

public partial class Truck
{
    public int Id { get; set; }

    public string Number { get; set; } = null!;

    public int Model { get; set; }

    public int Status { get; set; }

    public virtual TruckModel ModelNavigation { get; set; } = null!;

    public virtual TruckStatus StatusNavigation { get; set; } = null!;

    public virtual ICollection<Supply> Supplies { get; set; } = new List<Supply>();
}
