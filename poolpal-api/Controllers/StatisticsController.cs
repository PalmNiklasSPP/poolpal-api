using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using poolpal_api.Database;
using poolpal_api.Database.Entities;
using poolpal_api.Models.PoolTournamentApi.Models;
using poolpal_api.Models.RequestModels;
using System.Reflection.Metadata.Ecma335;
using System.Security.Principal;
using poolpal_api.Models;

namespace poolpal_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatisticsController(PoolTournamentContext context) : ControllerBase
    {

        [HttpGet("GetLeaderboard")]
        public IEnumerable<LeaderboardEntry> GetLeaderboard()
        {
            var leaderboardEntries = context.LeaderboardEntries.OrderByDescending(x => x.ELO).ToList();
            return leaderboardEntries;
        }
        [HttpGet("GetPlayerMatches")]
        public IEnumerable<MatchStatistics> GetRecentGames(string user, int numberOfGames = int.MaxValue)
        {
            var playerID = context.Players
                .SingleOrDefault(player => player.LoginId == user)?.PlayerId;

            if (!playerID.HasValue)
            {
                throw new ArgumentException($"No player with loginID {user} exists");
            }

            var matchIDs = context.PlayerMatches
                .AsNoTracking()
                .Where(pm => pm.PlayerId == playerID.Value)
                .Select(pm => pm.MatchId)
                .ToList();

            // Combine queries to fetch the recent games for the specified user directly.
            var recentGames = context.Matches
                .Include(pm => pm.PlayerMatches)
                .AsNoTracking()
                .Where(m => matchIDs.Contains(m.MatchId))
                .OrderByDescending(m => m.MatchDate)
                .Take(numberOfGames) // Limit the number of games fetched based on 'numberOfGames
                .ToList();

            var opponentsForMatches = GetOpponentsNameFromMatch(playerID.Value, matchIDs);

            var matchStatistics = recentGames.Select(game => new MatchStatistics
            {
                MatchID = game.MatchId,
                MatchDate = game.MatchDate.Value.ToString("yyyy-MM-dd"),
                Opponents = opponentsForMatches.TryGetValue(game.MatchId, out var opponents) ? string.Join(", ", opponents) : "Missing opponents",
                isWinner = game.PlayerMatches.Any(pm => pm.IsWinner && pm.PlayerId == playerID.Value),
                Winner = GetWinnerFromMatch(game.MatchId),
                TournamentFormat = game.PoolGameType,
                EloChange = game.PlayerMatches.SingleOrDefault(pm => pm.PlayerId == playerID.Value)?.EloChange ?? 0
            }).ToList();

            return matchStatistics;


        }

        [HttpGet("GetGeneralStatistics")]
        public ActionResult<GeneralStatistics> GEtGeneralStatistics()
        {
            var generalStatistics = new GeneralStatistics
            {
                TotalMatches = context.Matches.Count(),
                TotalPlayers = context.Players.Count(),
                TotalTournaments = context.Tournaments.Count()
            };

            return generalStatistics;
        }

        [HttpGet("GetTeamStatistics")]
        public ActionResult<TeamStatistics> GetTeamStatistics(int teamId)
        {
            var matches = context.Matches
                .Include(x => x.PlayerMatches)
                .ThenInclude(x => x.Player)
                .Where(m => m.PlayerMatches.Any(pm => pm.Player.SppTeamId == teamId))
                .ToList();
            var teamStatistics = new TeamStatistics
            {
                TotalMatches = matches.Count,
                TotalPlayers = context.Players.Count(p => p.SppTeamId == teamId),
                TotalTournaments = context.Tournaments.Include(x => x.Organiser).Count(x => x.Organiser != null && x.Organiser.SppTeamId == teamId),
                TotalWins = matches.SelectMany(m => m.PlayerMatches).Count(pm => pm.IsWinner && pm.Player.SppTeamId == teamId),
                TotalLosses = matches.SelectMany(m => m.PlayerMatches).Count(pm => !pm.IsWinner && pm.Player.SppTeamId == teamId)
            };

            return teamStatistics;
        }
     
        #region Private methods

        private Dictionary<int, List<string?>> GetOpponentsNameFromMatch(int playerId, ICollection<int> matchID)
        {

            var opponentPlayer = context.PlayerMatches
                .Include(pm => pm.Player)
                .AsNoTracking()
                .Where(pm => matchID.Contains(pm.MatchId) && pm.PlayerId != playerId)
               .GroupBy(pm => pm.MatchId)
                .ToDictionary(k => k.Key, k => k.Select(pm => pm.Player?.PlayerName).ToList());

            return opponentPlayer;
        }

        private string GetWinnerFromMatch(int matchId)
        {
            string winner = context.PlayerMatches
                .AsNoTracking()
                .Where(pm => pm.MatchId == matchId && pm.IsWinner)
                .Select(pm => pm.Player.LoginId)
                .FirstOrDefault();

            return winner;
        }

        #endregion
    }
}
