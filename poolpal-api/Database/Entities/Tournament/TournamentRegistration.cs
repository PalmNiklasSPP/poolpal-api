namespace poolpal_api.Database.Entities.Tournament
{
    public class TournamentRegistration
    {
        public int RegistrationId { get; set; }
        public int TournamentId { get; set; }
        public int? GroupId { get; set; } // Optional, for group-based tournaments
        public int PlayerId { get; set; } // or TeamId if it's team-based
        public RegistrationStatus Status { get; set; } // Enum for status like Confirmed, Pending, etc.

        // Navigation properties
        public Tournament Tournament { get; set; }
        public Player Player { get; set; } // or Team
        public Group Group { get; set; } // Optional, for group-based tournaments
    }

    public enum RegistrationStatus
    {
        Confirmed,
        Pending,
        Cancelled
    }

}
