using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using poolpal_api.Database;
using poolpal_api.Database.Entities;
using poolpal_api.Models.PoolTournamentApi.Models;
using System.Security.Principal;

namespace poolpal_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatisticsController(PoolTournamentContext context) : ControllerBase {

        [HttpGet("GetLeaderboard")]
        public IEnumerable<LeaderboardEntry> GetLeaderboard()
        {
            var leaderboardEntries = context.LeaderboardEntries.OrderByDescending(x => x.RankingPoints).ToList();
            return leaderboardEntries;
        }
        [HttpGet("GetPlayerMatches")]
        public IEnumerable<Match> GetRecentGames(string user, int? numberOfGames)
        {
            var playerWindowsPrinciple = HttpContext.User.Identity as WindowsIdentity;
            var whatisthis = playerWindowsPrinciple.Name;
            int playerId = context.Players.Where(p => p.LoginId == user).SingleOrDefault().PlayerId;
            var gamesIds = context.PlayerMatches.Where(pm => pm.PlayerId == playerId);
            List<int> matches = new List<int>();
            foreach(PlayerMatch match in gamesIds)
            {
                matches.Add(match.MatchId);
            }
            var latestGames = context.Matches.Where(m => matches.Contains(m.MatchId)).OrderByDescending(x => x.MatchDate).ToList();


            
            return latestGames;
        }
    }
}
