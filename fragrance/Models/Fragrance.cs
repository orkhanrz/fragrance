using System;
using System.Collections.Generic;

namespace fragrance.Models;

public partial class Fragrance
{
    public int FragranceId { get; set; }

    public string FragranceBrand { get; set; } = null!;

    public string FragranceLine { get; set; } = null!;

    public int FragrancePrice { get; set; }

    public short? FragranceGender { get; set; }

    public string? FragranceImage { get; set; }

    public int? FragranceOldPrice { get; set; }

    public DateOnly? CreatedAt { get; set; }

    public DateOnly? UpdatedAt { get; set; }

    public string? FragranceDesc { get; set; }

    public int? FragranceStock { get; set; }

    public string? FragranceLongDesc { get; set; }

    public virtual ICollection<FavoriteItem> FavoriteItems { get; set; } = new List<FavoriteItem>();

    public virtual ICollection<FragranceImage> FragranceImages { get; set; } = new List<FragranceImage>();

    public virtual ICollection<FragranceReview> FragranceReviews { get; set; } = new List<FragranceReview>();

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}
