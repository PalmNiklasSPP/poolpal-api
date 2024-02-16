namespace poolpal_api.Models
{
    public class GeneralStatistics
    {
        public int TotalMatches { get; set; }
        public int TotalPlayers { get; set; }
        public int TotalTournaments { get; set; }
    }


    public class TeamStatistics : GeneralStatistics
    {
        public int TotalWins { get; set; }
        public int TotalLosses { get; set; }

    }

    public class PlayerStatistics
    {
        public int PlayerId { get; set; }
        public string PlayerName { get; set; }

        public int TotalWins { get; set; }

        public int TotalLosses { get; set; }

        public int MatchesPlayed { get; set; }

        public int Score { get; set; }
    }
}
