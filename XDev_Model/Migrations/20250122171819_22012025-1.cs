using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XDev_Model.Migrations
{
    /// <inheritdoc />
    public partial class _220120251 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IDsId",
                table: "EBillingCompany");

            migrationBuilder.RenameColumn(
                name: "Nif2",
                table: "EBillingCompany",
                newName: "Nif2Id");

            migrationBuilder.RenameColumn(
                name: "Nif1",
                table: "EBillingCompany",
                newName: "Nif1Id");

            migrationBuilder.UpdateData(
                table: "EBilling",
                keyColumn: "Id",
                keyValue: new Guid("63be85df-6805-41c3-beb2-f6a44db746f6"),
                columns: new[] { "ConcurrencyStamp", "UrlSigner" },
                values: new object[] { "63be85df-6805-41c3-beb2-f6a44db746f6", "http://localhost:8113" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "8c5b29e5-fbb8-4cc9-871a-d61aaf739bf5",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "8d3b3462-7b90-40fc-b189-f4697135695c", "63753ed7-a92c-4848-8af2-9cd397e9a37b" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Nif2Id",
                table: "EBillingCompany",
                newName: "Nif2");

            migrationBuilder.RenameColumn(
                name: "Nif1Id",
                table: "EBillingCompany",
                newName: "Nif1");

            migrationBuilder.AddColumn<Guid>(
                name: "IDsId",
                table: "EBillingCompany",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "EBilling",
                keyColumn: "Id",
                keyValue: new Guid("63be85df-6805-41c3-beb2-f6a44db746f6"),
                columns: new[] { "ConcurrencyStamp", "UrlSigner" },
                values: new object[] { null, "http://localhost:8113/" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "8c5b29e5-fbb8-4cc9-871a-d61aaf739bf5",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "79cf651f-c824-4ca1-891a-1941d8e9ee9c", "42b93e5b-8117-4afd-af41-b5a440ec019b" });
        }
    }
}
