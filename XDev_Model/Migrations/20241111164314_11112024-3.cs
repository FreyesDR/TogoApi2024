using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace XDev_Model.Migrations
{
    /// <inheritdoc />
    public partial class _111120243 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "PartnerRole",
                columns: new[] { "Id", "Code", "ConcurrencyStamp", "CreatedAt", "CreatedBy", "LastUpdatedAt", "LastUpdatedBy", "Name" },
                values: new object[,]
                {
                    { new Guid("099a35d5-fddf-486d-b3b7-ba8011b1a7ff"), "A", "099a35d5-fddf-486d-b3b7-ba8011b1a7ff", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Acreedor" },
                    { new Guid("cf579d9d-18f9-432b-806a-4cad7311fb38"), "D", "cf579d9d-18f9-432b-806a-4cad7311fb38", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Deudor" }
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "8c5b29e5-fbb8-4cc9-871a-d61aaf739bf5",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "8b215942-735c-4b92-aa61-109d97a7589c", "a61eb396-3919-40bb-85b8-70ff4294cd60" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PartnerRole",
                keyColumn: "Id",
                keyValue: new Guid("099a35d5-fddf-486d-b3b7-ba8011b1a7ff"));

            migrationBuilder.DeleteData(
                table: "PartnerRole",
                keyColumn: "Id",
                keyValue: new Guid("cf579d9d-18f9-432b-806a-4cad7311fb38"));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "8c5b29e5-fbb8-4cc9-871a-d61aaf739bf5",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "e86b219a-92c7-4494-8616-43edfe746f6e", "96b80300-c5a6-4e78-9401-20e0eeb5affd" });
        }
    }
}
