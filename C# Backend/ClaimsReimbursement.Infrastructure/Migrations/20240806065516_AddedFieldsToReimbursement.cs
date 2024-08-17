using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClaimsReimbursement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedFieldsToReimbursement : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ApprovedValue",
                table: "Reimbursements",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsApproved",
                table: "Reimbursements",
                type: "bit",
                nullable: false,
                defaultValue: false);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApprovedValue",
                table: "Reimbursements");

            migrationBuilder.DropColumn(
                name: "IsApproved",
                table: "Reimbursements");

            migrationBuilder.DropColumn(
                name: "ReceiptAttached",
                table: "Reimbursements");

            migrationBuilder.DropColumn(
                name: "RequestPhase",
                table: "Reimbursements");
        }
    }
}
