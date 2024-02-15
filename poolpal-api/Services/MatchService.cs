using Microsoft.EntityFrameworkCore;
using poolpal_api.Database;
using poolpal_api.Database.Entities;
using poolpal_api.Database.Entities.Tournament;
using poolpal_api.Models.PoolTournamentApi.Models;

namespace poolpal_api.Services
{
    public interface IMatchService
    {
        Task CreateRoundRobinMatchesForTournament(int tournamentId);
    }

    public class MatchService(PoolTournamentContext dbContext) : IMatchService
    {
        public async Task CreateRoundRobinMatchesForTournament(int tournamentId)
        {
            var tournament = await dbContext.Tournaments
                .Include(t => t.Groups)
                .ThenInclude(g => g.Participants)
                .FirstOrDefaultAsync(t => t.TournamentId == tournamentId);

            if (tournament == null)
            {
                throw new InvalidOperationException("Tournament not found.");
            }

            foreach (var group in tournament.Groups)
            {
                CreateRoundRobinMatchesForGroup(group, tournament);
            }

            await dbContext.SaveChangesAsync();
        }

        private void CreateRoundRobinMatchesForGroup(Group group, Tournament tournament)
        {
            var participants = group.Participants;

            for (int i = 0; i < participants.Count; i++)
            {
                for (int j = i + 1; j < participants.Count; j++)
                {
                    var match = new Match
                    {
                        GroupId = group.GroupId,
                        TournamentId = tournament.TournamentId,
                        PoolGameType = tournament.GameType,
                        MatchDate = tournament.StartDate,
                        PlayerMatches = []
                    };

                    // Add relationships to the players/teams involved in the match
                    match.PlayerMatches.Add(new PlayerMatch { PlayerId = participants[i].PlayerId, Match = match });
                    match.PlayerMatches.Add(new PlayerMatch { PlayerId = participants[j].PlayerId, Match = match });

                    dbContext.Matches.Add(match);
                }
            }
        }
    }
}
