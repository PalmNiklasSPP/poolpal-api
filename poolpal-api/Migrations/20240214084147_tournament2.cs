using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace poolpal_api.Migrations
{
    /// <inheritdoc />
    public partial class tournament2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "TournamentRegistrations",
                keyColumn: "RegistrationId",
                keyValue: 7,
                columns: new[] { "PlayerId", "TournamentId" },
                values: new object[] { 8, 1 });

            migrationBuilder.UpdateData(
                table: "TournamentRegistrations",
                keyColumn: "RegistrationId",
                keyValue: 8,
                columns: new[] { "PlayerId", "TournamentId" },
                values: new object[] { 9, 1 });

            migrationBuilder.InsertData(
                table: "TournamentRegistrations",
                columns: new[] { "RegistrationId", "GroupId", "PlayerId", "Status", "TournamentId" },
                values: new object[,]
                {
                    { 3, null, 3, "Confirmed", 1 },
                    { 4, null, 4, "Pending", 1 },
                    { 5, null, 6, "Confirmed", 1 },
                    { 6, null, 7, "Confirmed", 1 },
                    { 9, null, 7, "Confirmed", 2 },
                    { 10, null, 8, "Confirmed", 2 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TournamentRegistrations",
                keyColumn: "RegistrationId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "TournamentRegistrations",
                keyColumn: "RegistrationId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "TournamentRegistrations",
                keyColumn: "RegistrationId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "TournamentRegistrations",
                keyColumn: "RegistrationId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "TournamentRegistrations",
                keyColumn: "RegistrationId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "TournamentRegistrations",
                keyColumn: "RegistrationId",
                keyValue: 10);

            migrationBuilder.UpdateData(
                table: "TournamentRegistrations",
                keyColumn: "RegistrationId",
                keyValue: 7,
                columns: new[] { "PlayerId", "TournamentId" },
                values: new object[] { 7, 2 });

            migrationBuilder.UpdateData(
                table: "TournamentRegistrations",
                keyColumn: "RegistrationId",
                keyValue: 8,
                columns: new[] { "PlayerId", "TournamentId" },
                values: new object[] { 8, 2 });
        }
    }
}
