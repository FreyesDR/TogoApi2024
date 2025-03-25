using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XDev_Model.Migrations
{
    /// <inheritdoc />
    public partial class _050220252 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AssignmentId",
                table: "SaleOrder",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "AssignmentId",
                table: "Invoice",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "EBillingDoc",
                table: "Invoice",
                type: "nvarchar(4)",
                maxLength: 4,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "8c5b29e5-fbb8-4cc9-871a-d61aaf739bf5",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "cd0cded2-3d0b-4029-aa6e-1d2e9481e7dd", "4d81e2fe-fec9-4474-8973-8e5fa8108649" });

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_CodGeneracion",
                table: "Invoice",
                column: "CodGeneracion",
                unique: true,
                filter: "[CodGeneracion] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Invoice_CodGeneracion",
                table: "Invoice");

            migrationBuilder.DropColumn(
                name: "AssignmentId",
                table: "SaleOrder");

            migrationBuilder.DropColumn(
                name: "AssignmentId",
                table: "Invoice");

            migrationBuilder.DropColumn(
                name: "EBillingDoc",
                table: "Invoice");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "8c5b29e5-fbb8-4cc9-871a-d61aaf739bf5",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "3216aa2e-e92e-49a2-a0ca-f3661a10338c", "2682d250-2e65-4fa6-80fe-eab03fb831a4" });
        }
    }
}
