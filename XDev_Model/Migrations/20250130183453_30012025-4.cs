using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XDev_Model.Migrations
{
    /// <inheritdoc />
    public partial class _300120254 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContacPersonId",
                table: "Partner");

            migrationBuilder.AddColumn<string>(
                name: "ContacPersonEmail",
                table: "Partner",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContacPersonIDNumber",
                table: "Partner",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContacPersonName",
                table: "Partner",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContacPersonPhone",
                table: "Partner",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "8c5b29e5-fbb8-4cc9-871a-d61aaf739bf5",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "89544389-d6be-4909-a137-783a8ca60b4e", "6ba9803d-a922-487b-9f6a-3a55ab839e1e" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContacPersonEmail",
                table: "Partner");

            migrationBuilder.DropColumn(
                name: "ContacPersonIDNumber",
                table: "Partner");

            migrationBuilder.DropColumn(
                name: "ContacPersonName",
                table: "Partner");

            migrationBuilder.DropColumn(
                name: "ContacPersonPhone",
                table: "Partner");

            migrationBuilder.AddColumn<Guid>(
                name: "ContacPersonId",
                table: "Partner",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "8c5b29e5-fbb8-4cc9-871a-d61aaf739bf5",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "1f1de615-47a3-4d7f-8712-d453b72b7c80", "71cc7796-a012-42a7-8f39-d9a0d61aa3a0" });
        }
    }
}
