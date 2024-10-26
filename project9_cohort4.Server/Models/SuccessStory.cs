using System;
using System.Collections.Generic;

namespace project9_cohort4.Server.Models;

public partial class SuccessStory
{
    public int StoryId { get; set; }

    public int UserId { get; set; }

    public int AnimalId { get; set; }

    public string? StoryText { get; set; }

    public DateTime? StoryDate { get; set; }

    public string? PhotoUrl { get; set; }

    public virtual Animal Animal { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
