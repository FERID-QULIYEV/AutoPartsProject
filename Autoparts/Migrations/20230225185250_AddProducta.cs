using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Autoparts.Migrations
{
    /// <inheritdoc />
    public partial class AddProducta : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AvailableOption_Product_ProductId",
                table: "AvailableOption");

            migrationBuilder.DropForeignKey(
                name: "FK_Color_BestSellers_BestSellerId",
                table: "Color");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_BestSellers_BestSellerId",
                table: "Product");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Product",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Product_BestSellerId",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "BestSellerId",
                table: "Product");

            migrationBuilder.RenameTable(
                name: "Product",
                newName: "Products");

            migrationBuilder.RenameColumn(
                name: "BestSellerId",
                table: "Color",
                newName: "ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_Color_BestSellerId",
                table: "Color",
                newName: "IX_Color_ProductId");

            migrationBuilder.AddColumn<int>(
                name: "ColorId",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Products",
                table: "Products",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AvailableOption_Products_ProductId",
                table: "AvailableOption",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Color_Products_ProductId",
                table: "Color",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AvailableOption_Products_ProductId",
                table: "AvailableOption");

            migrationBuilder.DropForeignKey(
                name: "FK_Color_Products_ProductId",
                table: "Color");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Products",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ColorId",
                table: "Products");

            migrationBuilder.RenameTable(
                name: "Products",
                newName: "Product");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "Color",
                newName: "BestSellerId");

            migrationBuilder.RenameIndex(
                name: "IX_Color_ProductId",
                table: "Color",
                newName: "IX_Color_BestSellerId");

            migrationBuilder.AddColumn<int>(
                name: "BestSellerId",
                table: "Product",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Product",
                table: "Product",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Product_BestSellerId",
                table: "Product",
                column: "BestSellerId");

            migrationBuilder.AddForeignKey(
                name: "FK_AvailableOption_Product_ProductId",
                table: "AvailableOption",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Color_BestSellers_BestSellerId",
                table: "Color",
                column: "BestSellerId",
                principalTable: "BestSellers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_BestSellers_BestSellerId",
                table: "Product",
                column: "BestSellerId",
                principalTable: "BestSellers",
                principalColumn: "Id");
        }
    }
}
