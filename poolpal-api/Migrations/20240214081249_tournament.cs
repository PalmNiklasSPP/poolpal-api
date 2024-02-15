using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace poolpal_api.Migrations
{
    /// <inheritdoc />
    public partial class tournament : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Tournaments_TournamentId",
                table: "Matches");

            migrationBuilder.AddColumn<string>(
                name: "GameType",
                table: "Tournaments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ParticipationType",
                table: "Tournaments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "GroupId",
                table: "Matches",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Group",
                columns: table => new
                {
                    GroupId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TournamentId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Group", x => x.GroupId);
                    table.ForeignKey(
                        name: "FK_Group_Tournaments_TournamentId",
                        column: x => x.TournamentId,
                        principalTable: "Tournaments",
                        principalColumn: "TournamentId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TournamentRegistration",
                columns: table => new
                {
                    RegistrationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TournamentId = table.Column<int>(type: "int", nullable: false),
                    GroupId = table.Column<int>(type: "int", nullable: true),
                    PlayerId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TournamentRegistration", x => x.RegistrationId);
                    table.ForeignKey(
                        name: "FK_TournamentRegistration_Group_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Group",
                        principalColumn: "GroupId");
                    table.ForeignKey(
                        name: "FK_TournamentRegistration_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "PlayerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TournamentRegistration_Tournaments_TournamentId",
                        column: x => x.TournamentId,
                        principalTable: "Tournaments",
                        principalColumn: "TournamentId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Group",
                columns: new[] { "GroupId", "Name", "TournamentId" },
                values: new object[,]
                {
                    { 1, "Group A", 1 },
                    { 2, "Group B", 1 }
                });

            migrationBuilder.UpdateData(
                table: "Matches",
                keyColumn: "MatchId",
                keyValue: 1,
                column: "GroupId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Matches",
                keyColumn: "MatchId",
                keyValue: 2,
                column: "GroupId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Matches",
                keyColumn: "MatchId",
                keyValue: 3,
                column: "GroupId",
                value: null);

            migrationBuilder.InsertData(
                table: "Matches",
                columns: new[] { "MatchId", "GroupId", "MatchDate", "Notes", "PoolGameType", "TournamentId" },
                values: new object[,]
                {
                    { 4, null, new DateTime(2024, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "EightBall", 1 },
                    { 5, null, new DateTime(2024, 1, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "EightBall", 1 }
                });

            migrationBuilder.InsertData(
                table: "TournamentRegistration",
                columns: new[] { "RegistrationId", "GroupId", "PlayerId", "Status", "TournamentId" },
                values: new object[,]
                {
                    { 1, null, 1, "Confirmed", 1 },
                    { 2, null, 2, "Confirmed", 1 }
                });

            migrationBuilder.UpdateData(
                table: "Tournaments",
                keyColumn: "TournamentId",
                keyValue: 1,
                columns: new[] { "EndDate", "Format", "GameType", "ParticipationType" },
                values: new object[] { new DateTime(2024, 1, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "RoundRobin", "EightBall", 0 });

            migrationBuilder.InsertData(
                table: "Tournaments",
                columns: new[] { "TournamentId", "EndDate", "Format", "GameType", "IsTeamBased", "Name", "ParticipantLimit", "ParticipationType", "StartDate" },
                values: new object[] { 2, new DateTime(2024, 2, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "SingleElimination", "EightBall", false, "Tournament 2", 8, 0, new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Group",
                columns: new[] { "GroupId", "Name", "TournamentId" },
                values: new object[,]
                {
                    { 3, "Group C", 2 },
                    { 4, "Group D", 2 }
                });

            migrationBuilder.InsertData(
                table: "Matches",
                columns: new[] { "MatchId", "GroupId", "MatchDate", "Notes", "PoolGameType", "TournamentId" },
                values: new object[,]
                {
                    { 6, null, new DateTime(2024, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "EightBall", 2 },
                    { 7, null, new DateTime(2024, 2, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "EightBall", 2 }
                });

            migrationBuilder.InsertData(
                table: "TournamentRegistration",
                columns: new[] { "RegistrationId", "GroupId", "PlayerId", "Status", "TournamentId" },
                values: new object[,]
                {
                    { 7, null, 7, "Confirmed", 2 },
                    { 8, null, 8, "Confirmed", 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Matches_GroupId",
                table: "Matches",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Group_TournamentId",
                table: "Group",
                column: "TournamentId");

            migrationBuilder.CreateIndex(
                name: "IX_TournamentRegistration_GroupId",
                table: "TournamentRegistration",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_TournamentRegistration_PlayerId",
                table: "TournamentRegistration",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_TournamentRegistration_TournamentId",
                table: "TournamentRegistration",
                column: "TournamentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Group_GroupId",
                table: "Matches",
                column: "GroupId",
                principalTable: "Group",
                principalColumn: "GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Tournaments_TournamentId",
                table: "Matches",
                column: "TournamentId",
                principalTable: "Tournaments",
                principalColumn: "TournamentId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Group_GroupId",
                table: "Matches");

            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Tournaments_TournamentId",
                table: "Matches");

            migrationBuilder.DropTable(
                name: "TournamentRegistration");

            migrationBuilder.DropTable(
                name: "Group");

            migrationBuilder.DropIndex(
                name: "IX_Matches_GroupId",
                table: "Matches");

            migrationBuilder.DeleteData(
                table: "Matches",
                keyColumn: "MatchId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Matches",
                keyColumn: "MatchId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Matches",
                keyColumn: "MatchId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Matches",
                keyColumn: "MatchId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Tournaments",
                keyColumn: "TournamentId",
                keyValue: 2);

            migrationBuilder.DropColumn(
                name: "GameType",
                table: "Tournaments");

            migrationBuilder.DropColumn(
                name: "ParticipationType",
                table: "Tournaments");

            migrationBuilder.DropColumn(
                name: "GroupId",
                table: "Matches");

            migrationBuilder.UpdateData(
                table: "Tournaments",
                keyColumn: "TournamentId",
                keyValue: 1,
                columns: new[] { "EndDate", "Format" },
                values: new object[] { new DateTime(2021, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "SingleElimination" });

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Tournaments_TournamentId",
                table: "Matches",
                column: "TournamentId",
                principalTable: "Tournaments",
                principalColumn: "TournamentId");
        }
    }
}
