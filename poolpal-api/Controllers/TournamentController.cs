using Microsoft.AspNetCore.Mvc;
using poolpal_api.Database.Entities;
using poolpal_api.Database.Entities.Tournament;
using poolpal_api.Models;
using poolpal_api.Models.RequestModels;
using poolpal_api.Services;

namespace poolpal_api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TournamentController : ControllerBase
{
    private readonly ITournamentService _tournamentService;
    private readonly IGroupGenerationService _groupGenerationService;

    public TournamentController(ITournamentService tournamentService, IGroupGenerationService groupGenerationService)
    {
        _tournamentService = tournamentService;
        _groupGenerationService = groupGenerationService;
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateTournament([FromBody] CreateTournamentRequest request)
    {
        var startDate = DateTime.Parse(request.StartDate);
        
        
        if(!Enum.TryParse<TournamentFormat>(request.Format, out var format) || !Enum.TryParse<TournamentParticipationType>(request.ParticipationType, out var participationType))
            return BadRequest("Invalid format or participation type.");

        var tournament = new Tournament
        {
            Name = request.Name,
            Format = format,
            StartDate = startDate,
            ParticipantLimit = request.ParticipantLimit,
            IsTeamBased = request.IsTeamBased,
            ParticipationType = participationType,
            Description = request.Description, // Set the description
            OrganiserId = request.OrganiserId, // Set the organiser
            Groups = [],
        };

        // Create groups based on NumberOfGroups
        for (var i = 0; i < request.NumberOfGroups; i++)
        {
            tournament.Groups.Add(new Group { Name = $"Group {i + 1}" });
        }

        tournament.TryChangeStatus(TournamentStatus.Scheduled);

        var createdTournament = await _tournamentService.CreateTournament(tournament);
        return Ok("Tournament created");
    }


    [HttpPost("{tournamentId}/register")]
    public async Task<IActionResult> RegisterForTournament(int tournamentId, [FromBody] int playerId)
    {
        var registration = await _tournamentService.RegisterForTournament(tournamentId, playerId);
        if (registration == null)
        {
            return BadRequest("Registration failed.");
        }
        return Ok(registration);
    }

    [HttpPost("{tournamentId}/start")]
    public async Task<IActionResult> StartTournament(int tournamentId)
    {
        try
        {
            await _tournamentService.StartTournament(tournamentId);
             
            
            return Ok("Tournament started and groups generated.");
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{tournamentId}")]
    public async Task<ActionResult<Tournament>> GetTournament(int tournamentId)
    {
        var tournament = await _tournamentService.GetTournamentById(tournamentId);
        if (tournament == null)
        {
            return NotFound();
        }
        return Ok(tournament);
    }

    //get open tournaments
    [HttpGet("getOpen")]
    public async Task<IEnumerable<TournamentOverview>> GetOpenTournaments(int playerId)
    {
        var tournaments = await _tournamentService.GetOpenTournaments(playerId);
        return tournaments;
    }

    [HttpGet("{tournamentId:int}/group/{groupId:int}/matches")]
    public async Task<IEnumerable<Match>> GetMatchesInGroup(int tournamentId, int groupId)
    {
        var matches = await _tournamentService.GetMatchesForGroupInTournament(tournamentId, groupId);
        return matches;
    }
}