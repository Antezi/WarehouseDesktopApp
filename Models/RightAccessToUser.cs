using System;
using System.Collections.Generic;

namespace WarehouseDesktopApp.Models;

public partial class RightAccessToUser
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int AccessId { get; set; }

    public virtual RightAccess Access { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
