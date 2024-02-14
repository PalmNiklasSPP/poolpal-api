using poolpal_api.Database.Entities.Tournament;

namespace poolpal_api.Models.RequestModels
{
    public class CreateTournamentRequest
    {
        public string Name { get; set; }
        public string Format { get; set; }
        public string StartDate { get; set; }
        public string? EndDate { get; set; }
        public int ParticipantLimit { get; set; }
        public bool IsTeamBased { get; set; } = false;
        public string ParticipationType { get; set; }
        public int? NumberOfGroups { get; set; }
        public string Description { get; set; }  
        public int OrganiserId { get; set; } 

        // Add any other properties relevant to tournament creation
    }

}
