using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MonosortMiniApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class NewUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsConfirmed",
                schema: "dictionary",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Login",
                schema: "dictionary",
                table: "User");

            migrationBuilder.RenameColumn(
                name: "Password",
                schema: "dictionary",
                table: "User",
                newName: "PhoneNumber");

            migrationBuilder.RenameColumn(
                name: "Name",
                schema: "dictionary",
                table: "User",
                newName: "Code");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PhoneNumber",
                schema: "dictionary",
                table: "User",
                newName: "Password");

            migrationBuilder.RenameColumn(
                name: "Code",
                schema: "dictionary",
                table: "User",
                newName: "Name");

            migrationBuilder.AddColumn<bool>(
                name: "IsConfirmed",
                schema: "dictionary",
                table: "User",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Login",
                schema: "dictionary",
                table: "User",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
