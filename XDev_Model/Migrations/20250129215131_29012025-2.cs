using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XDev_Model.Migrations
{
    /// <inheritdoc />
    public partial class _290120252 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MeanOfPaymentCode",
                table: "SaleOrderPayment",
                type: "nvarchar(2)",
                maxLength: 2,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MeanOfPaymentCode",
                table: "InvoicePayment",
                type: "nvarchar(2)",
                maxLength: 2,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "8c5b29e5-fbb8-4cc9-871a-d61aaf739bf5",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "c1a6a9a2-8bbd-4206-804e-70709e234e28", "c6cad6e1-9f00-46f5-9ffd-57a4eef4fd04" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MeanOfPaymentCode",
                table: "SaleOrderPayment");

            migrationBuilder.DropColumn(
                name: "MeanOfPaymentCode",
                table: "InvoicePayment");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "8c5b29e5-fbb8-4cc9-871a-d61aaf739bf5",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "bad6ab29-f131-4a69-8c19-ac3ff66da28e", "03462f5b-78d0-4556-b0e9-9c8cf39e01eb" });
        }
    }
}
