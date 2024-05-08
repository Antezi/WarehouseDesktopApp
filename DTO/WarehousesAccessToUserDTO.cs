using System;
using System.Collections.Generic;

namespace WarehouseAPI.Models;

public partial class WarehousesAccessToUserDTO
{
    public int Id { get; set; }

    public int WarehouseId { get; set; }

    public int UserId { get; set; }
}