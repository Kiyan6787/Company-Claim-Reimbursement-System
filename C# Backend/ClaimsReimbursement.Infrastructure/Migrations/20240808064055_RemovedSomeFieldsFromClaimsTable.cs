using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClaimsReimbursement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemovedSomeFieldsFromClaimsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReceiptAttached",
                table: "Reimbursements");

            migrationBuilder.DropColumn(
                name: "RequestPhase",
                table: "Reimbursements");

            migrationBuilder.AddColumn<string>(
                name: "ApprovedBy",
                table: "Reimbursements",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "InternalNotes",
                table: "Reimbursements",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApprovedBy",
                table: "Reimbursements");

            migrationBuilder.DropColumn(
                name: "InternalNotes",
                table: "Reimbursements");

            migrationBuilder.AddColumn<string>(
                name: "ReceiptAttached",
                table: "Reimbursements",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RequestPhase",
                table: "Reimbursements",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
