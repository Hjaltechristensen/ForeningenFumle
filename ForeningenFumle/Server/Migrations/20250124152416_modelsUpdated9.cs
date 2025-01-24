using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ForeningenFumle.Server.Migrations
{
    /// <inheritdoc />
    public partial class modelsUpdated9 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Registrations_Persons_PersonId1",
                table: "Registrations");

            migrationBuilder.DropIndex(
                name: "IX_Registrations_PersonId1",
                table: "Registrations");

            migrationBuilder.DropColumn(
                name: "PersonId1",
                table: "Registrations");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PersonId1",
                table: "Registrations",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Registrations_PersonId1",
                table: "Registrations",
                column: "PersonId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Registrations_Persons_PersonId1",
                table: "Registrations",
                column: "PersonId1",
                principalTable: "Persons",
                principalColumn: "Id");
        }
    }
}
