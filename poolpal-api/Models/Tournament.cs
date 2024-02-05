using System.Text.RegularExpressions;

namespace poolpal_api.Models
{
    public class Tournament
    {
        public int TournamentId { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public int ParticipantLimit { get; set; }
        public bool IsTeamEvent { get; set; }
        public string TournamentType { get; set; } // Single Elimination, Double Elimination

        // Navigation Properties
        public ICollection<Match> Matches { get; set; }
    }
}