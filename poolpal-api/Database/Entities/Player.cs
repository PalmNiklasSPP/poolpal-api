using poolpal_api.Models.PoolTournamentApi.Models;

namespace poolpal_api.Database.Entities
{
    public class Player
    {
        public int PlayerId { get; set; }
        public string? LoginId { get; set; }
        public string? PlayerName { get; set; }
        public string? Description { get; set; }
        public string? Avatar { get; set; }
        public int ELO { get; set; } = 1500;

        public int? SppTeamId { get; set; }

        // Navigation property
        public ICollection<PlayerMatch>? PlayerMatches { get; set; }
        public SppTeam? SppTeam { get; set; }

    }


}
