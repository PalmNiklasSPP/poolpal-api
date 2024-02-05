using System.Text.RegularExpressions;

namespace poolpal_api.Models
{
    public class Tournament
    {
        public int TournamentId { get; set; }
        public string Name { get; set; }
        public TournamentFormat Format { get; set; } 
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int ParticipantLimit { get; set; }
        public bool IsTeamBased { get; set; }


        // Navigation property
        public List<Match> Matches { get; set; }
    }

}