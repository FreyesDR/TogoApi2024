using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace XDev_Model.Migrations
{
    /// <inheritdoc />
    public partial class _271120241 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MaterialFeatures",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NumType = table.Column<short>(type: "smallint", nullable: false),
                    RangeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    LastUpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialFeatures", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "MaterialFeatures",
                columns: new[] { "Id", "ConcurrencyStamp", "CreatedAt", "CreatedBy", "LastUpdatedAt", "LastUpdatedBy", "NumType", "RangeId" },
                values: new object[] { new Guid("7ca98566-a39c-41fc-bd63-237dd34eb344"), "7ca98566-a39c-41fc-bd63-237dd34eb344", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, (short)0, new Guid("00000000-0000-0000-0000-000000000000") });

            migrationBuilder.InsertData(
                table: "MaterialType",
                columns: new[] { "Id", "Code", "ConcurrencyStamp", "CreatedAt", "CreatedBy", "LastUpdatedAt", "LastUpdatedBy", "Name" },
                values: new object[,]
                {
                    { new Guid("0f8d6ce6-6177-4892-843a-11e2af8aa134"), "1", "0f8d6ce6-6177-4892-843a-11e2af8aa134", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Bienes" },
                    { new Guid("691f3725-3ef8-434b-aecf-b663791cc501"), "2", "691f3725-3ef8-434b-aecf-b663791cc501", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Servicios" }
                });

            migrationBuilder.InsertData(
                table: "UnitMeasure",
                columns: new[] { "Id", "AltCode", "Code", "ConcurrencyStamp", "CreatedAt", "CreatedBy", "LastUpdatedAt", "LastUpdatedBy", "Name" },
                values: new object[] { new Guid("07f1cfbc-2b69-4c76-910c-e1c38eeaa9fc"), "59", "UN", "07f1cfbc-2b69-4c76-910c-e1c38eeaa9fc", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Unidad" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "8c5b29e5-fbb8-4cc9-871a-d61aaf739bf5",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "eba63e75-5a79-45cf-86ec-73817ba8cac3", "e84ae00f-d8bf-4693-b4fd-4fd0a30d5e44" });

            migrationBuilder.CreateIndex(
                name: "IX_MaterialFeatures_RangeId",
                table: "MaterialFeatures",
                column: "RangeId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MaterialFeatures");

            migrationBuilder.DeleteData(
                table: "MaterialType",
                keyColumn: "Id",
                keyValue: new Guid("0f8d6ce6-6177-4892-843a-11e2af8aa134"));

            migrationBuilder.DeleteData(
                table: "MaterialType",
                keyColumn: "Id",
                keyValue: new Guid("691f3725-3ef8-434b-aecf-b663791cc501"));

            migrationBuilder.DeleteData(
                table: "UnitMeasure",
                keyColumn: "Id",
                keyValue: new Guid("07f1cfbc-2b69-4c76-910c-e1c38eeaa9fc"));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "8c5b29e5-fbb8-4cc9-871a-d61aaf739bf5",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "b72a90fe-32be-4876-9efa-9e9221d248d1", "84ffeee2-c0e5-4b06-bdb3-6a1df12bbafa" });
        }
    }
}
