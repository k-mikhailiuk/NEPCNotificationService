using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aggregator.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class CreateBaseNotificationsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NotificationExtensions_AcctBalChanges_AcctBalChangeNotificationId",
                schema: "nepc",
                table: "NotificationExtensions");

            migrationBuilder.DropForeignKey(
                name: "FK_NotificationExtensions_AcqFinAuths_AcqFinAuthNotificationId",
                schema: "nepc",
                table: "NotificationExtensions");

            migrationBuilder.DropForeignKey(
                name: "FK_NotificationExtensions_AcsOtps_AcsOtpNotificationId",
                schema: "nepc",
                table: "NotificationExtensions");

            migrationBuilder.DropForeignKey(
                name: "FK_NotificationExtensions_CardStatusChanges_CardStatusChangeNotificationId",
                schema: "nepc",
                table: "NotificationExtensions");

            migrationBuilder.DropForeignKey(
                name: "FK_NotificationExtensions_IssFinAuths_IssFinAuthNotificationId",
                schema: "nepc",
                table: "NotificationExtensions");

            migrationBuilder.DropForeignKey(
                name: "FK_NotificationExtensions_OwiUserActions_OwiUserActionNotificationId",
                schema: "nepc",
                table: "NotificationExtensions");

            migrationBuilder.DropForeignKey(
                name: "FK_NotificationExtensions_PinChanges_PinChangeNotificationId",
                schema: "nepc",
                table: "NotificationExtensions");

            migrationBuilder.DropForeignKey(
                name: "FK_NotificationExtensions_TokenStatusChanges_TokenStatusChangeNotificationId",
                schema: "nepc",
                table: "NotificationExtensions");

            migrationBuilder.DropForeignKey(
                name: "FK_NotificationExtensions_Unholds_UnholdNotificationId",
                schema: "nepc",
                table: "NotificationExtensions");

            migrationBuilder.DropIndex(
                name: "IX_NotificationExtensions_AcctBalChangeNotificationId",
                schema: "nepc",
                table: "NotificationExtensions");

            migrationBuilder.DropIndex(
                name: "IX_NotificationExtensions_AcqFinAuthNotificationId",
                schema: "nepc",
                table: "NotificationExtensions");

            migrationBuilder.DropIndex(
                name: "IX_NotificationExtensions_AcsOtpNotificationId",
                schema: "nepc",
                table: "NotificationExtensions");

            migrationBuilder.DropIndex(
                name: "IX_NotificationExtensions_CardStatusChangeNotificationId",
                schema: "nepc",
                table: "NotificationExtensions");

            migrationBuilder.DropIndex(
                name: "IX_NotificationExtensions_IssFinAuthNotificationId",
                schema: "nepc",
                table: "NotificationExtensions");

            migrationBuilder.DropIndex(
                name: "IX_NotificationExtensions_OwiUserActionNotificationId",
                schema: "nepc",
                table: "NotificationExtensions");

            migrationBuilder.DropIndex(
                name: "IX_NotificationExtensions_PinChangeNotificationId",
                schema: "nepc",
                table: "NotificationExtensions");

            migrationBuilder.DropIndex(
                name: "IX_NotificationExtensions_TokenStatusChangeNotificationId",
                schema: "nepc",
                table: "NotificationExtensions");

            migrationBuilder.DropIndex(
                name: "IX_NotificationExtensions_UnholdNotificationId",
                schema: "nepc",
                table: "NotificationExtensions");

            migrationBuilder.DropColumn(
                name: "EventId",
                schema: "nepc",
                table: "Unholds");

            migrationBuilder.DropColumn(
                name: "NotificationType",
                schema: "nepc",
                table: "Unholds");

            migrationBuilder.DropColumn(
                name: "Time",
                schema: "nepc",
                table: "Unholds");

            migrationBuilder.DropColumn(
                name: "EventId",
                schema: "nepc",
                table: "TokenStatusChanges");

            migrationBuilder.DropColumn(
                name: "NotificationType",
                schema: "nepc",
                table: "TokenStatusChanges");

            migrationBuilder.DropColumn(
                name: "Time",
                schema: "nepc",
                table: "TokenStatusChanges");

            migrationBuilder.DropColumn(
                name: "EventId",
                schema: "nepc",
                table: "PinChanges");

            migrationBuilder.DropColumn(
                name: "NotificationType",
                schema: "nepc",
                table: "PinChanges");

            migrationBuilder.DropColumn(
                name: "Time",
                schema: "nepc",
                table: "PinChanges");

            migrationBuilder.DropColumn(
                name: "EventId",
                schema: "nepc",
                table: "OwiUserActions");

            migrationBuilder.DropColumn(
                name: "NotificationType",
                schema: "nepc",
                table: "OwiUserActions");

            migrationBuilder.DropColumn(
                name: "Time",
                schema: "nepc",
                table: "OwiUserActions");

            migrationBuilder.DropColumn(
                name: "AcctBalChangeNotificationId",
                schema: "nepc",
                table: "NotificationExtensions");

            migrationBuilder.DropColumn(
                name: "AcqFinAuthNotificationId",
                schema: "nepc",
                table: "NotificationExtensions");

            migrationBuilder.DropColumn(
                name: "AcsOtpNotificationId",
                schema: "nepc",
                table: "NotificationExtensions");

            migrationBuilder.DropColumn(
                name: "CardStatusChangeNotificationId",
                schema: "nepc",
                table: "NotificationExtensions");

            migrationBuilder.DropColumn(
                name: "IssFinAuthNotificationId",
                schema: "nepc",
                table: "NotificationExtensions");

            migrationBuilder.DropColumn(
                name: "OwiUserActionNotificationId",
                schema: "nepc",
                table: "NotificationExtensions");

            migrationBuilder.DropColumn(
                name: "PinChangeNotificationId",
                schema: "nepc",
                table: "NotificationExtensions");

            migrationBuilder.DropColumn(
                name: "TokenStatusChangeNotificationId",
                schema: "nepc",
                table: "NotificationExtensions");

            migrationBuilder.DropColumn(
                name: "UnholdNotificationId",
                schema: "nepc",
                table: "NotificationExtensions");

            migrationBuilder.DropColumn(
                name: "EventId",
                schema: "nepc",
                table: "IssFinAuths");

            migrationBuilder.DropColumn(
                name: "NotificationType",
                schema: "nepc",
                table: "IssFinAuths");

            migrationBuilder.DropColumn(
                name: "Time",
                schema: "nepc",
                table: "IssFinAuths");

            migrationBuilder.DropColumn(
                name: "EventId",
                schema: "nepc",
                table: "CardStatusChanges");

            migrationBuilder.DropColumn(
                name: "NotificationType",
                schema: "nepc",
                table: "CardStatusChanges");

            migrationBuilder.DropColumn(
                name: "Time",
                schema: "nepc",
                table: "CardStatusChanges");

            migrationBuilder.DropColumn(
                name: "EventId",
                schema: "nepc",
                table: "AcsOtps");

            migrationBuilder.DropColumn(
                name: "NotificationType",
                schema: "nepc",
                table: "AcsOtps");

            migrationBuilder.DropColumn(
                name: "Time",
                schema: "nepc",
                table: "AcsOtps");

            migrationBuilder.DropColumn(
                name: "EventId",
                schema: "nepc",
                table: "AcqFinAuths");

            migrationBuilder.DropColumn(
                name: "NotificationType",
                schema: "nepc",
                table: "AcqFinAuths");

            migrationBuilder.DropColumn(
                name: "Time",
                schema: "nepc",
                table: "AcqFinAuths");

            migrationBuilder.DropColumn(
                name: "EventId",
                schema: "nepc",
                table: "AcctBalChanges");

            migrationBuilder.DropColumn(
                name: "NotificationType",
                schema: "nepc",
                table: "AcctBalChanges");

            migrationBuilder.DropColumn(
                name: "Time",
                schema: "nepc",
                table: "AcctBalChanges");

            migrationBuilder.CreateTable(
                name: "Notifications",
                schema: "nepc",
                columns: table => new
                {
                    NotificationId = table.Column<long>(type: "bigint", nullable: false),
                    NotificationType = table.Column<int>(type: "int", nullable: false),
                    EventId = table.Column<long>(type: "bigint", nullable: false),
                    Time = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.NotificationId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NotificationExtensions_NotificationId",
                schema: "nepc",
                table: "NotificationExtensions",
                column: "NotificationId");

            migrationBuilder.AddForeignKey(
                name: "FK_AcctBalChanges_Notifications_NotificationId",
                schema: "nepc",
                table: "AcctBalChanges",
                column: "NotificationId",
                principalSchema: "nepc",
                principalTable: "Notifications",
                principalColumn: "NotificationId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AcqFinAuths_Notifications_NotificationId",
                schema: "nepc",
                table: "AcqFinAuths",
                column: "NotificationId",
                principalSchema: "nepc",
                principalTable: "Notifications",
                principalColumn: "NotificationId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AcsOtps_Notifications_NotificationId",
                schema: "nepc",
                table: "AcsOtps",
                column: "NotificationId",
                principalSchema: "nepc",
                principalTable: "Notifications",
                principalColumn: "NotificationId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CardStatusChanges_Notifications_NotificationId",
                schema: "nepc",
                table: "CardStatusChanges",
                column: "NotificationId",
                principalSchema: "nepc",
                principalTable: "Notifications",
                principalColumn: "NotificationId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_IssFinAuths_Notifications_NotificationId",
                schema: "nepc",
                table: "IssFinAuths",
                column: "NotificationId",
                principalSchema: "nepc",
                principalTable: "Notifications",
                principalColumn: "NotificationId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NotificationExtensions_Notifications_NotificationId",
                schema: "nepc",
                table: "NotificationExtensions",
                column: "NotificationId",
                principalSchema: "nepc",
                principalTable: "Notifications",
                principalColumn: "NotificationId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OwiUserActions_Notifications_NotificationId",
                schema: "nepc",
                table: "OwiUserActions",
                column: "NotificationId",
                principalSchema: "nepc",
                principalTable: "Notifications",
                principalColumn: "NotificationId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PinChanges_Notifications_NotificationId",
                schema: "nepc",
                table: "PinChanges",
                column: "NotificationId",
                principalSchema: "nepc",
                principalTable: "Notifications",
                principalColumn: "NotificationId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TokenStatusChanges_Notifications_NotificationId",
                schema: "nepc",
                table: "TokenStatusChanges",
                column: "NotificationId",
                principalSchema: "nepc",
                principalTable: "Notifications",
                principalColumn: "NotificationId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Unholds_Notifications_NotificationId",
                schema: "nepc",
                table: "Unholds",
                column: "NotificationId",
                principalSchema: "nepc",
                principalTable: "Notifications",
                principalColumn: "NotificationId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AcctBalChanges_Notifications_NotificationId",
                schema: "nepc",
                table: "AcctBalChanges");

            migrationBuilder.DropForeignKey(
                name: "FK_AcqFinAuths_Notifications_NotificationId",
                schema: "nepc",
                table: "AcqFinAuths");

            migrationBuilder.DropForeignKey(
                name: "FK_AcsOtps_Notifications_NotificationId",
                schema: "nepc",
                table: "AcsOtps");

            migrationBuilder.DropForeignKey(
                name: "FK_CardStatusChanges_Notifications_NotificationId",
                schema: "nepc",
                table: "CardStatusChanges");

            migrationBuilder.DropForeignKey(
                name: "FK_IssFinAuths_Notifications_NotificationId",
                schema: "nepc",
                table: "IssFinAuths");

            migrationBuilder.DropForeignKey(
                name: "FK_NotificationExtensions_Notifications_NotificationId",
                schema: "nepc",
                table: "NotificationExtensions");

            migrationBuilder.DropForeignKey(
                name: "FK_OwiUserActions_Notifications_NotificationId",
                schema: "nepc",
                table: "OwiUserActions");

            migrationBuilder.DropForeignKey(
                name: "FK_PinChanges_Notifications_NotificationId",
                schema: "nepc",
                table: "PinChanges");

            migrationBuilder.DropForeignKey(
                name: "FK_TokenStatusChanges_Notifications_NotificationId",
                schema: "nepc",
                table: "TokenStatusChanges");

            migrationBuilder.DropForeignKey(
                name: "FK_Unholds_Notifications_NotificationId",
                schema: "nepc",
                table: "Unholds");

            migrationBuilder.DropTable(
                name: "Notifications",
                schema: "nepc");

            migrationBuilder.DropIndex(
                name: "IX_NotificationExtensions_NotificationId",
                schema: "nepc",
                table: "NotificationExtensions");

            migrationBuilder.AddColumn<long>(
                name: "EventId",
                schema: "nepc",
                table: "Unholds",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<int>(
                name: "NotificationType",
                schema: "nepc",
                table: "Unholds",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "Time",
                schema: "nepc",
                table: "Unholds",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<long>(
                name: "EventId",
                schema: "nepc",
                table: "TokenStatusChanges",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<int>(
                name: "NotificationType",
                schema: "nepc",
                table: "TokenStatusChanges",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "Time",
                schema: "nepc",
                table: "TokenStatusChanges",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<long>(
                name: "EventId",
                schema: "nepc",
                table: "PinChanges",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<int>(
                name: "NotificationType",
                schema: "nepc",
                table: "PinChanges",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "Time",
                schema: "nepc",
                table: "PinChanges",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<long>(
                name: "EventId",
                schema: "nepc",
                table: "OwiUserActions",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<int>(
                name: "NotificationType",
                schema: "nepc",
                table: "OwiUserActions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "Time",
                schema: "nepc",
                table: "OwiUserActions",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<long>(
                name: "AcctBalChangeNotificationId",
                schema: "nepc",
                table: "NotificationExtensions",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "AcqFinAuthNotificationId",
                schema: "nepc",
                table: "NotificationExtensions",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "AcsOtpNotificationId",
                schema: "nepc",
                table: "NotificationExtensions",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CardStatusChangeNotificationId",
                schema: "nepc",
                table: "NotificationExtensions",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "IssFinAuthNotificationId",
                schema: "nepc",
                table: "NotificationExtensions",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "OwiUserActionNotificationId",
                schema: "nepc",
                table: "NotificationExtensions",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "PinChangeNotificationId",
                schema: "nepc",
                table: "NotificationExtensions",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "TokenStatusChangeNotificationId",
                schema: "nepc",
                table: "NotificationExtensions",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UnholdNotificationId",
                schema: "nepc",
                table: "NotificationExtensions",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "EventId",
                schema: "nepc",
                table: "IssFinAuths",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<int>(
                name: "NotificationType",
                schema: "nepc",
                table: "IssFinAuths",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "Time",
                schema: "nepc",
                table: "IssFinAuths",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<long>(
                name: "EventId",
                schema: "nepc",
                table: "CardStatusChanges",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<int>(
                name: "NotificationType",
                schema: "nepc",
                table: "CardStatusChanges",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "Time",
                schema: "nepc",
                table: "CardStatusChanges",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<long>(
                name: "EventId",
                schema: "nepc",
                table: "AcsOtps",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<int>(
                name: "NotificationType",
                schema: "nepc",
                table: "AcsOtps",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "Time",
                schema: "nepc",
                table: "AcsOtps",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<long>(
                name: "EventId",
                schema: "nepc",
                table: "AcqFinAuths",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<int>(
                name: "NotificationType",
                schema: "nepc",
                table: "AcqFinAuths",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "Time",
                schema: "nepc",
                table: "AcqFinAuths",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<long>(
                name: "EventId",
                schema: "nepc",
                table: "AcctBalChanges",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<int>(
                name: "NotificationType",
                schema: "nepc",
                table: "AcctBalChanges",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "Time",
                schema: "nepc",
                table: "AcctBalChanges",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.CreateIndex(
                name: "IX_NotificationExtensions_AcctBalChangeNotificationId",
                schema: "nepc",
                table: "NotificationExtensions",
                column: "AcctBalChangeNotificationId");

            migrationBuilder.CreateIndex(
                name: "IX_NotificationExtensions_AcqFinAuthNotificationId",
                schema: "nepc",
                table: "NotificationExtensions",
                column: "AcqFinAuthNotificationId");

            migrationBuilder.CreateIndex(
                name: "IX_NotificationExtensions_AcsOtpNotificationId",
                schema: "nepc",
                table: "NotificationExtensions",
                column: "AcsOtpNotificationId");

            migrationBuilder.CreateIndex(
                name: "IX_NotificationExtensions_CardStatusChangeNotificationId",
                schema: "nepc",
                table: "NotificationExtensions",
                column: "CardStatusChangeNotificationId");

            migrationBuilder.CreateIndex(
                name: "IX_NotificationExtensions_IssFinAuthNotificationId",
                schema: "nepc",
                table: "NotificationExtensions",
                column: "IssFinAuthNotificationId");

            migrationBuilder.CreateIndex(
                name: "IX_NotificationExtensions_OwiUserActionNotificationId",
                schema: "nepc",
                table: "NotificationExtensions",
                column: "OwiUserActionNotificationId");

            migrationBuilder.CreateIndex(
                name: "IX_NotificationExtensions_PinChangeNotificationId",
                schema: "nepc",
                table: "NotificationExtensions",
                column: "PinChangeNotificationId");

            migrationBuilder.CreateIndex(
                name: "IX_NotificationExtensions_TokenStatusChangeNotificationId",
                schema: "nepc",
                table: "NotificationExtensions",
                column: "TokenStatusChangeNotificationId");

            migrationBuilder.CreateIndex(
                name: "IX_NotificationExtensions_UnholdNotificationId",
                schema: "nepc",
                table: "NotificationExtensions",
                column: "UnholdNotificationId");

            migrationBuilder.AddForeignKey(
                name: "FK_NotificationExtensions_AcctBalChanges_AcctBalChangeNotificationId",
                schema: "nepc",
                table: "NotificationExtensions",
                column: "AcctBalChangeNotificationId",
                principalSchema: "nepc",
                principalTable: "AcctBalChanges",
                principalColumn: "NotificationId");

            migrationBuilder.AddForeignKey(
                name: "FK_NotificationExtensions_AcqFinAuths_AcqFinAuthNotificationId",
                schema: "nepc",
                table: "NotificationExtensions",
                column: "AcqFinAuthNotificationId",
                principalSchema: "nepc",
                principalTable: "AcqFinAuths",
                principalColumn: "NotificationId");

            migrationBuilder.AddForeignKey(
                name: "FK_NotificationExtensions_AcsOtps_AcsOtpNotificationId",
                schema: "nepc",
                table: "NotificationExtensions",
                column: "AcsOtpNotificationId",
                principalSchema: "nepc",
                principalTable: "AcsOtps",
                principalColumn: "NotificationId");

            migrationBuilder.AddForeignKey(
                name: "FK_NotificationExtensions_CardStatusChanges_CardStatusChangeNotificationId",
                schema: "nepc",
                table: "NotificationExtensions",
                column: "CardStatusChangeNotificationId",
                principalSchema: "nepc",
                principalTable: "CardStatusChanges",
                principalColumn: "NotificationId");

            migrationBuilder.AddForeignKey(
                name: "FK_NotificationExtensions_IssFinAuths_IssFinAuthNotificationId",
                schema: "nepc",
                table: "NotificationExtensions",
                column: "IssFinAuthNotificationId",
                principalSchema: "nepc",
                principalTable: "IssFinAuths",
                principalColumn: "NotificationId");

            migrationBuilder.AddForeignKey(
                name: "FK_NotificationExtensions_OwiUserActions_OwiUserActionNotificationId",
                schema: "nepc",
                table: "NotificationExtensions",
                column: "OwiUserActionNotificationId",
                principalSchema: "nepc",
                principalTable: "OwiUserActions",
                principalColumn: "NotificationId");

            migrationBuilder.AddForeignKey(
                name: "FK_NotificationExtensions_PinChanges_PinChangeNotificationId",
                schema: "nepc",
                table: "NotificationExtensions",
                column: "PinChangeNotificationId",
                principalSchema: "nepc",
                principalTable: "PinChanges",
                principalColumn: "NotificationId");

            migrationBuilder.AddForeignKey(
                name: "FK_NotificationExtensions_TokenStatusChanges_TokenStatusChangeNotificationId",
                schema: "nepc",
                table: "NotificationExtensions",
                column: "TokenStatusChangeNotificationId",
                principalSchema: "nepc",
                principalTable: "TokenStatusChanges",
                principalColumn: "NotificationId");

            migrationBuilder.AddForeignKey(
                name: "FK_NotificationExtensions_Unholds_UnholdNotificationId",
                schema: "nepc",
                table: "NotificationExtensions",
                column: "UnholdNotificationId",
                principalSchema: "nepc",
                principalTable: "Unholds",
                principalColumn: "NotificationId");
        }
    }
}
