using poolpal_api.Models;
using poolpal_api.Models.PoolTournamentApi.Models;

namespace poolpal_api.Database.Entities
{
    public class Match
    {
        public int MatchId { get; set; }
        public int? TournamentId { get; set; }
        public DateTime MatchDate { get; set; }
        public string? Notes { get; set; }
        public PoolGameType PoolGameType { get; set; }

        // Navigation properties
        public Tournament? Tournament { get; set; }
        public ICollection<PlayerMatch> PlayerMatches { get; set; }

    }

}
