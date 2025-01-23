using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XDev_Model.Migrations
{
    /// <inheritdoc />
    public partial class _090120252 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "ValueCondition",
                table: "SaleOrderPositionCondition",
                type: "decimal(18,7)",
                precision: 18,
                scale: 7,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldPrecision: 18,
                oldScale: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "Value",
                table: "SaleOrderPositionCondition",
                type: "decimal(18,7)",
                precision: 18,
                scale: 7,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldPrecision: 18,
                oldScale: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "BaseCondition",
                table: "SaleOrderPositionCondition",
                type: "decimal(18,7)",
                precision: 18,
                scale: 7,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldPrecision: 18,
                oldScale: 2);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "8c5b29e5-fbb8-4cc9-871a-d61aaf739bf5",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "1c78088b-1519-478b-8c41-fe7d1f99de31", "597ff81d-4226-4792-a186-9fe4ac054b08" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "ValueCondition",
                table: "SaleOrderPositionCondition",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,7)",
                oldPrecision: 18,
                oldScale: 7);

            migrationBuilder.AlterColumn<decimal>(
                name: "Value",
                table: "SaleOrderPositionCondition",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,7)",
                oldPrecision: 18,
                oldScale: 7);

            migrationBuilder.AlterColumn<decimal>(
                name: "BaseCondition",
                table: "SaleOrderPositionCondition",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,7)",
                oldPrecision: 18,
                oldScale: 7);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "8c5b29e5-fbb8-4cc9-871a-d61aaf739bf5",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "1f059e5c-56b7-4391-90ec-36986e0d1445", "5caa2401-304d-4082-b2ec-16a1e6fa70e6" });
        }
    }
}
