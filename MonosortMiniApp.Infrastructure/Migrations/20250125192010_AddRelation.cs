using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MonosortMiniApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Desserts",
                schema: "dictionary");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderItem",
                schema: "dictionary",
                table: "OrderItem");

            migrationBuilder.RenameTable(
                name: "OrderItem",
                schema: "dictionary",
                newName: "OrderItems",
                newSchema: "dictionary");

            migrationBuilder.RenameColumn(
                name: "PositionId",
                schema: "dictionary",
                table: "SiropsPosition",
                newName: "OrderItemId");

            migrationBuilder.RenameColumn(
                name: "Status",
                schema: "dictionary",
                table: "Orders",
                newName: "StatusId");

            migrationBuilder.AddColumn<int>(
                name: "OrderStatusId",
                schema: "dictionary",
                table: "Orders",
                type: "integer",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderItems",
                schema: "dictionary",
                table: "OrderItems",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Cart",
                schema: "dictionary",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cart", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cart_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CartItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CartId = table.Column<int>(type: "integer", nullable: false),
                    DrinkId = table.Column<int>(type: "integer", nullable: false),
                    VolumeId = table.Column<int>(type: "integer", nullable: false),
                    SugarCount = table.Column<int>(type: "integer", nullable: false),
                    MilkId = table.Column<int>(type: "integer", nullable: false),
                    ExtraShot = table.Column<bool>(type: "boolean", nullable: false),
                    Price = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CartItems_Cart_CartId",
                        column: x => x.CartId,
                        principalSchema: "dictionary",
                        principalTable: "Cart",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CartItems_Drinks_DrinkId",
                        column: x => x.DrinkId,
                        principalSchema: "dictionary",
                        principalTable: "Drinks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CartItems_Volumes_VolumeId",
                        column: x => x.VolumeId,
                        principalSchema: "dictionary",
                        principalTable: "Volumes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SiropsPosition_OrderItemId",
                schema: "dictionary",
                table: "SiropsPosition",
                column: "OrderItemId");

            migrationBuilder.CreateIndex(
                name: "IX_PriceDrink_DrinkId",
                schema: "dictionary",
                table: "PriceDrink",
                column: "DrinkId");

            migrationBuilder.CreateIndex(
                name: "IX_PriceDrink_VolumeId",
                schema: "dictionary",
                table: "PriceDrink",
                column: "VolumeId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_OrderStatusId",
                schema: "dictionary",
                table: "Orders",
                column: "OrderStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserId",
                schema: "dictionary",
                table: "Orders",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Drinks_MenuId",
                schema: "dictionary",
                table: "Drinks",
                column: "MenuId");

            migrationBuilder.CreateIndex(
                name: "IX_Additive_TypeAdditiveId",
                schema: "dictionary",
                table: "Additive",
                column: "TypeAdditiveId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_DrinkId",
                schema: "dictionary",
                table: "OrderItems",
                column: "DrinkId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderId",
                schema: "dictionary",
                table: "OrderItems",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_VolumeId",
                schema: "dictionary",
                table: "OrderItems",
                column: "VolumeId");

            migrationBuilder.CreateIndex(
                name: "IX_Cart_UserId",
                schema: "dictionary",
                table: "Cart",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_CartId",
                table: "CartItems",
                column: "CartId");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_DrinkId",
                table: "CartItems",
                column: "DrinkId");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_VolumeId",
                table: "CartItems",
                column: "VolumeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Additive_TypeAdditive_TypeAdditiveId",
                schema: "dictionary",
                table: "Additive",
                column: "TypeAdditiveId",
                principalSchema: "dictionary",
                principalTable: "TypeAdditive",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Drinks_Menu_MenuId",
                schema: "dictionary",
                table: "Drinks",
                column: "MenuId",
                principalSchema: "dictionary",
                principalTable: "Menu",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Drinks_DrinkId",
                schema: "dictionary",
                table: "OrderItems",
                column: "DrinkId",
                principalSchema: "dictionary",
                principalTable: "Drinks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Orders_OrderId",
                schema: "dictionary",
                table: "OrderItems",
                column: "OrderId",
                principalSchema: "dictionary",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Volumes_VolumeId",
                schema: "dictionary",
                table: "OrderItems",
                column: "VolumeId",
                principalSchema: "dictionary",
                principalTable: "Volumes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_OrderStatus_OrderStatusId",
                schema: "dictionary",
                table: "Orders",
                column: "OrderStatusId",
                principalSchema: "dictionary",
                principalTable: "OrderStatus",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Users_UserId",
                schema: "dictionary",
                table: "Orders",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PriceDrink_Drinks_DrinkId",
                schema: "dictionary",
                table: "PriceDrink",
                column: "DrinkId",
                principalSchema: "dictionary",
                principalTable: "Drinks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PriceDrink_Volumes_VolumeId",
                schema: "dictionary",
                table: "PriceDrink",
                column: "VolumeId",
                principalSchema: "dictionary",
                principalTable: "Volumes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SiropsPosition_OrderItems_OrderItemId",
                schema: "dictionary",
                table: "SiropsPosition",
                column: "OrderItemId",
                principalSchema: "dictionary",
                principalTable: "OrderItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Additive_TypeAdditive_TypeAdditiveId",
                schema: "dictionary",
                table: "Additive");

            migrationBuilder.DropForeignKey(
                name: "FK_Drinks_Menu_MenuId",
                schema: "dictionary",
                table: "Drinks");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Drinks_DrinkId",
                schema: "dictionary",
                table: "OrderItems");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Orders_OrderId",
                schema: "dictionary",
                table: "OrderItems");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Volumes_VolumeId",
                schema: "dictionary",
                table: "OrderItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_OrderStatus_OrderStatusId",
                schema: "dictionary",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Users_UserId",
                schema: "dictionary",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_PriceDrink_Drinks_DrinkId",
                schema: "dictionary",
                table: "PriceDrink");

            migrationBuilder.DropForeignKey(
                name: "FK_PriceDrink_Volumes_VolumeId",
                schema: "dictionary",
                table: "PriceDrink");

            migrationBuilder.DropForeignKey(
                name: "FK_SiropsPosition_OrderItems_OrderItemId",
                schema: "dictionary",
                table: "SiropsPosition");

            migrationBuilder.DropTable(
                name: "CartItems");

            migrationBuilder.DropTable(
                name: "Cart",
                schema: "dictionary");

            migrationBuilder.DropIndex(
                name: "IX_SiropsPosition_OrderItemId",
                schema: "dictionary",
                table: "SiropsPosition");

            migrationBuilder.DropIndex(
                name: "IX_PriceDrink_DrinkId",
                schema: "dictionary",
                table: "PriceDrink");

            migrationBuilder.DropIndex(
                name: "IX_PriceDrink_VolumeId",
                schema: "dictionary",
                table: "PriceDrink");

            migrationBuilder.DropIndex(
                name: "IX_Orders_OrderStatusId",
                schema: "dictionary",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_UserId",
                schema: "dictionary",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Drinks_MenuId",
                schema: "dictionary",
                table: "Drinks");

            migrationBuilder.DropIndex(
                name: "IX_Additive_TypeAdditiveId",
                schema: "dictionary",
                table: "Additive");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderItems",
                schema: "dictionary",
                table: "OrderItems");

            migrationBuilder.DropIndex(
                name: "IX_OrderItems_DrinkId",
                schema: "dictionary",
                table: "OrderItems");

            migrationBuilder.DropIndex(
                name: "IX_OrderItems_OrderId",
                schema: "dictionary",
                table: "OrderItems");

            migrationBuilder.DropIndex(
                name: "IX_OrderItems_VolumeId",
                schema: "dictionary",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "OrderStatusId",
                schema: "dictionary",
                table: "Orders");

            migrationBuilder.RenameTable(
                name: "OrderItems",
                schema: "dictionary",
                newName: "OrderItem",
                newSchema: "dictionary");

            migrationBuilder.RenameColumn(
                name: "OrderItemId",
                schema: "dictionary",
                table: "SiropsPosition",
                newName: "PositionId");

            migrationBuilder.RenameColumn(
                name: "StatusId",
                schema: "dictionary",
                table: "Orders",
                newName: "Status");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderItem",
                schema: "dictionary",
                table: "OrderItem",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Desserts",
                schema: "dictionary",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: true),
                    IsExistence = table.Column<bool>(type: "boolean", nullable: false),
                    MenuId = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Photo = table.Column<string>(type: "text", nullable: false),
                    Price = table.Column<int>(type: "integer", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Desserts", x => x.Id);
                });
        }
    }
}
