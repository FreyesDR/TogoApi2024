using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XDev_Model.Migrations
{
    /// <inheritdoc />
    public partial class _080120251 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UnitPrice",
                table: "SaleOrderPosition");

            migrationBuilder.RenameColumn(
                name: "TotalDiscount",
                table: "SaleOrderPosition",
                newName: "TaxAmount");

            migrationBuilder.RenameColumn(
                name: "TotalAmount",
                table: "SaleOrderPosition",
                newName: "NetAmount");

            migrationBuilder.AddColumn<decimal>(
                name: "DiscountAmount",
                table: "SaleOrderPosition",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "PriceType",
                table: "SaleOrderPosition",
                type: "nvarchar(2)",
                maxLength: 2,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "8c5b29e5-fbb8-4cc9-871a-d61aaf739bf5",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "d2339c86-c873-485d-a984-1b5a650a7d45", "d053d47f-1bdf-48be-b27a-a8fa15f5ffd6" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DiscountAmount",
                table: "SaleOrderPosition");

            migrationBuilder.DropColumn(
                name: "PriceType",
                table: "SaleOrderPosition");

            migrationBuilder.RenameColumn(
                name: "TaxAmount",
                table: "SaleOrderPosition",
                newName: "TotalDiscount");

            migrationBuilder.RenameColumn(
                name: "NetAmount",
                table: "SaleOrderPosition",
                newName: "TotalAmount");

            migrationBuilder.AddColumn<decimal>(
                name: "UnitPrice",
                table: "SaleOrderPosition",
                type: "decimal(18,5)",
                precision: 18,
                scale: 5,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "8c5b29e5-fbb8-4cc9-871a-d61aaf739bf5",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "2674f3d3-e3f7-416f-a7de-dd659e985768", "0a2d4246-a498-43d8-82ab-85a5d20d21b7" });
        }
    }
}
