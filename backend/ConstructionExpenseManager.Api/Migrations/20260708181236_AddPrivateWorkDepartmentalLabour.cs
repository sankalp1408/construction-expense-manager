using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ConstructionExpenseManager.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddPrivateWorkDepartmentalLabour : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PrivateWorkDepartmentalLabours",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PrivateWorkId = table.Column<int>(type: "integer", nullable: false),
                    LabourDate = table.Column<DateTime>(type: "date", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrivateWorkDepartmentalLabours", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PrivateWorkDepartmentalLabours_PrivateWorks_PrivateWorkId",
                        column: x => x.PrivateWorkId,
                        principalTable: "PrivateWorks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PrivateWorkDepartmentalLabourRows",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DepartmentalLabourId = table.Column<int>(type: "integer", nullable: false),
                    LabourType = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Count = table.Column<int>(type: "integer", nullable: false),
                    Rate = table.Column<decimal>(type: "numeric(14,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrivateWorkDepartmentalLabourRows", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PrivateWorkDepartmentalLabourRows_PrivateWorkDepartmentalLa~",
                        column: x => x.DepartmentalLabourId,
                        principalTable: "PrivateWorkDepartmentalLabours",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PrivateWorkDepartmentalLabourRows_DepartmentalLabourId",
                table: "PrivateWorkDepartmentalLabourRows",
                column: "DepartmentalLabourId");

            migrationBuilder.CreateIndex(
                name: "IX_PrivateWorkDepartmentalLabours_PrivateWorkId",
                table: "PrivateWorkDepartmentalLabours",
                column: "PrivateWorkId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PrivateWorkDepartmentalLabourRows");

            migrationBuilder.DropTable(
                name: "PrivateWorkDepartmentalLabours");
        }
    }
}
