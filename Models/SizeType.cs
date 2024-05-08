using System;
using System.Collections.Generic;

namespace WarehouseDesktopApp.Models;

public partial class SizeType
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Cell> Cells { get; set; } = new List<Cell>();

    public virtual ICollection<Supply> Supplies { get; set; } = new List<Supply>();
}
