using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XDev_Model.Migrations
{
    /// <inheritdoc />
    public partial class _240120255 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MeanOfPayment_PaymentCondition_PaymentConditionId",
                table: "MeanOfPayment");

            migrationBuilder.DropIndex(
                name: "IX_MeanOfPayment_PaymentConditionId",
                table: "MeanOfPayment");

            migrationBuilder.DropColumn(
                name: "PaymentConditionId",
                table: "MeanOfPayment");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "8c5b29e5-fbb8-4cc9-871a-d61aaf739bf5",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "edead372-8be6-40dc-87b9-68143a546b6b", "bde18150-e031-43b9-a7b3-fd6fc9e28108" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PaymentConditionId",
                table: "MeanOfPayment",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "MeanOfPayment",
                keyColumn: "Id",
                keyValue: new Guid("0674f1dd-e567-4ddd-90d6-500f01aaed2e"),
                column: "PaymentConditionId",
                value: new Guid("b3d138d6-0f3d-48be-a96a-e9d6f3922c05"));

            migrationBuilder.UpdateData(
                table: "MeanOfPayment",
                keyColumn: "Id",
                keyValue: new Guid("0ddf88a1-d603-4ec3-80e2-de7fccc4a1c4"),
                column: "PaymentConditionId",
                value: new Guid("b3d138d6-0f3d-48be-a96a-e9d6f3922c05"));

            migrationBuilder.UpdateData(
                table: "MeanOfPayment",
                keyColumn: "Id",
                keyValue: new Guid("0f6abca4-6b6a-4d13-8d32-761097171581"),
                column: "PaymentConditionId",
                value: new Guid("b3d138d6-0f3d-48be-a96a-e9d6f3922c05"));

            migrationBuilder.UpdateData(
                table: "MeanOfPayment",
                keyColumn: "Id",
                keyValue: new Guid("199b7755-1265-4cda-a1e3-535c867bac21"),
                column: "PaymentConditionId",
                value: new Guid("b3d138d6-0f3d-48be-a96a-e9d6f3922c05"));

            migrationBuilder.UpdateData(
                table: "MeanOfPayment",
                keyColumn: "Id",
                keyValue: new Guid("2084c2e1-5b24-4c79-bdab-ef75e18d559c"),
                column: "PaymentConditionId",
                value: new Guid("b3d138d6-0f3d-48be-a96a-e9d6f3922c05"));

            migrationBuilder.UpdateData(
                table: "MeanOfPayment",
                keyColumn: "Id",
                keyValue: new Guid("76c955a3-0c97-4f18-846f-2b0765ce3a66"),
                column: "PaymentConditionId",
                value: new Guid("b3d138d6-0f3d-48be-a96a-e9d6f3922c05"));

            migrationBuilder.UpdateData(
                table: "MeanOfPayment",
                keyColumn: "Id",
                keyValue: new Guid("8f305418-ec29-4231-a0fc-1ff523933f68"),
                column: "PaymentConditionId",
                value: new Guid("b3d138d6-0f3d-48be-a96a-e9d6f3922c05"));

            migrationBuilder.UpdateData(
                table: "MeanOfPayment",
                keyColumn: "Id",
                keyValue: new Guid("99b7591d-c073-4506-87d1-f01363444b66"),
                column: "PaymentConditionId",
                value: new Guid("b3d138d6-0f3d-48be-a96a-e9d6f3922c05"));

            migrationBuilder.UpdateData(
                table: "MeanOfPayment",
                keyColumn: "Id",
                keyValue: new Guid("a4dd8f5b-cd62-4157-9912-130b8ffccf27"),
                column: "PaymentConditionId",
                value: new Guid("b3d138d6-0f3d-48be-a96a-e9d6f3922c05"));

            migrationBuilder.UpdateData(
                table: "MeanOfPayment",
                keyColumn: "Id",
                keyValue: new Guid("b5b02924-fd31-4790-a220-3218698aac6e"),
                column: "PaymentConditionId",
                value: new Guid("b3d138d6-0f3d-48be-a96a-e9d6f3922c05"));

            migrationBuilder.UpdateData(
                table: "MeanOfPayment",
                keyColumn: "Id",
                keyValue: new Guid("ba614885-ea45-463a-8e3f-7e894902f74f"),
                column: "PaymentConditionId",
                value: new Guid("b3d138d6-0f3d-48be-a96a-e9d6f3922c05"));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "8c5b29e5-fbb8-4cc9-871a-d61aaf739bf5",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "b01b2a77-a61b-4192-89f0-da34348bbab6", "25c932a7-863b-4fc3-863b-febcbf847359" });

            migrationBuilder.CreateIndex(
                name: "IX_MeanOfPayment_PaymentConditionId",
                table: "MeanOfPayment",
                column: "PaymentConditionId");

            migrationBuilder.AddForeignKey(
                name: "FK_MeanOfPayment_PaymentCondition_PaymentConditionId",
                table: "MeanOfPayment",
                column: "PaymentConditionId",
                principalTable: "PaymentCondition",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
