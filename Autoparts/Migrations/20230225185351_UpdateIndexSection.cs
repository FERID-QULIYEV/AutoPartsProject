using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Autoparts.Migrations
{
    /// <inheritdoc />
    public partial class UpdateIndexSection : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SectionId",
                table: "IndexCategories",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_IndexCategories_SectionId",
                table: "IndexCategories",
                column: "SectionId");

            migrationBuilder.AddForeignKey(
                name: "FK_IndexCategories_Sections_SectionId",
                table: "IndexCategories",
                column: "SectionId",
                principalTable: "Sections",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IndexCategories_Sections_SectionId",
                table: "IndexCategories");

            migrationBuilder.DropIndex(
                name: "IX_IndexCategories_SectionId",
                table: "IndexCategories");

            migrationBuilder.DropColumn(
                name: "SectionId",
                table: "IndexCategories");
        }
    }
}
