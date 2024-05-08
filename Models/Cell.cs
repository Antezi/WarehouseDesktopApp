using System;
using System.Collections.Generic;

namespace WarehouseDesktopApp.Models;

public partial class Cell
{
    public int Id { get; set; }

    public int Size { get; set; }

    public int Warehouse { get; set; }

    public int SupplieId { get; set; }

    public bool IsFilled { get; set; }

    public int Floor { get; set; }

    public int Shelf { get; set; }

    public int Number { get; set; }

    public virtual SizeType SizeNavigation { get; set; } = null!;

    public virtual Supply Supplie { get; set; } = null!;

    public virtual Warehouse WarehouseNavigation { get; set; } = null!;
}
