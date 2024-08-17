using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClaimsReimbursement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemovedBaseEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ID",
                table: "ReimbursementTypes",
                newName: "ReimbursementTypeId");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Currencies",
                newName: "CurrencyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ReimbursementTypeId",
                table: "ReimbursementTypes",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "CurrencyId",
                table: "Currencies",
                newName: "ID");
        }
    }
}
