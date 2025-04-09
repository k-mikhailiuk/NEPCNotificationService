using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aggregator.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddFkToIssFinAuthAccountsInfo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "IssFinAuthNotificationId",
                schema: "nepc",
                table: "IssFinAuthAccountsInfos",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_IssFinAuthAccountsInfos_IssFinAuthNotificationId",
                schema: "nepc",
                table: "IssFinAuthAccountsInfos",
                column: "IssFinAuthNotificationId");

            migrationBuilder.AddForeignKey(
                name: "FK_IssFinAuthAccountsInfos_IssFinAuths_IssFinAuthNotificationId",
                schema: "nepc",
                table: "IssFinAuthAccountsInfos",
                column: "IssFinAuthNotificationId",
                principalSchema: "nepc",
                principalTable: "IssFinAuths",
                principalColumn: "NotificationId",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IssFinAuthAccountsInfos_IssFinAuths_IssFinAuthNotificationId",
                schema: "nepc",
                table: "IssFinAuthAccountsInfos");

            migrationBuilder.DropIndex(
                name: "IX_IssFinAuthAccountsInfos_IssFinAuthNotificationId",
                schema: "nepc",
                table: "IssFinAuthAccountsInfos");

            migrationBuilder.DropColumn(
                name: "IssFinAuthNotificationId",
                schema: "nepc",
                table: "IssFinAuthAccountsInfos");
        }
    }
}
