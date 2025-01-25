using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MonosortMiniApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class EditSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cart_Users_UserId",
                schema: "dictionary",
                table: "Cart");

            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_Cart_CartId",
                table: "CartItems");

            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_Drinks_DrinkId",
                table: "CartItems");

            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_Volumes_VolumeId",
                table: "CartItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Users_UserId",
                schema: "dictionary",
                table: "Orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CartItems",
                table: "CartItems");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "User",
                newSchema: "dictionary");

            migrationBuilder.RenameTable(
                name: "CartItems",
                newName: "CartItem",
                newSchema: "dictionary");

            migrationBuilder.RenameIndex(
                name: "IX_CartItems_VolumeId",
                schema: "dictionary",
                table: "CartItem",
                newName: "IX_CartItem_VolumeId");

            migrationBuilder.RenameIndex(
                name: "IX_CartItems_DrinkId",
                schema: "dictionary",
                table: "CartItem",
                newName: "IX_CartItem_DrinkId");

            migrationBuilder.RenameIndex(
                name: "IX_CartItems_CartId",
                schema: "dictionary",
                table: "CartItem",
                newName: "IX_CartItem_CartId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
                schema: "dictionary",
                table: "User",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CartItem",
                schema: "dictionary",
                table: "CartItem",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Cart_User_UserId",
                schema: "dictionary",
                table: "Cart",
                column: "UserId",
                principalSchema: "dictionary",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CartItem_Cart_CartId",
                schema: "dictionary",
                table: "CartItem",
                column: "CartId",
                principalSchema: "dictionary",
                principalTable: "Cart",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CartItem_Drinks_DrinkId",
                schema: "dictionary",
                table: "CartItem",
                column: "DrinkId",
                principalSchema: "dictionary",
                principalTable: "Drinks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CartItem_Volumes_VolumeId",
                schema: "dictionary",
                table: "CartItem",
                column: "VolumeId",
                principalSchema: "dictionary",
                principalTable: "Volumes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_User_UserId",
                schema: "dictionary",
                table: "Orders",
                column: "UserId",
                principalSchema: "dictionary",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cart_User_UserId",
                schema: "dictionary",
                table: "Cart");

            migrationBuilder.DropForeignKey(
                name: "FK_CartItem_Cart_CartId",
                schema: "dictionary",
                table: "CartItem");

            migrationBuilder.DropForeignKey(
                name: "FK_CartItem_Drinks_DrinkId",
                schema: "dictionary",
                table: "CartItem");

            migrationBuilder.DropForeignKey(
                name: "FK_CartItem_Volumes_VolumeId",
                schema: "dictionary",
                table: "CartItem");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_User_UserId",
                schema: "dictionary",
                table: "Orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User",
                schema: "dictionary",
                table: "User");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CartItem",
                schema: "dictionary",
                table: "CartItem");

            migrationBuilder.RenameTable(
                name: "User",
                schema: "dictionary",
                newName: "Users");

            migrationBuilder.RenameTable(
                name: "CartItem",
                schema: "dictionary",
                newName: "CartItems");

            migrationBuilder.RenameIndex(
                name: "IX_CartItem_VolumeId",
                table: "CartItems",
                newName: "IX_CartItems_VolumeId");

            migrationBuilder.RenameIndex(
                name: "IX_CartItem_DrinkId",
                table: "CartItems",
                newName: "IX_CartItems_DrinkId");

            migrationBuilder.RenameIndex(
                name: "IX_CartItem_CartId",
                table: "CartItems",
                newName: "IX_CartItems_CartId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CartItems",
                table: "CartItems",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Cart_Users_UserId",
                schema: "dictionary",
                table: "Cart",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_Cart_CartId",
                table: "CartItems",
                column: "CartId",
                principalSchema: "dictionary",
                principalTable: "Cart",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_Drinks_DrinkId",
                table: "CartItems",
                column: "DrinkId",
                principalSchema: "dictionary",
                principalTable: "Drinks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_Volumes_VolumeId",
                table: "CartItems",
                column: "VolumeId",
                principalSchema: "dictionary",
                principalTable: "Volumes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Users_UserId",
                schema: "dictionary",
                table: "Orders",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
