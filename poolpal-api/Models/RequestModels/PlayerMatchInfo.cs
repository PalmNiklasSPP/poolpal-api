namespace poolpal_api.Models.RequestModels
{
    public class PlayerMatchInfo
    {
        public int MatchID { get; set; }
        public List<string> Opponents {  get; set; }
        public bool isWinner{ get; set; }
        public int eloChange{ get; set; }
    }
}
