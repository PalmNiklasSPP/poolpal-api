using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
    public class StatisticsController(PoolTournamentContext context) : ControllerBase
    {
        [HttpGet("GetLeaderboard")]
        public IEnumerable<LeaderboardEntry> GetLeaderboard()
        {
            var leaderboardEntries = context.LeaderboardEntries.OrderByDescending(x => x.ELO).ToList();
            return leaderboardEntries;
        }

        [HttpGet("GetPlayerRanking")]
        public int GetRanking(string? inputId)
        {
            int playerId;
            if (!Int32.TryParse(inputId, out playerId))
            {
                throw new ArgumentException("Invalid input");
            }

            int playerElo = context.Players.SingleOrDefault(p => p.PlayerId == playerId)?.ELO ?? 0;

            int playersWithEqualOrHigherRanking = context.Players.Where(p => p.ELO >= playerElo).ToList().Count();

            return playersWithEqualOrHigherRanking;
        }

        [HttpGet("GetPlayerMatches")]
        public IEnumerable<MatchStatistics> GetRecentGames(string? userIdInput, int numberOfGames = int.MaxValue)
        {
            int userId;
            if (!Int32.TryParse(userIdInput, out userId))
            {
                throw new ArgumentException($"{userIdInput} is not a number");
            }

            var playerID = context.Players
                .SingleOrDefault(player => player.PlayerId == userId)?.PlayerId;

            if (!playerID.HasValue)
            {
                throw new ArgumentException($"No player with loginID {userId} exists");
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
                MatchDate = game.MatchDate.ToString("yyyy-MM-dd"),
                Opponents = opponentsForMatches.TryGetValue(game.MatchId, out var opponents) ? string.Join(", ", opponents) : "Missing opponents",
                isWinner = game.PlayerMatches.Any(pm => pm.IsWinner && pm.PlayerId == playerID.Value),
                Winner = GetWinnerFromMatch(game.MatchId),
                TournamentFormat = game.PoolGameType,
                EloChange = game.PlayerMatches.SingleOrDefault(pm => pm.PlayerId == playerID.Value)?.EloChange ?? 0
            }).OrderByDescending(m => m.MatchDate)
            .ThenByDescending(m => m.MatchID)
            .ToList();

            return matchStatistics;
        }

        [HttpGet("GetWinStatistics")]
        public WinStatistics GetWinStatistics(string? inputId)
        {
            int playerId;
            if (!Int32.TryParse(inputId, out playerId))
            {
                throw new ArgumentException("Invalid input");
            }

            var allPlayerMatches = context.PlayerMatches.Where(pm => pm.PlayerId == playerId);

            double wonGames = allPlayerMatches.Where(pm => pm.IsWinner).Count();
            double lostGames = allPlayerMatches.Count() - wonGames;
            double winRate = wonGames == 0 ? 0 : wonGames / allPlayerMatches.Count() * 100;

            return new WinStatistics() { totalWins = (int) wonGames, totalLoses = (int)lostGames, winrate = winRate };
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
