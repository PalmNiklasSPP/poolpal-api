using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace poolpal_api.Migrations
{
    /// <inheritdoc />
    public partial class InitalSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Matches",
                columns: new[] { "MatchId", "MatchDate", "Notes", "PoolGameType", "TournamentId" },
                values: new object[] { 3, new DateTime(2024, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "EightBall", null });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "PlayerId", "ELO", "LoginId", "PlayerName" },
                values: new object[,]
                {
                    { 1, 1500, "STB\\NIPA01", "NickeP" },
                    { 2, 1500, "STB\\TIAL01", "Timmy" },
                    { 3, 2000, "login1", "John Doe" },
                    { 4, 950, "login2", "Johnathan Doe" },
                    { 5, 900, "login3", "Johnny Dough" },
                    { 6, 850, "login4", "Jon Doe" },
                    { 7, 800, "login5", "Johannes Doe" },
                    { 8, 750, "login6", "John D." },
                    { 9, 700, "login7", "Jonny Doe" },
                    { 10, 650, "login8", "J. Doe" },
                    { 11, 600, "login9", "John Do" },
                    { 12, 550, "login10", "Jonathan Doe" }
                });

            migrationBuilder.InsertData(
                table: "Tournaments",
                columns: new[] { "TournamentId", "EndDate", "Format", "IsTeamBased", "Name", "ParticipantLimit", "StartDate" },
                values: new object[] { 1, new DateTime(2021, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "SingleElimination", false, "Tournament 1", 10, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Matches",
                columns: new[] { "MatchId", "MatchDate", "Notes", "PoolGameType", "TournamentId" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "EightBall", 1 },
                    { 2, new DateTime(2024, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "EightBall", 1 }
                });

            migrationBuilder.InsertData(
                table: "PlayerMatches",
                columns: new[] { "MatchId", "PlayerId", "EloChange", "IsWinner", "Score" },
                values: new object[,]
                {
                    { 3, 1, 0, true, 0 },
                    { 3, 2, 0, false, 0 },
                    { 1, 1, 0, true, 0 },
                    { 2, 1, 0, true, 0 },
                    { 1, 2, 0, false, 0 },
                    { 2, 2, 0, false, 0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PlayerMatches",
                keyColumns: new[] { "MatchId", "PlayerId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "PlayerMatches",
                keyColumns: new[] { "MatchId", "PlayerId" },
                keyValues: new object[] { 2, 1 });

            migrationBuilder.DeleteData(
                table: "PlayerMatches",
                keyColumns: new[] { "MatchId", "PlayerId" },
                keyValues: new object[] { 3, 1 });

            migrationBuilder.DeleteData(
                table: "PlayerMatches",
                keyColumns: new[] { "MatchId", "PlayerId" },
                keyValues: new object[] { 1, 2 });

            migrationBuilder.DeleteData(
                table: "PlayerMatches",
                keyColumns: new[] { "MatchId", "PlayerId" },
                keyValues: new object[] { 2, 2 });

            migrationBuilder.DeleteData(
                table: "PlayerMatches",
                keyColumns: new[] { "MatchId", "PlayerId" },
                keyValues: new object[] { 3, 2 });

            migrationBuilder.DeleteData(
                table: "Players",
                keyColumn: "PlayerId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Players",
                keyColumn: "PlayerId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Players",
                keyColumn: "PlayerId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Players",
                keyColumn: "PlayerId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Players",
                keyColumn: "PlayerId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Players",
                keyColumn: "PlayerId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Players",
                keyColumn: "PlayerId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Players",
                keyColumn: "PlayerId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Players",
                keyColumn: "PlayerId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Players",
                keyColumn: "PlayerId",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Matches",
                keyColumn: "MatchId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Matches",
                keyColumn: "MatchId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Matches",
                keyColumn: "MatchId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Players",
                keyColumn: "PlayerId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Players",
                keyColumn: "PlayerId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Tournaments",
                keyColumn: "TournamentId",
                keyValue: 1);
        }
    }
}
