using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace XDev_Model.Migrations
{
    /// <inheritdoc />
    public partial class _040220253 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "EBillingTax",
                columns: new[] { "Id", "ConcurrencyStamp", "CreatedAt", "CreatedBy", "EBillingId", "LastUpdatedAt", "LastUpdatedBy", "TaxCode", "TaxName" },
                values: new object[,]
                {
                    { new Guid("5714c5db-4033-4c8f-a425-13186fb02220"), "5714c5db-4033-4c8f-a425-13186fb02220", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new Guid("63be85df-6805-41c3-beb2-f6a44db746f6"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "C3", "Impuesto al valor agregado (exportaciones)" },
                    { new Guid("6b1fe653-82f8-43f5-a674-c4d5cba0b1b9"), "6b1fe653-82f8-43f5-a674-c4d5cba0b1b9", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new Guid("63be85df-6805-41c3-beb2-f6a44db746f6"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "20", "Impuesto al valor agregado" },
                    { new Guid("845ee146-38ca-4041-90f4-b8411274fa15"), "845ee146-38ca-4041-90f4-b8411274fa15", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new Guid("63be85df-6805-41c3-beb2-f6a44db746f6"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "59", "Turismo: por alojamiento" },
                    { new Guid("8507b0ca-b579-4c2a-90fc-d8ec516ba909"), "8507b0ca-b579-4c2a-90fc-d8ec516ba909", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new Guid("63be85df-6805-41c3-beb2-f6a44db746f6"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "71", "Turismo: salida del país por vía aérea" }
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "8c5b29e5-fbb8-4cc9-871a-d61aaf739bf5",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "8af41a83-cd90-4234-9253-716348f2c61f", "11413e0e-93d5-4e39-a396-8b619e54c0c9" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "EBillingTax",
                keyColumn: "Id",
                keyValue: new Guid("5714c5db-4033-4c8f-a425-13186fb02220"));

            migrationBuilder.DeleteData(
                table: "EBillingTax",
                keyColumn: "Id",
                keyValue: new Guid("6b1fe653-82f8-43f5-a674-c4d5cba0b1b9"));

            migrationBuilder.DeleteData(
                table: "EBillingTax",
                keyColumn: "Id",
                keyValue: new Guid("845ee146-38ca-4041-90f4-b8411274fa15"));

            migrationBuilder.DeleteData(
                table: "EBillingTax",
                keyColumn: "Id",
                keyValue: new Guid("8507b0ca-b579-4c2a-90fc-d8ec516ba909"));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "8c5b29e5-fbb8-4cc9-871a-d61aaf739bf5",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "1e868c0d-b778-45ea-9f88-98a97bf83c58", "071d9333-cdc4-49d1-838b-49be08c80cdf" });
        }
    }
}
