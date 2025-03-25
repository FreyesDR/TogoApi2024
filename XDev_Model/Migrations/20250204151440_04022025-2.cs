using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XDev_Model.Migrations
{
    /// <inheritdoc />
    public partial class _040220252 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AltCode",
                table: "SaleOrderPositionCondition",
                type: "nvarchar(4)",
                maxLength: 4,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AltCode",
                table: "PriceCondition",
                type: "nvarchar(4)",
                maxLength: 4,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AltCode",
                table: "InvoicePositionCondition",
                type: "nvarchar(4)",
                maxLength: 4,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "EBillingTax",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EBillingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TaxCode = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: true),
                    TaxName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    LastUpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EBillingTax", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EBillingTax_EBilling_EBillingId",
                        column: x => x.EBillingId,
                        principalTable: "EBilling",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "8c5b29e5-fbb8-4cc9-871a-d61aaf739bf5",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "1e868c0d-b778-45ea-9f88-98a97bf83c58", "071d9333-cdc4-49d1-838b-49be08c80cdf" });

            migrationBuilder.CreateIndex(
                name: "IX_EBillingTax_EBillingId",
                table: "EBillingTax",
                column: "EBillingId");

            migrationBuilder.CreateIndex(
                name: "IX_EBillingTax_TaxCode",
                table: "EBillingTax",
                column: "TaxCode");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EBillingTax");

            migrationBuilder.DropColumn(
                name: "AltCode",
                table: "SaleOrderPositionCondition");

            migrationBuilder.DropColumn(
                name: "AltCode",
                table: "PriceCondition");

            migrationBuilder.DropColumn(
                name: "AltCode",
                table: "InvoicePositionCondition");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "8c5b29e5-fbb8-4cc9-871a-d61aaf739bf5",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "468076e7-c80b-47ea-ac4a-626df2867872", "7175ba9e-0b42-46f2-b471-03b975920abd" });
        }
    }
}
