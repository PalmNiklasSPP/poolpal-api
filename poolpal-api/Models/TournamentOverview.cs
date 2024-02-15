using poolpal_api.Database.Entities.Tournament;

namespace poolpal_api.Models
{
    public class TournamentOverview
    {
        public int TournamentId { get; set; }
        public string Name { get; set; }
        public TournamentFormat Format { get; set; }
        public PoolGameType GameType { get; set; }
        public DateTime StartDate { get; set; }
        public int ParticipantLimit { get; set; }
        public bool IsTeamBased { get; set; }
        public TournamentParticipationType ParticipationType { get; set; }
        public TournamentStatus Status { get; set; }
        public string? Description { get; set; }
        public PlayerPreview? Organiser { get; set; }

        public bool IsRegistered { get; set; }
    }
}
