using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MonosortMiniApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddSprinkling : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Sprinkling",
                schema: "dictionary",
                table: "CartItem",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Sprinkling",
                schema: "dictionary",
                table: "CartItem");
        }
    }
}
