using System;
using System.Collections.Generic;

namespace WarehouseAPI.Models;

public partial class UserDTO
{
    public int Id { get; set; }

    public string Login { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int Type { get; set; }
    
}