﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using poolpal_api.Database;

#nullable disable

namespace poolpal_api.Migrations
{
    [DbContext(typeof(PoolPalDatabaseContext))]
    [Migration("20240212102348_InitalSeed")]
    partial class InitalSeed
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("poolpal_api.Database.Entities.LeaderboardEntry", b =>
                {
                    b.Property<int>("ELO")
                        .HasColumnType("int");

                    b.Property<int>("PlayerId")
                        .HasColumnType("int");

                    b.Property<string>("PlayerName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TotalLosses")
                        .HasColumnType("int");

                    b.Property<int>("TotalWins")
                        .HasColumnType("int");

                    b.ToTable((string)null);

                    b.ToView("Leaderboard", (string)null);
                });

            modelBuilder.Entity("poolpal_api.Database.Entities.Match", b =>
                {
                    b.Property<int>("MatchId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MatchId"));

                    b.Property<DateTime>("MatchDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Notes")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PoolGameType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("TournamentId")
                        .HasColumnType("int");

                    b.HasKey("MatchId");

                    b.HasIndex("TournamentId");

                    b.ToTable("Matches");

                    b.HasData(
                        new
                        {
                            MatchId = 1,
                            MatchDate = new DateTime(2024, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            PoolGameType = "EightBall",
                            TournamentId = 1
                        },
                        new
                        {
                            MatchId = 2,
                            MatchDate = new DateTime(2024, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            PoolGameType = "EightBall",
                            TournamentId = 1
                        },
                        new
                        {
                            MatchId = 3,
                            MatchDate = new DateTime(2024, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            PoolGameType = "EightBall"
                        });
                });

            modelBuilder.Entity("poolpal_api.Database.Entities.Player", b =>
                {
                    b.Property<int>("PlayerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PlayerId"));

                    b.Property<int>("ELO")
                        .HasColumnType("int");

                    b.Property<string>("LoginId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PlayerName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PlayerId");

                    b.ToTable("Players");

                    b.HasData(
                        new
                        {
                            PlayerId = 1,
                            ELO = 1500,
                            LoginId = "STB\\NIPA01",
                            PlayerName = "NickeP"
                        },
                        new
                        {
                            PlayerId = 2,
                            ELO = 1500,
                            LoginId = "STB\\TIAL01",
                            PlayerName = "Timmy"
                        },
                        new
                        {
                            PlayerId = 3,
                            ELO = 2000,
                            LoginId = "login1",
                            PlayerName = "John Doe"
                        },
                        new
                        {
                            PlayerId = 4,
                            ELO = 950,
                            LoginId = "login2",
                            PlayerName = "Johnathan Doe"
                        },
                        new
                        {
                            PlayerId = 5,
                            ELO = 900,
                            LoginId = "login3",
                            PlayerName = "Johnny Dough"
                        },
                        new
                        {
                            PlayerId = 6,
                            ELO = 850,
                            LoginId = "login4",
                            PlayerName = "Jon Doe"
                        },
                        new
                        {
                            PlayerId = 7,
                            ELO = 800,
                            LoginId = "login5",
                            PlayerName = "Johannes Doe"
                        },
                        new
                        {
                            PlayerId = 8,
                            ELO = 750,
                            LoginId = "login6",
                            PlayerName = "John D."
                        },
                        new
                        {
                            PlayerId = 9,
                            ELO = 700,
                            LoginId = "login7",
                            PlayerName = "Jonny Doe"
                        },
                        new
                        {
                            PlayerId = 10,
                            ELO = 650,
                            LoginId = "login8",
                            PlayerName = "J. Doe"
                        },
                        new
                        {
                            PlayerId = 11,
                            ELO = 600,
                            LoginId = "login9",
                            PlayerName = "John Do"
                        },
                        new
                        {
                            PlayerId = 12,
                            ELO = 550,
                            LoginId = "login10",
                            PlayerName = "Jonathan Doe"
                        });
                });

            modelBuilder.Entity("poolpal_api.Database.Entities.Tournament", b =>
                {
                    b.Property<int>("TournamentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TournamentId"));

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Format")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsTeamBased")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ParticipantLimit")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.HasKey("TournamentId");

                    b.ToTable("Tournaments");

                    b.HasData(
                        new
                        {
                            TournamentId = 1,
                            EndDate = new DateTime(2021, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Format = "SingleElimination",
                            IsTeamBased = false,
                            Name = "Tournament 1",
                            ParticipantLimit = 10,
                            StartDate = new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("poolpal_api.Models.PoolTournamentApi.Models.PlayerMatch", b =>
                {
                    b.Property<int>("PlayerId")
                        .HasColumnType("int");

                    b.Property<int>("MatchId")
                        .HasColumnType("int");

                    b.Property<int>("EloChange")
                        .HasColumnType("int");

                    b.Property<bool>("IsWinner")
                        .HasColumnType("bit");

                    b.Property<int>("Score")
                        .HasColumnType("int");

                    b.HasKey("PlayerId", "MatchId");

                    b.HasIndex("MatchId");

                    b.ToTable("PlayerMatches");

                    b.HasData(
                        new
                        {
                            PlayerId = 1,
                            MatchId = 1,
                            EloChange = 0,
                            IsWinner = true,
                            Score = 0
                        },
                        new
                        {
                            PlayerId = 2,
                            MatchId = 1,
                            EloChange = 0,
                            IsWinner = false,
                            Score = 0
                        },
                        new
                        {
                            PlayerId = 1,
                            MatchId = 2,
                            EloChange = 0,
                            IsWinner = true,
                            Score = 0
                        },
                        new
                        {
                            PlayerId = 2,
                            MatchId = 2,
                            EloChange = 0,
                            IsWinner = false,
                            Score = 0
                        },
                        new
                        {
                            PlayerId = 1,
                            MatchId = 3,
                            EloChange = 0,
                            IsWinner = true,
                            Score = 0
                        },
                        new
                        {
                            PlayerId = 2,
                            MatchId = 3,
                            EloChange = 0,
                            IsWinner = false,
                            Score = 0
                        });
                });

            modelBuilder.Entity("poolpal_api.Database.Entities.Match", b =>
                {
                    b.HasOne("poolpal_api.Database.Entities.Tournament", "Tournament")
                        .WithMany("Matches")
                        .HasForeignKey("TournamentId");

                    b.Navigation("Tournament");
                });

            modelBuilder.Entity("poolpal_api.Models.PoolTournamentApi.Models.PlayerMatch", b =>
                {
                    b.HasOne("poolpal_api.Database.Entities.Match", "Match")
                        .WithMany("PlayerMatches")
                        .HasForeignKey("MatchId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("poolpal_api.Database.Entities.Player", "Player")
                        .WithMany("PlayerMatches")
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Match");

                    b.Navigation("Player");
                });

            modelBuilder.Entity("poolpal_api.Database.Entities.Match", b =>
                {
                    b.Navigation("PlayerMatches");
                });

            modelBuilder.Entity("poolpal_api.Database.Entities.Player", b =>
                {
                    b.Navigation("PlayerMatches");
                });

            modelBuilder.Entity("poolpal_api.Database.Entities.Tournament", b =>
                {
                    b.Navigation("Matches");
                });
#pragma warning restore 612, 618
        }
    }
}
