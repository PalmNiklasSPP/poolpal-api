using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using poolpal_api.Database;
using poolpal_api.Database.Entities;
using poolpal_api.Models.PoolTournamentApi.Models;
using poolpal_api.Models.RequestModels;
using System.Reflection.Metadata.Ecma335;
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
                .Where(pm => pm.Player.LoginId == user)
                .ToList();

            // Combine queries to fetch the recent games for the specified user directly.
            var recentGames = context.Matches
                .Where(m => m.PlayerMatches.Any(pm => pm.Player.PlayerId == context.Players
                    .Where(p => p.LoginId == user)
                    .SingleOrDefault()
                    .PlayerId))
                .OrderByDescending(m => m.MatchDate)
                .Take(gamesToTake) // Limit the number of games fetched based on 'numberOfGames
                .ToList();

            var playerMatchInfo = matchIDs.Select(pm => new PlayerMatchInfo
            {
                MatchID = pm.MatchId,
                Opponents = getOpponentsNameFromMatch(pm.PlayerId, pm.MatchId),
                isWinner = pm.IsWinner,
                eloChange = pm.EloChange

            }).ToList();

            var matchStatistics = recentGames.Select(game => new MatchStatistics
            {
                MatchID = game.MatchId,
                MatchDate = game.MatchDate.ToString("yyyy-MM-dd"),
                Opponents = "Missing opponents",
                isWinner = true,
                Winner = "Unknown",
                TournamentFormat = game.PoolGameType,
                EloChange = 0
            }).ToList();

            //Updating missing values in model
            foreach (var match in matchStatistics)
            {
                var matchingMatchId = 
                    playerMatchInfo.FirstOrDefault(pm => pm.MatchID == match.MatchID);
                if(matchingMatchId != null)
                {
                    match.Opponents = String.Join(",",matchingMatchId.Opponents);
                    match.isWinner = matchingMatchId.isWinner;
                    match.Winner = getWinnerFromMatch(matchingMatchId.MatchID);
                    match.EloChange = matchingMatchId.eloChange;
                }
            }

            return matchStatistics;

            #region Local methods

            List<string> getOpponentsNameFromMatch(int playerId, int matchID){

                var opponentPlayer = context.PlayerMatches
                    .Where(pm => pm.MatchId == matchID && pm.PlayerId != playerId)
                   .Select(pm => pm.Player.PlayerName)
                    .ToList();

                return opponentPlayer;
            }

            string getWinnerFromMatch(int matchId)
            {
                string winner = context.PlayerMatches
                    .Where(pm => pm.MatchId == matchId && pm.IsWinner)
                    .Select(pm => pm.Player.LoginId)
                    .FirstOrDefault();

                return winner;
            }

            #endregion
        }
    }
}
