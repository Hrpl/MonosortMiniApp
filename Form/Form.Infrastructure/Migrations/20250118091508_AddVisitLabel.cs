using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Form.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddVisitLabel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsVisit",
                table: "Forms",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsVisit",
                table: "Forms");
        }
    }
}
