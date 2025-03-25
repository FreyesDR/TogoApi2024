using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XDev_Model.Migrations
{
    /// <inheritdoc />
    public partial class _030220252 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AddressId",
                table: "EBillingCompany");

            migrationBuilder.DropColumn(
                name: "Nif1Id",
                table: "EBillingCompany");

            migrationBuilder.DropColumn(
                name: "Nif2Id",
                table: "EBillingCompany");

            migrationBuilder.AddColumn<string>(
                name: "ApiKeyProd",
                table: "EBillingCompany",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApiKeyTest",
                table: "EBillingCompany",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApiUser",
                table: "EBillingCompany",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PrivateKeyProd",
                table: "EBillingCompany",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PrivateKeyTest",
                table: "EBillingCompany",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "8c5b29e5-fbb8-4cc9-871a-d61aaf739bf5",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "b2358517-688d-4f6c-a5dd-7d1fac9ad956", "c64fceb8-b848-416f-96b3-302924698ddb" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApiKeyProd",
                table: "EBillingCompany");

            migrationBuilder.DropColumn(
                name: "ApiKeyTest",
                table: "EBillingCompany");

            migrationBuilder.DropColumn(
                name: "ApiUser",
                table: "EBillingCompany");

            migrationBuilder.DropColumn(
                name: "PrivateKeyProd",
                table: "EBillingCompany");

            migrationBuilder.DropColumn(
                name: "PrivateKeyTest",
                table: "EBillingCompany");

            migrationBuilder.AddColumn<Guid>(
                name: "AddressId",
                table: "EBillingCompany",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "Nif1Id",
                table: "EBillingCompany",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "Nif2Id",
                table: "EBillingCompany",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "8c5b29e5-fbb8-4cc9-871a-d61aaf739bf5",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "1568e119-eaf0-49f1-afea-2ad9a50ce593", "af5e190e-e900-4f65-b532-b00cff892edf" });
        }
    }
}
