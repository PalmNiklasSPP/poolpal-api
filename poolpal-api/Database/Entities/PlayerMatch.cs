using poolpal_api.Database.Entities;

namespace poolpal_api.Models
{
    namespace PoolTournamentApi.Models
    {
        public class PlayerMatch
        {
            public int PlayerId { get; set; }
            public Player? Player { get; set; }

            public int MatchId { get; set; } 
            public Match? Match { get; set; }

            public int Score { get; set; } = 0;
            public int EloChange { get; set; } = 0;
            public bool IsWinner { get; set; }
        }
    }


}
