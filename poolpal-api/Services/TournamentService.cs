using Microsoft.EntityFrameworkCore;
using poolpal_api.Database;
using poolpal_api.Database.Entities.Tournament;

namespace poolpal_api.Services;

public interface ITournamentService
{
    Task<Tournament> CreateTournament(Tournament tournament);
    Task<TournamentRegistration> RegisterForTournament(int tournamentId, int playerId);
    Task<Tournament> GetTournamentById(int tournamentId);
}
public class TournamentService(PoolTournamentContext dbContext) : ITournamentService
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
        if (tournament == null || tournament.Registrations.Count >= tournament.ParticipantLimit)
        {
            return null; // Tournament not found or registration limit reached
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
            .Include(t => t.Groups)
            .FirstOrDefaultAsync(t => t.TournamentId == tournamentId);
    }
}