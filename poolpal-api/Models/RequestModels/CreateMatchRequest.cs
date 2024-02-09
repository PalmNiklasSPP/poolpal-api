using poolpal_api.Database.Entities;

namespace poolpal_api.Models.RequestModels
{
    public class CreateMatchRequest
    {
        public string Date { get; set; }
        public string? Notes { get; set; }
        public int PoolGameType { get; set; }
        public int? TournamentId { get; set; }

        public int? WinnerId { get; set; }

        public int? Player1 { get; set; }
        public int? Player2 { get; set; }


    }
}
