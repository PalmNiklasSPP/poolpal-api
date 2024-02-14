using Microsoft.EntityFrameworkCore;
using poolpal_api.Database.Entities;
using poolpal_api.Models;
using poolpal_api.Models.PoolTournamentApi.Models;

namespace poolpal_api.Database.Seeders
{
    public class Seed
    {
        private const string Desc =
            "Introducing NickeP, a master of precision and strategy on the pool table. Known for exceptional cue control and tactical gameplay, NickeP brings a unique blend of focus and flair to every match. With numerous victories under their belt, NickeP is a formidable opponent and a crowd favorite. Watch as NickeP lines up their shots, showcasing a perfect blend of skill and style.";
        public static void DoSeed(ModelBuilder mb)
        {
            SeedSppTeam(mb);
            SeedPlayers(mb);
            SeedTournaments(mb);
            SeedMatches(mb);
            SeedPlayerMatches(mb);
        }

        private static void SeedSppTeam(ModelBuilder mb)
        {
            var data = new SppTeam[]
            {
                new() { Id = 1, ShortName = "BoB", FullName = "Business & Broker", OrganisationUnit = OrganisationUnit.Tech},
                new() { Id = 2, ShortName = "PW", FullName = "Private Web", OrganisationUnit = OrganisationUnit.Tech},
                new() { Id = 99, ShortName = "Other", FullName = "Other", OrganisationUnit = OrganisationUnit.Other},
            };
            mb.Entity<SppTeam>().HasData(data);

        }

        private static void SeedPlayers(ModelBuilder mb)
        {
            var data = new Player[]
            {
                new() { PlayerId = 1, PlayerName = "NickeP", LoginId = "STB\\NIPA01",Description = Desc, SppTeamId = 1},
                new() { PlayerId = 2, PlayerName = "Timmy", LoginId = "STB\\TIAL01", SppTeamId = 1},
                new() { PlayerId = 3, PlayerName = "John Doe", LoginId = "login1", ELO = 2000 },
                new() { PlayerId = 4, PlayerName = "Johnathan Doe", LoginId = "login2" },
                new() { PlayerId = 5, PlayerName = "Johnny Dough", LoginId = "login3" },
                new() { PlayerId = 6, PlayerName = "Jon Doe", LoginId = "login4" },
                new() { PlayerId = 7, PlayerName = "Johannes Doe", LoginId = "login5" },
                new() { PlayerId = 8, PlayerName = "John D.", LoginId = "login6", ELO = 750 },
                new() { PlayerId = 9, PlayerName = "Jonny Doe", LoginId = "login7" },
                new() { PlayerId = 10, PlayerName = "J. Doe", LoginId = "login8" },
                new() { PlayerId = 11, PlayerName = "John Do", LoginId = "login9" },
                new() { PlayerId = 12, PlayerName = "Jonathan Doe", LoginId = "login10" }
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
                new() { PlayerId = 1, MatchId = 1,  IsWinner = true},
                new() { PlayerId = 2, MatchId = 1, IsWinner = false},
                new() { PlayerId = 1, MatchId = 2, IsWinner = false},
                new() { PlayerId = 2, MatchId = 2,  IsWinner = true},
                new() { PlayerId = 1, MatchId = 3,  IsWinner = true},
                new() { PlayerId = 2, MatchId = 3,IsWinner = false},
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
