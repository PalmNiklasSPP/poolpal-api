using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace poolpal_api.Migrations
{
    /// <inheritdoc />
    public partial class SppTeam : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Players",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SppTeamId",
                table: "Players",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SppTeams",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShortName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrganisationUnit = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SppTeams", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "PlayerId",
                keyValue: 1,
                columns: new[] { "Description", "SppTeamId" },
                values: new object[] { "Introducing NickeP, a master of precision and strategy on the pool table. Known for exceptional cue control and tactical gameplay, NickeP brings a unique blend of focus and flair to every match. With numerous victories under their belt, NickeP is a formidable opponent and a crowd favorite. Watch as NickeP lines up their shots, showcasing a perfect blend of skill and style.", 1 });

            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "PlayerId",
                keyValue: 2,
                columns: new[] { "Description", "SppTeamId" },
                values: new object[] { null, 1 });

            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "PlayerId",
                keyValue: 3,
                columns: new[] { "Description", "SppTeamId" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "PlayerId",
                keyValue: 4,
                columns: new[] { "Description", "SppTeamId" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "PlayerId",
                keyValue: 5,
                columns: new[] { "Description", "SppTeamId" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "PlayerId",
                keyValue: 6,
                columns: new[] { "Description", "SppTeamId" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "PlayerId",
                keyValue: 7,
                columns: new[] { "Description", "SppTeamId" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "PlayerId",
                keyValue: 8,
                columns: new[] { "Description", "SppTeamId" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "PlayerId",
                keyValue: 9,
                columns: new[] { "Description", "SppTeamId" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "PlayerId",
                keyValue: 10,
                columns: new[] { "Description", "SppTeamId" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "PlayerId",
                keyValue: 11,
                columns: new[] { "Description", "SppTeamId" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "PlayerId",
                keyValue: 12,
                columns: new[] { "Description", "SppTeamId" },
                values: new object[] { null, null });

            migrationBuilder.InsertData(
                table: "SppTeams",
                columns: new[] { "Id", "Description", "FullName", "OrganisationUnit", "ShortName" },
                values: new object[,]
                {
                    { 1, null, "Business & Broker", "Tech", "BoB" },
                    { 2, null, "Private Web", "Tech", "PW" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Players_SppTeamId",
                table: "Players",
                column: "SppTeamId");

            migrationBuilder.AddForeignKey(
                name: "FK_Players_SppTeams_SppTeamId",
                table: "Players",
                column: "SppTeamId",
                principalTable: "SppTeams",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Players_SppTeams_SppTeamId",
                table: "Players");

            migrationBuilder.DropTable(
                name: "SppTeams");

            migrationBuilder.DropIndex(
                name: "IX_Players_SppTeamId",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "SppTeamId",
                table: "Players");
        }
    }
}
