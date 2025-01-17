using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Form.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class DeleteFood : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Food",
                table: "Forms");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Food",
                table: "Forms",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
