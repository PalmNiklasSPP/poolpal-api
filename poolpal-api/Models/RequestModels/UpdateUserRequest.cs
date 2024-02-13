using poolpal_api.Database.Entities;

namespace poolpal_api.Models.RequestModels
{
    public class UpdateUserRequest
    {
        public string? PlayerName { get; set; }
        public string? Description { get; set; }
        public int SppTeamId { get; set; }
    }
}
