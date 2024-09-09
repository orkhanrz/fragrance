using System;
using System.Collections.Generic;

namespace fragrance.Models;

public partial class FragranceImage
{
    public int FragranceImageId { get; set; }

    public string? FragranceImageUrl { get; set; }

    public int? FragranceId { get; set; }

    public virtual Fragrance? Fragrance { get; set; }
}
