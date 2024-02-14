using Microsoft.EntityFrameworkCore;
using poolpal_api.Database.Entities;
using poolpal_api.Database.Entities.Tournament;
using poolpal_api.Database.Seeders;
using poolpal_api.Models.PoolTournamentApi.Models;

namespace poolpal_api.Database
{

    public class PoolTournamentContext : DbContext
    {
        public PoolTournamentContext(DbContextOptions<PoolTournamentContext> options)
        : base(options)
        {
        }

        public DbSet<Tournament> Tournaments { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<PlayerMatch> PlayerMatches { get; set; }
        public DbSet<LeaderboardEntry> LeaderboardEntries { get; set; }
        public DbSet<SppTeam> SppTeams { get; set; }
        public DbSet<TournamentRegistration> TournamentRegistrations { get; set; }
        public DbSet<Group> Groups { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Tournament to Groups relationship
            modelBuilder.Entity<Tournament>()
                .HasMany(t => t.Groups)
                .WithOne(g => g.Tournament)
                .HasForeignKey(g => g.TournamentId)
                .OnDelete(DeleteBehavior.Cascade);

            // Tournament to TournamentRegistrations relationship
            modelBuilder.Entity<Tournament>()
                .HasMany(t => t.Registrations)
                .WithOne(r => r.Tournament)
                .HasForeignKey(r => r.TournamentId)
                .OnDelete(DeleteBehavior.Cascade);

            // Tournament to Matches relationship
            modelBuilder.Entity<Tournament>()
                .HasMany(t => t.Matches)
                .WithOne(m => m.Tournament)
                .HasForeignKey(m => m.TournamentId)
                .OnDelete(DeleteBehavior.Restrict); // Set to Restrict to prevent cascade

            modelBuilder.Entity<Tournament>()
                .Property(t => t.Format)
                .HasConversion<string>();
            modelBuilder.Entity<Tournament>()
                .Property(t => t.GameType)
                .HasConversion<string>();
            modelBuilder.Entity<Tournament>()
                .Property(t => t.ParticipationType)
                .HasConversion<string>();

            modelBuilder.Entity<TournamentRegistration>()
                .Property(t => t.Status)
                .HasConversion<string>();

            modelBuilder.Entity<Group>()
                .HasOne(g => g.Tournament)
                .WithMany(t => t.Groups)
                .HasForeignKey(g => g.TournamentId)
                .OnDelete(DeleteBehavior.Restrict); // Preventing reverse cascading

            modelBuilder.Entity<TournamentRegistration>()
                .HasOne(r => r.Tournament)
                .WithMany(t => t.Registrations)
                .HasForeignKey(r => r.TournamentId)
                .OnDelete(DeleteBehavior.Restrict); // Preventing reverse cascading

            modelBuilder.Entity<Tournament>().HasKey(x => x.TournamentId);
            modelBuilder.Entity<TournamentRegistration>().HasKey(x => x.RegistrationId);
            modelBuilder.Entity<Group>().HasKey(x => x.GroupId);

            // Match configuration
            modelBuilder.Entity<Match>()
                .HasKey(m => m.MatchId);
            modelBuilder.Entity<Match>()
               .Property(m => m.PoolGameType)
               .HasConversion<string>();

            // Player configuration
            modelBuilder.Entity<Player>()
                .HasKey(p => p.PlayerId);
            // Add more configurations as needed

            // PlayerMatch (many-to-many) configuration
            modelBuilder.Entity<PlayerMatch>()
                .HasKey(pm => new { pm.PlayerId, pm.MatchId });

            modelBuilder.Entity<PlayerMatch>()
                .HasOne(pm => pm.Player)
                .WithMany(p => p.PlayerMatches)
                .HasForeignKey(pm => pm.PlayerId);

            modelBuilder.Entity<PlayerMatch>()
                .HasOne(pm => pm.Match)
                .WithMany(m => m.PlayerMatches)
                .HasForeignKey(pm => pm.MatchId);

            modelBuilder.Entity<SppTeam>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<SppTeam>()
                .Property(x => x.OrganisationUnit)
                .HasConversion<string>();



            modelBuilder.Entity<LeaderboardEntry>().ToView("Leaderboard").HasNoKey();

            Seed.DoSeed(modelBuilder);
        }
    }

}
