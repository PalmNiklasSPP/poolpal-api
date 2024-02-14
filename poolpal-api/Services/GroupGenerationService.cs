using poolpal_api.Database;
using Microsoft.EntityFrameworkCore;
using poolpal_api.Database.Entities.Tournament;

namespace poolpal_api.Services
{
    public interface IGroupGenerationService
    {
        Task GenerateGroupsForTournament(int tournamentId);
    }

    public class GroupGenerationService(PoolTournamentContext dbContext) : IGroupGenerationService
    {
        public async Task GenerateGroupsForTournament(int tournamentId)
        {
            // Get the tournament's registrations and groups
            var tournament = await dbContext.Tournaments
                .Include(t => t.Registrations)
                .Include(t => t.Groups)
                .FirstOrDefaultAsync(t => t.TournamentId == tournamentId);

            if (tournament == null || !tournament.Groups.Any())
            {
                throw new InvalidOperationException("Tournament or groups not found.");
            }

            // Shuffle the registrations
            var shuffledRegistrations = tournament.Registrations.Where(x => x.Status == RegistrationStatus.Confirmed).OrderBy(r => Guid.NewGuid()).ToList();

            // Distribute registrations across groups
            var groupCount = tournament.Groups.Count;
            for (var i = 0; i < shuffledRegistrations.Count; i++)
            {
                var groupIndex = i % groupCount;
                var group = tournament.Groups.ElementAt(groupIndex);
                var registration = shuffledRegistrations[i];

                registration.GroupId = group.GroupId; // Assuming GroupId is nullable in TournamentRegistration
            }

            await dbContext.SaveChangesAsync();
        }
    }

}
