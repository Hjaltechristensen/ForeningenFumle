using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ForeningenFumle.Server.Migrations
{
    /// <inheritdoc />
    public partial class modelsUpdated8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Registrations_Persons_MemberId",
                table: "Registrations");

            migrationBuilder.RenameColumn(
                name: "MemberId",
                table: "Registrations",
                newName: "PersonId1");

            migrationBuilder.RenameIndex(
                name: "IX_Registrations_MemberId",
                table: "Registrations",
                newName: "IX_Registrations_PersonId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Registrations_Persons_PersonId1",
                table: "Registrations",
                column: "PersonId1",
                principalTable: "Persons",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Registrations_Persons_PersonId1",
                table: "Registrations");

            migrationBuilder.RenameColumn(
                name: "PersonId1",
                table: "Registrations",
                newName: "MemberId");

            migrationBuilder.RenameIndex(
                name: "IX_Registrations_PersonId1",
                table: "Registrations",
                newName: "IX_Registrations_MemberId");

            migrationBuilder.AddForeignKey(
                name: "FK_Registrations_Persons_MemberId",
                table: "Registrations",
                column: "MemberId",
                principalTable: "Persons",
                principalColumn: "Id");
        }
    }
}
