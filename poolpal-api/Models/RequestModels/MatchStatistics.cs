namespace poolpal_api.Models.RequestModels
{
    public class MatchStatistics
    {
        public string MatchDate { get; set; }
        public string Opponent { get; set; }
        public bool isWinner { get; set; }
        public string Winner{ get; set; }
        public PoolGameType TournamentFormat { get; set; }
        public int EloChange { get; set; }
    }
}
