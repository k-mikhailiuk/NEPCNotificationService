using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aggregator.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class FixedLimitFK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CardInfoLimitWrappers_Limits_LimitId",
                schema: "nepc",
                table: "CardInfoLimitWrappers");
            
            migrationBuilder.DropForeignKey(
                name: "FK_AccountsInfoLimitWrappers_Limits_LimitId",
                schema: "nepc",
                table: "AccountsInfoLimitWrappers");
            
            migrationBuilder.DropPrimaryKey(
                name: "PK_Limits",
                schema: "nepc",
                table: "Limits");
            
            migrationBuilder.AddPrimaryKey(
                name: "PK_Limits",
                schema: "nepc",
                table: "Limits",
                column: "Id");
            
            migrationBuilder.AddForeignKey(
                name: "FK_CardInfoLimitWrappers_Limits_LimitId",
                schema: "nepc",
                table: "CardInfoLimitWrappers",
                column: "LimitId",
                principalSchema: "nepc",
                principalTable: "Limits",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
            
            migrationBuilder.AddForeignKey(
                name: "FK_AccountsInfoLimitWrappers_Limits_LimitId",
                schema: "nepc",
                table: "AccountsInfoLimitWrappers",
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
                name: "FK_CardInfoLimitWrappers_Limits_LimitId",
                schema: "nepc",
                table: "CardInfoLimitWrappers");
            
            migrationBuilder.DropForeignKey(
                name: "FK_AccountsInfoLimitWrappers_Limits_LimitId",
                schema: "nepc",
                table: "AccountsInfoLimitWrappers");
            
            migrationBuilder.DropPrimaryKey(
                name: "PK_Limits",
                schema: "nepc",
                table: "Limits");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Limits",
                schema: "nepc",
                table: "Limits",
                column: "LimitId");
            
            migrationBuilder.AddForeignKey(
                name: "FK_CardInfoLimitWrappers_Limits_LimitId",
                schema: "nepc",
                table: "CardInfoLimitWrappers",
                column: "LimitId",
                principalSchema: "nepc",
                principalTable: "Limits",
                principalColumn: "LimitId",
                onDelete: ReferentialAction.Cascade);
            
            migrationBuilder.AddForeignKey(
                name: "FK_AccountsInfoLimitWrappers_Limits_LimitId",
                schema: "nepc",
                table: "AccountsInfoLimitWrappers",
                column: "LimitId",
                principalSchema: "nepc",
                principalTable: "Limits",
                principalColumn: "LimitId",
                onDelete: ReferentialAction.Cascade);
            
            
        }
    }
}
