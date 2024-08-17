using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClaimsReimbursement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemovedApprovedClaims : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApprovedClaims");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApprovedClaims",
                columns: table => new
                {
                    ApprovedClaimId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ID = table.Column<int>(type: "int", nullable: false),
                    ApprovedAmount = table.Column<int>(type: "int", nullable: false),
                    ApprovedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InternalNotes = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApprovedClaims", x => x.ApprovedClaimId);
                    table.ForeignKey(
                        name: "FK_ApprovedClaims_Reimbursements_ID",
                        column: x => x.ID,
                        principalTable: "Reimbursements",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApprovedClaims_ID",
                table: "ApprovedClaims",
                column: "ID");
        }
    }
}
