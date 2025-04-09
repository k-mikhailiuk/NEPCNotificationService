using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aggregator.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class WrapperRelationFixes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AcctBalChangeAccountsInfos_AcctBalChanges_AcctBalChangeId",
                schema: "nepc",
                table: "AcctBalChangeAccountsInfos");

            migrationBuilder.RenameColumn(
                name: "IssFinAuthId",
                schema: "nepc",
                table: "IssFinAuthAccountsInfos",
                newName: "NotificationId");

            migrationBuilder.RenameIndex(
                name: "IX_IssFinAuthAccountsInfos_IssFinAuthId",
                schema: "nepc",
                table: "IssFinAuthAccountsInfos",
                newName: "IX_IssFinAuthAccountsInfos_NotificationId");

            migrationBuilder.RenameColumn(
                name: "AcctBalChangeId",
                schema: "nepc",
                table: "AcctBalChangeAccountsInfos",
                newName: "NotificationId");

            migrationBuilder.RenameIndex(
                name: "IX_AcctBalChangeAccountsInfos_AcctBalChangeId",
                schema: "nepc",
                table: "AcctBalChangeAccountsInfos",
                newName: "IX_AcctBalChangeAccountsInfos_NotificationId");

            migrationBuilder.AddForeignKey(
                name: "FK_AcctBalChangeAccountsInfos_AcctBalChanges_NotificationId",
                schema: "nepc",
                table: "AcctBalChangeAccountsInfos",
                column: "NotificationId",
                principalSchema: "nepc",
                principalTable: "AcctBalChanges",
                principalColumn: "NotificationId");

            migrationBuilder.AddForeignKey(
                name: "FK_IssFinAuthAccountsInfos_IssFinAuths_NotificationId",
                schema: "nepc",
                table: "IssFinAuthAccountsInfos",
                column: "NotificationId",
                principalSchema: "nepc",
                principalTable: "IssFinAuths",
                principalColumn: "NotificationId");

            migrationBuilder.DropForeignKey(
                name: "FK_IssFinAuthAccountsInfos_IssFinAuths_IssFinAuthNotificationId",
                schema: "nepc",
                table: "IssFinAuthAccountsInfos");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AcctBalChangeAccountsInfos_AcctBalChanges_NotificationId",
                schema: "nepc",
                table: "AcctBalChangeAccountsInfos");

            migrationBuilder.DropForeignKey(
                name: "FK_IssFinAuthAccountsInfos_IssFinAuths_NotificationId",
                schema: "nepc",
                table: "IssFinAuthAccountsInfos");

            migrationBuilder.RenameColumn(
                name: "NotificationId",
                schema: "nepc",
                table: "IssFinAuthAccountsInfos",
                newName: "IssFinAuthId");

            migrationBuilder.RenameIndex(
                name: "IX_IssFinAuthAccountsInfos_NotificationId",
                schema: "nepc",
                table: "IssFinAuthAccountsInfos",
                newName: "IX_IssFinAuthAccountsInfos_IssFinAuthId");

            migrationBuilder.RenameColumn(
                name: "NotificationId",
                schema: "nepc",
                table: "AcctBalChangeAccountsInfos",
                newName: "AcctBalChangeId");

            migrationBuilder.RenameIndex(
                name: "IX_AcctBalChangeAccountsInfos_NotificationId",
                schema: "nepc",
                table: "AcctBalChangeAccountsInfos",
                newName: "IX_AcctBalChangeAccountsInfos_AcctBalChangeId");

            migrationBuilder.AddForeignKey(
                name: "FK_AcctBalChangeAccountsInfos_AcctBalChanges_AcctBalChangeId",
                schema: "nepc",
                table: "AcctBalChangeAccountsInfos",
                column: "AcctBalChangeId",
                principalSchema: "nepc",
                principalTable: "AcctBalChanges",
                principalColumn: "NotificationId");
            
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
    }
}
