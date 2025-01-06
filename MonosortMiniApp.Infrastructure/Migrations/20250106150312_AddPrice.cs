using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MonosortMiniApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddPrice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Volume",
                schema: "dictionary",
                table: "Volume");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Sirup",
                schema: "dictionary",
                table: "Sirup");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Milk",
                schema: "dictionary",
                table: "Milk");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Coffee",
                schema: "dictionary",
                table: "Coffee");

            migrationBuilder.RenameTable(
                name: "Volume",
                schema: "dictionary",
                newName: "Volumes",
                newSchema: "dictionary");

            migrationBuilder.RenameTable(
                name: "Sirup",
                schema: "dictionary",
                newName: "Sirups",
                newSchema: "dictionary");

            migrationBuilder.RenameTable(
                name: "Milk",
                schema: "dictionary",
                newName: "Milks",
                newSchema: "dictionary");

            migrationBuilder.RenameTable(
                name: "Coffee",
                schema: "dictionary",
                newName: "Coffees",
                newSchema: "dictionary");

            migrationBuilder.AddColumn<int>(
                name: "Price",
                schema: "dictionary",
                table: "Tea",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Price",
                schema: "dictionary",
                table: "Limonades",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Price",
                schema: "dictionary",
                table: "Volumes",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Price",
                schema: "dictionary",
                table: "Sirups",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Price",
                schema: "dictionary",
                table: "Milks",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Price",
                schema: "dictionary",
                table: "Coffees",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Volumes",
                schema: "dictionary",
                table: "Volumes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Sirups",
                schema: "dictionary",
                table: "Sirups",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Milks",
                schema: "dictionary",
                table: "Milks",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Coffees",
                schema: "dictionary",
                table: "Coffees",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Desserts",
                schema: "dictionary",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Price = table.Column<int>(type: "integer", nullable: false),
                    IsExistence = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Desserts", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Desserts",
                schema: "dictionary");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Volumes",
                schema: "dictionary",
                table: "Volumes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Sirups",
                schema: "dictionary",
                table: "Sirups");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Milks",
                schema: "dictionary",
                table: "Milks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Coffees",
                schema: "dictionary",
                table: "Coffees");

            migrationBuilder.DropColumn(
                name: "Price",
                schema: "dictionary",
                table: "Tea");

            migrationBuilder.DropColumn(
                name: "Price",
                schema: "dictionary",
                table: "Limonades");

            migrationBuilder.DropColumn(
                name: "Price",
                schema: "dictionary",
                table: "Volumes");

            migrationBuilder.DropColumn(
                name: "Price",
                schema: "dictionary",
                table: "Sirups");

            migrationBuilder.DropColumn(
                name: "Price",
                schema: "dictionary",
                table: "Milks");

            migrationBuilder.DropColumn(
                name: "Price",
                schema: "dictionary",
                table: "Coffees");

            migrationBuilder.RenameTable(
                name: "Volumes",
                schema: "dictionary",
                newName: "Volume",
                newSchema: "dictionary");

            migrationBuilder.RenameTable(
                name: "Sirups",
                schema: "dictionary",
                newName: "Sirup",
                newSchema: "dictionary");

            migrationBuilder.RenameTable(
                name: "Milks",
                schema: "dictionary",
                newName: "Milk",
                newSchema: "dictionary");

            migrationBuilder.RenameTable(
                name: "Coffees",
                schema: "dictionary",
                newName: "Coffee",
                newSchema: "dictionary");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Volume",
                schema: "dictionary",
                table: "Volume",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Sirup",
                schema: "dictionary",
                table: "Sirup",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Milk",
                schema: "dictionary",
                table: "Milk",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Coffee",
                schema: "dictionary",
                table: "Coffee",
                column: "Id");
        }
    }
}
