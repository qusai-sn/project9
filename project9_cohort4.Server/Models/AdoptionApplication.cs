using System;
using System.Collections.Generic;

namespace project9_cohort4.Server.Models;

public partial class AdoptionApplication
{
    public int ApplicationId { get; set; }

    public int UserId { get; set; }

    public int AnimalId { get; set; }

    public DateTime? ApplicationDate { get; set; }

    public string? Status { get; set; }

    public virtual Animal Animal { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
