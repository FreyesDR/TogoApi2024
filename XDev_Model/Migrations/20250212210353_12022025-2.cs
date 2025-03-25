using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XDev_Model.Migrations
{
    /// <inheritdoc />
    public partial class _120220252 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IDCode",
                table: "Users",
                type: "nvarchar(5)",
                maxLength: 5,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Cancel",
                table: "EBillingLog",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "CancelInvoiceId",
                table: "EBillingLog",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "8c5b29e5-fbb8-4cc9-871a-d61aaf739bf5",
                columns: new[] { "ConcurrencyStamp", "IDCode", "SecurityStamp" },
                values: new object[] { "c244074a-26f5-4e4a-b867-e3d13081341f", null, "ee70b11d-4ba8-44ec-bcd2-aab702c6c348" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IDCode",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Cancel",
                table: "EBillingLog");

            migrationBuilder.DropColumn(
                name: "CancelInvoiceId",
                table: "EBillingLog");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "8c5b29e5-fbb8-4cc9-871a-d61aaf739bf5",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "c1cafe9d-cf54-4c75-8b1b-63d69b756fb0", "533875d5-9233-49c2-8f81-8c8ccd7a72ec" });
        }
    }
}
