using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XDev_Model.Migrations
{
    /// <inheritdoc />
    public partial class _040220251 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IDCode2",
                table: "SaleOrderSporadicPartner",
                type: "nvarchar(5)",
                maxLength: 5,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IDNumber2",
                table: "SaleOrderSporadicPartner",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "IDTypeId2",
                table: "SaleOrderSporadicPartner",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "IDCode2",
                table: "InvoiceSporadicPartner",
                type: "nvarchar(5)",
                maxLength: 5,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IDNumber2",
                table: "InvoiceSporadicPartner",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "IDTypeId2",
                table: "InvoiceSporadicPartner",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "8c5b29e5-fbb8-4cc9-871a-d61aaf739bf5",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "468076e7-c80b-47ea-ac4a-626df2867872", "7175ba9e-0b42-46f2-b471-03b975920abd" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IDCode2",
                table: "SaleOrderSporadicPartner");

            migrationBuilder.DropColumn(
                name: "IDNumber2",
                table: "SaleOrderSporadicPartner");

            migrationBuilder.DropColumn(
                name: "IDTypeId2",
                table: "SaleOrderSporadicPartner");

            migrationBuilder.DropColumn(
                name: "IDCode2",
                table: "InvoiceSporadicPartner");

            migrationBuilder.DropColumn(
                name: "IDNumber2",
                table: "InvoiceSporadicPartner");

            migrationBuilder.DropColumn(
                name: "IDTypeId2",
                table: "InvoiceSporadicPartner");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "8c5b29e5-fbb8-4cc9-871a-d61aaf739bf5",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "b59552a3-06f6-422b-98dc-4db60e23cacc", "e6e8780c-8e50-418f-a84b-f673a857ac83" });
        }
    }
}
