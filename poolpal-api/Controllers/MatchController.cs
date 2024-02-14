using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using poolpal_api.Database;
using poolpal_api.Database.Entities;
using poolpal_api.Models;
using poolpal_api.Models.PoolTournamentApi.Models;
using poolpal_api.Models.RequestModels;
using poolpal_api.Services;

namespace poolpal_api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class MatchesController(PoolTournamentContext context) : ControllerBase
    {
        // POST: api/Matches/Create
        [HttpPost("CreateMatch")]
        public IActionResult CreateMatch([FromBody] CreateMatchRequest match)
        {
            if (match == null)
            {
                return BadRequest();
            }


            var newMatch = new Match
            {
                MatchDate = DateTime.Parse(match.Date),
                Notes = match.Notes,
                PoolGameType = (PoolGameType)match.PoolGameType,
                TournamentId = match.TournamentId
            };

            context.Matches.Add(newMatch);
            context.SaveChanges();

            return Ok(match);
        }
        [HttpPost("CreateMatchAndAddPlayers")]
        public IActionResult CreateMatchAndAddPlayers([FromBody] CreateMatchRequest match)
        {
            if (match == null)
            {
                return BadRequest();
            }


            var newMatch = new Match
            {
                MatchDate = DateTime.Parse(match.Date),
                Notes = match.Notes,
                PoolGameType = (PoolGameType)match.PoolGameType,
                TournamentId = match.TournamentId
            };

            context.Matches.Add(newMatch);
            context.SaveChanges();


            if (match.Player1.HasValue)
            {

                AddPlayerToMatch(newMatch.MatchId, match.Player1.Value);
            }

            if (match.Player2.HasValue)
            {
                AddPlayerToMatch(newMatch.MatchId, match.Player2.Value);
            }

            if (match.WinnerId.HasValue)
            {
                RecordResult(newMatch.MatchId, match.WinnerId.Value);
            }

            return Ok(match);
        }

        // POST: api/Matches/AddPlayerToMatch
        [HttpPost("AddPlayerToMatch")]
        public IActionResult AddPlayerToMatch(int matchId, int playerId, int eloChange = 0)
        {
            var match = context.Matches.Find(matchId);
            var player = context.Players.Find(playerId);

            if (match == null || player == null)
            {
                return NotFound();
            }

            var playerMatch = new PlayerMatch
            {
                PlayerId = playerId,
                MatchId = matchId,
                Score = eloChange, // Default score, update later
                IsWinner = false // Default winner status, update later
            };

            context.PlayerMatches.Add(playerMatch);
            context.SaveChanges();

            return Ok(playerMatch);
        }

        // POST: api/Matches/RecordResult
        [HttpPost("RecordResult")]
        public IActionResult RecordResult(int matchId, int winnerPlayerId)
        {
            var match = context.Matches.Include(m => m.PlayerMatches).FirstOrDefault(m => m.MatchId == matchId);
            int[] playerNewElo = new int[2];
            int matchIndex = 0;

            if (match == null)
            {
                return NotFound();
            }

            // Check if there's already a winner for this match
            var existingWinner = match.PlayerMatches.FirstOrDefault(pm => pm.IsWinner);
            if (existingWinner != null && existingWinner.PlayerId != winnerPlayerId)
            {
                // Handle the case where a different winner is already set. 
                // You might return a BadRequest or handle it differently.
                return BadRequest("A winner has already been set for this match.");
            }

            //TODO: Make it not ugly
            foreach (var playerMatch in match.PlayerMatches)
            {
                bool isWinner = playerMatch.PlayerId == winnerPlayerId;
                int opponentElo = match.PlayerMatches
                    .Where(pm => pm.PlayerId != playerMatch.PlayerId)
                    .Select(pm => pm.Player.ELO)
                    .FirstOrDefault();

                playerNewElo[matchIndex] = EloService.CalculateNewPlayerElo(playerMatch.Player.ELO,opponentElo,winner: isWinner);
                playerMatch.EloChange = playerNewElo[matchIndex] - playerMatch.Player.ELO;
                playerMatch.IsWinner = isWinner;
                matchIndex++;
            }

            matchIndex = 0;

            foreach(var playerMatch in match.PlayerMatches)
            {
                playerMatch.Player.ELO = playerNewElo[matchIndex];
                matchIndex++;
            }

            context.SaveChanges();

            return Ok(match);
        }

        //Get recent matches
        [HttpGet("GetRecentMatches")]
        public IActionResult GetRecentMatches(int userId)
        {
            // Get the 10 most recent matches for the user
            var matches = context.Matches
                .Include(m => m.PlayerMatches)
                .AsNoTracking()
                .Where(m => m.PlayerMatches.Any(pm => pm.PlayerId == userId))
                .OrderByDescending(m => m.MatchDate)
                .Take(10)
                .ToList();
            return Ok(matches);
        }


        //Get Upcoming matches
        [HttpGet("GetUpcomingMatches")]
        public IActionResult GetUpcomingMatches(int userId)
        {

            var matches = context.Matches
                .Include(m => m.PlayerMatches)
                .ThenInclude(pm => pm.Player)
                .Where(m => m.PlayerMatches.Any(pm => pm.PlayerId == userId) && m.MatchDate > DateTime.Now)
                .OrderBy(m => m.MatchDate)
                .Take(5)
                .ToList();

            return Ok(matches);
        }
    }

}
