using System;
using System.Collections.Generic;

namespace fragrance.Models;

public partial class FragranceReview
{
    public int FragranceReviewId { get; set; }

    public string FragranceReviewText { get; set; } = null!;

    public double? FragranceReviewRating { get; set; }

    public int? FragranceReviewUserId { get; set; }

    public string? FragranceReviewTitle { get; set; }

    public DateOnly? FragranceReviewDate { get; set; }

    public int? FragranceReviewFragranceId { get; set; }

    public virtual Fragrance? FragranceReviewFragrance { get; set; }

    public virtual User? FragranceReviewUser { get; set; }
}
