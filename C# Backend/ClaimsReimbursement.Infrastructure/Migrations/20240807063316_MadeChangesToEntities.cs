using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ClaimsReimbursement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MadeChangesToEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Currency",
                table: "Reimbursements");

            migrationBuilder.DropColumn(
                name: "ReimbursementType",
                table: "Reimbursements");

            migrationBuilder.AddColumn<int>(
                name: "CurrencyId",
                table: "Reimbursements",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ReimbursementTypeId",
                table: "Reimbursements",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Currencies",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currencies", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ReimbursementTypes",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReimbursementTypes", x => x.ID);
                });

            migrationBuilder.InsertData(
                table: "Currencies",
                columns: new[] { "ID", "Code" },
                values: new object[,]
                {
                    { 1, "USD" },
                    { 2, "EUR" },
                    { 3, "INR" },
                    { 4, "ZAR" }
                });

            migrationBuilder.InsertData(
                table: "ReimbursementTypes",
                columns: new[] { "ID", "Type" },
                values: new object[,]
                {
                    { 1, "Travel" },
                    { 2, "Medical" },
                    { 3, "Food" },
                    { 4, "Entertainment" },
                    { 5, "Miscellaneous" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reimbursements_CurrencyId",
                table: "Reimbursements",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_Reimbursements_ReimbursementTypeId",
                table: "Reimbursements",
                column: "ReimbursementTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reimbursements_Currencies_CurrencyId",
                table: "Reimbursements",
                column: "CurrencyId",
                principalTable: "Currencies",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reimbursements_ReimbursementTypes_ReimbursementTypeId",
                table: "Reimbursements",
                column: "ReimbursementTypeId",
                principalTable: "ReimbursementTypes",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reimbursements_Currencies_CurrencyId",
                table: "Reimbursements");

            migrationBuilder.DropForeignKey(
                name: "FK_Reimbursements_ReimbursementTypes_ReimbursementTypeId",
                table: "Reimbursements");

            migrationBuilder.DropTable(
                name: "Currencies");

            migrationBuilder.DropTable(
                name: "ReimbursementTypes");

            migrationBuilder.DropIndex(
                name: "IX_Reimbursements_CurrencyId",
                table: "Reimbursements");

            migrationBuilder.DropIndex(
                name: "IX_Reimbursements_ReimbursementTypeId",
                table: "Reimbursements");

            migrationBuilder.DropColumn(
                name: "CurrencyId",
                table: "Reimbursements");

            migrationBuilder.DropColumn(
                name: "ReimbursementTypeId",
                table: "Reimbursements");

            migrationBuilder.AddColumn<string>(
                name: "Currency",
                table: "Reimbursements",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ReimbursementType",
                table: "Reimbursements",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
