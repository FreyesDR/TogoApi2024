using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace XDev_Model.Migrations
{
    /// <inheritdoc />
    public partial class _031120241 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AddressType",
                columns: new[] { "Id", "Code", "ConcurrencyStamp", "CreatedAt", "CreatedBy", "LastUpdatedAt", "LastUpdatedBy", "Name" },
                values: new object[,]
                {
                    { new Guid("b880fa62-d045-40f6-8a80-71abeaffa289"), "FE", "b880fa62-d045-40f6-8a80-71abeaffa289", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Facturación" },
                    { new Guid("ca17d52b-7221-4476-82c4-ef77b8203d37"), "DL", "ca17d52b-7221-4476-82c4-ef77b8203d37", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Entrega" },
                    { new Guid("f79873a7-7f73-4f29-ad1b-b345265a9738"), "DO", "f79873a7-7f73-4f29-ad1b-b345265a9738", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Domicilio" }
                });

            migrationBuilder.InsertData(
                table: "BranchType",
                columns: new[] { "Id", "Code", "ConcurrencyStamp", "CreatedAt", "CreatedBy", "LastUpdatedAt", "LastUpdatedBy", "Name" },
                values: new object[,]
                {
                    { new Guid("1190eda6-4aec-493b-876f-c834f8c0da9b"), "07", "24dd7daf-bc2c-4a25-9a8e-c056b4eb1a8a", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Predio y/o patio" },
                    { new Guid("2baf6241-db40-4291-8e80-6652f239783a"), "20", "24dd7daf-bc2c-4a25-9a8e-c056b4eb1a8a", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Otro" },
                    { new Guid("34b9aa6b-657e-4d57-8bd2-41ba2a8ebc49"), "04", "24dd7daf-bc2c-4a25-9a8e-c056b4eb1a8a", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Bodega" },
                    { new Guid("7ca98566-a39c-41fc-bd63-237dd34eb344"), "01", "24dd7daf-bc2c-4a25-9a8e-c056b4eb1a8a", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Sucursal/Agencia" },
                    { new Guid("ee69a655-5b95-4597-99be-9cb35ed2bd50"), "02", "24dd7daf-bc2c-4a25-9a8e-c056b4eb1a8a", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Casa matriz" }
                });

            migrationBuilder.InsertData(
                table: "CompanyType",
                columns: new[] { "Id", "Code", "ConcurrencyStamp", "CreatedAt", "CreatedBy", "LastUpdatedAt", "LastUpdatedBy", "Name" },
                values: new object[,]
                {
                    { new Guid("099a35d5-fddf-486d-b3b7-ba8011b1a7ff"), "J", "099a35d5-fddf-486d-b3b7-ba8011b1a7ff", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Jurídica" },
                    { new Guid("cf579d9d-18f9-432b-806a-4cad7311fb38"), "N", "cf579d9d-18f9-432b-806a-4cad7311fb38", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Persona Natural" }
                });

            migrationBuilder.InsertData(
                table: "PartnerType",
                columns: new[] { "Id", "Code", "ConcurrencyStamp", "CreatedAt", "CreatedBy", "LastUpdatedAt", "LastUpdatedBy", "Name" },
                values: new object[,]
                {
                    { new Guid("099a35d5-fddf-486d-b3b7-ba8011b1a7ff"), "O", "099a35d5-fddf-486d-b3b7-ba8011b1a7ff", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Organización" },
                    { new Guid("cf579d9d-18f9-432b-806a-4cad7311fb38"), "P", "cf579d9d-18f9-432b-806a-4cad7311fb38", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Persona" }
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "8c5b29e5-fbb8-4cc9-871a-d61aaf739bf5",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "873e3b71-9526-447e-90a8-fd6eddb460f9", "46c1b5fd-99b5-404c-a1c0-5cd4415dbf79" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AddressType",
                keyColumn: "Id",
                keyValue: new Guid("b880fa62-d045-40f6-8a80-71abeaffa289"));

            migrationBuilder.DeleteData(
                table: "AddressType",
                keyColumn: "Id",
                keyValue: new Guid("ca17d52b-7221-4476-82c4-ef77b8203d37"));

            migrationBuilder.DeleteData(
                table: "AddressType",
                keyColumn: "Id",
                keyValue: new Guid("f79873a7-7f73-4f29-ad1b-b345265a9738"));

            migrationBuilder.DeleteData(
                table: "BranchType",
                keyColumn: "Id",
                keyValue: new Guid("1190eda6-4aec-493b-876f-c834f8c0da9b"));

            migrationBuilder.DeleteData(
                table: "BranchType",
                keyColumn: "Id",
                keyValue: new Guid("2baf6241-db40-4291-8e80-6652f239783a"));

            migrationBuilder.DeleteData(
                table: "BranchType",
                keyColumn: "Id",
                keyValue: new Guid("34b9aa6b-657e-4d57-8bd2-41ba2a8ebc49"));

            migrationBuilder.DeleteData(
                table: "BranchType",
                keyColumn: "Id",
                keyValue: new Guid("7ca98566-a39c-41fc-bd63-237dd34eb344"));

            migrationBuilder.DeleteData(
                table: "BranchType",
                keyColumn: "Id",
                keyValue: new Guid("ee69a655-5b95-4597-99be-9cb35ed2bd50"));

            migrationBuilder.DeleteData(
                table: "CompanyType",
                keyColumn: "Id",
                keyValue: new Guid("099a35d5-fddf-486d-b3b7-ba8011b1a7ff"));

            migrationBuilder.DeleteData(
                table: "CompanyType",
                keyColumn: "Id",
                keyValue: new Guid("cf579d9d-18f9-432b-806a-4cad7311fb38"));

            migrationBuilder.DeleteData(
                table: "PartnerType",
                keyColumn: "Id",
                keyValue: new Guid("099a35d5-fddf-486d-b3b7-ba8011b1a7ff"));

            migrationBuilder.DeleteData(
                table: "PartnerType",
                keyColumn: "Id",
                keyValue: new Guid("cf579d9d-18f9-432b-806a-4cad7311fb38"));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "8c5b29e5-fbb8-4cc9-871a-d61aaf739bf5",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "71b63a91-94c1-4db0-b657-cd7a1d4a5f5c", "4c8168ca-e7f7-445f-9fec-7f64239d20e2" });
        }
    }
}
