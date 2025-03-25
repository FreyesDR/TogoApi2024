using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XDev_Model.Migrations
{
    /// <inheritdoc />
    public partial class _300120253 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ContacPersonId",
                table: "Partner",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.InsertData(
                table: "PartnerRole",
                columns: new[] { "Id", "Code", "ConcurrencyStamp", "CreatedAt", "CreatedBy", "LastUpdatedAt", "LastUpdatedBy", "Name" },
                values: new object[] { new Guid("0f8d6ce6-6177-4892-843a-11e2af8aa134"), "C", "0f8d6ce6-6177-4892-843a-11e2af8aa134", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Contacto" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "8c5b29e5-fbb8-4cc9-871a-d61aaf739bf5",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "1f1de615-47a3-4d7f-8712-d453b72b7c80", "71cc7796-a012-42a7-8f39-d9a0d61aa3a0" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PartnerRole",
                keyColumn: "Id",
                keyValue: new Guid("0f8d6ce6-6177-4892-843a-11e2af8aa134"));

            migrationBuilder.DropColumn(
                name: "ContacPersonId",
                table: "Partner");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "8c5b29e5-fbb8-4cc9-871a-d61aaf739bf5",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "f028142f-97c0-499b-b32e-8b862b707f53", "ce9d0ded-37f8-4989-b0cd-921551037f24" });
        }
    }
}
