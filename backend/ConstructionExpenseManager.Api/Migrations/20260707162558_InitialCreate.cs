using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ConstructionExpenseManager.Api.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CommissionWorks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    WorkName = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    PartyName = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    TenderWorkAmount = table.Column<decimal>(type: "numeric(14,2)", nullable: false),
                    CommissionPercent = table.Column<decimal>(type: "numeric(5,2)", nullable: false),
                    GstAmount = table.Column<decimal>(type: "numeric(14,2)", nullable: false),
                    BilledGst = table.Column<decimal>(type: "numeric(14,2)", nullable: false),
                    ExtraGstBill = table.Column<decimal>(type: "numeric(14,2)", nullable: false),
                    GstBillCommission = table.Column<decimal>(type: "numeric(14,2)", nullable: false),
                    CreatedByUserId = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommissionWorks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GstVendorEntries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    WorkType = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    WorkId = table.Column<int>(type: "integer", nullable: false),
                    VendorName = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    GstBillAmount = table.Column<decimal>(type: "numeric(14,2)", nullable: false),
                    SentDate = table.Column<DateTime>(type: "date", nullable: false),
                    CommissionPercent = table.Column<decimal>(type: "numeric(5,2)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GstVendorEntries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PrivateWorks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ClientName = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    WorkDescription = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    AreaSqft = table.Column<decimal>(type: "numeric(14,2)", nullable: false),
                    RatePerSqft = table.Column<decimal>(type: "numeric(14,2)", nullable: false),
                    CreatedByUserId = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrivateWorks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TenderWorks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TenderName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    NameOfWork = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    TenderAmount = table.Column<decimal>(type: "numeric(14,2)", nullable: false),
                    TenderFee = table.Column<decimal>(type: "numeric(14,2)", nullable: false),
                    TenderEMD = table.Column<decimal>(type: "numeric(14,2)", nullable: false),
                    TenderFilingAmount = table.Column<decimal>(type: "numeric(14,2)", nullable: false),
                    GstTotal = table.Column<decimal>(type: "numeric(14,2)", nullable: false),
                    BilledGst = table.Column<decimal>(type: "numeric(14,2)", nullable: false),
                    ExtraGstBill = table.Column<decimal>(type: "numeric(14,2)", nullable: false),
                    WorkExpenditure = table.Column<decimal>(type: "numeric(14,2)", nullable: false),
                    SecurityDepositPercent = table.Column<decimal>(type: "numeric(5,2)", nullable: false),
                    OfficeProtocolPercent = table.Column<decimal>(type: "numeric(5,2)", nullable: false),
                    CorporatorName = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    CorporatorProtocolPercent = table.Column<decimal>(type: "numeric(5,2)", nullable: false),
                    GstBillCommission = table.Column<decimal>(type: "numeric(14,2)", nullable: false),
                    CreatedByUserId = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TenderWorks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: false),
                    MobileNumber = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    Role = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GstVendorRepayments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    GstVendorEntryId = table.Column<int>(type: "integer", nullable: false),
                    ReceivedDate = table.Column<DateTime>(type: "date", nullable: false),
                    AmountReceived = table.Column<decimal>(type: "numeric(14,2)", nullable: false),
                    Mode = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GstVendorRepayments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GstVendorRepayments_GstVendorEntries_GstVendorEntryId",
                        column: x => x.GstVendorEntryId,
                        principalTable: "GstVendorEntries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PrivateWorkCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PrivateWorkId = table.Column<int>(type: "integer", nullable: false),
                    CategoryName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    WorkerName = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    RateBasis = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    AgreedTotalAmount = table.Column<decimal>(type: "numeric(14,2)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrivateWorkCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PrivateWorkCategories_PrivateWorks_PrivateWorkId",
                        column: x => x.PrivateWorkId,
                        principalTable: "PrivateWorks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PrivateWorkMaterials",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PrivateWorkId = table.Column<int>(type: "integer", nullable: false),
                    MaterialName = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    VendorName = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Amount = table.Column<decimal>(type: "numeric(14,2)", nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "date", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrivateWorkMaterials", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PrivateWorkMaterials_PrivateWorks_PrivateWorkId",
                        column: x => x.PrivateWorkId,
                        principalTable: "PrivateWorks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PrivateWorkMilestones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PrivateWorkId = table.Column<int>(type: "integer", nullable: false),
                    StageName = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    PercentOfTotal = table.Column<decimal>(type: "numeric(5,2)", nullable: false),
                    PaidAmount = table.Column<decimal>(type: "numeric(14,2)", nullable: false),
                    PaidDate = table.Column<DateTime>(type: "date", nullable: true),
                    Status = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    SortOrder = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrivateWorkMilestones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PrivateWorkMilestones_PrivateWorks_PrivateWorkId",
                        column: x => x.PrivateWorkId,
                        principalTable: "PrivateWorks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TenderRaBills",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TenderWorkId = table.Column<int>(type: "integer", nullable: false),
                    RaBillNumber = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    BillDate = table.Column<DateTime>(type: "date", nullable: false),
                    BilledAmount = table.Column<decimal>(type: "numeric(14,2)", nullable: false),
                    CorporatorCommissionPercent = table.Column<decimal>(type: "numeric(5,2)", nullable: false),
                    OfficerCommissionPercent = table.Column<decimal>(type: "numeric(5,2)", nullable: false),
                    Remarks = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TenderRaBills", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TenderRaBills_TenderWorks_TenderWorkId",
                        column: x => x.TenderWorkId,
                        principalTable: "TenderWorks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PrivateWorkCategoryPayments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CategoryId = table.Column<int>(type: "integer", nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "date", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric(14,2)", nullable: false),
                    Remarks = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrivateWorkCategoryPayments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PrivateWorkCategoryPayments_PrivateWorkCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "PrivateWorkCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GstVendorEntries_WorkType_WorkId",
                table: "GstVendorEntries",
                columns: new[] { "WorkType", "WorkId" });

            migrationBuilder.CreateIndex(
                name: "IX_GstVendorRepayments_GstVendorEntryId",
                table: "GstVendorRepayments",
                column: "GstVendorEntryId");

            migrationBuilder.CreateIndex(
                name: "IX_PrivateWorkCategories_PrivateWorkId",
                table: "PrivateWorkCategories",
                column: "PrivateWorkId");

            migrationBuilder.CreateIndex(
                name: "IX_PrivateWorkCategoryPayments_CategoryId",
                table: "PrivateWorkCategoryPayments",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_PrivateWorkMaterials_PrivateWorkId",
                table: "PrivateWorkMaterials",
                column: "PrivateWorkId");

            migrationBuilder.CreateIndex(
                name: "IX_PrivateWorkMilestones_PrivateWorkId",
                table: "PrivateWorkMilestones",
                column: "PrivateWorkId");

            migrationBuilder.CreateIndex(
                name: "IX_TenderRaBills_TenderWorkId",
                table: "TenderRaBills",
                column: "TenderWorkId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_MobileNumber",
                table: "Users",
                column: "MobileNumber",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CommissionWorks");

            migrationBuilder.DropTable(
                name: "GstVendorRepayments");

            migrationBuilder.DropTable(
                name: "PrivateWorkCategoryPayments");

            migrationBuilder.DropTable(
                name: "PrivateWorkMaterials");

            migrationBuilder.DropTable(
                name: "PrivateWorkMilestones");

            migrationBuilder.DropTable(
                name: "TenderRaBills");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "GstVendorEntries");

            migrationBuilder.DropTable(
                name: "PrivateWorkCategories");

            migrationBuilder.DropTable(
                name: "TenderWorks");

            migrationBuilder.DropTable(
                name: "PrivateWorks");
        }
    }
}
