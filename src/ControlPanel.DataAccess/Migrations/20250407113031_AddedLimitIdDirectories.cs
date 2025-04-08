using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ControlPanel.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddedLimitIdDirectories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LimitIdDescriptionDirectories",
                schema: "nepc",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DescriptionRu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DescriptionKg = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DescriptionEn = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LimitIdDescriptionDirectories", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LimitIdDescriptionDirectories",
                schema: "nepc");
        }
    }
}
