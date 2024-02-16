namespace poolpal_api.Models
{
    public class StandingsModel(List<PlayerStatistics> playerStatistics)
    {
        public int GroupId { get; set; }
        public int TournamentId { get; set; }
        public  List<PlayerStatistics> PlayerStatistics { get; set; } = playerStatistics;
    }
}
