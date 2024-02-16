using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using poolpal_api.Database;
using poolpal_api.Database.Entities;
using poolpal_api.Database.Entities.Tournament;
using poolpal_api.Models;

namespace poolpal_api.Services;

public interface ITournamentService
{
    Task<Tournament> CreateTournament(Tournament tournament);
    Task<TournamentRegistration> RegisterForTournament(int tournamentId, int playerId);
    Task<Tournament> GetTournamentById(int tournamentId);

    Task<List<TournamentOverview>> GetOpenTournaments(int playerId);

    Task<bool> StartTournament(int tournamentId);

    Task<List<Match>> GetMatchesForGroupInTournament(int tournamentId, int groupId);

    Task<StandingsModel> GetStandingsForGroup(int tournamentId, int groupId);
}
public class TournamentService(PoolTournamentContext dbContext, IGroupGenerationService groupService, IMatchService matchService) : ITournamentService
{
    public async Task<Tournament> CreateTournament(Tournament tournament)
    {
        dbContext.Tournaments.Add(tournament);
        await dbContext.SaveChangesAsync();
        return tournament;
    }

    public async Task<TournamentRegistration> RegisterForTournament(int tournamentId, int playerId)
    {
        var tournament = await dbContext.Tournaments.FindAsync(tournamentId);
        tournament.Registrations ??= new List<TournamentRegistration>();


        if (tournament == null || tournament.Registrations.Count >= tournament.ParticipantLimit)
        {
            return null; // Tournament not found or registration limit reached
        }


        // Check if the player is already registered
        if (tournament.Registrations.Any(r => r.PlayerId == playerId))
        {
            return null; // Player is already registered
        }

        var registration = new TournamentRegistration
        {
            TournamentId = tournamentId,
            PlayerId = playerId,
            Status = RegistrationStatus.Confirmed // Assuming default status is Confirmed
        };

        dbContext.TournamentRegistrations.Add(registration);
        await dbContext.SaveChangesAsync();
        return registration;
    }

    public async Task<Tournament> GetTournamentById(int tournamentId)
    {
        return await dbContext.Tournaments
            .Include(t => t.Registrations)
            .ThenInclude(x => x.Player)
            .ThenInclude(x => x.SppTeam)
            .Include(t => t.Groups)
            .FirstOrDefaultAsync(t => t.TournamentId == tournamentId);
    }

    public  Task<List<TournamentOverview>> GetOpenTournaments(int playerId)
    {
        var tournaments = dbContext.Tournaments
            .Where(t => t.ParticipationType == TournamentParticipationType.Open && t.Status == TournamentStatus.Open || t.Status == TournamentStatus.Scheduled || t.Status == TournamentStatus.Draft).Select(x => new TournamentOverview
            {
                // Map properties from Tournament to TournamentOverview
                TournamentId = x.TournamentId,
                Name = x.Name,
                Format = x.Format,
                GameType = x.GameType,
                StartDate = x.StartDate,
                ParticipantLimit = x.ParticipantLimit,
                IsTeamBased = x.IsTeamBased,
                ParticipationType = x.ParticipationType,
                Status = x.Status,
                Description = x.Description,
                Organiser = new PlayerPreview
                {
                    PlayerId = x.OrganiserId ?? -1,
                    Name = x.Organiser.PlayerName,
                    Avatar = x.Organiser.Avatar
                },
                IsRegistered = x.Registrations.Any(r => r.PlayerId == playerId)
            })
            .ToListAsync();
        return tournaments;

    }

    public async Task<bool> StartTournament(int tournamentId)
    {
        await groupService.GenerateGroupsForTournament(tournamentId);
        var tournament = await dbContext.Tournaments.FindAsync(tournamentId);
       await matchService.CreateRoundRobinMatchesForTournament(tournamentId);
        var success = tournament.TryChangeStatus(TournamentStatus.InProgress);
        if (!success)
        {
            throw new InvalidOperationException("Tournament could not be started.");
        }
        await dbContext.SaveChangesAsync();
        return success;
    }


    public Task<List<Match>> GetMatchesForGroupInTournament(int tournamentId, int groupId)
    {
        return dbContext.Matches.AsNoTracking()
            .Where(m => m.TournamentId == tournamentId && m.GroupId == groupId)
            .Include(x => x.PlayerMatches)
            .ThenInclude(x => x.Player)
            .OrderBy(x => x.MatchDate)
            .ThenBy(x => x.HasBeenPlayed)
            .ToListAsync();
    }

    public async Task<StandingsModel> GetStandingsForGroup(int tournamentId, int groupId)
    {
        var groupMatches = await dbContext.Matches
            .Where(m => m.GroupId == groupId)
            .ToListAsync();

        var playedMatchIds = groupMatches
            .Where(m => m.HasBeenPlayed)
            .Select(m => m.MatchId)
            .ToList();

        var playerIdsInGroup = await dbContext.TournamentRegistrations
            .Where(tr => tr.GroupId == groupId)
            .Select(tr => tr.PlayerId)
            .ToListAsync();

        var playerMatchesInGroup = await dbContext.PlayerMatches
            .Where(pm => playerIdsInGroup.Contains(pm.PlayerId))
            .ToListAsync();

        var playerStatistics = playerIdsInGroup
            .Select(playerId => new PlayerStatistics
            {
                PlayerId = playerId,
                PlayerName = dbContext.Players.FirstOrDefault(p => p.PlayerId == playerId)?.PlayerName ?? "Unknown",
                TotalWins = playerMatchesInGroup.Count(pm => pm.PlayerId == playerId && pm.IsWinner && playedMatchIds.Contains(pm.MatchId)),
                TotalLosses = playerMatchesInGroup.Count(pm => pm.PlayerId == playerId && !pm.IsWinner && playedMatchIds.Contains(pm.MatchId)),
                MatchesPlayed = playerMatchesInGroup.Count(pm => pm.PlayerId == playerId && playedMatchIds.Contains(pm.MatchId)),
                Score = playerMatchesInGroup.Where(pm => pm.PlayerId == playerId).Sum(pm => pm.Score)
            }).OrderByDescending(x => x.MatchesPlayed).ThenByDescending(x => x.TotalWins).ToList();

        var standings = new StandingsModel(playerStatistics)
        {
            GroupId = groupId,
            TournamentId = tournamentId,
        };

        return standings;
    }



}