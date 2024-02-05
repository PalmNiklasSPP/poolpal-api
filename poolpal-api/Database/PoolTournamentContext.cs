using Microsoft.EntityFrameworkCore;
using poolpal_api.Database.Entities;
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


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Tournament configuration
            modelBuilder.Entity<Tournament>()
                .HasKey(t => t.TournamentId);
            modelBuilder.Entity<Tournament>()
                .Property(t => t.Format)
                .HasConversion<string>();

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

            modelBuilder.Entity<LeaderboardEntry>().ToView("Leaderboard").HasNoKey();

            Seed.DoSeed(modelBuilder);
        }
    }

}
