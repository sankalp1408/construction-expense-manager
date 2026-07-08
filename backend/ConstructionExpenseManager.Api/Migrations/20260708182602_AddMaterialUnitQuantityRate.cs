using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConstructionExpenseManager.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddMaterialUnitQuantityRate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Quantity",
                table: "PrivateWorkMaterials",
                type: "numeric(14,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Rate",
                table: "PrivateWorkMaterials",
                type: "numeric(14,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Unit",
                table: "PrivateWorkMaterials",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "PrivateWorkMaterials");

            migrationBuilder.DropColumn(
                name: "Rate",
                table: "PrivateWorkMaterials");

            migrationBuilder.DropColumn(
                name: "Unit",
                table: "PrivateWorkMaterials");
        }
    }
}
