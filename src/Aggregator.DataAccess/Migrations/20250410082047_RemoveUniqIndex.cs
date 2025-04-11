using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aggregator.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class RemoveUniqIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AccountsInfo_AccountsInfoId_NotificationId",
                schema: "nepc",
                table: "AccountsInfo");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_AccountsInfo_AccountsInfoId_NotificationId",
                schema: "nepc",
                table: "AccountsInfo",
                columns: new[] { "AccountsInfoId", "NotificationId" },
                unique: true);
        }
    }
}
