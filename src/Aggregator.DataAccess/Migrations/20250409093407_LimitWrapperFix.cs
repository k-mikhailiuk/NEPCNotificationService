using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aggregator.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class LimitWrapperFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountsInfoLimitWrappers_AcctBalChangeAccountsInfos_Id",
                schema: "nepc",
                table: "AccountsInfoLimitWrappers");

            migrationBuilder.DropForeignKey(
                name: "FK_AccountsInfoLimitWrappers_IssFinAuthAccountsInfos_Id",
                schema: "nepc",
                table: "AccountsInfoLimitWrappers");

            migrationBuilder.DropIndex(
                name: "IX_AccountsInfoLimitWrappers_LimitId",
                schema: "nepc",
                table: "AccountsInfoLimitWrappers");

            migrationBuilder.DropPrimaryKey(name: "PK_AccountsInfoLimitWrappers",
                schema: "nepc",
                table: "AccountsInfoLimitWrappers");
            
            migrationBuilder.DropColumn(
                name: "Id",
                schema: "nepc",
                table: "AccountsInfoLimitWrappers");
            
            migrationBuilder.AddColumn<long>(
                    name: "Id",
                    schema: "nepc",
                    table: "AccountsInfoLimitWrappers",
                    nullable: false,
                    defaultValue: 0L,
                    type: "bigint")
                .Annotation("SqlServer:Identity", "1, 1");
                
            migrationBuilder.AddPrimaryKey(
                name: "PK_AccountsInfoLimitWrappers",
                schema: "nepc",
                table: "AccountsInfoLimitWrappers",
                column: "Id");

            migrationBuilder.AddColumn<long>(
                name: "AccountsInfoNotificationId",
                schema: "nepc",
                table: "AccountsInfoLimitWrappers",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_AccountsInfoLimitWrappers_AccountsInfoNotificationId",
                schema: "nepc",
                table: "AccountsInfoLimitWrappers",
                column: "AccountsInfoNotificationId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountsInfoLimitWrappers_LimitId",
                schema: "nepc",
                table: "AccountsInfoLimitWrappers",
                column: "LimitId");

            migrationBuilder.AddForeignKey(
                name: "FK_AccountsInfoLimitWrappers_AcctBalChangeAccountsInfos_AccountsInfoNotificationId",
                schema: "nepc",
                table: "AccountsInfoLimitWrappers",
                column: "AccountsInfoNotificationId",
                principalSchema: "nepc",
                principalTable: "AcctBalChangeAccountsInfos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AccountsInfoLimitWrappers_IssFinAuthAccountsInfos_AccountsInfoNotificationId",
                schema: "nepc",
                table: "AccountsInfoLimitWrappers",
                column: "AccountsInfoNotificationId",
                principalSchema: "nepc",
                principalTable: "IssFinAuthAccountsInfos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountsInfoLimitWrappers_AcctBalChangeAccountsInfos_AccountsInfoNotificationId",
                schema: "nepc",
                table: "AccountsInfoLimitWrappers");

            migrationBuilder.DropForeignKey(
                name: "FK_AccountsInfoLimitWrappers_IssFinAuthAccountsInfos_AccountsInfoNotificationId",
                schema: "nepc",
                table: "AccountsInfoLimitWrappers");

            migrationBuilder.DropIndex(
                name: "IX_AccountsInfoLimitWrappers_AccountsInfoNotificationId",
                schema: "nepc",
                table: "AccountsInfoLimitWrappers");

            migrationBuilder.DropIndex(
                name: "IX_AccountsInfoLimitWrappers_LimitId",
                schema: "nepc",
                table: "AccountsInfoLimitWrappers");

            migrationBuilder.DropColumn(
                name: "AccountsInfoNotificationId",
                schema: "nepc",
                table: "AccountsInfoLimitWrappers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AccountsInfoLimitWrappers",
                schema: "nepc",
                table: "AccountsInfoLimitWrappers");

            migrationBuilder.DropColumn(
                name: "Id",
                schema: "nepc",
                table: "AccountsInfoLimitWrappers");

            migrationBuilder.AddColumn<long>(
                name: "Id",
                schema: "nepc",
                table: "AccountsInfoLimitWrappers",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddPrimaryKey(
                name: "PK_AccountsInfoLimitWrappers",
                schema: "nepc",
                table: "AccountsInfoLimitWrappers",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_AccountsInfoLimitWrappers_LimitId",
                schema: "nepc",
                table: "AccountsInfoLimitWrappers",
                column: "LimitId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AccountsInfoLimitWrappers_AcctBalChangeAccountsInfos_Id",
                schema: "nepc",
                table: "AccountsInfoLimitWrappers",
                column: "Id",
                principalSchema: "nepc",
                principalTable: "AcctBalChangeAccountsInfos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AccountsInfoLimitWrappers_IssFinAuthAccountsInfos_Id",
                schema: "nepc",
                table: "AccountsInfoLimitWrappers",
                column: "Id",
                principalSchema: "nepc",
                principalTable: "IssFinAuthAccountsInfos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
