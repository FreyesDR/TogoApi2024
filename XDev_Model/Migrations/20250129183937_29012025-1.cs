using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XDev_Model.Migrations
{
    /// <inheritdoc />
    public partial class _290120251 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InvoicePayment",
                columns: table => new
                {
                    InvoiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MeanOfPaymentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Position = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Reference = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Tipo = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: true),
                    Plazo = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: true),
                    Periodo = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoicePayment", x => new { x.InvoiceId, x.MeanOfPaymentId, x.Position });
                    table.ForeignKey(
                        name: "FK_InvoicePayment_Invoice_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "Invoice",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "8c5b29e5-fbb8-4cc9-871a-d61aaf739bf5",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "bad6ab29-f131-4a69-8c19-ac3ff66da28e", "03462f5b-78d0-4556-b0e9-9c8cf39e01eb" });

            migrationBuilder.CreateIndex(
                name: "IX_SaleOrder_PaymentConditionId",
                table: "SaleOrder",
                column: "PaymentConditionId");

            migrationBuilder.CreateIndex(
                name: "IX_SaleOrder_SaleOrderTypeId",
                table: "SaleOrder",
                column: "SaleOrderTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_InvoiceTypeId",
                table: "Invoice",
                column: "InvoiceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_PaymentConditionId",
                table: "Invoice",
                column: "PaymentConditionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoice_InvoiceType_InvoiceTypeId",
                table: "Invoice",
                column: "InvoiceTypeId",
                principalTable: "InvoiceType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Invoice_PaymentCondition_PaymentConditionId",
                table: "Invoice",
                column: "PaymentConditionId",
                principalTable: "PaymentCondition",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SaleOrder_PaymentCondition_PaymentConditionId",
                table: "SaleOrder",
                column: "PaymentConditionId",
                principalTable: "PaymentCondition",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SaleOrder_SaleOrderType_SaleOrderTypeId",
                table: "SaleOrder",
                column: "SaleOrderTypeId",
                principalTable: "SaleOrderType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoice_InvoiceType_InvoiceTypeId",
                table: "Invoice");

            migrationBuilder.DropForeignKey(
                name: "FK_Invoice_PaymentCondition_PaymentConditionId",
                table: "Invoice");

            migrationBuilder.DropForeignKey(
                name: "FK_SaleOrder_PaymentCondition_PaymentConditionId",
                table: "SaleOrder");

            migrationBuilder.DropForeignKey(
                name: "FK_SaleOrder_SaleOrderType_SaleOrderTypeId",
                table: "SaleOrder");

            migrationBuilder.DropTable(
                name: "InvoicePayment");

            migrationBuilder.DropIndex(
                name: "IX_SaleOrder_PaymentConditionId",
                table: "SaleOrder");

            migrationBuilder.DropIndex(
                name: "IX_SaleOrder_SaleOrderTypeId",
                table: "SaleOrder");

            migrationBuilder.DropIndex(
                name: "IX_Invoice_InvoiceTypeId",
                table: "Invoice");

            migrationBuilder.DropIndex(
                name: "IX_Invoice_PaymentConditionId",
                table: "Invoice");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "8c5b29e5-fbb8-4cc9-871a-d61aaf739bf5",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "1c25ac70-6b12-43d7-ab3c-a9c8f02fdc4e", "0752dd23-2fe2-4643-9e3c-6c6172c78367" });
        }
    }
}
