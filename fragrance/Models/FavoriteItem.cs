using System;
using System.Collections.Generic;

namespace fragrance.Models;

public partial class FavoriteItem
{
    public int FavoriteItemId { get; set; }

    public int? FavoriteItemFragranceId { get; set; }

    public int? FavoriteItemUserId { get; set; }

    public DateOnly? FavoriteItemDate { get; set; }

    public virtual Fragrance? FavoriteItemFragrance { get; set; }

    public virtual User? FavoriteItemUser { get; set; }
}
