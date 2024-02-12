using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using poolpal_api.Database;
using poolpal_api.Database.Entities;
using poolpal_api.Models.PoolTournamentApi.Models;
using poolpal_api.Models.RequestModels;
using System.Security.Principal;

namespace poolpal_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatisticsController(PoolTournamentContext context) : ControllerBase {

        [HttpGet("GetLeaderboard")]
        public IEnumerable<LeaderboardEntry> GetLeaderboard()
        {
            var leaderboardEntries = context.LeaderboardEntries.OrderByDescending(x => x.ELO).ToList();
            return leaderboardEntries;
        }
        [HttpGet("GetPlayerMatches")]
        public IEnumerable<MatchStatistics> GetRecentGames(string user, int? numberOfGames)
        {
            // Assuming 'numberOfGames' can be null, we set a default value if it's not provided.
            int gamesToTake = numberOfGames ?? int.MaxValue; // If numberOfGames is null, fetch as many games as possible.

            var matchIDs = context.PlayerMatches
                .Select(pm => pm.Player.LoginId == user).ToList();

            // Combine queries to fetch the recent games for the specified user directly.
            var recentGames = context.Matches
                .Where(m => m.PlayerMatches.Any(pm => pm.Player.PlayerId == context.Players
                    .Where(p => p.LoginId == user)
                    .SingleOrDefault()
                    .PlayerId))
                .OrderByDescending(m => m.MatchDate)
                .Take(gamesToTake) // Limit the number of games fetched based on 'numberOfGames'
                .ToList();



            var matchStatistics = recentGames.Select(game => new MatchStatistics
            {
                MatchDate = game.MatchDate.ToString("yyyy-MM-dd"),
                Opponent = "RIP",
                isWinner = true,
                Winner = "RIP",
                TournamentFormat = game.PoolGameType,
                EloChange = 0
            }).ToList();

            return new List<MatchStatistics>();
        }
    }
}
