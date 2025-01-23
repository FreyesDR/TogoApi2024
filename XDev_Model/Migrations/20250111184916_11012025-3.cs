using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XDev_Model.Migrations
{
    /// <inheritdoc />
    public partial class _110120253 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NIFNum",
                table: "CompanyID",
                type: "nvarchar(1)",
                maxLength: 1,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "IDType",
                keyColumn: "Id",
                keyValue: new Guid("7ae985b6-e461-4a66-b028-badf1f16f9f0"),
                column: "Name",
                value: "Número Registro Contribuyente");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "8c5b29e5-fbb8-4cc9-871a-d61aaf739bf5",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "2bd7d4c4-6839-4df5-9b02-600817dc3904", "a8d2cd4d-2184-41e0-9723-19305565b077" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NIFNum",
                table: "CompanyID");

            migrationBuilder.UpdateData(
                table: "IDType",
                keyColumn: "Id",
                keyValue: new Guid("7ae985b6-e461-4a66-b028-badf1f16f9f0"),
                column: "Name",
                value: "Carnet Residente");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "8c5b29e5-fbb8-4cc9-871a-d61aaf739bf5",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "3c1a16fb-2f35-4fd1-ad0b-2c097c4e06e2", "d8651208-b467-4da1-a3b2-1dc41b3411dd" });
        }
    }
}
