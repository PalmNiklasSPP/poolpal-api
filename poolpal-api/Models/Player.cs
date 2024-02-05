using poolpal_api.Models.PoolTournamentApi.Models;

namespace poolpal_api.Models
{
    public class Player
    {
        public int PlayerId { get; set; }
        public string LoginId { get; set; }
        public string PlayerName { get; set; }

        public int RankingPoints { get; set; }

        // Navigation property
        public ICollection<PlayerMatch> PlayerMatches { get; set; }
    }


}
