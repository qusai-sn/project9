namespace project9_cohort4.Server.DTOs
{
    public class updateAnimalDTO
    {
        public string Name { get; set; } = null!;

        public string Species { get; set; } = null!;

        public string? Breed { get; set; }

        public int? Age { get; set; }

        public string? Size { get; set; }

        public string? Temperament { get; set; }

        public string? SpecialNeeds { get; set; }

        public string? Description { get; set; }

        public string? AdoptionStatus { get; set; }

        public string? PhotoUrl { get; set; }

    }
}
