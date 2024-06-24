using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CLDV6211POE.Data.Migrations
{
    /// <inheritdoc />
    public partial class addingStatusToOrderAndPreviousOrders : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "PreviousOrders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "PreviousOrders");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Orders");
        }
    }
}
