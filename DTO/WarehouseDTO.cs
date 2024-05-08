using System;
using System.Collections.Generic;

namespace WarehouseAPI.Models;

public partial class WarehouseDTO
{
    public int Id { get; set; }
    
    public string Address { get; set; } = null!;

    public int Type { get; set; }

    public int Class { get; set; }
    
}