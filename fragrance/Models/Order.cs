using System;
using System.Collections.Generic;

namespace fragrance.Models;

public partial class Order
{
    public int OrderId { get; set; }

    public int? OrderUserId { get; set; }

    public DateOnly? OrderDate { get; set; }

    public int? OrderTotalPrice { get; set; }

    public string? OrderStatus { get; set; }

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual User? OrderUser { get; set; }
}
