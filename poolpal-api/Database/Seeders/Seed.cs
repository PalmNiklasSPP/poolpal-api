using Microsoft.EntityFrameworkCore;
using poolpal_api.Models;
using poolpal_api.Models.PoolTournamentApi.Models;

namespace poolpal_api.Database.Seeders
{
    public class Seed
    {
        public static void DoSeed(ModelBuilder mb)
        {
            SeedPlayers(mb);
            SeedTournaments(mb);
            SeedMatches(mb);
            SeedPlayerMatches(mb);
        }

        private static void SeedPlayers(ModelBuilder mb)
        {
            var data = new Player[]
            {
                new() { PlayerId = 1, PlayerName = "Player 1", LoginId = "STB\\NIPA01", RankingPoints = 100},
                new() { PlayerId = 2, PlayerName = "Player 2", LoginId = "STB\\TIAL01", RankingPoints = 200},
            };
            mb.Entity<Player>().HasData(data);
        }

        private static void SeedMatches(ModelBuilder mb)
        {
            var data = new Match[]
            {
                new() { MatchId = 1, PoolGameType = PoolGameType.EightBall, TournamentId = 1, MatchDate = new DateTime(2024, 1, 2)},
                new() { MatchId = 2, PoolGameType = PoolGameType.EightBall, TournamentId = 1, MatchDate = new DateTime(2024, 1, 2)},
                new() { MatchId = 3, PoolGameType = PoolGameType.EightBall, MatchDate = new DateTime(2024, 1, 2)}
            };
            mb.Entity<Match>().HasData(data);
        }

        //playermatch
        private static void SeedPlayerMatches(ModelBuilder mb)
        {
            var data = new PlayerMatch[]
            {
                new() { PlayerId = 1, MatchId = 1, Score = 5, IsWinner = true},
                new() { PlayerId = 2, MatchId = 1, Score = 3, IsWinner = false},
                new() { PlayerId = 1, MatchId = 2, Score = 5, IsWinner = true},
                new() { PlayerId = 2, MatchId = 2, Score = 3, IsWinner = false},
                new() { PlayerId = 1, MatchId = 3, Score = 5, IsWinner = true},
                new() { PlayerId = 2, MatchId = 3, Score = 3, IsWinner = false},
            };
            mb.Entity<PlayerMatch>().HasData(data);
        }

        private static void SeedTournaments(ModelBuilder mb)
        {
            // 2 TOurnaments data
            var data = new Tournament[]
            {
                new() { TournamentId = 1, Name = "Tournament 1", StartDate = new DateTime(2024, 1, 1), EndDate = new DateTime(2021, 2, 2), Format = TournamentFormat.SingleElimination, IsTeamBased = false, ParticipantLimit = 10},
            };
            mb.Entity<Tournament>().HasData(data);
        }

    }
}
