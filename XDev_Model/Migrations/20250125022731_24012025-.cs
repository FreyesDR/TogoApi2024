using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XDev_Model.Migrations
{
    /// <inheritdoc />
    public partial class _24012025 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_MeanOfPayment_Code",
                table: "MeanOfPayment");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "8c5b29e5-fbb8-4cc9-871a-d61aaf739bf5",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "691aaec1-d7f0-4c03-8403-7e9538ae4f91", "ba8736ff-aa6e-4932-a4c1-a2822327a4ce" });

            migrationBuilder.CreateIndex(
                name: "IX_MeanOfPayment_Code",
                table: "MeanOfPayment",
                column: "Code",
                unique: true,
                filter: "[Code] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_MeanOfPayment_Code",
                table: "MeanOfPayment");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "8c5b29e5-fbb8-4cc9-871a-d61aaf739bf5",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "edead372-8be6-40dc-87b9-68143a546b6b", "bde18150-e031-43b9-a7b3-fd6fc9e28108" });

            migrationBuilder.CreateIndex(
                name: "IX_MeanOfPayment_Code",
                table: "MeanOfPayment",
                column: "Code");
        }
    }
}
