using System;
using System.Collections.Generic;

namespace WarehouseDesktopApp.Models;

public partial class User
{
    public int Id { get; set; }

    public string Login { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int Type { get; set; }

    public virtual ICollection<AccessToUser> AccessToUsers { get; set; } = new List<AccessToUser>();

    public virtual ICollection<RightAccessToUser> RightAccessToUsers { get; set; } = new List<RightAccessToUser>();

    public virtual UsersType TypeNavigation { get; set; } = null!;

    public virtual ICollection<WarehousesAccessToUser> WarehousesAccessToUsers { get; set; } = new List<WarehousesAccessToUser>();
}
