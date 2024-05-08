using System;
using System.Collections.Generic;

namespace WarehouseDesktopApp.Models;

public partial class RightAccess
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<RightAccessToUser> RightAccessToUsers { get; set; } = new List<RightAccessToUser>();
}
