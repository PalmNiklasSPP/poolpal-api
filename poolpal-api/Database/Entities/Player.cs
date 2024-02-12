using poolpal_api.Models.PoolTournamentApi.Models;

namespace poolpal_api.Database.Entities
{
    public class Player
    {
        public int PlayerId { get; set; }
        public string LoginId { get; set; }
        public string PlayerName { get; set; }

        public int ELO { get; set; }

        // Navigation property
        public ICollection<PlayerMatch> PlayerMatches { get; set; }
    }


}
