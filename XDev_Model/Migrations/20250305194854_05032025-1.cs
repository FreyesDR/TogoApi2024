using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace XDev_Model.Migrations
{
    /// <inheritdoc />
    public partial class _050320251 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Policy",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Policy", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EndPointPolicy",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MethodHttp = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    MethodPath = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PolicyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PolicyParams = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    LastUpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EndPointPolicy", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EndPointPolicy_Policy_PolicyId",
                        column: x => x.PolicyId,
                        principalTable: "Policy",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Policy",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("35efe9e5-c406-492c-bc8f-af5589bad426"), "Actualización" },
                    { new Guid("3cefd509-9d98-4295-bb45-0b7624b13b3d"), "Lectura" },
                    { new Guid("5fdca958-95a0-4656-bc19-688d60d4ffe6"), "Creación" },
                    { new Guid("81630d88-7dd9-4100-bc3b-85dc36b8ae9c"), "Listado" },
                    { new Guid("db8f9a1e-39f4-4ce8-bbb7-479843ee9ef3"), "Eliminación" }
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "8c5b29e5-fbb8-4cc9-871a-d61aaf739bf5",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "1eeb46be-247c-4bc4-9b84-54feee1f7c4b", "962178f4-c09b-4f47-81e5-7ca3228e7cd7" });

            migrationBuilder.CreateIndex(
                name: "IX_EndPointPolicy_MethodHttp",
                table: "EndPointPolicy",
                column: "MethodHttp");

            migrationBuilder.CreateIndex(
                name: "IX_EndPointPolicy_MethodPath",
                table: "EndPointPolicy",
                column: "MethodPath");

            migrationBuilder.CreateIndex(
                name: "IX_EndPointPolicy_PolicyId",
                table: "EndPointPolicy",
                column: "PolicyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EndPointPolicy");

            migrationBuilder.DropTable(
                name: "Policy");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "8c5b29e5-fbb8-4cc9-871a-d61aaf739bf5",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "b475b1d8-7f33-4553-9954-d25163e1def3", "1c7a131e-c29a-4c03-b083-85e20154934e" });
        }
    }
}
