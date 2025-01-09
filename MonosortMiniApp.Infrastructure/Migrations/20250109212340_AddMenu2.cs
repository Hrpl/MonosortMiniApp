using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MonosortMiniApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddMenu2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TypeDrink",
                schema: "dictionary",
                table: "TypeDrink");

            migrationBuilder.RenameTable(
                name: "TypeDrink",
                schema: "dictionary",
                newName: "Menu",
                newSchema: "dictionary");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Menu",
                schema: "dictionary",
                table: "Menu",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Menu",
                schema: "dictionary",
                table: "Menu");

            migrationBuilder.RenameTable(
                name: "Menu",
                schema: "dictionary",
                newName: "TypeDrink",
                newSchema: "dictionary");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TypeDrink",
                schema: "dictionary",
                table: "TypeDrink",
                column: "Id");
        }
    }
}
