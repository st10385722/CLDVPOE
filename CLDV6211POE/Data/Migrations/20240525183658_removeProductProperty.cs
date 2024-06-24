using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CLDV6211POE.Data.Migrations
{
    /// <inheritdoc />
    public partial class removeProductProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Products_ProductsProductId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_ProductsProductId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ProductsProductId",
                table: "Orders");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductsProductId",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ProductsProductId",
                table: "Orders",
                column: "ProductsProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Products_ProductsProductId",
                table: "Orders",
                column: "ProductsProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
