using System;
using System.Collections.Generic;

namespace WarehouseDesktopApp.Models;

public partial class UsersType
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
