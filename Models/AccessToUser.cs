using System;
using System.Collections.Generic;

namespace WarehouseDesktopApp.Models;

public partial class AccessToUser
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int AccessId { get; set; }

    public virtual Access Access { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
