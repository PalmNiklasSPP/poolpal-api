namespace poolpal_api.Models.RequestModels
{
    public class MatchStatistics
    {
        public int MatchID { get; set; }
        public string MatchDate { get; set; }
        public string Opponents { get; set; }
        public bool isWinner { get; set; }
        public string Winner{ get; set; }
        public PoolGameType TournamentFormat { get; set; }
        public int EloChange { get; set; }
    }
}
