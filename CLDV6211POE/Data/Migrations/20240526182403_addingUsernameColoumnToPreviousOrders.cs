using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CLDV6211POE.Data.Migrations
{
    /// <inheritdoc />
    public partial class addingUsernameColoumnToPreviousOrders : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "PreviousOrders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "PreviousOrders");
        }
    }
}
