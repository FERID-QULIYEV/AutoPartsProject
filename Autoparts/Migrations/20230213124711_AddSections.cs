using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Autoparts.Migrations
{
    /// <inheritdoc />
    public partial class AddSections : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Sections",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Section1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Section2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Section3 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Section4 = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sections", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Sections");
        }
    }
}
