using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aggregator.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class DetailsRelationsFixes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Unholds_DetailsId",
                schema: "nepc",
                table: "Unholds");

            migrationBuilder.DropIndex(
                name: "IX_TokenStatusChanges_CardInfoId",
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

            migrationBuilder.DropColumn(
                name: "Details_AuthMoney_Amount",
                schema: "nepc",
                table: "AcsOtps");

            migrationBuilder.DropColumn(
                name: "Details_AuthMoney_Currency",
                schema: "nepc",
                table: "AcsOtps");

            migrationBuilder.DropColumn(
                name: "Details_OtpInfo_ExpirationTime",
                schema: "nepc",
                table: "AcsOtps");

            migrationBuilder.DropColumn(
                name: "Details_OtpInfo_Otp",
                schema: "nepc",
                table: "AcsOtps");

            migrationBuilder.DropColumn(
                name: "Details_TransactionTime",
                schema: "nepc",
                table: "AcsOtps");

            migrationBuilder.AlterColumn<string>(
                name: "WalletProvider_Id",
                schema: "nepc",
                table: "IssFinAuthDetails",
                type: "nvarchar(11)",
                maxLength: 11,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Rrn",
                schema: "nepc",
                table: "IssFinAuthDetails",
                type: "nvarchar(12)",
                maxLength: 12,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "IssInstId",
                schema: "nepc",
                table: "IssFinAuthDetails",
                type: "nvarchar(4)",
                maxLength: 4,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "IssFee_Currency",
                schema: "nepc",
                table: "IssFinAuthDetails",
                type: "nvarchar(3)",
                maxLength: 3,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Dpan",
                schema: "nepc",
                table: "IssFinAuthDetails",
                type: "nvarchar(19)",
                maxLength: 19,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CorrespondingAccount",
                schema: "nepc",
                table: "IssFinAuthDetails",
                type: "nvarchar(4)",
                maxLength: 4,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "ConvMoney_Currency",
                schema: "nepc",
                table: "IssFinAuthDetails",
                type: "nvarchar(3)",
                maxLength: 3,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "BillingMoney_Currency",
                schema: "nepc",
                table: "IssFinAuthDetails",
                type: "nvarchar(3)",
                maxLength: 3,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AuthorizationCondition",
                schema: "nepc",
                table: "IssFinAuthDetails",
                type: "nvarchar(12)",
                maxLength: 12,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AuthMoney_Currency",
                schema: "nepc",
                table: "IssFinAuthDetails",
                type: "nvarchar(3)",
                maxLength: 3,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "AuthMoney_Amount",
                schema: "nepc",
                table: "IssFinAuthDetails",
                type: "decimal(15,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(15,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ApprovalCode",
                schema: "nepc",
                table: "IssFinAuthDetails",
                type: "nvarchar(6)",
                maxLength: 6,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AcqFee_Currency",
                schema: "nepc",
                table: "IssFinAuthDetails",
                type: "nvarchar(3)",
                maxLength: 3,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AccountId",
                schema: "nepc",
                table: "IssFinAuthDetails",
                type: "nvarchar(32)",
                maxLength: 32,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AccountBalance_Currency",
                schema: "nepc",
                table: "IssFinAuthDetails",
                type: "nvarchar(3)",
                maxLength: 3,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DetailsId",
                schema: "nepc",
                table: "AcsOtps",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "AcsOtpsDetails",
                schema: "nepc",
                columns: table => new
                {
                    DetailsId = table.Column<long>(type: "bigint", nullable: false),
                    OtpInfo_Otp = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OtpInfo_ExpirationTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    AuthMoney_Amount = table.Column<decimal>(type: "decimal(15,2)", nullable: true),
                    AuthMoney_Currency = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: true),
                    TransactionTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AcsOtpsDetails", x => x.DetailsId);
                    table.ForeignKey(
                        name: "FK_AcsOtpsDetails_AcsOtps_DetailsId",
                        column: x => x.DetailsId,
                        principalSchema: "nepc",
                        principalTable: "AcsOtps",
                        principalColumn: "NotificationId",
                        onDelete: ReferentialAction.Cascade);
                });

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AcsOtpsDetails",
                schema: "nepc");

            migrationBuilder.DropIndex(
                name: "IX_Unholds_DetailsId",
                schema: "nepc",
                table: "Unholds");

            migrationBuilder.DropIndex(
                name: "IX_TokenStatusChanges_CardInfoId",
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

            migrationBuilder.DropColumn(
                name: "DetailsId",
                schema: "nepc",
                table: "AcsOtps");

            migrationBuilder.AlterColumn<string>(
                name: "WalletProvider_Id",
                schema: "nepc",
                table: "IssFinAuthDetails",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(11)",
                oldMaxLength: 11,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Rrn",
                schema: "nepc",
                table: "IssFinAuthDetails",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(12)",
                oldMaxLength: 12,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "IssInstId",
                schema: "nepc",
                table: "IssFinAuthDetails",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(4)",
                oldMaxLength: 4);

            migrationBuilder.AlterColumn<string>(
                name: "IssFee_Currency",
                schema: "nepc",
                table: "IssFinAuthDetails",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(3)",
                oldMaxLength: 3,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Dpan",
                schema: "nepc",
                table: "IssFinAuthDetails",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(19)",
                oldMaxLength: 19,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CorrespondingAccount",
                schema: "nepc",
                table: "IssFinAuthDetails",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(4)",
                oldMaxLength: 4);

            migrationBuilder.AlterColumn<string>(
                name: "ConvMoney_Currency",
                schema: "nepc",
                table: "IssFinAuthDetails",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(3)",
                oldMaxLength: 3,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "BillingMoney_Currency",
                schema: "nepc",
                table: "IssFinAuthDetails",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(3)",
                oldMaxLength: 3,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AuthorizationCondition",
                schema: "nepc",
                table: "IssFinAuthDetails",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(12)",
                oldMaxLength: 12,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AuthMoney_Currency",
                schema: "nepc",
                table: "IssFinAuthDetails",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(3)",
                oldMaxLength: 3);

            migrationBuilder.AlterColumn<decimal>(
                name: "AuthMoney_Amount",
                schema: "nepc",
                table: "IssFinAuthDetails",
                type: "decimal(15,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(15,2)");

            migrationBuilder.AlterColumn<string>(
                name: "ApprovalCode",
                schema: "nepc",
                table: "IssFinAuthDetails",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(6)",
                oldMaxLength: 6,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AcqFee_Currency",
                schema: "nepc",
                table: "IssFinAuthDetails",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(3)",
                oldMaxLength: 3,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AccountId",
                schema: "nepc",
                table: "IssFinAuthDetails",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(32)",
                oldMaxLength: 32,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AccountBalance_Currency",
                schema: "nepc",
                table: "IssFinAuthDetails",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(3)",
                oldMaxLength: 3,
                oldNullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Details_AuthMoney_Amount",
                schema: "nepc",
                table: "AcsOtps",
                type: "decimal(15,2)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Details_AuthMoney_Currency",
                schema: "nepc",
                table: "AcsOtps",
                type: "nvarchar(3)",
                maxLength: 3,
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "Details_OtpInfo_ExpirationTime",
                schema: "nepc",
                table: "AcsOtps",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<string>(
                name: "Details_OtpInfo_Otp",
                schema: "nepc",
                table: "AcsOtps",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "Details_TransactionTime",
                schema: "nepc",
                table: "AcsOtps",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.CreateIndex(
                name: "IX_Unholds_DetailsId",
                schema: "nepc",
                table: "Unholds",
                column: "DetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_TokenStatusChanges_CardInfoId",
                schema: "nepc",
                table: "TokenStatusChanges",
                column: "CardInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_PinChanges_DetailsId",
                schema: "nepc",
                table: "PinChanges",
                column: "DetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_OwiUserActions_DetailsId",
                schema: "nepc",
                table: "OwiUserActions",
                column: "DetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_IssFinAuths_DetailsId",
                schema: "nepc",
                table: "IssFinAuths",
                column: "DetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_CardStatusChanges_DetailsId",
                schema: "nepc",
                table: "CardStatusChanges",
                column: "DetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_AcqFinAuths_DetailsId",
                schema: "nepc",
                table: "AcqFinAuths",
                column: "DetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_AcctBalChanges_DetailsId",
                schema: "nepc",
                table: "AcctBalChanges",
                column: "DetailsId");
        }
    }
}
