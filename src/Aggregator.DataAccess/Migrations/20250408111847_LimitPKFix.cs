using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aggregator.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class LimitPKFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountsInfoLimitWrappers_Limits_LimitId",
                schema: "nepc",
                table: "AccountsInfoLimitWrappers");

            migrationBuilder.DropForeignKey(
                name: "FK_CardInfoLimitWrappers_Limits_LimitId",
                schema: "nepc",
                table: "CardInfoLimitWrappers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Limits",
                schema: "nepc",
                table: "Limits");

            migrationBuilder.AddColumn<long>(
                name: "Id",
                schema: "nepc",
                table: "Limits",
                type: "bigint",
                nullable: false,
                defaultValue: 0L)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<long>(
                name: "AcctBalChangeNotificationId",
                schema: "nepc",
                table: "AcctBalChangeAccountsInfos",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Limits",
                schema: "nepc",
                table: "Limits",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_AcctBalChangeAccountsInfos_AcctBalChangeNotificationId",
                schema: "nepc",
                table: "AcctBalChangeAccountsInfos",
                column: "AcctBalChangeNotificationId");

            migrationBuilder.AddForeignKey(
                name: "FK_AccountsInfoLimitWrappers_Limits_LimitId",
                schema: "nepc",
                table: "AccountsInfoLimitWrappers",
                column: "LimitId",
                principalSchema: "nepc",
                principalTable: "Limits",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AcctBalChangeAccountsInfos_AcctBalChanges_AcctBalChangeNotificationId",
                schema: "nepc",
                table: "AcctBalChangeAccountsInfos",
                column: "AcctBalChangeNotificationId",
                principalSchema: "nepc",
                principalTable: "AcctBalChanges",
                principalColumn: "NotificationId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CardInfoLimitWrappers_Limits_LimitId",
                schema: "nepc",
                table: "CardInfoLimitWrappers",
                column: "LimitId",
                principalSchema: "nepc",
                principalTable: "Limits",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountsInfoLimitWrappers_Limits_LimitId",
                schema: "nepc",
                table: "AccountsInfoLimitWrappers");

            migrationBuilder.DropForeignKey(
                name: "FK_AcctBalChangeAccountsInfos_AcctBalChanges_AcctBalChangeNotificationId",
                schema: "nepc",
                table: "AcctBalChangeAccountsInfos");

            migrationBuilder.DropForeignKey(
                name: "FK_CardInfoLimitWrappers_Limits_LimitId",
                schema: "nepc",
                table: "CardInfoLimitWrappers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Limits",
                schema: "nepc",
                table: "Limits");

            migrationBuilder.DropIndex(
                name: "IX_AcctBalChangeAccountsInfos_AcctBalChangeNotificationId",
                schema: "nepc",
                table: "AcctBalChangeAccountsInfos");

            migrationBuilder.DropColumn(
                name: "Id",
                schema: "nepc",
                table: "Limits");

            migrationBuilder.DropColumn(
                name: "AcctBalChangeNotificationId",
                schema: "nepc",
                table: "AcctBalChangeAccountsInfos");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Limits",
                schema: "nepc",
                table: "Limits",
                column: "LimitId");

            migrationBuilder.AddForeignKey(
                name: "FK_AccountsInfoLimitWrappers_Limits_LimitId",
                schema: "nepc",
                table: "AccountsInfoLimitWrappers",
                column: "LimitId",
                principalSchema: "nepc",
                principalTable: "Limits",
                principalColumn: "LimitId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CardInfoLimitWrappers_Limits_LimitId",
                schema: "nepc",
                table: "CardInfoLimitWrappers",
                column: "LimitId",
                principalSchema: "nepc",
                principalTable: "Limits",
                principalColumn: "LimitId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
