using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aggregator.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class ChangedFinTransRelative : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AcctBalChangeDetails_FinTransId",
                schema: "nepc",
                table: "AcctBalChangeDetails");

            migrationBuilder.CreateIndex(
                name: "IX_AcctBalChangeDetails_FinTransId",
                schema: "nepc",
                table: "AcctBalChangeDetails",
                column: "FinTransId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AcctBalChangeDetails_FinTransId",
                schema: "nepc",
                table: "AcctBalChangeDetails");

            migrationBuilder.CreateIndex(
                name: "IX_AcctBalChangeDetails_FinTransId",
                schema: "nepc",
                table: "AcctBalChangeDetails",
                column: "FinTransId",
                unique: true,
                filter: "[FinTransId] IS NOT NULL");
        }
    }
}
