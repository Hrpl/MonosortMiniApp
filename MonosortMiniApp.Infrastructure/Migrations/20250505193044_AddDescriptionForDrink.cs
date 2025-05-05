using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MonosortMiniApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddDescriptionForDrink : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                schema: "dictionary",
                table: "Drinks",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                schema: "dictionary",
                table: "Drinks");
        }
    }
}
