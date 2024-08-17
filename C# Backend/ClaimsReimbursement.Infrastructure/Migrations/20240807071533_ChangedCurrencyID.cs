using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClaimsReimbursement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangedCurrencyID : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reimbursements_Currencies_CurrencyId",
                table: "Reimbursements");

            migrationBuilder.RenameColumn(
                name: "CurrencyId",
                table: "Reimbursements",
                newName: "ID1");

            migrationBuilder.RenameIndex(
                name: "IX_Reimbursements_CurrencyId",
                table: "Reimbursements",
                newName: "IX_Reimbursements_ID1");

            migrationBuilder.AddForeignKey(
                name: "FK_Reimbursements_Currencies_ID1",
                table: "Reimbursements",
                column: "ID1",
                principalTable: "Currencies",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reimbursements_Currencies_ID1",
                table: "Reimbursements");

            migrationBuilder.RenameColumn(
                name: "ID1",
                table: "Reimbursements",
                newName: "CurrencyId");

            migrationBuilder.RenameIndex(
                name: "IX_Reimbursements_ID1",
                table: "Reimbursements",
                newName: "IX_Reimbursements_CurrencyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reimbursements_Currencies_CurrencyId",
                table: "Reimbursements",
                column: "CurrencyId",
                principalTable: "Currencies",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
