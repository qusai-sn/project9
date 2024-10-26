using System;
using System.Collections.Generic;

namespace project9_cohort4.Server.Models;

public partial class Shelter
{
    public int ShelterId { get; set; }

    public string ShelterName { get; set; } = null!;

    public string? Description { get; set; }

    public string? ContactEmail { get; set; }

    public string? Phone { get; set; }

    public string? Address { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<ShelterAnimal> ShelterAnimals { get; set; } = new List<ShelterAnimal>();
}
