using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XDev_Model.Migrations
{
    /// <inheritdoc />
    public partial class _240120251 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CityCode",
                table: "SaleOrderSporadicPartner",
                type: "nvarchar(4)",
                maxLength: 4,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CityName",
                table: "SaleOrderSporadicPartner",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CountryCode",
                table: "SaleOrderSporadicPartner",
                type: "nvarchar(4)",
                maxLength: 4,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CountryName",
                table: "SaleOrderSporadicPartner",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EcoActivityCode",
                table: "SaleOrderSporadicPartner",
                type: "nvarchar(5)",
                maxLength: 5,
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "EcoActivityId",
                table: "SaleOrderSporadicPartner",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "EcoActivityName",
                table: "SaleOrderSporadicPartner",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IDCode",
                table: "SaleOrderSporadicPartner",
                type: "nvarchar(5)",
                maxLength: 5,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RegionCode",
                table: "SaleOrderSporadicPartner",
                type: "nvarchar(4)",
                maxLength: 4,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RegionName",
                table: "SaleOrderSporadicPartner",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CurrencyCode",
                table: "SaleOrder",
                type: "nvarchar(4)",
                maxLength: 4,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PointSaleCode",
                table: "SaleOrder",
                type: "nvarchar(4)",
                maxLength: 4,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CityCode",
                table: "InvoiceSporadicPartner",
                type: "nvarchar(4)",
                maxLength: 4,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CityName",
                table: "InvoiceSporadicPartner",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CountryCode",
                table: "InvoiceSporadicPartner",
                type: "nvarchar(4)",
                maxLength: 4,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CountryName",
                table: "InvoiceSporadicPartner",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EcoActivityCode",
                table: "InvoiceSporadicPartner",
                type: "nvarchar(5)",
                maxLength: 5,
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "EcoActivityId",
                table: "InvoiceSporadicPartner",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "EcoActivityName",
                table: "InvoiceSporadicPartner",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IDCode",
                table: "InvoiceSporadicPartner",
                type: "nvarchar(5)",
                maxLength: 5,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RegionCode",
                table: "InvoiceSporadicPartner",
                type: "nvarchar(4)",
                maxLength: 4,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RegionName",
                table: "InvoiceSporadicPartner",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CurrencyCode",
                table: "Invoice",
                type: "nvarchar(4)",
                maxLength: 4,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PointSaleCode",
                table: "Invoice",
                type: "nvarchar(4)",
                maxLength: 4,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "8c5b29e5-fbb8-4cc9-871a-d61aaf739bf5",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "4f8f96e5-e003-4468-b75d-9f1385c693df", "5ecbe195-80f0-4c6f-a5c9-1f9f13a75eb2" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CityCode",
                table: "SaleOrderSporadicPartner");

            migrationBuilder.DropColumn(
                name: "CityName",
                table: "SaleOrderSporadicPartner");

            migrationBuilder.DropColumn(
                name: "CountryCode",
                table: "SaleOrderSporadicPartner");

            migrationBuilder.DropColumn(
                name: "CountryName",
                table: "SaleOrderSporadicPartner");

            migrationBuilder.DropColumn(
                name: "EcoActivityCode",
                table: "SaleOrderSporadicPartner");

            migrationBuilder.DropColumn(
                name: "EcoActivityId",
                table: "SaleOrderSporadicPartner");

            migrationBuilder.DropColumn(
                name: "EcoActivityName",
                table: "SaleOrderSporadicPartner");

            migrationBuilder.DropColumn(
                name: "IDCode",
                table: "SaleOrderSporadicPartner");

            migrationBuilder.DropColumn(
                name: "RegionCode",
                table: "SaleOrderSporadicPartner");

            migrationBuilder.DropColumn(
                name: "RegionName",
                table: "SaleOrderSporadicPartner");

            migrationBuilder.DropColumn(
                name: "CurrencyCode",
                table: "SaleOrder");

            migrationBuilder.DropColumn(
                name: "PointSaleCode",
                table: "SaleOrder");

            migrationBuilder.DropColumn(
                name: "CityCode",
                table: "InvoiceSporadicPartner");

            migrationBuilder.DropColumn(
                name: "CityName",
                table: "InvoiceSporadicPartner");

            migrationBuilder.DropColumn(
                name: "CountryCode",
                table: "InvoiceSporadicPartner");

            migrationBuilder.DropColumn(
                name: "CountryName",
                table: "InvoiceSporadicPartner");

            migrationBuilder.DropColumn(
                name: "EcoActivityCode",
                table: "InvoiceSporadicPartner");

            migrationBuilder.DropColumn(
                name: "EcoActivityId",
                table: "InvoiceSporadicPartner");

            migrationBuilder.DropColumn(
                name: "EcoActivityName",
                table: "InvoiceSporadicPartner");

            migrationBuilder.DropColumn(
                name: "IDCode",
                table: "InvoiceSporadicPartner");

            migrationBuilder.DropColumn(
                name: "RegionCode",
                table: "InvoiceSporadicPartner");

            migrationBuilder.DropColumn(
                name: "RegionName",
                table: "InvoiceSporadicPartner");

            migrationBuilder.DropColumn(
                name: "CurrencyCode",
                table: "Invoice");

            migrationBuilder.DropColumn(
                name: "PointSaleCode",
                table: "Invoice");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "8c5b29e5-fbb8-4cc9-871a-d61aaf739bf5",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "a656964f-b6b7-4265-9eda-587225df7da6", "aafe72f5-dcf7-49fa-a02a-5031e863243e" });
        }
    }
}
