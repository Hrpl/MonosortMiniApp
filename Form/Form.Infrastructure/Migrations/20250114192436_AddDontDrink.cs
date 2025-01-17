using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Form.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddDontDrink : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "DontDrink",
                table: "Forms",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DontDrink",
                table: "Forms");
        }
    }
}
