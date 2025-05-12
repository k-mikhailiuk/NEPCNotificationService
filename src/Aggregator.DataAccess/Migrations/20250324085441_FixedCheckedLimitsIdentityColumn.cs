using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aggregator.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class FixedCheckedLimitsIdentityColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CheckedLimits",
                schema: "nepc",
                table: "CheckedLimits");

            migrationBuilder.DropColumn(
                name: "Id",
                schema: "nepc",
                table: "CheckedLimits");

            migrationBuilder.AddColumn<long>(
                name: "Id",
                schema: "nepc",
                table: "CheckedLimits",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CheckedLimits",
                schema: "nepc",
                table: "CheckedLimits",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CheckedLimits",
                schema: "nepc",
                table: "CheckedLimits");

            migrationBuilder.DropColumn(
                name: "Id",
                schema: "nepc",
                table: "CheckedLimits");

            migrationBuilder.AddColumn<long>(
                name: "Id",
                schema: "nepc",
                table: "CheckedLimits",
                type: "bigint",
                nullable: false,
                defaultValue: 0L)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CheckedLimits",
                schema: "nepc",
                table: "CheckedLimits",
                column: "Id");
        }
    }
}
