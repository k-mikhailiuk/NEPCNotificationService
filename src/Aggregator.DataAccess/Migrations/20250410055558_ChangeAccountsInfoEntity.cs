using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aggregator.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class ChangeAccountsInfoEntity : Migration
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

            migrationBuilder.DropTable(
                name: "AcctBalChangeAccountsInfos",
                schema: "nepc");

            migrationBuilder.DropTable(
                name: "IssFinAuthAccountsInfos",
                schema: "nepc");

            migrationBuilder.DropIndex(
                name: "IX_AccountsInfoLimitWrappers_LimitId",
                schema: "nepc",
                table: "AccountsInfoLimitWrappers");

            migrationBuilder.AddColumn<long>(
                name: "Id",
                schema: "nepc",
                table: "Limits",
                type: "bigint",
                nullable: false,
                defaultValue: 0L)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.DropPrimaryKey(
                name:"PK_AccountsInfoLimitWrappers",
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

            migrationBuilder.AddColumn<long>(
                name: "AccountsInfoId",
                schema: "nepc",
                table: "AccountsInfoLimitWrappers",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "AccountsInfo",
                schema: "nepc",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NotificationId = table.Column<long>(type: "bigint", nullable: false),
                    NotificationType = table.Column<int>(type: "int", nullable: false),
                    AccountsInfoId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    AviableBalance_Amount = table.Column<decimal>(type: "decimal(15,2)", nullable: false),
                    AviableBalance_Currency = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    ExceedLimit_Amount = table.Column<decimal>(type: "decimal(15,2)", nullable: true),
                    ExceedLimit_Currency = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: true),
                    AcctBalChangeNotificationId = table.Column<long>(type: "bigint", nullable: true),
                    IssFinAuthNotificationId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountsInfo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccountsInfo_AcctBalChanges_AcctBalChangeNotificationId",
                        column: x => x.AcctBalChangeNotificationId,
                        principalSchema: "nepc",
                        principalTable: "AcctBalChanges",
                        principalColumn: "NotificationId");
                    table.ForeignKey(
                        name: "FK_AccountsInfo_IssFinAuths_IssFinAuthNotificationId",
                        column: x => x.IssFinAuthNotificationId,
                        principalSchema: "nepc",
                        principalTable: "IssFinAuths",
                        principalColumn: "NotificationId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccountsInfoLimitWrappers_AccountsInfoId",
                schema: "nepc",
                table: "AccountsInfoLimitWrappers",
                column: "AccountsInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountsInfoLimitWrappers_LimitId",
                schema: "nepc",
                table: "AccountsInfoLimitWrappers",
                column: "LimitId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountsInfo_AccountsInfoId_NotificationId",
                schema: "nepc",
                table: "AccountsInfo",
                columns: new[] { "AccountsInfoId", "NotificationId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AccountsInfo_AcctBalChangeNotificationId",
                schema: "nepc",
                table: "AccountsInfo",
                column: "AcctBalChangeNotificationId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountsInfo_IssFinAuthNotificationId",
                schema: "nepc",
                table: "AccountsInfo",
                column: "IssFinAuthNotificationId");

            migrationBuilder.AddForeignKey(
                name: "FK_AccountsInfoLimitWrappers_AccountsInfo_AccountsInfoId",
                schema: "nepc",
                table: "AccountsInfoLimitWrappers",
                column: "AccountsInfoId",
                principalSchema: "nepc",
                principalTable: "AccountsInfo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountsInfoLimitWrappers_AccountsInfo_AccountsInfoId",
                schema: "nepc",
                table: "AccountsInfoLimitWrappers");

            migrationBuilder.DropTable(
                name: "AccountsInfo",
                schema: "nepc");

            migrationBuilder.DropIndex(
                name: "IX_AccountsInfoLimitWrappers_AccountsInfoId",
                schema: "nepc",
                table: "AccountsInfoLimitWrappers");

            migrationBuilder.DropIndex(
                name: "IX_AccountsInfoLimitWrappers_LimitId",
                schema: "nepc",
                table: "AccountsInfoLimitWrappers");

            migrationBuilder.DropColumn(
                name: "Id",
                schema: "nepc",
                table: "Limits");

            migrationBuilder.DropColumn(
                name: "AccountsInfoId",
                schema: "nepc",
                table: "AccountsInfoLimitWrappers");

            migrationBuilder.DropPrimaryKey(
                name:"PK_AccountsInfoLimitWrappers",
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

            migrationBuilder.CreateTable(
                name: "AcctBalChangeAccountsInfos",
                schema: "nepc",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountsInfoId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    AcctBalChangeId = table.Column<long>(type: "bigint", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    AviableBalance_Amount = table.Column<decimal>(type: "decimal(15,2)", nullable: false),
                    AviableBalance_Currency = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    ExceedLimit_Amount = table.Column<decimal>(type: "decimal(15,2)", nullable: true),
                    ExceedLimit_Currency = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AcctBalChangeAccountsInfos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AcctBalChangeAccountsInfos_AcctBalChanges_AcctBalChangeId",
                        column: x => x.AcctBalChangeId,
                        principalSchema: "nepc",
                        principalTable: "AcctBalChanges",
                        principalColumn: "NotificationId");
                });

            migrationBuilder.CreateTable(
                name: "IssFinAuthAccountsInfos",
                schema: "nepc",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IssFinAuthId = table.Column<long>(type: "bigint", nullable: false),
                    AccountsInfoId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    AviableBalance_Amount = table.Column<decimal>(type: "decimal(15,2)", nullable: false),
                    AviableBalance_Currency = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    ExceedLimit_Amount = table.Column<decimal>(type: "decimal(15,2)", nullable: true),
                    ExceedLimit_Currency = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IssFinAuthAccountsInfos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IssFinAuthAccountsInfos_IssFinAuths_IssFinAuthId",
                        column: x => x.IssFinAuthId,
                        principalSchema: "nepc",
                        principalTable: "IssFinAuths",
                        principalColumn: "NotificationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccountsInfoLimitWrappers_LimitId",
                schema: "nepc",
                table: "AccountsInfoLimitWrappers",
                column: "LimitId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AcctBalChangeAccountsInfos_AcctBalChangeId",
                schema: "nepc",
                table: "AcctBalChangeAccountsInfos",
                column: "AcctBalChangeId");

            migrationBuilder.CreateIndex(
                name: "IX_IssFinAuthAccountsInfos_IssFinAuthId",
                schema: "nepc",
                table: "IssFinAuthAccountsInfos",
                column: "IssFinAuthId");

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
