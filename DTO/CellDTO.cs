using System;
using System.Collections.Generic;
using WarehouseDesktopApp.Models;

namespace WarehouseAPI.Models;

public partial class CellDTO
{
    public int Id { get; set; }

    public int Size { get; set; }

    public int Warehouse { get; set; }

    public int SupplieId { get; set; }

    public bool IsFilled { get; set; }

    public int Floor { get; set; }

    public int Shelf { get; set; }

    public int Number { get; set; }
    public virtual Supply Supplie { get; set; } = null!;
    
}