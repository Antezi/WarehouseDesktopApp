using System;
using System.Collections.Generic;
using Avalonia.Media.Imaging;

namespace WarehouseDesktopApp.Models;

public partial class UserDTO
{
    public int Id { get; set; }

    public string Login { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int Type { get; set; }

    public string? Firstname { get; set; }

    public string? Lastname { get; set; }

    public string? Patronymic { get; set; }

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public string? Passport { get; set; }

    public string? Photopath { get; set; }
    
    public Bitmap PhotopathView { get; set; }
    public string TypeView { get; set; }

    public virtual ICollection<AccessToUser> AccessToUsers { get; set; } = new List<AccessToUser>();

    public virtual ICollection<RightAccessToUser> RightAccessToUsers { get; set; } = new List<RightAccessToUser>();

    public virtual UsersType TypeNavigation { get; set; } = null!;

    public virtual ICollection<WarehousesAccessToUser> WarehousesAccessToUsers { get; set; } = new List<WarehousesAccessToUser>();
}