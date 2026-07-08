using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConstructionExpenseManager.Api.Migrations
{
    /// <inheritdoc />
    public partial class RemoveOfficeProtocolPercentFromTender : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OfficeProtocolPercent",
                table: "TenderWorks");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "OfficeProtocolPercent",
                table: "TenderWorks",
                type: "numeric(5,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
