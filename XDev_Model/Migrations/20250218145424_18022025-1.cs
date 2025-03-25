using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XDev_Model.Migrations
{
    /// <inheritdoc />
    public partial class _180220251 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CertPathProd",
                table: "EBilling",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CertPathTest",
                table: "EBilling",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "EBilling",
                keyColumn: "Id",
                keyValue: new Guid("63be85df-6805-41c3-beb2-f6a44db746f6"),
                columns: new[] { "CertPathProd", "CertPathTest" },
                values: new object[] { "Certificados\\Prod", "Certificados\\Test" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "8c5b29e5-fbb8-4cc9-871a-d61aaf739bf5",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "a4237c89-373f-4b0d-9164-4f46e28b04ef", "b06498bd-f087-407d-acbe-de4165621c8c" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CertPathProd",
                table: "EBilling");

            migrationBuilder.DropColumn(
                name: "CertPathTest",
                table: "EBilling");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "8c5b29e5-fbb8-4cc9-871a-d61aaf739bf5",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "f55ba1bc-47ef-4dc4-a368-a4a6de8410c2", "b1c7add4-6132-49a0-a557-98608c7cb2f5" });
        }
    }
}
