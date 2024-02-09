using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using poolpal_api.Database;
using poolpal_api.Database.Entities;
using poolpal_api.Models;
using poolpal_api.Models.PoolTournamentApi.Models;
using poolpal_api.Models.RequestModels;

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
        public IActionResult AddPlayerToMatch(int matchId, int playerId)
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
                Score = 0, // Default score, update later
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

            foreach (var playerMatch in match.PlayerMatches)
            {
                playerMatch.IsWinner = playerMatch.PlayerId == winnerPlayerId;
            }

            context.SaveChanges();

            return Ok(match);
        }
    }

}
