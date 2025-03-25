using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XDev_Model.Migrations
{
    /// <inheritdoc />
    public partial class _230120254 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "EBilling",
                type: "nvarchar(4)",
                maxLength: 4,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "EBilling",
                keyColumn: "Id",
                keyValue: new Guid("63be85df-6805-41c3-beb2-f6a44db746f6"),
                column: "Code",
                value: "MHSV");

            migrationBuilder.UpdateData(
                table: "PartnerRole",
                keyColumn: "Id",
                keyValue: new Guid("099a35d5-fddf-486d-b3b7-ba8011b1a7ff"),
                column: "Name",
                value: "Proveedor (Acreedor)");

            migrationBuilder.UpdateData(
                table: "PartnerRole",
                keyColumn: "Id",
                keyValue: new Guid("cf579d9d-18f9-432b-806a-4cad7311fb38"),
                column: "Name",
                value: "Cliente (Deudor)");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "8c5b29e5-fbb8-4cc9-871a-d61aaf739bf5",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "a656964f-b6b7-4265-9eda-587225df7da6", "aafe72f5-dcf7-49fa-a02a-5031e863243e" });

            migrationBuilder.CreateIndex(
                name: "IX_EBilling_Code",
                table: "EBilling",
                column: "Code",
                unique: true,
                filter: "[Code] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_EBilling_Code",
                table: "EBilling");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "EBilling");

            migrationBuilder.UpdateData(
                table: "PartnerRole",
                keyColumn: "Id",
                keyValue: new Guid("099a35d5-fddf-486d-b3b7-ba8011b1a7ff"),
                column: "Name",
                value: "Acreedor");

            migrationBuilder.UpdateData(
                table: "PartnerRole",
                keyColumn: "Id",
                keyValue: new Guid("cf579d9d-18f9-432b-806a-4cad7311fb38"),
                column: "Name",
                value: "Deudor");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "8c5b29e5-fbb8-4cc9-871a-d61aaf739bf5",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "fe056ec6-6d24-4f5a-b36d-c4868084f7bd", "a45440b6-fba0-4878-a7d7-516eb8c9b7d1" });
        }
    }
}
