using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aggregator.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class ChangeDetailsIdIdentityGeneration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TokenStatusChangeDetails",
                schema: "nepc",
                table: "TokenStatusChangeDetails");
            migrationBuilder.DropColumn(
                name: "TokenStatusChangeDetailsId",
                schema: "nepc",
                table: "TokenStatusChangeDetails");

            migrationBuilder.AddColumn<long>(
                name: "TokenStatusChangeDetailsId",
                schema: "nepc",
                table: "TokenStatusChangeDetails",
                type: "bigint",
                nullable: false)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TokenStatusChangeDetails",
                schema: "nepc",
                table: "TokenStatusChangeDetails",
                column: "TokenStatusChangeDetailsId");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PinChangeDetails",
                schema: "nepc",
                table: "PinChangeDetails");
            migrationBuilder.DropColumn(
                name: "PinChangeDetailsId",
                schema: "nepc",
                table: "PinChangeDetails");
            migrationBuilder.AddColumn<long>(
                name: "PinChangeDetailsId",
                schema: "nepc",
                table: "PinChangeDetails",
                type: "bigint",
                nullable: false)
                .Annotation("SqlServer:Identity", "1, 1");
            migrationBuilder.AddPrimaryKey(
                name: "PK_PinChangeDetails",
                schema: "nepc",
                table: "PinChangeDetails",
                column: "PinChangeDetailsId");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OwiUserActionDetails",
                schema: "nepc",
                table: "OwiUserActionDetails");
            migrationBuilder.DropColumn(
                name: "OwiUserActionDetailsId",
                schema: "nepc",
                table: "OwiUserActionDetails");
            migrationBuilder.AddColumn<long>(
                name: "OwiUserActionDetailsId",
                schema: "nepc",
                table: "OwiUserActionDetails",
                type: "bigint",
                nullable: false)
                .Annotation("SqlServer:Identity", "1, 1");
            migrationBuilder.AddPrimaryKey(
                name: "PK_OwiUserActionDetails",
                schema: "nepc",
                table: "OwiUserActionDetails",
                column: "OwiUserActionDetailsId");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CardStatusChangeDetails",
                schema: "nepc",
                table: "CardStatusChangeDetails");
            migrationBuilder.DropColumn(
                name: "CardStatusChangeDetailsId",
                schema: "nepc",
                table: "CardStatusChangeDetails");
            migrationBuilder.AddColumn<long>(
                name: "CardStatusChangeDetailsId",
                schema: "nepc",
                table: "CardStatusChangeDetails",
                type: "bigint",
                nullable: false)
                .Annotation("SqlServer:Identity", "1, 1");
            migrationBuilder.AddPrimaryKey(
                name: "PK_CardStatusChangeDetails",
                schema: "nepc",
                table: "CardStatusChangeDetails",
                column: "CardStatusChangeDetailsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TokenStatusChangeDetails",
                schema: "nepc",
                table: "TokenStatusChangeDetails");
            migrationBuilder.DropColumn(
                name: "TokenStatusChangeDetailsId",
                schema: "nepc",
                table: "TokenStatusChangeDetails");
            migrationBuilder.AddColumn<long>(
                name: "TokenStatusChangeDetailsId",
                schema: "nepc",
                table: "TokenStatusChangeDetails",
                type: "bigint",
                nullable: false)
                .Annotation("SqlServer:Identity", "1, 1");
            migrationBuilder.AddPrimaryKey(
                name: "PK_TokenStatusChangeDetails",
                schema: "nepc",
                table: "TokenStatusChangeDetails",
                column: "TokenStatusChangeDetailsId");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PinChangeDetails",
                schema: "nepc",
                table: "PinChangeDetails");
            migrationBuilder.DropColumn(
                name: "PinChangeDetailsId",
                schema: "nepc",
                table: "PinChangeDetails");
            migrationBuilder.AddColumn<long>(
                name: "PinChangeDetailsId",
                schema: "nepc",
                table: "PinChangeDetails",
                type: "bigint",
                nullable: false)
                .Annotation("SqlServer:Identity", "1, 1");
            migrationBuilder.AddPrimaryKey(
                name: "PK_PinChangeDetails",
                schema: "nepc",
                table: "PinChangeDetails",
                column: "PinChangeDetailsId");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OwiUserActionDetails",
                schema: "nepc",
                table: "OwiUserActionDetails");
            migrationBuilder.DropColumn(
                name: "OwiUserActionDetailsId",
                schema: "nepc",
                table: "OwiUserActionDetails");
            migrationBuilder.AddColumn<long>(
                name: "OwiUserActionDetailsId",
                schema: "nepc",
                table: "OwiUserActionDetails",
                type: "bigint",
                nullable: false)
                .Annotation("SqlServer:Identity", "1, 1");
            migrationBuilder.AddPrimaryKey(
                name: "PK_OwiUserActionDetails",
                schema: "nepc",
                table: "OwiUserActionDetails",
                column: "OwiUserActionDetailsId");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CardStatusChangeDetails",
                schema: "nepc",
                table: "CardStatusChangeDetails");
            migrationBuilder.DropColumn(
                name: "CardStatusChangeDetailsId",
                schema: "nepc",
                table: "CardStatusChangeDetails");
            migrationBuilder.AddColumn<long>(
                name: "CardStatusChangeDetailsId",
                schema: "nepc",
                table: "CardStatusChangeDetails",
                type: "bigint",
                nullable: false)
                .Annotation("SqlServer:Identity", "1, 1");
            migrationBuilder.AddPrimaryKey(
                name: "PK_CardStatusChangeDetails",
                schema: "nepc",
                table: "CardStatusChangeDetails",
                column: "CardStatusChangeDetailsId");
        }
    }
}
