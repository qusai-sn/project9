namespace project9_cohort4.Server.DTOs
{
    public class UpdateuserDTO
    {
        public string Username { get; set; } = null!;

        public string PasswordHash { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string? FullName { get; set; }

        public bool? IsAdmin { get; set; }

    }
}
