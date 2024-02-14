namespace poolpal_api.Database.Entities.Tournament
{
    public class Group
    {
        public int GroupId { get; set; }
        public int TournamentId { get; set; }
        public string Name { get; set; } // Optional, for naming groups (e.g., Group A, Group B)

        // Navigation properties
        public Tournament Tournament { get; set; }
        public List<Match> Matches { get; set; } // Matches within this group
        public List<TournamentRegistration> Participants { get; set; } // Participants in this group
    }
}
