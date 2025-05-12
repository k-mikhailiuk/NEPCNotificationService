using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aggregator.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class NotificationDetailsRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AcctBalChanges_AcctBalChangeDetails_DetailsId",
                schema: "nepc",
                table: "AcctBalChanges");

            migrationBuilder.DropForeignKey(
                name: "FK_AcqFinAuths_AcqFinAuthDetails_DetailsId",
                schema: "nepc",
                table: "AcqFinAuths");

            migrationBuilder.DropForeignKey(
                name: "FK_AcsOtpsDetails_AcsOtps_DetailsId",
                schema: "nepc",
                table: "AcsOtpsDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_CardStatusChanges_CardStatusChangeDetails_DetailsId",
                schema: "nepc",
                table: "CardStatusChanges");

            migrationBuilder.DropForeignKey(
                name: "FK_IssFinAuths_IssFinAuthDetails_DetailsId",
                schema: "nepc",
                table: "IssFinAuths");

            migrationBuilder.DropForeignKey(
                name: "FK_OwiUserActions_OwiUserActionDetails_DetailsId",
                schema: "nepc",
                table: "OwiUserActions");

            migrationBuilder.DropForeignKey(
                name: "FK_PinChanges_PinChangeDetails_DetailsId",
                schema: "nepc",
                table: "PinChanges");

            migrationBuilder.DropForeignKey(
                name: "FK_TokenStatusChanges_TokenStatusChangeDetails_DetailsId",
                schema: "nepc",
                table: "TokenStatusChanges");

            migrationBuilder.DropForeignKey(
                name: "FK_Unholds_UnholdDetails_DetailsId",
                schema: "nepc",
                table: "Unholds");

            migrationBuilder.DropIndex(
                name: "IX_Unholds_DetailsId",
                schema: "nepc",
                table: "Unholds");

            migrationBuilder.DropIndex(
                name: "IX_TokenStatusChanges_CardInfoId",
                schema: "nepc",
                table: "TokenStatusChanges");

            migrationBuilder.DropIndex(
                name: "IX_TokenStatusChanges_DetailsId",
                schema: "nepc",
                table: "TokenStatusChanges");

            migrationBuilder.DropIndex(
                name: "IX_PinChanges_DetailsId",
                schema: "nepc",
                table: "PinChanges");

            migrationBuilder.DropIndex(
                name: "IX_OwiUserActions_DetailsId",
                schema: "nepc",
                table: "OwiUserActions");

            migrationBuilder.DropIndex(
                name: "IX_IssFinAuths_DetailsId",
                schema: "nepc",
                table: "IssFinAuths");

            migrationBuilder.DropIndex(
                name: "IX_CardStatusChanges_DetailsId",
                schema: "nepc",
                table: "CardStatusChanges");

            migrationBuilder.DropIndex(
                name: "IX_AcqFinAuths_DetailsId",
                schema: "nepc",
                table: "AcqFinAuths");

            migrationBuilder.DropIndex(
                name: "IX_AcctBalChanges_DetailsId",
                schema: "nepc",
                table: "AcctBalChanges");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AcsOtpsDetails",
                schema: "nepc",
                table: "AcsOtpsDetails");

            migrationBuilder.DropColumn(
                name: "DetailsId",
                schema: "nepc",
                table: "Unholds");

            migrationBuilder.DropColumn(
                name: "DetailsId",
                schema: "nepc",
                table: "TokenStatusChanges");

            migrationBuilder.DropColumn(
                name: "DetailsId",
                schema: "nepc",
                table: "PinChanges");

            migrationBuilder.DropColumn(
                name: "DetailsId",
                schema: "nepc",
                table: "OwiUserActions");

            migrationBuilder.DropColumn(
                name: "DetailsId",
                schema: "nepc",
                table: "IssFinAuths");

            migrationBuilder.DropColumn(
                name: "DetailsId",
                schema: "nepc",
                table: "CardStatusChanges");

            migrationBuilder.DropColumn(
                name: "DetailsId",
                schema: "nepc",
                table: "AcsOtps");

            migrationBuilder.DropColumn(
                name: "DetailsId",
                schema: "nepc",
                table: "AcqFinAuths");

            migrationBuilder.DropColumn(
                name: "DetailsId",
                schema: "nepc",
                table: "AcctBalChanges");

            migrationBuilder.RenameTable(
                name: "AcsOtpsDetails",
                schema: "nepc",
                newName: "AcsOtpDetails",
                newSchema: "nepc");

            migrationBuilder.AddColumn<long>(
                name: "NotificationId",
                schema: "nepc",
                table: "UnholdDetails",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "NotificationId",
                schema: "nepc",
                table: "TokenStatusChangeDetails",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "NotificationId",
                schema: "nepc",
                table: "PinChangeDetails",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "NotificationId",
                schema: "nepc",
                table: "OwiUserActionDetails",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "NotificationId",
                schema: "nepc",
                table: "IssFinAuthDetails",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "NotificationId",
                schema: "nepc",
                table: "CardStatusChangeDetails",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "NotificationId",
                schema: "nepc",
                table: "AcqFinAuthDetails",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "NotificationId",
                schema: "nepc",
                table: "AcctBalChangeDetails",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "NotificationId",
                schema: "nepc",
                table: "AcsOtpDetails",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddPrimaryKey(
                name: "PK_AcsOtpDetails",
                schema: "nepc",
                table: "AcsOtpDetails",
                column: "DetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_UnholdDetails_NotificationId",
                schema: "nepc",
                table: "UnholdDetails",
                column: "NotificationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TokenStatusChanges_CardInfoId",
                schema: "nepc",
                table: "TokenStatusChanges",
                column: "CardInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_TokenStatusChangeDetails_NotificationId",
                schema: "nepc",
                table: "TokenStatusChangeDetails",
                column: "NotificationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PinChangeDetails_NotificationId",
                schema: "nepc",
                table: "PinChangeDetails",
                column: "NotificationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OwiUserActionDetails_NotificationId",
                schema: "nepc",
                table: "OwiUserActionDetails",
                column: "NotificationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_IssFinAuthDetails_NotificationId",
                schema: "nepc",
                table: "IssFinAuthDetails",
                column: "NotificationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CardStatusChangeDetails_NotificationId",
                schema: "nepc",
                table: "CardStatusChangeDetails",
                column: "NotificationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AcqFinAuthDetails_NotificationId",
                schema: "nepc",
                table: "AcqFinAuthDetails",
                column: "NotificationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AcctBalChangeDetails_NotificationId",
                schema: "nepc",
                table: "AcctBalChangeDetails",
                column: "NotificationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AcsOtpDetails_NotificationId",
                schema: "nepc",
                table: "AcsOtpDetails",
                column: "NotificationId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AcctBalChangeDetails_AcctBalChanges_NotificationId",
                schema: "nepc",
                table: "AcctBalChangeDetails",
                column: "NotificationId",
                principalSchema: "nepc",
                principalTable: "AcctBalChanges",
                principalColumn: "NotificationId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AcqFinAuthDetails_AcqFinAuths_NotificationId",
                schema: "nepc",
                table: "AcqFinAuthDetails",
                column: "NotificationId",
                principalSchema: "nepc",
                principalTable: "AcqFinAuths",
                principalColumn: "NotificationId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AcsOtpDetails_AcsOtps_NotificationId",
                schema: "nepc",
                table: "AcsOtpDetails",
                column: "NotificationId",
                principalSchema: "nepc",
                principalTable: "AcsOtps",
                principalColumn: "NotificationId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CardStatusChangeDetails_CardStatusChanges_NotificationId",
                schema: "nepc",
                table: "CardStatusChangeDetails",
                column: "NotificationId",
                principalSchema: "nepc",
                principalTable: "CardStatusChanges",
                principalColumn: "NotificationId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_IssFinAuthDetails_IssFinAuths_NotificationId",
                schema: "nepc",
                table: "IssFinAuthDetails",
                column: "NotificationId",
                principalSchema: "nepc",
                principalTable: "IssFinAuths",
                principalColumn: "NotificationId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OwiUserActionDetails_OwiUserActions_NotificationId",
                schema: "nepc",
                table: "OwiUserActionDetails",
                column: "NotificationId",
                principalSchema: "nepc",
                principalTable: "OwiUserActions",
                principalColumn: "NotificationId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PinChangeDetails_PinChanges_NotificationId",
                schema: "nepc",
                table: "PinChangeDetails",
                column: "NotificationId",
                principalSchema: "nepc",
                principalTable: "PinChanges",
                principalColumn: "NotificationId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TokenStatusChangeDetails_TokenStatusChanges_NotificationId",
                schema: "nepc",
                table: "TokenStatusChangeDetails",
                column: "NotificationId",
                principalSchema: "nepc",
                principalTable: "TokenStatusChanges",
                principalColumn: "NotificationId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UnholdDetails_Unholds_NotificationId",
                schema: "nepc",
                table: "UnholdDetails",
                column: "NotificationId",
                principalSchema: "nepc",
                principalTable: "Unholds",
                principalColumn: "NotificationId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AcctBalChangeDetails_AcctBalChanges_NotificationId",
                schema: "nepc",
                table: "AcctBalChangeDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_AcqFinAuthDetails_AcqFinAuths_NotificationId",
                schema: "nepc",
                table: "AcqFinAuthDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_AcsOtpDetails_AcsOtps_NotificationId",
                schema: "nepc",
                table: "AcsOtpDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_CardStatusChangeDetails_CardStatusChanges_NotificationId",
                schema: "nepc",
                table: "CardStatusChangeDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_IssFinAuthDetails_IssFinAuths_NotificationId",
                schema: "nepc",
                table: "IssFinAuthDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_OwiUserActionDetails_OwiUserActions_NotificationId",
                schema: "nepc",
                table: "OwiUserActionDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_PinChangeDetails_PinChanges_NotificationId",
                schema: "nepc",
                table: "PinChangeDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_TokenStatusChangeDetails_TokenStatusChanges_NotificationId",
                schema: "nepc",
                table: "TokenStatusChangeDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_UnholdDetails_Unholds_NotificationId",
                schema: "nepc",
                table: "UnholdDetails");

            migrationBuilder.DropIndex(
                name: "IX_UnholdDetails_NotificationId",
                schema: "nepc",
                table: "UnholdDetails");

            migrationBuilder.DropIndex(
                name: "IX_TokenStatusChanges_CardInfoId",
                schema: "nepc",
                table: "TokenStatusChanges");

            migrationBuilder.DropIndex(
                name: "IX_TokenStatusChangeDetails_NotificationId",
                schema: "nepc",
                table: "TokenStatusChangeDetails");

            migrationBuilder.DropIndex(
                name: "IX_PinChangeDetails_NotificationId",
                schema: "nepc",
                table: "PinChangeDetails");

            migrationBuilder.DropIndex(
                name: "IX_OwiUserActionDetails_NotificationId",
                schema: "nepc",
                table: "OwiUserActionDetails");

            migrationBuilder.DropIndex(
                name: "IX_IssFinAuthDetails_NotificationId",
                schema: "nepc",
                table: "IssFinAuthDetails");

            migrationBuilder.DropIndex(
                name: "IX_CardStatusChangeDetails_NotificationId",
                schema: "nepc",
                table: "CardStatusChangeDetails");

            migrationBuilder.DropIndex(
                name: "IX_AcqFinAuthDetails_NotificationId",
                schema: "nepc",
                table: "AcqFinAuthDetails");

            migrationBuilder.DropIndex(
                name: "IX_AcctBalChangeDetails_NotificationId",
                schema: "nepc",
                table: "AcctBalChangeDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AcsOtpDetails",
                schema: "nepc",
                table: "AcsOtpDetails");

            migrationBuilder.DropIndex(
                name: "IX_AcsOtpDetails_NotificationId",
                schema: "nepc",
                table: "AcsOtpDetails");

            migrationBuilder.DropColumn(
                name: "NotificationId",
                schema: "nepc",
                table: "UnholdDetails");

            migrationBuilder.DropColumn(
                name: "NotificationId",
                schema: "nepc",
                table: "TokenStatusChangeDetails");

            migrationBuilder.DropColumn(
                name: "NotificationId",
                schema: "nepc",
                table: "PinChangeDetails");

            migrationBuilder.DropColumn(
                name: "NotificationId",
                schema: "nepc",
                table: "OwiUserActionDetails");

            migrationBuilder.DropColumn(
                name: "NotificationId",
                schema: "nepc",
                table: "IssFinAuthDetails");

            migrationBuilder.DropColumn(
                name: "NotificationId",
                schema: "nepc",
                table: "CardStatusChangeDetails");

            migrationBuilder.DropColumn(
                name: "NotificationId",
                schema: "nepc",
                table: "AcqFinAuthDetails");

            migrationBuilder.DropColumn(
                name: "NotificationId",
                schema: "nepc",
                table: "AcctBalChangeDetails");

            migrationBuilder.DropColumn(
                name: "NotificationId",
                schema: "nepc",
                table: "AcsOtpDetails");

            migrationBuilder.RenameTable(
                name: "AcsOtpDetails",
                schema: "nepc",
                newName: "AcsOtpsDetails",
                newSchema: "nepc");

            migrationBuilder.AddColumn<long>(
                name: "DetailsId",
                schema: "nepc",
                table: "Unholds",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "DetailsId",
                schema: "nepc",
                table: "TokenStatusChanges",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "DetailsId",
                schema: "nepc",
                table: "PinChanges",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "DetailsId",
                schema: "nepc",
                table: "OwiUserActions",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "DetailsId",
                schema: "nepc",
                table: "IssFinAuths",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "DetailsId",
                schema: "nepc",
                table: "CardStatusChanges",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "DetailsId",
                schema: "nepc",
                table: "AcsOtps",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "DetailsId",
                schema: "nepc",
                table: "AcqFinAuths",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "DetailsId",
                schema: "nepc",
                table: "AcctBalChanges",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddPrimaryKey(
                name: "PK_AcsOtpsDetails",
                schema: "nepc",
                table: "AcsOtpsDetails",
                column: "DetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_Unholds_DetailsId",
                schema: "nepc",
                table: "Unholds",
                column: "DetailsId",
                unique: true,
                filter: "[DetailsId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_TokenStatusChanges_CardInfoId",
                schema: "nepc",
                table: "TokenStatusChanges",
                column: "CardInfoId",
                unique: true,
                filter: "[CardInfoId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_TokenStatusChanges_DetailsId",
                schema: "nepc",
                table: "TokenStatusChanges",
                column: "DetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_PinChanges_DetailsId",
                schema: "nepc",
                table: "PinChanges",
                column: "DetailsId",
                unique: true,
                filter: "[DetailsId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_OwiUserActions_DetailsId",
                schema: "nepc",
                table: "OwiUserActions",
                column: "DetailsId",
                unique: true,
                filter: "[DetailsId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_IssFinAuths_DetailsId",
                schema: "nepc",
                table: "IssFinAuths",
                column: "DetailsId",
                unique: true,
                filter: "[DetailsId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_CardStatusChanges_DetailsId",
                schema: "nepc",
                table: "CardStatusChanges",
                column: "DetailsId",
                unique: true,
                filter: "[DetailsId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AcqFinAuths_DetailsId",
                schema: "nepc",
                table: "AcqFinAuths",
                column: "DetailsId",
                unique: true,
                filter: "[DetailsId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AcctBalChanges_DetailsId",
                schema: "nepc",
                table: "AcctBalChanges",
                column: "DetailsId",
                unique: true,
                filter: "[DetailsId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_AcctBalChanges_AcctBalChangeDetails_DetailsId",
                schema: "nepc",
                table: "AcctBalChanges",
                column: "DetailsId",
                principalSchema: "nepc",
                principalTable: "AcctBalChangeDetails",
                principalColumn: "AcctBalChangeDetailsId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AcqFinAuths_AcqFinAuthDetails_DetailsId",
                schema: "nepc",
                table: "AcqFinAuths",
                column: "DetailsId",
                principalSchema: "nepc",
                principalTable: "AcqFinAuthDetails",
                principalColumn: "AcqFinAuthDetailsId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AcsOtpsDetails_AcsOtps_DetailsId",
                schema: "nepc",
                table: "AcsOtpsDetails",
                column: "DetailsId",
                principalSchema: "nepc",
                principalTable: "AcsOtps",
                principalColumn: "NotificationId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CardStatusChanges_CardStatusChangeDetails_DetailsId",
                schema: "nepc",
                table: "CardStatusChanges",
                column: "DetailsId",
                principalSchema: "nepc",
                principalTable: "CardStatusChangeDetails",
                principalColumn: "CardStatusChangeDetailsId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_IssFinAuths_IssFinAuthDetails_DetailsId",
                schema: "nepc",
                table: "IssFinAuths",
                column: "DetailsId",
                principalSchema: "nepc",
                principalTable: "IssFinAuthDetails",
                principalColumn: "IssFinAuthDetailsId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OwiUserActions_OwiUserActionDetails_DetailsId",
                schema: "nepc",
                table: "OwiUserActions",
                column: "DetailsId",
                principalSchema: "nepc",
                principalTable: "OwiUserActionDetails",
                principalColumn: "OwiUserActionDetailsId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PinChanges_PinChangeDetails_DetailsId",
                schema: "nepc",
                table: "PinChanges",
                column: "DetailsId",
                principalSchema: "nepc",
                principalTable: "PinChangeDetails",
                principalColumn: "PinChangeDetailsId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TokenStatusChanges_TokenStatusChangeDetails_DetailsId",
                schema: "nepc",
                table: "TokenStatusChanges",
                column: "DetailsId",
                principalSchema: "nepc",
                principalTable: "TokenStatusChangeDetails",
                principalColumn: "TokenStatusChangeDetailsId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Unholds_UnholdDetails_DetailsId",
                schema: "nepc",
                table: "Unholds",
                column: "DetailsId",
                principalSchema: "nepc",
                principalTable: "UnholdDetails",
                principalColumn: "UnholdDetailsId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
