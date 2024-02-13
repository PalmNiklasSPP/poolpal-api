using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace poolpal_api.Migrations
{
    /// <inheritdoc />
    public partial class MatchUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "PlayerMatches",
                keyColumns: new[] { "MatchId", "PlayerId" },
                keyValues: new object[] { 2, 1 },
                column: "IsWinner",
                value: false);

            migrationBuilder.UpdateData(
                table: "PlayerMatches",
                keyColumns: new[] { "MatchId", "PlayerId" },
                keyValues: new object[] { 2, 2 },
                column: "IsWinner",
                value: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "PlayerMatches",
                keyColumns: new[] { "MatchId", "PlayerId" },
                keyValues: new object[] { 2, 1 },
                column: "IsWinner",
                value: true);

            migrationBuilder.UpdateData(
                table: "PlayerMatches",
                keyColumns: new[] { "MatchId", "PlayerId" },
                keyValues: new object[] { 2, 2 },
                column: "IsWinner",
                value: false);
        }
    }
}
