namespace poolpal_api.Database.Entities
{
    public class LeaderboardEntry //  Maybe use a view
    {
        public int PlayerId { get; set; }
        public string PlayerName { get; set; }
        public int TotalWins { get; set; }
        public int TotalLosses { get; set; }
        public int ELO { get; set; }
    }

}
