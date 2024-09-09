using System;
using System.Collections.Generic;

namespace fragrance.Models;

public partial class User
{
    public int UserId { get; set; }

    public string UserName { get; set; } = null!;

    public string UserSurname { get; set; } = null!;

    public short? UserGender { get; set; }

    public string UserEmail { get; set; } = null!;

    public string UserPassword { get; set; } = null!;

    public string UserRole { get; set; } = null!;

    public virtual ICollection<FavoriteItem> FavoriteItems { get; set; } = new List<FavoriteItem>();

    public virtual ICollection<FragranceReview> FragranceReviews { get; set; } = new List<FragranceReview>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
