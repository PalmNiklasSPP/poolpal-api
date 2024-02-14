using System.Text.RegularExpressions;
using poolpal_api.Models;

namespace poolpal_api.Database.Entities.Tournament
{
    public class Tournament
    {
        public int TournamentId { get; set; }
        public string Name { get; set; }
        public TournamentFormat Format { get; set; }
        public PoolGameType GameType { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int ParticipantLimit { get; set; }
        public bool IsTeamBased { get; set; }
        public TournamentParticipationType ParticipationType { get; set; }
        public string? Description { get; set; }
        public int OrganiserId { get; set; }

        // New navigation properties
        public List<Group> Groups { get; set; } // Groups/Brackets in the tournament
        public List<TournamentRegistration> Registrations { get; set; } // Registrations for the tournament

        // Existing navigation property
        public List<Match> Matches { get; set; } // Consider if this should be moved under Group

        public Player Organiser { get; set; }
    }



    public enum TournamentParticipationType
    {
        Open,
        InviteOnly,
    }

}