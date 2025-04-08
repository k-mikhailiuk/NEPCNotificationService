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
                type: "bigint");
                
            migrationBuilder.AddPrimaryKey(
                name: "PK_AccountsInfoLimitWrappers",
                schema: "nepc",
                table: "AccountsInfoLimitWrappers",
                column: "Id");
            
            migrationBuilder.DropForeignKey(
                name: "FK_AccountsInfoLimitWrappers_AcctBalChangeAccountsInfos_LimitId",
                schema: "nepc",
                table: "AccountsInfoLimitWrappers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
                    defaultValue: 0L)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AccountsInfoLimitWrappers",
                schema: "nepc",
                table: "AccountsInfoLimitWrappers",
                column: "Id");
            
            migrationBuilder.AddForeignKey(
                name: "FK_AccountsInfoLimitWrappers_AcctBalChangeAccountsInfos_LimitId",
                schema: "nepc",
                table: "AccountsInfoLimitWrappers",
                column: "LimitId",
                principalSchema: "nepc",
                principalTable: "AcctBalChangeAccountsInfos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade );
        }
    }
}
