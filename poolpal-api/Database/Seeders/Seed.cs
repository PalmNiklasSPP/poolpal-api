using Microsoft.EntityFrameworkCore;
using poolpal_api.Database.Entities;
using poolpal_api.Database.Entities.Tournament;
using poolpal_api.Models;
using poolpal_api.Models.PoolTournamentApi.Models;

namespace poolpal_api.Database.Seeders
{
    public class Seed
    {
        private const string Desc =
            "Introducing NickeP, a master of precision and strategy on the pool table. Known for exceptional cue control and tactical gameplay, NickeP brings a unique blend of focus and flair to every match. With numerous victories under their belt, NickeP is a formidable opponent and a crowd favorite. Watch as NickeP lines up their shots, showcasing a perfect blend of skill and style.";

        private const string BoBDesc =
            "Meet BoB: Where Business meets Brokers and billiards balls meet pockets! We're a team of tech aces and poolside tacticians. Whether it's tackling complex algorithms or tricky bank shots, our approach is all about precision, power, and a bit of playful banter. Get ready, competitors! In the digital and felt world, we're here to rack up successes, and pocket victories with a smile.";
        public static void DoSeed(ModelBuilder mb)
        {
            //ORDER IS IMPORTANT HERE
            SeedSppTeam(mb);
            SeedPlayers(mb);
            SeedTournaments(mb);
            SeedMatches(mb);
            SeedPlayerMatches(mb);
            SeedGroups(mb);
            SeedTournamentRegistrations(mb);
        }

        private static void SeedSppTeam(ModelBuilder mb)
        {
            var data = new SppTeam[]
            {
                new() { Id = 1, ShortName = "BoB", FullName = "Business & Broker", OrganisationUnit = OrganisationUnit.Tech, Description = BoBDesc},
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
                new() { MatchId = 3, PoolGameType = PoolGameType.EightBall, MatchDate = new DateTime(2024, 1, 2)},

                // Additional Matches for Tournament 1
                new() { MatchId = 4, PoolGameType = PoolGameType.EightBall, TournamentId = 1, MatchDate = new DateTime(2024, 1, 3)},
                new() { MatchId = 5, PoolGameType = PoolGameType.EightBall, TournamentId = 1, MatchDate = new DateTime(2024, 1, 4)},
                // ... add more matches for Tournament 1

                // Matches for Tournament 2
                new() { MatchId = 6, PoolGameType = PoolGameType.EightBall, TournamentId = 2, MatchDate = new DateTime(2024, 2, 2)},
                new() { MatchId = 7, PoolGameType = PoolGameType.EightBall, TournamentId = 2, MatchDate = new DateTime(2024, 2, 3)},
                // ... add more matches for Tournament 2
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
            var data = new Tournament[]
            {
                new(TournamentStatus.Open) { TournamentId = 1, Name = "Tournament 1", Format = TournamentFormat.RoundRobin, StartDate = new DateTime(2024, 1, 1), EndDate = new DateTime(2024, 1, 7), ParticipantLimit = 10, IsTeamBased = false, OrganiserId = 1, Description = "Description for Tournament 1"},
                new(TournamentStatus.Open) { TournamentId = 2, Name = "Tournament 2", Format = TournamentFormat.SingleElimination, StartDate = new DateTime(2024, 2, 1), EndDate = new DateTime(2024, 2, 7), ParticipantLimit = 8, IsTeamBased = false, OrganiserId = 1, Description = "Description for Tournament 2"}
            };
            mb.Entity<Tournament>().HasData(data);
        }


        private static void SeedTournamentRegistrations(ModelBuilder mb)
        {
            var data = new TournamentRegistration[]
            {
                // Registrations for Tournament 1
                new() { RegistrationId = 1, TournamentId = 1, PlayerId = 1, Status = RegistrationStatus.Confirmed },
                new() { RegistrationId = 2, TournamentId = 1, PlayerId = 2, Status = RegistrationStatus.Confirmed },
                new() { RegistrationId = 3, TournamentId = 1, PlayerId = 3, Status = RegistrationStatus.Confirmed },
                new() { RegistrationId = 4, TournamentId = 1, PlayerId = 4, Status = RegistrationStatus.Pending },
                new() { RegistrationId = 5, TournamentId = 1, PlayerId = 6, Status = RegistrationStatus.Confirmed },
                new() { RegistrationId = 6, TournamentId = 1, PlayerId = 7, Status = RegistrationStatus.Confirmed },
                new() { RegistrationId = 7, TournamentId = 1, PlayerId = 8, Status = RegistrationStatus.Confirmed },
                new() { RegistrationId = 8, TournamentId = 1, PlayerId = 9, Status = RegistrationStatus.Confirmed },
                // ... add more registrations for Tournament 1

                // Registrations for Tournament 2
                new() { RegistrationId = 9, TournamentId = 2, PlayerId = 7, Status = RegistrationStatus.Confirmed },
                new() { RegistrationId = 10, TournamentId = 2, PlayerId = 8, Status = RegistrationStatus.Confirmed },
                // ... add more registrations for Tournament 2
            };
            mb.Entity<TournamentRegistration>().HasData(data);
        }

        private static void SeedGroups(ModelBuilder mb)
        {
            var data = new Group[]
            {
                // Groups for Tournament 1
                new() { GroupId = 1, TournamentId = 1, Name = "Group A" },
                new() { GroupId = 2, TournamentId = 1, Name = "Group B" },
                // Groups for Tournament 2
                new() { GroupId = 3, TournamentId = 2, Name = "Group C" },
                new() { GroupId = 4, TournamentId = 2, Name = "Group D" }
            };
            mb.Entity<Group>().HasData(data);
        }

    }
}
