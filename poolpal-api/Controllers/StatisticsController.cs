using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using poolpal_api.Database;
using poolpal_api.Database.Entities;

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
       
    }
}
