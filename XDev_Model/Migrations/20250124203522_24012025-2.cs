using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XDev_Model.Migrations
{
    /// <inheritdoc />
    public partial class _240120252 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UnitMeasureAltCode",
                table: "SaleOrderPosition",
                type: "nvarchar(2)",
                maxLength: 2,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UnitMeasureAltCode",
                table: "InvoicePosition",
                type: "nvarchar(2)",
                maxLength: 2,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "8c5b29e5-fbb8-4cc9-871a-d61aaf739bf5",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "97571ac9-176e-4694-b448-7869cbf95d8a", "de5ec355-645d-4088-8657-77e61db41a62" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UnitMeasureAltCode",
                table: "SaleOrderPosition");

            migrationBuilder.DropColumn(
                name: "UnitMeasureAltCode",
                table: "InvoicePosition");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "8c5b29e5-fbb8-4cc9-871a-d61aaf739bf5",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "4f8f96e5-e003-4468-b75d-9f1385c693df", "5ecbe195-80f0-4c6f-a5c9-1f9f13a75eb2" });
        }
    }
}
