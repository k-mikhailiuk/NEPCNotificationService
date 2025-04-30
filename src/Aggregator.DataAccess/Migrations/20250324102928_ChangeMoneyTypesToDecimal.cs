using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aggregator.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class ChangeMoneyTypesToDecimal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "UnholdMoney_Amount",
                schema: "nepc",
                table: "UnholdDetails",
                type: "decimal(15,2)",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<decimal>(
                name: "IssFee_Amount",
                schema: "nepc",
                table: "UnholdDetails",
                type: "decimal(15,2)",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "AuthMoney_Amount",
                schema: "nepc",
                table: "UnholdDetails",
                type: "decimal(15,2)",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<decimal>(
                name: "IssFee_Amount",
                schema: "nepc",
                table: "IssFinAuthDetails",
                type: "decimal(15,2)",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ConvMoney_Amount",
                schema: "nepc",
                table: "IssFinAuthDetails",
                type: "decimal(15,2)",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "BillingMoney_Amount",
                schema: "nepc",
                table: "IssFinAuthDetails",
                type: "decimal(15,2)",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "AuthMoney_Amount",
                schema: "nepc",
                table: "IssFinAuthDetails",
                type: "decimal(15,2)",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "AuthMoneyDetails_OwnFundsMoney_Amount",
                schema: "nepc",
                table: "IssFinAuthDetails",
                type: "decimal(15,2)",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "AuthMoneyDetails_ExceedLimitMoney_Amount",
                schema: "nepc",
                table: "IssFinAuthDetails",
                type: "decimal(15,2)",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "AcqFee_Amount",
                schema: "nepc",
                table: "IssFinAuthDetails",
                type: "decimal(15,2)",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "AccountBalance_Amount",
                schema: "nepc",
                table: "IssFinAuthDetails",
                type: "decimal(15,2)",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ExceedLimit_Amount",
                schema: "nepc",
                table: "IssFinAuthAccountsInfos",
                type: "decimal(15,2)",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "AviableBalance_Amount",
                schema: "nepc",
                table: "IssFinAuthAccountsInfos",
                type: "decimal(15,2)",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<decimal>(
                name: "TranMoney_Amount",
                schema: "nepc",
                table: "FinTransactions",
                type: "decimal(15,2)",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ConvMoney_Amount",
                schema: "nepc",
                table: "AcqFinAuthDetails",
                type: "decimal(15,2)",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "AuthMoney_Amount",
                schema: "nepc",
                table: "AcqFinAuthDetails",
                type: "decimal(15,2)",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<decimal>(
                name: "AcqFee_Amount",
                schema: "nepc",
                table: "AcqFinAuthDetails",
                type: "decimal(15,2)",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "AccountBalance_Amount",
                schema: "nepc",
                table: "AcctBalChangeDetails",
                type: "decimal(15,2)",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<decimal>(
                name: "AccountAmount_Amount",
                schema: "nepc",
                table: "AcctBalChangeDetails",
                type: "decimal(15,2)",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<decimal>(
                name: "ExceedLimit_Amount",
                schema: "nepc",
                table: "AcctBalChangeAccountsInfos",
                type: "decimal(15,2)",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "AviableBalance_Amount",
                schema: "nepc",
                table: "AcctBalChangeAccountsInfos",
                type: "decimal(15,2)",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "UnholdMoney_Amount",
                schema: "nepc",
                table: "UnholdDetails",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(15,2)");

            migrationBuilder.AlterColumn<long>(
                name: "IssFee_Amount",
                schema: "nepc",
                table: "UnholdDetails",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(15,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "AuthMoney_Amount",
                schema: "nepc",
                table: "UnholdDetails",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(15,2)");

            migrationBuilder.AlterColumn<long>(
                name: "IssFee_Amount",
                schema: "nepc",
                table: "IssFinAuthDetails",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(15,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "ConvMoney_Amount",
                schema: "nepc",
                table: "IssFinAuthDetails",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(15,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "BillingMoney_Amount",
                schema: "nepc",
                table: "IssFinAuthDetails",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(15,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "AuthMoney_Amount",
                schema: "nepc",
                table: "IssFinAuthDetails",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(15,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "AuthMoneyDetails_OwnFundsMoney_Amount",
                schema: "nepc",
                table: "IssFinAuthDetails",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(15,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "AuthMoneyDetails_ExceedLimitMoney_Amount",
                schema: "nepc",
                table: "IssFinAuthDetails",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(15,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "AcqFee_Amount",
                schema: "nepc",
                table: "IssFinAuthDetails",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(15,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "AccountBalance_Amount",
                schema: "nepc",
                table: "IssFinAuthDetails",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(15,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "ExceedLimit_Amount",
                schema: "nepc",
                table: "IssFinAuthAccountsInfos",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(15,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "AviableBalance_Amount",
                schema: "nepc",
                table: "IssFinAuthAccountsInfos",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(15,2)");

            migrationBuilder.AlterColumn<long>(
                name: "TranMoney_Amount",
                schema: "nepc",
                table: "FinTransactions",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(15,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "ConvMoney_Amount",
                schema: "nepc",
                table: "AcqFinAuthDetails",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(15,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "AuthMoney_Amount",
                schema: "nepc",
                table: "AcqFinAuthDetails",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(15,2)");

            migrationBuilder.AlterColumn<long>(
                name: "AcqFee_Amount",
                schema: "nepc",
                table: "AcqFinAuthDetails",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(15,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "AccountBalance_Amount",
                schema: "nepc",
                table: "AcctBalChangeDetails",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(15,2)");

            migrationBuilder.AlterColumn<long>(
                name: "AccountAmount_Amount",
                schema: "nepc",
                table: "AcctBalChangeDetails",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(15,2)");

            migrationBuilder.AlterColumn<long>(
                name: "ExceedLimit_Amount",
                schema: "nepc",
                table: "AcctBalChangeAccountsInfos",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(15,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "AviableBalance_Amount",
                schema: "nepc",
                table: "AcctBalChangeAccountsInfos",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(15,2)");
        }
    }
}
