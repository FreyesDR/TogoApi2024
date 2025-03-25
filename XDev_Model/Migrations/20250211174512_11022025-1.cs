using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XDev_Model.Migrations
{
    /// <inheritdoc />
    public partial class _110220251 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "SaleOrderId",
                table: "EBillingLog",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "8c5b29e5-fbb8-4cc9-871a-d61aaf739bf5",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "9990bf35-0011-4c1e-91a2-bdbcfa92f142", "9dc55d29-9bde-45d6-b446-6a28223aee7c" });

            migrationBuilder.CreateIndex(
                name: "IX_EBillingLog_InvoiceId",
                table: "EBillingLog",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_EBillingLog_SaleOrderId",
                table: "EBillingLog",
                column: "SaleOrderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_EBillingLog_InvoiceId",
                table: "EBillingLog");

            migrationBuilder.DropIndex(
                name: "IX_EBillingLog_SaleOrderId",
                table: "EBillingLog");

            migrationBuilder.DropColumn(
                name: "SaleOrderId",
                table: "EBillingLog");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "8c5b29e5-fbb8-4cc9-871a-d61aaf739bf5",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "1b270f14-9d40-4fb7-8648-ddebe490a436", "73c3ce53-c561-4b64-badc-d6ac8dab223a" });
        }
    }
}
