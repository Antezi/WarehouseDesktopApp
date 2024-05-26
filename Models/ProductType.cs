using System;
using System.Collections.Generic;

namespace WarehouseDesktopApp.Models;

public partial class ProductType
{
    public string Name { get; set; } = null!;

    public int Id { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
