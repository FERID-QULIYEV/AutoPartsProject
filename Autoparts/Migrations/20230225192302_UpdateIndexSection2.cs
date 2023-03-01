using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Autoparts.Migrations
{
    /// <inheritdoc />
    public partial class UpdateIndexSection2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Sections",
                table: "IndexCategories");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Sections",
                table: "IndexCategories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
