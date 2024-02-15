using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace poolpal_api.Migrations
{
    /// <inheritdoc />
    public partial class tournament1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Group_Tournaments_TournamentId",
                table: "Group");

            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Group_GroupId",
                table: "Matches");

            migrationBuilder.DropForeignKey(
                name: "FK_TournamentRegistration_Group_GroupId",
                table: "TournamentRegistration");

            migrationBuilder.DropForeignKey(
                name: "FK_TournamentRegistration_Players_PlayerId",
                table: "TournamentRegistration");

            migrationBuilder.DropForeignKey(
                name: "FK_TournamentRegistration_Tournaments_TournamentId",
                table: "TournamentRegistration");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TournamentRegistration",
                table: "TournamentRegistration");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Group",
                table: "Group");

            migrationBuilder.RenameTable(
                name: "TournamentRegistration",
                newName: "TournamentRegistrations");

            migrationBuilder.RenameTable(
                name: "Group",
                newName: "Groups");

            migrationBuilder.RenameIndex(
                name: "IX_TournamentRegistration_TournamentId",
                table: "TournamentRegistrations",
                newName: "IX_TournamentRegistrations_TournamentId");

            migrationBuilder.RenameIndex(
                name: "IX_TournamentRegistration_PlayerId",
                table: "TournamentRegistrations",
                newName: "IX_TournamentRegistrations_PlayerId");

            migrationBuilder.RenameIndex(
                name: "IX_TournamentRegistration_GroupId",
                table: "TournamentRegistrations",
                newName: "IX_TournamentRegistrations_GroupId");

            migrationBuilder.RenameIndex(
                name: "IX_Group_TournamentId",
                table: "Groups",
                newName: "IX_Groups_TournamentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TournamentRegistrations",
                table: "TournamentRegistrations",
                column: "RegistrationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Groups",
                table: "Groups",
                column: "GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_Tournaments_TournamentId",
                table: "Groups",
                column: "TournamentId",
                principalTable: "Tournaments",
                principalColumn: "TournamentId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Groups_GroupId",
                table: "Matches",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_TournamentRegistrations_Groups_GroupId",
                table: "TournamentRegistrations",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_TournamentRegistrations_Players_PlayerId",
                table: "TournamentRegistrations",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "PlayerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TournamentRegistrations_Tournaments_TournamentId",
                table: "TournamentRegistrations",
                column: "TournamentId",
                principalTable: "Tournaments",
                principalColumn: "TournamentId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Groups_Tournaments_TournamentId",
                table: "Groups");

            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Groups_GroupId",
                table: "Matches");

            migrationBuilder.DropForeignKey(
                name: "FK_TournamentRegistrations_Groups_GroupId",
                table: "TournamentRegistrations");

            migrationBuilder.DropForeignKey(
                name: "FK_TournamentRegistrations_Players_PlayerId",
                table: "TournamentRegistrations");

            migrationBuilder.DropForeignKey(
                name: "FK_TournamentRegistrations_Tournaments_TournamentId",
                table: "TournamentRegistrations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TournamentRegistrations",
                table: "TournamentRegistrations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Groups",
                table: "Groups");

            migrationBuilder.RenameTable(
                name: "TournamentRegistrations",
                newName: "TournamentRegistration");

            migrationBuilder.RenameTable(
                name: "Groups",
                newName: "Group");

            migrationBuilder.RenameIndex(
                name: "IX_TournamentRegistrations_TournamentId",
                table: "TournamentRegistration",
                newName: "IX_TournamentRegistration_TournamentId");

            migrationBuilder.RenameIndex(
                name: "IX_TournamentRegistrations_PlayerId",
                table: "TournamentRegistration",
                newName: "IX_TournamentRegistration_PlayerId");

            migrationBuilder.RenameIndex(
                name: "IX_TournamentRegistrations_GroupId",
                table: "TournamentRegistration",
                newName: "IX_TournamentRegistration_GroupId");

            migrationBuilder.RenameIndex(
                name: "IX_Groups_TournamentId",
                table: "Group",
                newName: "IX_Group_TournamentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TournamentRegistration",
                table: "TournamentRegistration",
                column: "RegistrationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Group",
                table: "Group",
                column: "GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Group_Tournaments_TournamentId",
                table: "Group",
                column: "TournamentId",
                principalTable: "Tournaments",
                principalColumn: "TournamentId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Group_GroupId",
                table: "Matches",
                column: "GroupId",
                principalTable: "Group",
                principalColumn: "GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_TournamentRegistration_Group_GroupId",
                table: "TournamentRegistration",
                column: "GroupId",
                principalTable: "Group",
                principalColumn: "GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_TournamentRegistration_Players_PlayerId",
                table: "TournamentRegistration",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "PlayerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TournamentRegistration_Tournaments_TournamentId",
                table: "TournamentRegistration",
                column: "TournamentId",
                principalTable: "Tournaments",
                principalColumn: "TournamentId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
