using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ControlPanel.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddNotificationMessageTextDirectoriesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NotificationMessageTextDirectories",
                schema: "nepc",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NotificationType = table.Column<byte>(type: "tinyint", nullable: false),
                    OperationType = table.Column<int>(type: "int", nullable: true),
                    MessageTextRu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MessageTextEn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MessageTextKg = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsNeedSend = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationMessageTextDirectories", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NotificationMessageTextDirectories_NotificationType",
                schema: "nepc",
                table: "NotificationMessageTextDirectories",
                column: "NotificationType",
                unique: true,
                filter: "OperationType IS NULL");

            migrationBuilder.CreateIndex(
                name: "IX_NotificationMessageTextDirectories_NotificationType_OperationType",
                schema: "nepc",
                table: "NotificationMessageTextDirectories",
                columns: new[] { "NotificationType", "OperationType" },
                unique: true,
                filter: "OperationType IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NotificationMessageTextDirectories",
                schema: "nepc");
        }
    }
}
