using System;
using System.Collections.Generic;

namespace fragrance.Models;

public partial class OrderItem
{
    public int OrderItemId { get; set; }

    public int? OrderItemOrderId { get; set; }

    public int? OrderItemFragranceId { get; set; }

    public int? OrderItemQuantity { get; set; }

    public int? OrderItemUnitPrice { get; set; }

    public virtual Fragrance? OrderItemFragrance { get; set; }

    public virtual Order? OrderItemOrder { get; set; }
}
