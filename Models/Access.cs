using System;
using System.Collections.Generic;

namespace WarehouseDesktopApp.Models;

public partial class Access
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<AccessToUser> AccessToUsers { get; set; } = new List<AccessToUser>();
}
