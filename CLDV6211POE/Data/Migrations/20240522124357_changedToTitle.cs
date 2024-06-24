using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CLDV6211POE.Data.Migrations
{
    /// <inheritdoc />
    public partial class changedToTitle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageFilePath",
                table: "Products",
                newName: "ImageFileName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageFileName",
                table: "Products",
                newName: "ImageFilePath");
        }
    }
}
