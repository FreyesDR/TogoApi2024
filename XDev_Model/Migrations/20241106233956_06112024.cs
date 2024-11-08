using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace XDev_Model.Migrations
{
    /// <inheritdoc />
    public partial class _06112024 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Country",
                columns: new[] { "Id", "Code", "CodeMH", "ConcurrencyStamp", "CreatedAt", "CreatedBy", "LastUpdatedAt", "LastUpdatedBy", "Name" },
                values: new object[] { new Guid("e4100766-8988-47c0-97aa-34be5e88bb44"), "SV", null, "e4100766-8988-47c0-97aa-34be5e88bb44", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "El Salvador" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "8c5b29e5-fbb8-4cc9-871a-d61aaf739bf5",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "48e4f77e-571a-48a5-95fd-c393ab1a8c4a", "7125829b-e026-4c13-bc83-1257e79bde52" });

            migrationBuilder.InsertData(
                table: "Region",
                columns: new[] { "Id", "Code", "ConcurrencyStamp", "CountryId", "CreatedAt", "CreatedBy", "LastUpdatedAt", "LastUpdatedBy", "Name" },
                values: new object[,]
                {
                    { new Guid("06bea8a8-5c85-4523-87fd-dde6b981d2fd"), "07", "e4100766-8988-47c0-97aa-34be5e88bb44", new Guid("e4100766-8988-47c0-97aa-34be5e88bb44"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Cuscatlán" },
                    { new Guid("0cabeef6-210c-4e02-b826-4d0dd12435ca"), "13", "e4100766-8988-47c0-97aa-34be5e88bb44", new Guid("e4100766-8988-47c0-97aa-34be5e88bb44"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Morazán" },
                    { new Guid("4bb49b77-bb90-41b2-91fd-52badd37c844"), "14", "e4100766-8988-47c0-97aa-34be5e88bb44", new Guid("e4100766-8988-47c0-97aa-34be5e88bb44"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "La Unión" },
                    { new Guid("581ff0b4-7fb5-4702-8ff8-e040a93aa669"), "09", "e4100766-8988-47c0-97aa-34be5e88bb44", new Guid("e4100766-8988-47c0-97aa-34be5e88bb44"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Cabañas" },
                    { new Guid("60873ca6-590b-4f63-b680-a4fc38338de5"), "06", "e4100766-8988-47c0-97aa-34be5e88bb44", new Guid("e4100766-8988-47c0-97aa-34be5e88bb44"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "San Salvador" },
                    { new Guid("68792ae9-a79d-4c01-b0b8-fdd27d90eee8"), "12", "e4100766-8988-47c0-97aa-34be5e88bb44", new Guid("e4100766-8988-47c0-97aa-34be5e88bb44"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "San Miguel" },
                    { new Guid("9db618b7-d112-4e22-bed7-43ed14c930cf"), "05", "e4100766-8988-47c0-97aa-34be5e88bb44", new Guid("e4100766-8988-47c0-97aa-34be5e88bb44"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "La Libertad" },
                    { new Guid("a5dd1706-916c-4fa2-9d00-20c1b7fa60a8"), "03", "e4100766-8988-47c0-97aa-34be5e88bb44", new Guid("e4100766-8988-47c0-97aa-34be5e88bb44"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Sonsonate" },
                    { new Guid("a673111d-5d43-4ef9-bd8a-e22c0d626ad3"), "08", "e4100766-8988-47c0-97aa-34be5e88bb44", new Guid("e4100766-8988-47c0-97aa-34be5e88bb44"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "La Paz" },
                    { new Guid("cee1a53c-864a-4067-a321-07e14a672536"), "04", "e4100766-8988-47c0-97aa-34be5e88bb44", new Guid("e4100766-8988-47c0-97aa-34be5e88bb44"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Chalatenango" },
                    { new Guid("d1d544bd-87bb-4938-b706-a4f759634855"), "01", "e4100766-8988-47c0-97aa-34be5e88bb44", new Guid("e4100766-8988-47c0-97aa-34be5e88bb44"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Ahuachapán" },
                    { new Guid("d7647a83-a047-426b-976d-77ec7b62e9f2"), "10", "e4100766-8988-47c0-97aa-34be5e88bb44", new Guid("e4100766-8988-47c0-97aa-34be5e88bb44"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "San Viciente" },
                    { new Guid("e304c87c-6253-421f-9b34-31f32bf334af"), "02", "e4100766-8988-47c0-97aa-34be5e88bb44", new Guid("e4100766-8988-47c0-97aa-34be5e88bb44"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Santa Ana" },
                    { new Guid("e6651357-535d-489c-9d58-0fd514df9c08"), "11", "e4100766-8988-47c0-97aa-34be5e88bb44", new Guid("e4100766-8988-47c0-97aa-34be5e88bb44"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Usulután" }
                });

            migrationBuilder.InsertData(
                table: "City",
                columns: new[] { "Id", "Code", "ConcurrencyStamp", "CreatedAt", "CreatedBy", "LastUpdatedAt", "LastUpdatedBy", "Name", "RegionId" },
                values: new object[,]
                {
                    { new Guid("077fe4b8-1725-43af-8f60-b5ab6a37fa1d"), "26", "e4100766-8988-47c0-97aa-34be5e88bb44", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Usulután Oeste", new Guid("e6651357-535d-489c-9d58-0fd514df9c08") },
                    { new Guid("07da0c22-42c9-4c6e-94c0-1ed0a77338a6"), "24", "e4100766-8988-47c0-97aa-34be5e88bb44", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "La Libertad Centro", new Guid("9db618b7-d112-4e22-bed7-43ed14c930cf") },
                    { new Guid("0ccf047f-a16a-4df2-84f1-19e8ff66a478"), "17", "e4100766-8988-47c0-97aa-34be5e88bb44", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Santa Ana Oeste", new Guid("e304c87c-6253-421f-9b34-31f32bf334af") },
                    { new Guid("16b8e71d-b065-422b-80e8-5f34b0ab8325"), "16", "e4100766-8988-47c0-97aa-34be5e88bb44", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Santa Ana Este", new Guid("e304c87c-6253-421f-9b34-31f32bf334af") },
                    { new Guid("18104203-f67f-403c-bc4d-57e4b2a99660"), "14", "e4100766-8988-47c0-97aa-34be5e88bb44", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Santa Ana Norte", new Guid("e304c87c-6253-421f-9b34-31f32bf334af") },
                    { new Guid("18f034f3-6f14-42ab-b136-0b45f4d69041"), "13", "e4100766-8988-47c0-97aa-34be5e88bb44", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Ahuachapán Norte", new Guid("d1d544bd-87bb-4938-b706-a4f759634855") },
                    { new Guid("1b37faab-df87-41a5-83fb-849964858948"), "24", "e4100766-8988-47c0-97aa-34be5e88bb44", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Usulután Norte", new Guid("e6651357-535d-489c-9d58-0fd514df9c08") },
                    { new Guid("1e445660-55ed-48f5-94da-bc82a39a19a7"), "18", "e4100766-8988-47c0-97aa-34be5e88bb44", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Cuscatlán Sur", new Guid("06bea8a8-5c85-4523-87fd-dde6b981d2fd") },
                    { new Guid("250d6f4a-b0dd-4cf2-820f-b687a48d5411"), "28", "e4100766-8988-47c0-97aa-34be5e88bb44", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "La Libertad Sur", new Guid("9db618b7-d112-4e22-bed7-43ed14c930cf") },
                    { new Guid("344e3a42-bc56-4312-a1dc-3291ae8b960c"), "26", "e4100766-8988-47c0-97aa-34be5e88bb44", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "La Libertad Este", new Guid("9db618b7-d112-4e22-bed7-43ed14c930cf") },
                    { new Guid("482bc1a5-d37c-4d8f-8fb5-12645a257372"), "27", "e4100766-8988-47c0-97aa-34be5e88bb44", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "La Libertad Costa", new Guid("9db618b7-d112-4e22-bed7-43ed14c930cf") },
                    { new Guid("48950ab6-a7cb-4dd3-b3b8-07d206d6edee"), "22", "e4100766-8988-47c0-97aa-34be5e88bb44", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "San Salvador Este", new Guid("60873ca6-590b-4f63-b680-a4fc38338de5") },
                    { new Guid("4f0336a6-af12-40eb-9e31-feae9ed39ad1"), "18", "e4100766-8988-47c0-97aa-34be5e88bb44", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Sonsonate Centro", new Guid("a5dd1706-916c-4fa2-9d00-20c1b7fa60a8") },
                    { new Guid("55be3620-bbc8-448a-9414-cf072ada0e66"), "34", "e4100766-8988-47c0-97aa-34be5e88bb44", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Chalatenango Norte", new Guid("cee1a53c-864a-4067-a321-07e14a672536") },
                    { new Guid("5ca38eb9-4aa7-4781-b63f-6ccb8756ee0d"), "15", "e4100766-8988-47c0-97aa-34be5e88bb44", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Ahuachapán Sur", new Guid("d1d544bd-87bb-4938-b706-a4f759634855") },
                    { new Guid("5e6d2441-9e29-4ca0-b280-c172b77c21c0"), "25", "e4100766-8988-47c0-97aa-34be5e88bb44", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Usulután Este", new Guid("e6651357-535d-489c-9d58-0fd514df9c08") },
                    { new Guid("6170dfff-3bfd-4761-af82-f429615e6e2a"), "20", "e4100766-8988-47c0-97aa-34be5e88bb44", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Sonsonate Oeste", new Guid("a5dd1706-916c-4fa2-9d00-20c1b7fa60a8") },
                    { new Guid("618a98fd-1398-4d51-aa21-4da10282aab3"), "15", "e4100766-8988-47c0-97aa-34be5e88bb44", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Santa Ana Centro", new Guid("e304c87c-6253-421f-9b34-31f32bf334af") },
                    { new Guid("629daa24-d3cc-479a-b6df-ec82f3c9eb59"), "14", "e4100766-8988-47c0-97aa-34be5e88bb44", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "San Vicente Norte", new Guid("d7647a83-a047-426b-976d-77ec7b62e9f2") },
                    { new Guid("62f027d6-a995-4f75-8db9-661d32c6f350"), "21", "e4100766-8988-47c0-97aa-34be5e88bb44", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "San Miguel Norte", new Guid("68792ae9-a79d-4c01-b0b8-fdd27d90eee8") },
                    { new Guid("64c92aba-4806-4c3b-8d81-7eeb330ed99f"), "10", "e4100766-8988-47c0-97aa-34be5e88bb44", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Cabañas Oeste", new Guid("581ff0b4-7fb5-4702-8ff8-e040a93aa669") },
                    { new Guid("698f82d6-e58b-494f-ac81-167bda60f538"), "20", "e4100766-8988-47c0-97aa-34be5e88bb44", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "La Unión Sur", new Guid("4bb49b77-bb90-41b2-91fd-52badd37c844") },
                    { new Guid("7fb4517c-3bfb-4a18-adde-4fb1ecffaddc"), "35", "e4100766-8988-47c0-97aa-34be5e88bb44", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Chalatenango Centro", new Guid("cee1a53c-864a-4067-a321-07e14a672536") },
                    { new Guid("86dc9ec6-18fc-4b6c-80d9-c9958b47ff09"), "24", "e4100766-8988-47c0-97aa-34be5e88bb44", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "San Salvador Sur", new Guid("60873ca6-590b-4f63-b680-a4fc38338de5") },
                    { new Guid("89cc4d95-696b-41d9-b6ac-8bdf16edc5c4"), "25", "e4100766-8988-47c0-97aa-34be5e88bb44", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "La Paz Este", new Guid("a673111d-5d43-4ef9-bd8a-e22c0d626ad3") },
                    { new Guid("8e9b24f6-20cd-4aa0-9948-c4751496f015"), "19", "e4100766-8988-47c0-97aa-34be5e88bb44", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "La Unión Norte", new Guid("4bb49b77-bb90-41b2-91fd-52badd37c844") },
                    { new Guid("96170536-93df-453b-9449-ac0ba51773b2"), "23", "e4100766-8988-47c0-97aa-34be5e88bb44", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "San Miguel Oeste", new Guid("68792ae9-a79d-4c01-b0b8-fdd27d90eee8") },
                    { new Guid("9b8604b8-26b2-446c-9756-32f8754f1b17"), "28", "e4100766-8988-47c0-97aa-34be5e88bb44", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Morazán Sur", new Guid("0cabeef6-210c-4e02-b826-4d0dd12435ca") },
                    { new Guid("9ce12c0c-ec42-45e3-ace0-3068108ecfdc"), "17", "e4100766-8988-47c0-97aa-34be5e88bb44", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Cuscatlán Norte", new Guid("06bea8a8-5c85-4523-87fd-dde6b981d2fd") },
                    { new Guid("a159243d-f033-420f-a5fb-bc6ff14990a8"), "17", "e4100766-8988-47c0-97aa-34be5e88bb44", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Sonsonate Norte", new Guid("a5dd1706-916c-4fa2-9d00-20c1b7fa60a8") },
                    { new Guid("a51b63d3-833b-470c-8d45-55ab7813a5b6"), "11", "e4100766-8988-47c0-97aa-34be5e88bb44", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Cabañas Este", new Guid("581ff0b4-7fb5-4702-8ff8-e040a93aa669") },
                    { new Guid("a675251d-7724-4518-854c-63702c3d9ad7"), "35", "e4100766-8988-47c0-97aa-34be5e88bb44", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Chalatenango Sur", new Guid("cee1a53c-864a-4067-a321-07e14a672536") },
                    { new Guid("b1eb55d2-861b-4e7b-ab23-87e5cad5589e"), "24", "e4100766-8988-47c0-97aa-34be5e88bb44", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "La Paz Centro", new Guid("a673111d-5d43-4ef9-bd8a-e22c0d626ad3") },
                    { new Guid("b2fb4766-2c8e-496e-baa8-1ea23198eb6f"), "20", "e4100766-8988-47c0-97aa-34be5e88bb44", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "San Salvador Norte", new Guid("60873ca6-590b-4f63-b680-a4fc38338de5") },
                    { new Guid("b4570640-2ced-4b69-9b11-ee627d837f54"), "25", "e4100766-8988-47c0-97aa-34be5e88bb44", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "La Libertad Oeste", new Guid("9db618b7-d112-4e22-bed7-43ed14c930cf") },
                    { new Guid("c4ec20e4-9827-4763-9d26-b794d07edad4"), "21", "e4100766-8988-47c0-97aa-34be5e88bb44", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "San Salvador Oeste", new Guid("60873ca6-590b-4f63-b680-a4fc38338de5") },
                    { new Guid("c9bb2770-5bb3-40ae-a13b-49c1bb1756fe"), "23", "e4100766-8988-47c0-97aa-34be5e88bb44", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "San Salvador Centro", new Guid("60873ca6-590b-4f63-b680-a4fc38338de5") },
                    { new Guid("c9e803de-0b24-44f7-817a-ee573f916fc8"), "23", "e4100766-8988-47c0-97aa-34be5e88bb44", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "La Libertad Norte", new Guid("9db618b7-d112-4e22-bed7-43ed14c930cf") },
                    { new Guid("ce1dd042-4fbd-4ffc-8742-2d370a434afb"), "27", "e4100766-8988-47c0-97aa-34be5e88bb44", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Morazán Norte", new Guid("0cabeef6-210c-4e02-b826-4d0dd12435ca") },
                    { new Guid("d9e42bc5-6735-4970-9e07-da3ee30846b1"), "23", "e4100766-8988-47c0-97aa-34be5e88bb44", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "La Paz Oeste", new Guid("a673111d-5d43-4ef9-bd8a-e22c0d626ad3") },
                    { new Guid("dd502b5c-1e53-4f54-8076-0f083ed753e2"), "14", "e4100766-8988-47c0-97aa-34be5e88bb44", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Ahuachapán Centro", new Guid("d1d544bd-87bb-4938-b706-a4f759634855") },
                    { new Guid("e6505b36-f339-4f4d-a89f-4c7f94ec9a69"), "19", "e4100766-8988-47c0-97aa-34be5e88bb44", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Sonsonate Este", new Guid("a5dd1706-916c-4fa2-9d00-20c1b7fa60a8") },
                    { new Guid("f0e33c30-a55b-47af-a4a7-394838d9b5e7"), "15", "e4100766-8988-47c0-97aa-34be5e88bb44", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "San Vicente Sur", new Guid("d7647a83-a047-426b-976d-77ec7b62e9f2") },
                    { new Guid("fbe21ebf-4ecf-4065-8026-9eccc562df68"), "22", "e4100766-8988-47c0-97aa-34be5e88bb44", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "San Miguel Centro", new Guid("68792ae9-a79d-4c01-b0b8-fdd27d90eee8") }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "City",
                keyColumn: "Id",
                keyValue: new Guid("077fe4b8-1725-43af-8f60-b5ab6a37fa1d"));

            migrationBuilder.DeleteData(
                table: "City",
                keyColumn: "Id",
                keyValue: new Guid("07da0c22-42c9-4c6e-94c0-1ed0a77338a6"));

            migrationBuilder.DeleteData(
                table: "City",
                keyColumn: "Id",
                keyValue: new Guid("0ccf047f-a16a-4df2-84f1-19e8ff66a478"));

            migrationBuilder.DeleteData(
                table: "City",
                keyColumn: "Id",
                keyValue: new Guid("16b8e71d-b065-422b-80e8-5f34b0ab8325"));

            migrationBuilder.DeleteData(
                table: "City",
                keyColumn: "Id",
                keyValue: new Guid("18104203-f67f-403c-bc4d-57e4b2a99660"));

            migrationBuilder.DeleteData(
                table: "City",
                keyColumn: "Id",
                keyValue: new Guid("18f034f3-6f14-42ab-b136-0b45f4d69041"));

            migrationBuilder.DeleteData(
                table: "City",
                keyColumn: "Id",
                keyValue: new Guid("1b37faab-df87-41a5-83fb-849964858948"));

            migrationBuilder.DeleteData(
                table: "City",
                keyColumn: "Id",
                keyValue: new Guid("1e445660-55ed-48f5-94da-bc82a39a19a7"));

            migrationBuilder.DeleteData(
                table: "City",
                keyColumn: "Id",
                keyValue: new Guid("250d6f4a-b0dd-4cf2-820f-b687a48d5411"));

            migrationBuilder.DeleteData(
                table: "City",
                keyColumn: "Id",
                keyValue: new Guid("344e3a42-bc56-4312-a1dc-3291ae8b960c"));

            migrationBuilder.DeleteData(
                table: "City",
                keyColumn: "Id",
                keyValue: new Guid("482bc1a5-d37c-4d8f-8fb5-12645a257372"));

            migrationBuilder.DeleteData(
                table: "City",
                keyColumn: "Id",
                keyValue: new Guid("48950ab6-a7cb-4dd3-b3b8-07d206d6edee"));

            migrationBuilder.DeleteData(
                table: "City",
                keyColumn: "Id",
                keyValue: new Guid("4f0336a6-af12-40eb-9e31-feae9ed39ad1"));

            migrationBuilder.DeleteData(
                table: "City",
                keyColumn: "Id",
                keyValue: new Guid("55be3620-bbc8-448a-9414-cf072ada0e66"));

            migrationBuilder.DeleteData(
                table: "City",
                keyColumn: "Id",
                keyValue: new Guid("5ca38eb9-4aa7-4781-b63f-6ccb8756ee0d"));

            migrationBuilder.DeleteData(
                table: "City",
                keyColumn: "Id",
                keyValue: new Guid("5e6d2441-9e29-4ca0-b280-c172b77c21c0"));

            migrationBuilder.DeleteData(
                table: "City",
                keyColumn: "Id",
                keyValue: new Guid("6170dfff-3bfd-4761-af82-f429615e6e2a"));

            migrationBuilder.DeleteData(
                table: "City",
                keyColumn: "Id",
                keyValue: new Guid("618a98fd-1398-4d51-aa21-4da10282aab3"));

            migrationBuilder.DeleteData(
                table: "City",
                keyColumn: "Id",
                keyValue: new Guid("629daa24-d3cc-479a-b6df-ec82f3c9eb59"));

            migrationBuilder.DeleteData(
                table: "City",
                keyColumn: "Id",
                keyValue: new Guid("62f027d6-a995-4f75-8db9-661d32c6f350"));

            migrationBuilder.DeleteData(
                table: "City",
                keyColumn: "Id",
                keyValue: new Guid("64c92aba-4806-4c3b-8d81-7eeb330ed99f"));

            migrationBuilder.DeleteData(
                table: "City",
                keyColumn: "Id",
                keyValue: new Guid("698f82d6-e58b-494f-ac81-167bda60f538"));

            migrationBuilder.DeleteData(
                table: "City",
                keyColumn: "Id",
                keyValue: new Guid("7fb4517c-3bfb-4a18-adde-4fb1ecffaddc"));

            migrationBuilder.DeleteData(
                table: "City",
                keyColumn: "Id",
                keyValue: new Guid("86dc9ec6-18fc-4b6c-80d9-c9958b47ff09"));

            migrationBuilder.DeleteData(
                table: "City",
                keyColumn: "Id",
                keyValue: new Guid("89cc4d95-696b-41d9-b6ac-8bdf16edc5c4"));

            migrationBuilder.DeleteData(
                table: "City",
                keyColumn: "Id",
                keyValue: new Guid("8e9b24f6-20cd-4aa0-9948-c4751496f015"));

            migrationBuilder.DeleteData(
                table: "City",
                keyColumn: "Id",
                keyValue: new Guid("96170536-93df-453b-9449-ac0ba51773b2"));

            migrationBuilder.DeleteData(
                table: "City",
                keyColumn: "Id",
                keyValue: new Guid("9b8604b8-26b2-446c-9756-32f8754f1b17"));

            migrationBuilder.DeleteData(
                table: "City",
                keyColumn: "Id",
                keyValue: new Guid("9ce12c0c-ec42-45e3-ace0-3068108ecfdc"));

            migrationBuilder.DeleteData(
                table: "City",
                keyColumn: "Id",
                keyValue: new Guid("a159243d-f033-420f-a5fb-bc6ff14990a8"));

            migrationBuilder.DeleteData(
                table: "City",
                keyColumn: "Id",
                keyValue: new Guid("a51b63d3-833b-470c-8d45-55ab7813a5b6"));

            migrationBuilder.DeleteData(
                table: "City",
                keyColumn: "Id",
                keyValue: new Guid("a675251d-7724-4518-854c-63702c3d9ad7"));

            migrationBuilder.DeleteData(
                table: "City",
                keyColumn: "Id",
                keyValue: new Guid("b1eb55d2-861b-4e7b-ab23-87e5cad5589e"));

            migrationBuilder.DeleteData(
                table: "City",
                keyColumn: "Id",
                keyValue: new Guid("b2fb4766-2c8e-496e-baa8-1ea23198eb6f"));

            migrationBuilder.DeleteData(
                table: "City",
                keyColumn: "Id",
                keyValue: new Guid("b4570640-2ced-4b69-9b11-ee627d837f54"));

            migrationBuilder.DeleteData(
                table: "City",
                keyColumn: "Id",
                keyValue: new Guid("c4ec20e4-9827-4763-9d26-b794d07edad4"));

            migrationBuilder.DeleteData(
                table: "City",
                keyColumn: "Id",
                keyValue: new Guid("c9bb2770-5bb3-40ae-a13b-49c1bb1756fe"));

            migrationBuilder.DeleteData(
                table: "City",
                keyColumn: "Id",
                keyValue: new Guid("c9e803de-0b24-44f7-817a-ee573f916fc8"));

            migrationBuilder.DeleteData(
                table: "City",
                keyColumn: "Id",
                keyValue: new Guid("ce1dd042-4fbd-4ffc-8742-2d370a434afb"));

            migrationBuilder.DeleteData(
                table: "City",
                keyColumn: "Id",
                keyValue: new Guid("d9e42bc5-6735-4970-9e07-da3ee30846b1"));

            migrationBuilder.DeleteData(
                table: "City",
                keyColumn: "Id",
                keyValue: new Guid("dd502b5c-1e53-4f54-8076-0f083ed753e2"));

            migrationBuilder.DeleteData(
                table: "City",
                keyColumn: "Id",
                keyValue: new Guid("e6505b36-f339-4f4d-a89f-4c7f94ec9a69"));

            migrationBuilder.DeleteData(
                table: "City",
                keyColumn: "Id",
                keyValue: new Guid("f0e33c30-a55b-47af-a4a7-394838d9b5e7"));

            migrationBuilder.DeleteData(
                table: "City",
                keyColumn: "Id",
                keyValue: new Guid("fbe21ebf-4ecf-4065-8026-9eccc562df68"));

            migrationBuilder.DeleteData(
                table: "Region",
                keyColumn: "Id",
                keyValue: new Guid("06bea8a8-5c85-4523-87fd-dde6b981d2fd"));

            migrationBuilder.DeleteData(
                table: "Region",
                keyColumn: "Id",
                keyValue: new Guid("0cabeef6-210c-4e02-b826-4d0dd12435ca"));

            migrationBuilder.DeleteData(
                table: "Region",
                keyColumn: "Id",
                keyValue: new Guid("4bb49b77-bb90-41b2-91fd-52badd37c844"));

            migrationBuilder.DeleteData(
                table: "Region",
                keyColumn: "Id",
                keyValue: new Guid("581ff0b4-7fb5-4702-8ff8-e040a93aa669"));

            migrationBuilder.DeleteData(
                table: "Region",
                keyColumn: "Id",
                keyValue: new Guid("60873ca6-590b-4f63-b680-a4fc38338de5"));

            migrationBuilder.DeleteData(
                table: "Region",
                keyColumn: "Id",
                keyValue: new Guid("68792ae9-a79d-4c01-b0b8-fdd27d90eee8"));

            migrationBuilder.DeleteData(
                table: "Region",
                keyColumn: "Id",
                keyValue: new Guid("9db618b7-d112-4e22-bed7-43ed14c930cf"));

            migrationBuilder.DeleteData(
                table: "Region",
                keyColumn: "Id",
                keyValue: new Guid("a5dd1706-916c-4fa2-9d00-20c1b7fa60a8"));

            migrationBuilder.DeleteData(
                table: "Region",
                keyColumn: "Id",
                keyValue: new Guid("a673111d-5d43-4ef9-bd8a-e22c0d626ad3"));

            migrationBuilder.DeleteData(
                table: "Region",
                keyColumn: "Id",
                keyValue: new Guid("cee1a53c-864a-4067-a321-07e14a672536"));

            migrationBuilder.DeleteData(
                table: "Region",
                keyColumn: "Id",
                keyValue: new Guid("d1d544bd-87bb-4938-b706-a4f759634855"));

            migrationBuilder.DeleteData(
                table: "Region",
                keyColumn: "Id",
                keyValue: new Guid("d7647a83-a047-426b-976d-77ec7b62e9f2"));

            migrationBuilder.DeleteData(
                table: "Region",
                keyColumn: "Id",
                keyValue: new Guid("e304c87c-6253-421f-9b34-31f32bf334af"));

            migrationBuilder.DeleteData(
                table: "Region",
                keyColumn: "Id",
                keyValue: new Guid("e6651357-535d-489c-9d58-0fd514df9c08"));

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: new Guid("e4100766-8988-47c0-97aa-34be5e88bb44"));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "8c5b29e5-fbb8-4cc9-871a-d61aaf739bf5",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "873e3b71-9526-447e-90a8-fd6eddb460f9", "46c1b5fd-99b5-404c-a1c0-5cd4415dbf79" });
        }
    }
}
