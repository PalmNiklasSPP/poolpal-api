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
}
