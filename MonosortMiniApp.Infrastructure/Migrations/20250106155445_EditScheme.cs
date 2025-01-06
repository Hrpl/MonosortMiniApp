using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MonosortMiniApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class EditScheme : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TypeDrinks",
                table: "TypeDrinks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PriceDrinks",
                table: "PriceDrinks");

            migrationBuilder.RenameTable(
                name: "Drinks",
                newName: "Drinks",
                newSchema: "dictionary");

            migrationBuilder.RenameTable(
                name: "TypeDrinks",
                newName: "TypeDrink",
                newSchema: "dictionary");

            migrationBuilder.RenameTable(
                name: "PriceDrinks",
                newName: "PriceDrink",
                newSchema: "dictionary");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TypeDrink",
                schema: "dictionary",
                table: "TypeDrink",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PriceDrink",
                schema: "dictionary",
                table: "PriceDrink",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TypeDrink",
                schema: "dictionary",
                table: "TypeDrink");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PriceDrink",
                schema: "dictionary",
                table: "PriceDrink");

            migrationBuilder.RenameTable(
                name: "Drinks",
                schema: "dictionary",
                newName: "Drinks");

            migrationBuilder.RenameTable(
                name: "TypeDrink",
                schema: "dictionary",
                newName: "TypeDrinks");

            migrationBuilder.RenameTable(
                name: "PriceDrink",
                schema: "dictionary",
                newName: "PriceDrinks");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TypeDrinks",
                table: "TypeDrinks",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PriceDrinks",
                table: "PriceDrinks",
                column: "Id");
        }
    }
}
