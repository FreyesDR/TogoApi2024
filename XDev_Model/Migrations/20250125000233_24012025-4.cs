using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace XDev_Model.Migrations
{
    /// <inheritdoc />
    public partial class _240120254 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "PaymentCondition",
                columns: new[] { "Id", "Code", "ConcurrencyStamp", "CreatedAt", "CreatedBy", "LastUpdatedAt", "LastUpdatedBy", "Name", "Plazo", "PlazoCount", "Tipo" },
                values: new object[,]
                {
                    { new Guid("5fd46367-0c46-4fe0-b648-29dafd49b80c"), "D08", "5fd46367-0c46-4fe0-b648-29dafd49b80c", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Crédito 8 días", "01", 8, "2" },
                    { new Guid("b3d138d6-0f3d-48be-a96a-e9d6f3922c05"), "D00", "b3d138d6-0f3d-48be-a96a-e9d6f3922c05", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Contado", "", 0, "1" },
                    { new Guid("b500120a-72f0-47cf-b4a0-edfd4fca7abb"), "D15", "b500120a-72f0-47cf-b4a0-edfd4fca7abb", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Crédito 15 días", "01", 15, "2" },
                    { new Guid("c1484b56-6de2-46fb-97f8-a22aae14480d"), "D30", "c1484b56-6de2-46fb-97f8-a22aae14480d", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Crédito 30 días", "01", 30, "2" }
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "8c5b29e5-fbb8-4cc9-871a-d61aaf739bf5",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "b01b2a77-a61b-4192-89f0-da34348bbab6", "25c932a7-863b-4fc3-863b-febcbf847359" });

            migrationBuilder.InsertData(
                table: "MeanOfPayment",
                columns: new[] { "Id", "Code", "ConcurrencyStamp", "CreatedAt", "CreatedBy", "LastUpdatedAt", "LastUpdatedBy", "Name", "PaymentConditionId" },
                values: new object[,]
                {
                    { new Guid("0674f1dd-e567-4ddd-90d6-500f01aaed2e"), "01", "0674f1dd-e567-4ddd-90d6-500f01aaed2e", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Billetes y monedas", new Guid("b3d138d6-0f3d-48be-a96a-e9d6f3922c05") },
                    { new Guid("0ddf88a1-d603-4ec3-80e2-de7fccc4a1c4"), "14", "a4dd8f5b-cd62-4157-9912-130b8ffccf27", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Giro bancario", new Guid("b3d138d6-0f3d-48be-a96a-e9d6f3922c05") },
                    { new Guid("0f6abca4-6b6a-4d13-8d32-761097171581"), "08", "0f6abca4-6b6a-4d13-8d32-761097171581", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Dinero electrónico", new Guid("b3d138d6-0f3d-48be-a96a-e9d6f3922c05") },
                    { new Guid("199b7755-1265-4cda-a1e3-535c867bac21"), "05", "199b7755-1265-4cda-a1e3-535c867bac21", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Transferencia-Depósito Bancario", new Guid("b3d138d6-0f3d-48be-a96a-e9d6f3922c05") },
                    { new Guid("2084c2e1-5b24-4c79-bdab-ef75e18d559c"), "09", "2084c2e1-5b24-4c79-bdab-ef75e18d559c", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Monedero electrónico", new Guid("b3d138d6-0f3d-48be-a96a-e9d6f3922c05") },
                    { new Guid("76c955a3-0c97-4f18-846f-2b0765ce3a66"), "02", "76c955a3-0c97-4f18-846f-2b0765ce3a66", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Tarjeta de débito", new Guid("b3d138d6-0f3d-48be-a96a-e9d6f3922c05") },
                    { new Guid("8f305418-ec29-4231-a0fc-1ff523933f68"), "11", "8f305418-ec29-4231-a0fc-1ff523933f68", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Bitcoin", new Guid("b3d138d6-0f3d-48be-a96a-e9d6f3922c05") },
                    { new Guid("99b7591d-c073-4506-87d1-f01363444b66"), "13", "a4dd8f5b-cd62-4157-9912-130b8ffccf27", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Cuentas por pagar del receptor", new Guid("b3d138d6-0f3d-48be-a96a-e9d6f3922c05") },
                    { new Guid("a4dd8f5b-cd62-4157-9912-130b8ffccf27"), "12", "a4dd8f5b-cd62-4157-9912-130b8ffccf27", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Otras criptomonedas", new Guid("b3d138d6-0f3d-48be-a96a-e9d6f3922c05") },
                    { new Guid("b5b02924-fd31-4790-a220-3218698aac6e"), "04", "b5b02924-fd31-4790-a220-3218698aac6e", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Cheque", new Guid("b3d138d6-0f3d-48be-a96a-e9d6f3922c05") },
                    { new Guid("ba614885-ea45-463a-8e3f-7e894902f74f"), "03", "ba614885-ea45-463a-8e3f-7e894902f74f", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Tarjeta de crédito", new Guid("b3d138d6-0f3d-48be-a96a-e9d6f3922c05") }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "MeanOfPayment",
                keyColumn: "Id",
                keyValue: new Guid("0674f1dd-e567-4ddd-90d6-500f01aaed2e"));

            migrationBuilder.DeleteData(
                table: "MeanOfPayment",
                keyColumn: "Id",
                keyValue: new Guid("0ddf88a1-d603-4ec3-80e2-de7fccc4a1c4"));

            migrationBuilder.DeleteData(
                table: "MeanOfPayment",
                keyColumn: "Id",
                keyValue: new Guid("0f6abca4-6b6a-4d13-8d32-761097171581"));

            migrationBuilder.DeleteData(
                table: "MeanOfPayment",
                keyColumn: "Id",
                keyValue: new Guid("199b7755-1265-4cda-a1e3-535c867bac21"));

            migrationBuilder.DeleteData(
                table: "MeanOfPayment",
                keyColumn: "Id",
                keyValue: new Guid("2084c2e1-5b24-4c79-bdab-ef75e18d559c"));

            migrationBuilder.DeleteData(
                table: "MeanOfPayment",
                keyColumn: "Id",
                keyValue: new Guid("76c955a3-0c97-4f18-846f-2b0765ce3a66"));

            migrationBuilder.DeleteData(
                table: "MeanOfPayment",
                keyColumn: "Id",
                keyValue: new Guid("8f305418-ec29-4231-a0fc-1ff523933f68"));

            migrationBuilder.DeleteData(
                table: "MeanOfPayment",
                keyColumn: "Id",
                keyValue: new Guid("99b7591d-c073-4506-87d1-f01363444b66"));

            migrationBuilder.DeleteData(
                table: "MeanOfPayment",
                keyColumn: "Id",
                keyValue: new Guid("a4dd8f5b-cd62-4157-9912-130b8ffccf27"));

            migrationBuilder.DeleteData(
                table: "MeanOfPayment",
                keyColumn: "Id",
                keyValue: new Guid("b5b02924-fd31-4790-a220-3218698aac6e"));

            migrationBuilder.DeleteData(
                table: "MeanOfPayment",
                keyColumn: "Id",
                keyValue: new Guid("ba614885-ea45-463a-8e3f-7e894902f74f"));

            migrationBuilder.DeleteData(
                table: "PaymentCondition",
                keyColumn: "Id",
                keyValue: new Guid("5fd46367-0c46-4fe0-b648-29dafd49b80c"));

            migrationBuilder.DeleteData(
                table: "PaymentCondition",
                keyColumn: "Id",
                keyValue: new Guid("b500120a-72f0-47cf-b4a0-edfd4fca7abb"));

            migrationBuilder.DeleteData(
                table: "PaymentCondition",
                keyColumn: "Id",
                keyValue: new Guid("c1484b56-6de2-46fb-97f8-a22aae14480d"));

            migrationBuilder.DeleteData(
                table: "PaymentCondition",
                keyColumn: "Id",
                keyValue: new Guid("b3d138d6-0f3d-48be-a96a-e9d6f3922c05"));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "8c5b29e5-fbb8-4cc9-871a-d61aaf739bf5",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "8544ea20-f5da-4e11-8140-ad939d5d16d7", "ff2e579e-69b6-4bc8-bc3d-05b6d2c10802" });
        }
    }
}
