namespace poolpal_api.Database.Entities
{
    public class SppTeam
    {
        public int Id { get; set; }
        public string? ShortName { get; set; }
        public string? FullName { get; set; }
        public string? Description { get; set; }
        public OrganisationUnit OrganisationUnit { get; set; }
    }


    public enum OrganisationUnit
    {
          
        Tech = 1,
        ProductAndOperation,
        MarketAndCommunication,
        Law,
        Other = 99,
    }
}
