using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XDev_Model.Migrations
{
    /// <inheritdoc />
    public partial class _230120252 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Invoice",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Number = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InvoiceTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PartnerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CurrencyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NetAmount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    TaxAmount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PointSaleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RefDocument = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    RefDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Sporadic = table.Column<bool>(type: "bit", nullable: false),
                    Per1 = table.Column<decimal>(type: "decimal(18,5)", precision: 18, scale: 5, nullable: false),
                    Ret1 = table.Column<decimal>(type: "decimal(18,5)", precision: 18, scale: 5, nullable: false),
                    Ret10 = table.Column<decimal>(type: "decimal(18,5)", precision: 18, scale: 5, nullable: false),
                    SaleOrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Canceled = table.Column<bool>(type: "bit", nullable: false),
                    CanceledDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CanceledUserId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    NumControl = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CodGeneración = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    SelloRecepcion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    LastUpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoice", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InvoicePosition",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InvoiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Position = table.Column<int>(type: "int", nullable: false),
                    MaterialId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MaterialTypeCode = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: true),
                    MaterialCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    MaterialName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    PriceType = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: true),
                    UnitMeasureId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UnitMeasureCode = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: true),
                    Quantity = table.Column<decimal>(type: "decimal(18,3)", precision: 18, scale: 3, nullable: false),
                    DiscountAmount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    TaxAmount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    NetAmount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    WareHouseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoicePosition", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InvoicePosition_Invoice_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "Invoice",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InvoiceSporadicPartner",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InvoiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IDTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IDNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CountryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    RegionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CityId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceSporadicPartner", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InvoiceSporadicPartner_Invoice_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "Invoice",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InvoicePositionCondition",
                columns: table => new
                {
                    InvoicePositionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PriceConditionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Position = table.Column<int>(type: "int", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    Type = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: true),
                    Source = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: true),
                    SourceConditionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Value = table.Column<decimal>(type: "decimal(18,7)", precision: 18, scale: 7, nullable: false),
                    ValueType = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: true),
                    Edit = table.Column<bool>(type: "bit", nullable: false),
                    BaseCondition = table.Column<decimal>(type: "decimal(18,7)", precision: 18, scale: 7, nullable: false),
                    ValueCondition = table.Column<decimal>(type: "decimal(18,7)", precision: 18, scale: 7, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoicePositionCondition", x => new { x.InvoicePositionId, x.PriceConditionId });
                    table.ForeignKey(
                        name: "FK_InvoicePositionCondition_InvoicePosition_InvoicePositionId",
                        column: x => x.InvoicePositionId,
                        principalTable: "InvoicePosition",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "8c5b29e5-fbb8-4cc9-871a-d61aaf739bf5",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "dc7896ef-35f1-43ed-b200-a4fd1de1d220", "e4f3a68f-ded5-44a2-865a-2d587f1ba7e4" });

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_Number",
                table: "Invoice",
                column: "Number",
                unique: true,
                filter: "[Number] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_InvoicePosition_InvoiceId",
                table: "InvoicePosition",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoicePositionCondition_Code",
                table: "InvoicePositionCondition",
                column: "Code");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceSporadicPartner_InvoiceId",
                table: "InvoiceSporadicPartner",
                column: "InvoiceId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InvoicePositionCondition");

            migrationBuilder.DropTable(
                name: "InvoiceSporadicPartner");

            migrationBuilder.DropTable(
                name: "InvoicePosition");

            migrationBuilder.DropTable(
                name: "Invoice");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "8c5b29e5-fbb8-4cc9-871a-d61aaf739bf5",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "1118fd0b-feb3-4444-b293-50907557cea5", "27aa74fc-cd74-4e93-a446-142260b5f3d5" });
        }
    }
}
