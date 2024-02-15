using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace poolpal_api.Migrations
{
    /// <inheritdoc />
    public partial class tournament4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ParticipationType",
                table: "Tournaments",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Tournaments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrganiserId",
                table: "Tournaments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Tournaments",
                keyColumn: "TournamentId",
                keyValue: 1,
                columns: new[] { "Description", "OrganiserId", "ParticipationType" },
                values: new object[] { "Description for Tournament 1", 1, "Open" });

            migrationBuilder.UpdateData(
                table: "Tournaments",
                keyColumn: "TournamentId",
                keyValue: 2,
                columns: new[] { "Description", "OrganiserId", "ParticipationType" },
                values: new object[] { "Description for Tournament 2", 1, "Open" });

            migrationBuilder.CreateIndex(
                name: "IX_Tournaments_OrganiserId",
                table: "Tournaments",
                column: "OrganiserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tournaments_Players_OrganiserId",
                table: "Tournaments",
                column: "OrganiserId",
                principalTable: "Players",
                principalColumn: "PlayerId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tournaments_Players_OrganiserId",
                table: "Tournaments");

            migrationBuilder.DropIndex(
                name: "IX_Tournaments_OrganiserId",
                table: "Tournaments");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Tournaments");

            migrationBuilder.DropColumn(
                name: "OrganiserId",
                table: "Tournaments");

            migrationBuilder.AlterColumn<int>(
                name: "ParticipationType",
                table: "Tournaments",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "Tournaments",
                keyColumn: "TournamentId",
                keyValue: 1,
                column: "ParticipationType",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Tournaments",
                keyColumn: "TournamentId",
                keyValue: 2,
                column: "ParticipationType",
                value: 0);
        }
    }
}
