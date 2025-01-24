using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ForeningenFumle.Server.Migrations
{
    /// <inheritdoc />
    public partial class modelsUpdated5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Persons_Email",
                table: "Persons");

            migrationBuilder.DropIndex(
                name: "IX_Persons_Phonenumber",
                table: "Persons");

            migrationBuilder.DropIndex(
                name: "IX_Persons_Username",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "Rolle",
                table: "Persons");

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "Persons",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Phonenumber",
                table: "Persons",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Persons",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Persons",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "Persons",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Persons_Email",
                table: "Persons",
                column: "Email",
                unique: true,
                filter: "[Email] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Persons_Phonenumber",
                table: "Persons",
                column: "Phonenumber",
                unique: true,
                filter: "[Phonenumber] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Persons_Username",
                table: "Persons",
                column: "Username",
                unique: true,
                filter: "[Username] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Persons_Email",
                table: "Persons");

            migrationBuilder.DropIndex(
                name: "IX_Persons_Phonenumber",
                table: "Persons");

            migrationBuilder.DropIndex(
                name: "IX_Persons_Username",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "Persons");

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "Persons",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Phonenumber",
                table: "Persons",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Persons",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Persons",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Rolle",
                table: "Persons",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Persons_Email",
                table: "Persons",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Persons_Phonenumber",
                table: "Persons",
                column: "Phonenumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Persons_Username",
                table: "Persons",
                column: "Username",
                unique: true);
        }
    }
}
