using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ForeningenFumle.Server.Migrations
{
    /// <inheritdoc />
    public partial class modelsUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Registrations_Person_MemberId",
                table: "Registrations");

            migrationBuilder.DropForeignKey(
                name: "FK_Registrations_Person_PersonId",
                table: "Registrations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Person",
                table: "Person");

            migrationBuilder.RenameTable(
                name: "Person",
                newName: "Persons");

            migrationBuilder.RenameIndex(
                name: "IX_Person_Phonenumber",
                table: "Persons",
                newName: "IX_Persons_Phonenumber");

            migrationBuilder.RenameIndex(
                name: "IX_Person_Email",
                table: "Persons",
                newName: "IX_Persons_Email");

            migrationBuilder.AddColumn<string>(
                name: "PasswordHash",
                table: "Persons",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "Persons",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Persons",
                table: "Persons",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Persons_Username",
                table: "Persons",
                column: "Username",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Registrations_Persons_MemberId",
                table: "Registrations",
                column: "MemberId",
                principalTable: "Persons",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Registrations_Persons_PersonId",
                table: "Registrations",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Registrations_Persons_MemberId",
                table: "Registrations");

            migrationBuilder.DropForeignKey(
                name: "FK_Registrations_Persons_PersonId",
                table: "Registrations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Persons",
                table: "Persons");

            migrationBuilder.DropIndex(
                name: "IX_Persons_Username",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "Username",
                table: "Persons");

            migrationBuilder.RenameTable(
                name: "Persons",
                newName: "Person");

            migrationBuilder.RenameIndex(
                name: "IX_Persons_Phonenumber",
                table: "Person",
                newName: "IX_Person_Phonenumber");

            migrationBuilder.RenameIndex(
                name: "IX_Persons_Email",
                table: "Person",
                newName: "IX_Person_Email");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Person",
                table: "Person",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Registrations_Person_MemberId",
                table: "Registrations",
                column: "MemberId",
                principalTable: "Person",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Registrations_Person_PersonId",
                table: "Registrations",
                column: "PersonId",
                principalTable: "Person",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
