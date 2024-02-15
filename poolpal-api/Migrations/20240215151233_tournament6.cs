using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace poolpal_api.Migrations
{
    /// <inheritdoc />
    public partial class tournament6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlayerMatches_Matches_MatchId",
                table: "PlayerMatches");

            migrationBuilder.DropForeignKey(
                name: "FK_Tournaments_Players_OrganiserId",
                table: "Tournaments");

            migrationBuilder.AlterColumn<int>(
                name: "OrganiserId",
                table: "Tournaments",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Tournaments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<DateTime>(
                name: "MatchDate",
                table: "Matches",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<bool>(
                name: "HasBeenPlayed",
                table: "Matches",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Matches",
                keyColumn: "MatchId",
                keyValue: 1,
                column: "HasBeenPlayed",
                value: false);

            migrationBuilder.UpdateData(
                table: "Matches",
                keyColumn: "MatchId",
                keyValue: 2,
                column: "HasBeenPlayed",
                value: false);

            migrationBuilder.UpdateData(
                table: "Matches",
                keyColumn: "MatchId",
                keyValue: 3,
                column: "HasBeenPlayed",
                value: false);

            migrationBuilder.UpdateData(
                table: "Matches",
                keyColumn: "MatchId",
                keyValue: 4,
                column: "HasBeenPlayed",
                value: false);

            migrationBuilder.UpdateData(
                table: "Matches",
                keyColumn: "MatchId",
                keyValue: 5,
                column: "HasBeenPlayed",
                value: false);

            migrationBuilder.UpdateData(
                table: "Matches",
                keyColumn: "MatchId",
                keyValue: 6,
                column: "HasBeenPlayed",
                value: false);

            migrationBuilder.UpdateData(
                table: "Matches",
                keyColumn: "MatchId",
                keyValue: 7,
                column: "HasBeenPlayed",
                value: false);

            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "PlayerId",
                keyValue: 1,
                column: "Avatar",
                value: "/static/avatars/avatar-default.webp");

            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "PlayerId",
                keyValue: 2,
                column: "Avatar",
                value: "/static/avatars/avatar-default.webp");

            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "PlayerId",
                keyValue: 3,
                column: "Avatar",
                value: "/static/avatars/avatar-default.webp");

            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "PlayerId",
                keyValue: 4,
                column: "Avatar",
                value: "/static/avatars/avatar-default.webp");

            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "PlayerId",
                keyValue: 5,
                column: "Avatar",
                value: "/static/avatars/avatar-default.webp");

            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "PlayerId",
                keyValue: 6,
                column: "Avatar",
                value: "/static/avatars/avatar-default.webp");

            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "PlayerId",
                keyValue: 7,
                column: "Avatar",
                value: "/static/avatars/avatar-default.webp");

            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "PlayerId",
                keyValue: 8,
                column: "Avatar",
                value: "/static/avatars/avatar-default.webp");

            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "PlayerId",
                keyValue: 9,
                column: "Avatar",
                value: "/static/avatars/avatar-default.webp");

            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "PlayerId",
                keyValue: 10,
                column: "Avatar",
                value: "/static/avatars/avatar-default.webp");

            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "PlayerId",
                keyValue: 11,
                column: "Avatar",
                value: "/static/avatars/avatar-default.webp");

            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "PlayerId",
                keyValue: 12,
                column: "Avatar",
                value: "/static/avatars/avatar-default.webp");

            migrationBuilder.UpdateData(
                table: "SppTeams",
                keyColumn: "Id",
                keyValue: 1,
                column: "Description",
                value: "Meet BoB: Where Business meets Brokers and billiards balls meet pockets! We're a team of tech aces and poolside tacticians. Whether it's tackling complex algorithms or tricky bank shots, our approach is all about precision, power, and a bit of playful banter. Get ready, competitors! In the digital and felt world, we're here to rack up successes, and pocket victories with a smile.");

            migrationBuilder.UpdateData(
                table: "Tournaments",
                keyColumn: "TournamentId",
                keyValue: 1,
                column: "Status",
                value: "Open");

            migrationBuilder.UpdateData(
                table: "Tournaments",
                keyColumn: "TournamentId",
                keyValue: 2,
                column: "Status",
                value: "Open");

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerMatches_Matches_MatchId",
                table: "PlayerMatches",
                column: "MatchId",
                principalTable: "Matches",
                principalColumn: "MatchId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tournaments_Players_OrganiserId",
                table: "Tournaments",
                column: "OrganiserId",
                principalTable: "Players",
                principalColumn: "PlayerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlayerMatches_Matches_MatchId",
                table: "PlayerMatches");

            migrationBuilder.DropForeignKey(
                name: "FK_Tournaments_Players_OrganiserId",
                table: "Tournaments");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Tournaments");

            migrationBuilder.DropColumn(
                name: "HasBeenPlayed",
                table: "Matches");

            migrationBuilder.AlterColumn<int>(
                name: "OrganiserId",
                table: "Tournaments",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "MatchDate",
                table: "Matches",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "PlayerId",
                keyValue: 1,
                column: "Avatar",
                value: null);

            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "PlayerId",
                keyValue: 2,
                column: "Avatar",
                value: null);

            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "PlayerId",
                keyValue: 3,
                column: "Avatar",
                value: null);

            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "PlayerId",
                keyValue: 4,
                column: "Avatar",
                value: null);

            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "PlayerId",
                keyValue: 5,
                column: "Avatar",
                value: null);

            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "PlayerId",
                keyValue: 6,
                column: "Avatar",
                value: null);

            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "PlayerId",
                keyValue: 7,
                column: "Avatar",
                value: null);

            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "PlayerId",
                keyValue: 8,
                column: "Avatar",
                value: null);

            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "PlayerId",
                keyValue: 9,
                column: "Avatar",
                value: null);

            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "PlayerId",
                keyValue: 10,
                column: "Avatar",
                value: null);

            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "PlayerId",
                keyValue: 11,
                column: "Avatar",
                value: null);

            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "PlayerId",
                keyValue: 12,
                column: "Avatar",
                value: null);

            migrationBuilder.UpdateData(
                table: "SppTeams",
                keyColumn: "Id",
                keyValue: 1,
                column: "Description",
                value: null);

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerMatches_Matches_MatchId",
                table: "PlayerMatches",
                column: "MatchId",
                principalTable: "Matches",
                principalColumn: "MatchId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tournaments_Players_OrganiserId",
                table: "Tournaments",
                column: "OrganiserId",
                principalTable: "Players",
                principalColumn: "PlayerId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
