using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aggregator.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddAcsOtpEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "AcsOtpNotificationId",
                schema: "nepc",
                table: "NotificationExtensions",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AcsOtps",
                schema: "nepc",
                columns: table => new
                {
                    NotificationId = table.Column<long>(type: "bigint", nullable: false),
                    NotificationType = table.Column<int>(type: "int", nullable: false),
                    EventId = table.Column<long>(type: "bigint", nullable: false),
                    Time = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Details_OtpInfo_Otp = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Details_OtpInfo_ExpirationTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Details_AuthMoney_Amount = table.Column<decimal>(type: "decimal(15,2)", nullable: true),
                    Details_AuthMoney_Currency = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: true),
                    Details_TransactionTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CardInfoId = table.Column<long>(type: "bigint", nullable: false),
                    MerchantInfo_MerchantId = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    MerchantInfo_Name = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    MerchantInfo_Country = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    MerchantInfo_Url = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AcsOtps", x => x.NotificationId);
                    table.ForeignKey(
                        name: "FK_AcsOtps_CardInfos_CardInfoId",
                        column: x => x.CardInfoId,
                        principalSchema: "nepc",
                        principalTable: "CardInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NotificationExtensions_AcsOtpNotificationId",
                schema: "nepc",
                table: "NotificationExtensions",
                column: "AcsOtpNotificationId");

            migrationBuilder.CreateIndex(
                name: "IX_AcsOtps_CardInfoId",
                schema: "nepc",
                table: "AcsOtps",
                column: "CardInfoId");

            migrationBuilder.AddForeignKey(
                name: "FK_NotificationExtensions_AcsOtps_AcsOtpNotificationId",
                schema: "nepc",
                table: "NotificationExtensions",
                column: "AcsOtpNotificationId",
                principalSchema: "nepc",
                principalTable: "AcsOtps",
                principalColumn: "NotificationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NotificationExtensions_AcsOtps_AcsOtpNotificationId",
                schema: "nepc",
                table: "NotificationExtensions");

            migrationBuilder.DropTable(
                name: "AcsOtps",
                schema: "nepc");

            migrationBuilder.DropIndex(
                name: "IX_NotificationExtensions_AcsOtpNotificationId",
                schema: "nepc",
                table: "NotificationExtensions");

            migrationBuilder.DropColumn(
                name: "AcsOtpNotificationId",
                schema: "nepc",
                table: "NotificationExtensions");
        }
    }
}
