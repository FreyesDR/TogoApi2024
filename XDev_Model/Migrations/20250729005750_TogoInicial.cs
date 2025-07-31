using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace XDev_Model.Migrations
{
    /// <inheritdoc />
    public partial class TogoInicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AccountType",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    CreatedBy = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastUpdatedBy = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    LastUpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AddressType",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Code = table.Column<string>(type: "character varying(2)", maxLength: 2, nullable: true),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    CreatedBy = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastUpdatedBy = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    LastUpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AddressType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppLog",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Source = table.Column<string>(type: "text", nullable: true),
                    Message = table.Column<string>(type: "text", nullable: true),
                    StackTrace = table.Column<string>(type: "text", nullable: true),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppLog", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BranchType",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Code = table.Column<string>(type: "character varying(2)", maxLength: 2, nullable: true),
                    Name = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    CreatedBy = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastUpdatedBy = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    LastUpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BranchType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CompanyType",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Code = table.Column<string>(type: "character varying(1)", maxLength: 1, nullable: true),
                    Name = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: true),
                    CreatedBy = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastUpdatedBy = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    LastUpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Country",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Code = table.Column<string>(type: "character varying(4)", maxLength: 4, nullable: true),
                    CodeMH = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: true),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    CreatedBy = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastUpdatedBy = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    LastUpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Country", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Currency",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Code = table.Column<string>(type: "character varying(3)", maxLength: 3, nullable: true),
                    ISOCode = table.Column<string>(type: "character varying(3)", maxLength: 3, nullable: true),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    CreatedBy = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastUpdatedBy = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    LastUpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currency", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EBilling",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Code = table.Column<string>(type: "character varying(4)", maxLength: 4, nullable: true),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    UrlTest = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    UrlProd = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    UrlSigner = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    CertPathTest = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    CertPathProd = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    CreatedBy = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastUpdatedBy = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    LastUpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EBilling", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EBillingLog",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uuid", nullable: false),
                    BranchId = table.Column<Guid>(type: "uuid", nullable: false),
                    PointSaleId = table.Column<Guid>(type: "uuid", nullable: false),
                    CodGen = table.Column<Guid>(type: "uuid", nullable: false),
                    NumControl = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    SelloRecibido = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    TipoDte = table.Column<string>(type: "character varying(4)", maxLength: 4, nullable: true),
                    Request = table.Column<string>(type: "text", nullable: true),
                    Response = table.Column<string>(type: "text", nullable: true),
                    ResponseDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ResponseStatus = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    ResponseStatusCode = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    ResponseMessage = table.Column<string>(type: "text", nullable: true),
                    StatusCode = table.Column<string>(type: "character varying(4)", maxLength: 4, nullable: true),
                    Observaciones = table.Column<string>(type: "text", nullable: true),
                    InvoiceId = table.Column<Guid>(type: "uuid", nullable: false),
                    SaleOrderId = table.Column<Guid>(type: "uuid", nullable: false),
                    Cancel = table.Column<bool>(type: "boolean", nullable: false),
                    CancelInvoiceId = table.Column<Guid>(type: "uuid", nullable: false),
                    IsProd = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EBillingLog", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EconomicActivities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Code = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: true),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    CreatedBy = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastUpdatedBy = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    LastUpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EconomicActivities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IDType",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Code = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: true),
                    AltCode = table.Column<string>(type: "character varying(3)", maxLength: 3, nullable: true),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    CreatedBy = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastUpdatedBy = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    LastUpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IDType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IncoTerms",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Code = table.Column<string>(type: "character varying(4)", maxLength: 4, nullable: true),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    CreatedBy = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastUpdatedBy = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    LastUpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IncoTerms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InvoiceType",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Code = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: true),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    RangeId = table.Column<Guid>(type: "uuid", nullable: false),
                    FormName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Export = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedBy = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastUpdatedBy = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    LastUpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "JournalType",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    CreatedBy = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastUpdatedBy = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    LastUpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JournalType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MaterialBranch",
                columns: table => new
                {
                    MaterialId = table.Column<Guid>(type: "uuid", nullable: false),
                    BranchId = table.Column<Guid>(type: "uuid", nullable: false),
                    PriceSale = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    IsLockedSale = table.Column<bool>(type: "boolean", nullable: false),
                    PricePurchase = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    IsLockedPurchase = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedBy = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastUpdatedBy = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    LastUpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialBranch", x => new { x.MaterialId, x.BranchId });
                });

            migrationBuilder.CreateTable(
                name: "MaterialFeatures",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    NumType = table.Column<short>(type: "smallint", nullable: false),
                    RangeId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedBy = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastUpdatedBy = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    LastUpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialFeatures", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MaterialType",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Code = table.Column<string>(type: "character varying(2)", maxLength: 2, nullable: true),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    CreatedBy = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastUpdatedBy = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    LastUpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MaterialWareHouse",
                columns: table => new
                {
                    MaterialId = table.Column<Guid>(type: "uuid", nullable: false),
                    BranchId = table.Column<Guid>(type: "uuid", nullable: false),
                    WareHouseId = table.Column<Guid>(type: "uuid", nullable: false),
                    Stock = table.Column<decimal>(type: "numeric(18,3)", precision: 18, scale: 3, nullable: false),
                    SoldStock = table.Column<decimal>(type: "numeric(18,3)", precision: 18, scale: 3, nullable: false),
                    PurchasedStock = table.Column<decimal>(type: "numeric(18,3)", precision: 18, scale: 3, nullable: false),
                    LockedStock = table.Column<decimal>(type: "numeric(18,3)", precision: 18, scale: 3, nullable: false),
                    InTransitStock = table.Column<decimal>(type: "numeric(18,3)", precision: 18, scale: 3, nullable: false),
                    CreatedBy = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastUpdatedBy = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    LastUpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialWareHouse", x => new { x.MaterialId, x.BranchId, x.WareHouseId });
                });

            migrationBuilder.CreateTable(
                name: "MeanOfPayment",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Code = table.Column<string>(type: "character varying(4)", maxLength: 4, nullable: true),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    CreatedBy = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastUpdatedBy = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    LastUpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeanOfPayment", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NumberRange",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    NumStart = table.Column<long>(type: "bigint", nullable: false),
                    NumEnd = table.Column<long>(type: "bigint", nullable: false),
                    NumCurrent = table.Column<long>(type: "bigint", nullable: false),
                    Active = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedBy = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastUpdatedBy = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    LastUpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NumberRange", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PartnerFeatures",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    NumType = table.Column<short>(type: "smallint", nullable: false),
                    RangeId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedBy = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastUpdatedBy = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    LastUpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PartnerFeatures", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PartnerRole",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Code = table.Column<string>(type: "character varying(2)", maxLength: 2, nullable: true),
                    Name = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: true),
                    CreatedBy = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastUpdatedBy = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    LastUpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PartnerRole", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PartnerType",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Code = table.Column<string>(type: "character varying(2)", maxLength: 2, nullable: true),
                    Name = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: true),
                    CreatedBy = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastUpdatedBy = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    LastUpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PartnerType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PaymentCondition",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Code = table.Column<string>(type: "character varying(4)", maxLength: 4, nullable: true),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Tipo = table.Column<string>(type: "character varying(1)", maxLength: 1, nullable: true),
                    Plazo = table.Column<string>(type: "character varying(2)", maxLength: 2, nullable: true),
                    PlazoCount = table.Column<int>(type: "integer", nullable: false),
                    CreatedBy = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastUpdatedBy = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    LastUpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentCondition", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Policy",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Policy", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PriceCondition",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Code = table.Column<string>(type: "character varying(3)", maxLength: 3, nullable: true),
                    AltCode = table.Column<string>(type: "character varying(4)", maxLength: 4, nullable: true),
                    Name = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    Type = table.Column<string>(type: "character varying(1)", maxLength: 1, nullable: true),
                    Source = table.Column<string>(type: "character varying(1)", maxLength: 1, nullable: true),
                    SourceConditionId = table.Column<Guid>(type: "uuid", nullable: false),
                    Value = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    ValueType = table.Column<string>(type: "character varying(1)", maxLength: 1, nullable: true),
                    Edit = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedBy = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastUpdatedBy = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    LastUpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriceCondition", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PriceScheme",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Code = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: true),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    CreatedBy = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastUpdatedBy = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    LastUpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriceScheme", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RecintoFiscal",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Code = table.Column<string>(type: "character varying(4)", maxLength: 4, nullable: true),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    CreatedBy = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastUpdatedBy = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    LastUpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecintoFiscal", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RegimenExport",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Code = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    CreatedBy = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastUpdatedBy = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    LastUpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegimenExport", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    RoleName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    CreatedBy = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastUpdatedBy = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    LastUpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SaleOrderType",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Code = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: true),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Delivery = table.Column<bool>(type: "boolean", nullable: false),
                    Invoice = table.Column<bool>(type: "boolean", nullable: false),
                    InvoiceTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    RangeId = table.Column<Guid>(type: "uuid", nullable: false),
                    PriceSchemeId = table.Column<Guid>(type: "uuid", nullable: false),
                    PdfFormName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Inventory = table.Column<string>(type: "character varying(1)", maxLength: 1, nullable: true),
                    ApplyRet1 = table.Column<bool>(type: "boolean", nullable: false),
                    ApplyRet10 = table.Column<bool>(type: "boolean", nullable: false),
                    ApplyPer1 = table.Column<bool>(type: "boolean", nullable: false),
                    AssignmentRequired = table.Column<bool>(type: "boolean", nullable: false),
                    Export = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedBy = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastUpdatedBy = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    LastUpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SaleOrderType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UnitMeasure",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Code = table.Column<string>(type: "character varying(4)", maxLength: 4, nullable: true),
                    AltCode = table.Column<string>(type: "character varying(2)", maxLength: 2, nullable: true),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    CreatedBy = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastUpdatedBy = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    LastUpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnitMeasure", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Active = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedBy = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastUpdatedBy = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    LastUpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IDTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    IDNumber = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    IDCode = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: true),
                    UserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AccountCatalog",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Code = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Name = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    Level = table.Column<short>(type: "smallint", nullable: false),
                    ParentId = table.Column<Guid>(type: "uuid", nullable: true),
                    Active = table.Column<bool>(type: "boolean", nullable: false),
                    AccountTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedBy = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastUpdatedBy = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    LastUpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountCatalog", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccountCatalog_AccountCatalog_ParentId",
                        column: x => x.ParentId,
                        principalTable: "AccountCatalog",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AccountCatalog_AccountType_AccountTypeId",
                        column: x => x.AccountTypeId,
                        principalTable: "AccountType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Company",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CompanyTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    Code = table.Column<string>(type: "character varying(4)", maxLength: 4, nullable: true),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    TradeName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Active = table.Column<bool>(type: "boolean", nullable: false),
                    UrlLogo = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    CreatedBy = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastUpdatedBy = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    LastUpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Company", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Company_CompanyType_CompanyTypeId",
                        column: x => x.CompanyTypeId,
                        principalTable: "CompanyType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Region",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CountryId = table.Column<Guid>(type: "uuid", nullable: false),
                    Code = table.Column<string>(type: "character varying(4)", maxLength: 4, nullable: true),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    CreatedBy = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastUpdatedBy = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    LastUpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Region", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Region_Country_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Country",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EBillingCompany",
                columns: table => new
                {
                    EBillingId = table.Column<Guid>(type: "uuid", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uuid", nullable: false),
                    IsProd = table.Column<bool>(type: "boolean", nullable: false),
                    ApiUser = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    ApiKeyTest = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    ApiKeyProd = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    PrivateKeyTest = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    PrivateKeyProd = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Active = table.Column<bool>(type: "boolean", nullable: false),
                    Contingency = table.Column<bool>(type: "boolean", nullable: false),
                    SmtpService = table.Column<string>(type: "character varying(1)", maxLength: 1, nullable: true),
                    SmtpHost = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    SmtpPort = table.Column<int>(type: "integer", nullable: false),
                    SmtpUserName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    SmtpUserPassword = table.Column<string>(type: "text", nullable: true),
                    SmtpEnableSsl = table.Column<bool>(type: "boolean", nullable: false),
                    FromName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    CcEmail1 = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    CcEmail2 = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    CreatedBy = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastUpdatedBy = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    LastUpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EBillingCompany", x => new { x.EBillingId, x.CompanyId });
                    table.ForeignKey(
                        name: "FK_EBillingCompany_EBilling_EBillingId",
                        column: x => x.EBillingId,
                        principalTable: "EBilling",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EBillingDocument",
                columns: table => new
                {
                    EBillingId = table.Column<Guid>(type: "uuid", nullable: false),
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Code = table.Column<string>(type: "character varying(4)", maxLength: 4, nullable: true),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    CreatedBy = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastUpdatedBy = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    LastUpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EBillingDocument", x => new { x.EBillingId, x.Id });
                    table.ForeignKey(
                        name: "FK_EBillingDocument_EBilling_EBillingId",
                        column: x => x.EBillingId,
                        principalTable: "EBilling",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EBillingTax",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    EBillingId = table.Column<Guid>(type: "uuid", nullable: false),
                    TaxCode = table.Column<string>(type: "character varying(4)", maxLength: 4, nullable: true),
                    TaxName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    CreatedBy = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastUpdatedBy = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    LastUpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EBillingTax", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EBillingTax_EBilling_EBillingId",
                        column: x => x.EBillingId,
                        principalTable: "EBilling",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Journal",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Code = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    JournalTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedBy = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastUpdatedBy = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    LastUpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Journal", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Journal_JournalType_JournalTypeId",
                        column: x => x.JournalTypeId,
                        principalTable: "JournalType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Material",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Code = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    MaterialTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    OldCode = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    PriceType = table.Column<string>(type: "character varying(2)", maxLength: 2, nullable: true),
                    Price = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    UnitMeasureId = table.Column<Guid>(type: "uuid", nullable: false),
                    Active = table.Column<bool>(type: "boolean", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedBy = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastUpdatedBy = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    LastUpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Material", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Material_MaterialType_MaterialTypeId",
                        column: x => x.MaterialTypeId,
                        principalTable: "MaterialType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Invoice",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Number = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    InvoiceTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    PartnerId = table.Column<Guid>(type: "uuid", nullable: true),
                    PaymentConditionId = table.Column<Guid>(type: "uuid", nullable: false),
                    CurrencyId = table.Column<Guid>(type: "uuid", nullable: false),
                    CurrencyCode = table.Column<string>(type: "character varying(4)", maxLength: 4, nullable: true),
                    NetAmount = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    TaxAmount = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    DiscountPorcent = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    CompanyId = table.Column<Guid>(type: "uuid", nullable: false),
                    BranchId = table.Column<Guid>(type: "uuid", nullable: false),
                    PointSaleId = table.Column<Guid>(type: "uuid", nullable: false),
                    PointSaleCode = table.Column<string>(type: "character varying(4)", maxLength: 4, nullable: true),
                    RefDocument = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    RefDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Sporadic = table.Column<bool>(type: "boolean", nullable: false),
                    Per1 = table.Column<decimal>(type: "numeric(18,5)", precision: 18, scale: 5, nullable: false),
                    Ret1 = table.Column<decimal>(type: "numeric(18,5)", precision: 18, scale: 5, nullable: false),
                    Ret10 = table.Column<decimal>(type: "numeric(18,5)", precision: 18, scale: 5, nullable: false),
                    SaleOrderId = table.Column<Guid>(type: "uuid", nullable: false),
                    Canceled = table.Column<bool>(type: "boolean", nullable: false),
                    CanceledDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CanceledUserId = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    NumControl = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    CodGeneracion = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    SelloRecepcion = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    FechaRecepcion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EBillingDoc = table.Column<string>(type: "character varying(4)", maxLength: 4, nullable: true),
                    Assignment = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    AssignmentId = table.Column<Guid>(type: "uuid", nullable: false),
                    RecintoFiscalId = table.Column<Guid>(type: "uuid", nullable: false),
                    RecintoFiscalCode = table.Column<string>(type: "character varying(4)", maxLength: 4, nullable: true),
                    RegimenExportId = table.Column<Guid>(type: "uuid", nullable: false),
                    RegimenExportCode = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    IncoTermsId = table.Column<Guid>(type: "uuid", nullable: false),
                    IncoTerms = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    SentEmail = table.Column<bool>(type: "boolean", nullable: false),
                    Contingency = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedBy = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastUpdatedBy = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    LastUpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoice", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Invoice_InvoiceType_InvoiceTypeId",
                        column: x => x.InvoiceTypeId,
                        principalTable: "InvoiceType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Invoice_PaymentCondition_PaymentConditionId",
                        column: x => x.PaymentConditionId,
                        principalTable: "PaymentCondition",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Partner",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PartnerTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    Code = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    OldCode = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: true),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    TradeName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    ContactPersonName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    ContactPersonIDNumber = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    ContactPersonPhone = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    ContactPersonEmail = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Active = table.Column<bool>(type: "boolean", nullable: false),
                    PaymentConditionId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedBy = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastUpdatedBy = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    LastUpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Partner", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Partner_PartnerType_PartnerTypeId",
                        column: x => x.PartnerTypeId,
                        principalTable: "PartnerType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Partner_PaymentCondition_PaymentConditionId",
                        column: x => x.PaymentConditionId,
                        principalTable: "PaymentCondition",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EndPointPolicy",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    MethodHttp = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: true),
                    MethodPath = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    Description = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    Module = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    PolicyId = table.Column<Guid>(type: "uuid", nullable: false),
                    PolicyParams = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    CreatedBy = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastUpdatedBy = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    LastUpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
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

            migrationBuilder.CreateTable(
                name: "PriceSchemeCondition",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PriceSchemeId = table.Column<Guid>(type: "uuid", nullable: false),
                    PriceConditionId = table.Column<Guid>(type: "uuid", nullable: false),
                    Position = table.Column<short>(type: "smallint", nullable: false),
                    CreatedBy = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastUpdatedBy = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    LastUpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriceSchemeCondition", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PriceSchemeCondition_PriceCondition_PriceConditionId",
                        column: x => x.PriceConditionId,
                        principalTable: "PriceCondition",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PriceSchemeCondition_PriceScheme_PriceSchemeId",
                        column: x => x.PriceSchemeId,
                        principalTable: "PriceScheme",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<string>(type: "text", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SaleOrder",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Number = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    SaleOrderTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    PartnerId = table.Column<Guid>(type: "uuid", nullable: true),
                    PaymentConditionId = table.Column<Guid>(type: "uuid", nullable: false),
                    CurrencyId = table.Column<Guid>(type: "uuid", nullable: false),
                    CurrencyCode = table.Column<string>(type: "character varying(4)", maxLength: 4, nullable: true),
                    NetAmount = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    TaxAmount = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    DiscountPorcent = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    CompanyId = table.Column<Guid>(type: "uuid", nullable: false),
                    BranchId = table.Column<Guid>(type: "uuid", nullable: false),
                    PointSaleId = table.Column<Guid>(type: "uuid", nullable: false),
                    PointSaleCode = table.Column<string>(type: "character varying(4)", maxLength: 4, nullable: true),
                    RefDocument = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    RefDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Delivered = table.Column<bool>(type: "boolean", nullable: false),
                    Invoiced = table.Column<bool>(type: "boolean", nullable: false),
                    Sporadic = table.Column<bool>(type: "boolean", nullable: false),
                    Per1 = table.Column<decimal>(type: "numeric(18,5)", precision: 18, scale: 5, nullable: false),
                    Ret1 = table.Column<decimal>(type: "numeric(18,5)", precision: 18, scale: 5, nullable: false),
                    Ret10 = table.Column<decimal>(type: "numeric(18,5)", precision: 18, scale: 5, nullable: false),
                    Assignment = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    AssignmentId = table.Column<Guid>(type: "uuid", nullable: false),
                    RecintoFiscalId = table.Column<Guid>(type: "uuid", nullable: false),
                    RecintoFiscalCode = table.Column<string>(type: "character varying(4)", maxLength: 4, nullable: true),
                    RegimenExportId = table.Column<Guid>(type: "uuid", nullable: false),
                    RegimenExportCode = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    IncoTermsId = table.Column<Guid>(type: "uuid", nullable: false),
                    IncoTerms = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    CreatedBy = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastUpdatedBy = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    LastUpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SaleOrder", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SaleOrder_PaymentCondition_PaymentConditionId",
                        column: x => x.PaymentConditionId,
                        principalTable: "PaymentCondition",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SaleOrder_SaleOrderType_SaleOrderTypeId",
                        column: x => x.SaleOrderTypeId,
                        principalTable: "SaleOrderType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    ProviderKey = table.Column<string>(type: "text", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    RoleId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Branch",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uuid", nullable: false),
                    BranchTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    Code = table.Column<string>(type: "character varying(4)", maxLength: 4, nullable: true),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    CreatedBy = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastUpdatedBy = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    LastUpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Branch", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Branch_BranchType_BranchTypeId",
                        column: x => x.BranchTypeId,
                        principalTable: "BranchType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Branch_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Company",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CompanyEconomicActivities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uuid", nullable: false),
                    EconomicActivityId = table.Column<Guid>(type: "uuid", nullable: false),
                    Principal = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedBy = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastUpdatedBy = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    LastUpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyEconomicActivities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyEconomicActivities_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Company",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompanyEconomicActivities_EconomicActivities_EconomicActivi~",
                        column: x => x.EconomicActivityId,
                        principalTable: "EconomicActivities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CompanyID",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uuid", nullable: false),
                    IDTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    DocumentNumber = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    DateIssue = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DateExpira = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CountryId = table.Column<Guid>(type: "uuid", nullable: false),
                    RegionId = table.Column<Guid>(type: "uuid", nullable: false),
                    NIFNum = table.Column<string>(type: "character varying(1)", maxLength: 1, nullable: true),
                    CreatedBy = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastUpdatedBy = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    LastUpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyID", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyID_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Company",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompanyID_IDType_IDTypeId",
                        column: x => x.IDTypeId,
                        principalTable: "IDType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "City",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    RegionId = table.Column<Guid>(type: "uuid", nullable: false),
                    Code = table.Column<string>(type: "character varying(4)", maxLength: 4, nullable: true),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    CreatedBy = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastUpdatedBy = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    LastUpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_City", x => x.Id);
                    table.ForeignKey(
                        name: "FK_City_Region_RegionId",
                        column: x => x.RegionId,
                        principalTable: "Region",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EBillingCompanyInvoice",
                columns: table => new
                {
                    EBillingId = table.Column<Guid>(type: "uuid", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uuid", nullable: false),
                    InvoiceTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    EBillingDocumentId = table.Column<Guid>(type: "uuid", nullable: false),
                    RangeStart = table.Column<long>(type: "bigint", nullable: false),
                    RangeEnd = table.Column<long>(type: "bigint", nullable: false),
                    Current = table.Column<long>(type: "bigint", nullable: false),
                    ReStartYear = table.Column<bool>(type: "boolean", nullable: false),
                    NextReStart = table.Column<int>(type: "integer", nullable: false),
                    CreatedBy = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastUpdatedBy = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    LastUpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EBillingCompanyInvoice", x => new { x.EBillingId, x.CompanyId, x.InvoiceTypeId });
                    table.ForeignKey(
                        name: "FK_EBillingCompanyInvoice_EBillingCompany_EBillingId_CompanyId",
                        columns: x => new { x.EBillingId, x.CompanyId },
                        principalTable: "EBillingCompany",
                        principalColumns: new[] { "EBillingId", "CompanyId" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InvoicePayment",
                columns: table => new
                {
                    InvoiceId = table.Column<Guid>(type: "uuid", nullable: false),
                    MeanOfPaymentId = table.Column<Guid>(type: "uuid", nullable: false),
                    Position = table.Column<int>(type: "integer", nullable: false),
                    MeanOfPaymentCode = table.Column<string>(type: "character varying(2)", maxLength: 2, nullable: true),
                    Amount = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    Reference = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Tipo = table.Column<string>(type: "character varying(1)", maxLength: 1, nullable: true),
                    Plazo = table.Column<string>(type: "character varying(2)", maxLength: 2, nullable: true),
                    Periodo = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoicePayment", x => new { x.InvoiceId, x.MeanOfPaymentId, x.Position });
                    table.ForeignKey(
                        name: "FK_InvoicePayment_Invoice_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "Invoice",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InvoicePosition",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    InvoiceId = table.Column<Guid>(type: "uuid", nullable: false),
                    Position = table.Column<int>(type: "integer", nullable: false),
                    MaterialId = table.Column<Guid>(type: "uuid", nullable: false),
                    MaterialTypeCode = table.Column<string>(type: "character varying(2)", maxLength: 2, nullable: true),
                    MaterialCode = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    MaterialName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    GrossPrice = table.Column<decimal>(type: "numeric(18,5)", precision: 18, scale: 5, nullable: false),
                    NetPrice = table.Column<decimal>(type: "numeric(18,5)", precision: 18, scale: 5, nullable: false),
                    PriceType = table.Column<string>(type: "character varying(2)", maxLength: 2, nullable: true),
                    UnitMeasureId = table.Column<Guid>(type: "uuid", nullable: false),
                    UnitMeasureCode = table.Column<string>(type: "character varying(4)", maxLength: 4, nullable: true),
                    UnitMeasureAltCode = table.Column<string>(type: "character varying(2)", maxLength: 2, nullable: true),
                    Quantity = table.Column<decimal>(type: "numeric(18,3)", precision: 18, scale: 3, nullable: false),
                    DiscountAmount = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    TaxAmount = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    NetAmount = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    WareHouseId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoicePosition", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InvoicePosition_Invoice_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "Invoice",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InvoiceSporadicPartner",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    InvoiceId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    IDTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    IDCode = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: true),
                    IDNumber = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    IDTypeId2 = table.Column<Guid>(type: "uuid", nullable: false),
                    IDCode2 = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: true),
                    IDNumber2 = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    Address = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    CountryId = table.Column<Guid>(type: "uuid", nullable: true),
                    CountryName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    CountryCode = table.Column<string>(type: "character varying(4)", maxLength: 4, nullable: true),
                    RegionId = table.Column<Guid>(type: "uuid", nullable: true),
                    RegionName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    RegionCode = table.Column<string>(type: "character varying(4)", maxLength: 4, nullable: true),
                    CityId = table.Column<Guid>(type: "uuid", nullable: true),
                    CityName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    CityCode = table.Column<string>(type: "character varying(4)", maxLength: 4, nullable: true),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Phone = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    EcoActivityId = table.Column<Guid>(type: "uuid", nullable: false),
                    EcoActivityName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    EcoActivityCode = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: true),
                    TypePerson = table.Column<string>(type: "character varying(1)", maxLength: 1, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceSporadicPartner", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InvoiceSporadicPartner_Invoice_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "Invoice",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PartnerCompany",
                columns: table => new
                {
                    PartnerId = table.Column<Guid>(type: "uuid", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PartnerCompany", x => new { x.PartnerId, x.CompanyId });
                    table.ForeignKey(
                        name: "FK_PartnerCompany_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Company",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PartnerCompany_Partner_PartnerId",
                        column: x => x.PartnerId,
                        principalTable: "Partner",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PartnerEconomicActivity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PartnerId = table.Column<Guid>(type: "uuid", nullable: false),
                    EconomicActivityId = table.Column<Guid>(type: "uuid", nullable: false),
                    Principal = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedBy = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastUpdatedBy = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    LastUpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PartnerEconomicActivity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PartnerEconomicActivity_EconomicActivities_EconomicActivity~",
                        column: x => x.EconomicActivityId,
                        principalTable: "EconomicActivities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PartnerEconomicActivity_Partner_PartnerId",
                        column: x => x.PartnerId,
                        principalTable: "Partner",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PartnerID",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PartnerId = table.Column<Guid>(type: "uuid", nullable: false),
                    IDTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    DocumentNumber = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    DateIssue = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DateExpira = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CountryId = table.Column<Guid>(type: "uuid", nullable: false),
                    RegionId = table.Column<Guid>(type: "uuid", nullable: false),
                    NIFNum = table.Column<string>(type: "character varying(1)", maxLength: 1, nullable: true),
                    CreatedBy = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastUpdatedBy = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    LastUpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PartnerID", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PartnerID_IDType_IDTypeId",
                        column: x => x.IDTypeId,
                        principalTable: "IDType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PartnerID_Partner_PartnerId",
                        column: x => x.PartnerId,
                        principalTable: "Partner",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PartnerRoles",
                columns: table => new
                {
                    RoleId = table.Column<Guid>(type: "uuid", nullable: false),
                    PartnerId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PartnerRoles", x => new { x.RoleId, x.PartnerId });
                    table.ForeignKey(
                        name: "FK_PartnerRoles_PartnerRole_RoleId",
                        column: x => x.RoleId,
                        principalTable: "PartnerRole",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PartnerRoles_Partner_PartnerId",
                        column: x => x.PartnerId,
                        principalTable: "Partner",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SaleOrderPayment",
                columns: table => new
                {
                    SaleOrderId = table.Column<Guid>(type: "uuid", nullable: false),
                    MeanOfPaymentId = table.Column<Guid>(type: "uuid", nullable: false),
                    Position = table.Column<int>(type: "integer", nullable: false),
                    MeanOfPaymentCode = table.Column<string>(type: "character varying(2)", maxLength: 2, nullable: true),
                    Amount = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    Reference = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Tipo = table.Column<string>(type: "character varying(1)", maxLength: 1, nullable: true),
                    Plazo = table.Column<string>(type: "character varying(2)", maxLength: 2, nullable: true),
                    Periodo = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SaleOrderPayment", x => new { x.SaleOrderId, x.MeanOfPaymentId, x.Position });
                    table.ForeignKey(
                        name: "FK_SaleOrderPayment_SaleOrder_SaleOrderId",
                        column: x => x.SaleOrderId,
                        principalTable: "SaleOrder",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SaleOrderPosition",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SaleOrderId = table.Column<Guid>(type: "uuid", nullable: false),
                    Position = table.Column<int>(type: "integer", nullable: false),
                    MaterialId = table.Column<Guid>(type: "uuid", nullable: false),
                    MaterialTypeCode = table.Column<string>(type: "character varying(2)", maxLength: 2, nullable: true),
                    MaterialCode = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    MaterialName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    GrossPrice = table.Column<decimal>(type: "numeric(18,5)", precision: 18, scale: 5, nullable: false),
                    NetPrice = table.Column<decimal>(type: "numeric(18,5)", precision: 18, scale: 5, nullable: false),
                    PriceType = table.Column<string>(type: "character varying(2)", maxLength: 2, nullable: true),
                    UnitMeasureId = table.Column<Guid>(type: "uuid", nullable: false),
                    UnitMeasureCode = table.Column<string>(type: "character varying(4)", maxLength: 4, nullable: true),
                    UnitMeasureAltCode = table.Column<string>(type: "character varying(2)", maxLength: 2, nullable: true),
                    Quantity = table.Column<decimal>(type: "numeric(18,3)", precision: 18, scale: 3, nullable: false),
                    DiscountAmount = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    TaxAmount = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    NetAmount = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    WareHouseId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedBy = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastUpdatedBy = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    LastUpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SaleOrderPosition", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SaleOrderPosition_SaleOrder_SaleOrderId",
                        column: x => x.SaleOrderId,
                        principalTable: "SaleOrder",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SaleOrderSporadicPartner",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SaleOrderId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    IDTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    IDCode = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: true),
                    IDNumber = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    IDTypeId2 = table.Column<Guid>(type: "uuid", nullable: false),
                    IDCode2 = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: true),
                    IDNumber2 = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    Address = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    CountryId = table.Column<Guid>(type: "uuid", nullable: true),
                    CountryName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    CountryCode = table.Column<string>(type: "character varying(4)", maxLength: 4, nullable: true),
                    RegionId = table.Column<Guid>(type: "uuid", nullable: true),
                    RegionName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    RegionCode = table.Column<string>(type: "character varying(4)", maxLength: 4, nullable: true),
                    CityId = table.Column<Guid>(type: "uuid", nullable: true),
                    CityName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    CityCode = table.Column<string>(type: "character varying(4)", maxLength: 4, nullable: true),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Phone = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    EcoActivityId = table.Column<Guid>(type: "uuid", nullable: false),
                    EcoActivityName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    EcoActivityCode = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: true),
                    TypePerson = table.Column<string>(type: "character varying(1)", maxLength: 1, nullable: true),
                    CreatedBy = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastUpdatedBy = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    LastUpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SaleOrderSporadicPartner", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SaleOrderSporadicPartner_SaleOrder_SaleOrderId",
                        column: x => x.SaleOrderId,
                        principalTable: "SaleOrder",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PointSale",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    BranchId = table.Column<Guid>(type: "uuid", nullable: false),
                    Code = table.Column<string>(type: "character varying(4)", maxLength: 4, nullable: true),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    CreatedBy = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastUpdatedBy = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    LastUpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PointSale", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PointSale_Branch_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branch",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WareHouse",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    BranchId = table.Column<Guid>(type: "uuid", nullable: false),
                    Code = table.Column<string>(type: "character varying(4)", maxLength: 4, nullable: true),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    CreatedBy = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastUpdatedBy = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    LastUpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WareHouse", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WareHouse_Branch_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branch",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Address",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AddressTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    Address1 = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    CountryId = table.Column<Guid>(type: "uuid", nullable: true),
                    RegionId = table.Column<Guid>(type: "uuid", nullable: true),
                    CityId = table.Column<Guid>(type: "uuid", nullable: true),
                    CompanyId = table.Column<Guid>(type: "uuid", nullable: true),
                    BranchId = table.Column<Guid>(type: "uuid", nullable: true),
                    PartnerId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedBy = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastUpdatedBy = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    LastUpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Address_AddressType_AddressTypeId",
                        column: x => x.AddressTypeId,
                        principalTable: "AddressType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Address_Branch_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branch",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Address_City_CityId",
                        column: x => x.CityId,
                        principalTable: "City",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Address_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Company",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Address_Country_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Country",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Address_Partner_PartnerId",
                        column: x => x.PartnerId,
                        principalTable: "Partner",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Address_Region_RegionId",
                        column: x => x.RegionId,
                        principalTable: "Region",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "InvoicePositionCondition",
                columns: table => new
                {
                    InvoicePositionId = table.Column<Guid>(type: "uuid", nullable: false),
                    PriceConditionId = table.Column<Guid>(type: "uuid", nullable: false),
                    Position = table.Column<int>(type: "integer", nullable: false),
                    Code = table.Column<string>(type: "character varying(3)", maxLength: 3, nullable: true),
                    AltCode = table.Column<string>(type: "character varying(4)", maxLength: 4, nullable: true),
                    Name = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    Type = table.Column<string>(type: "character varying(1)", maxLength: 1, nullable: true),
                    Source = table.Column<string>(type: "character varying(1)", maxLength: 1, nullable: true),
                    SourceConditionId = table.Column<Guid>(type: "uuid", nullable: false),
                    Value = table.Column<decimal>(type: "numeric(18,7)", precision: 18, scale: 7, nullable: false),
                    ValueType = table.Column<string>(type: "character varying(1)", maxLength: 1, nullable: true),
                    Edit = table.Column<bool>(type: "boolean", nullable: false),
                    BaseCondition = table.Column<decimal>(type: "numeric(18,7)", precision: 18, scale: 7, nullable: false),
                    ValueCondition = table.Column<decimal>(type: "numeric(18,7)", precision: 18, scale: 7, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoicePositionCondition", x => new { x.InvoicePositionId, x.PriceConditionId });
                    table.ForeignKey(
                        name: "FK_InvoicePositionCondition_InvoicePosition_InvoicePositionId",
                        column: x => x.InvoicePositionId,
                        principalTable: "InvoicePosition",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SaleOrderPositionCondition",
                columns: table => new
                {
                    SaleOrderPositionId = table.Column<Guid>(type: "uuid", nullable: false),
                    PriceConditionId = table.Column<Guid>(type: "uuid", nullable: false),
                    Position = table.Column<int>(type: "integer", nullable: false),
                    Code = table.Column<string>(type: "character varying(3)", maxLength: 3, nullable: true),
                    AltCode = table.Column<string>(type: "character varying(4)", maxLength: 4, nullable: true),
                    Name = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    Type = table.Column<string>(type: "character varying(1)", maxLength: 1, nullable: true),
                    Source = table.Column<string>(type: "character varying(1)", maxLength: 1, nullable: true),
                    SourceConditionId = table.Column<Guid>(type: "uuid", nullable: false),
                    Value = table.Column<decimal>(type: "numeric(18,7)", precision: 18, scale: 7, nullable: false),
                    ValueType = table.Column<string>(type: "character varying(1)", maxLength: 1, nullable: true),
                    Edit = table.Column<bool>(type: "boolean", nullable: false),
                    BaseCondition = table.Column<decimal>(type: "numeric(18,7)", precision: 18, scale: 7, nullable: false),
                    ValueCondition = table.Column<decimal>(type: "numeric(18,7)", precision: 18, scale: 7, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SaleOrderPositionCondition", x => new { x.SaleOrderPositionId, x.PriceConditionId });
                    table.ForeignKey(
                        name: "FK_SaleOrderPositionCondition_SaleOrderPosition_SaleOrderPosit~",
                        column: x => x.SaleOrderPositionId,
                        principalTable: "SaleOrderPosition",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AddressEmail",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AddressId = table.Column<Guid>(type: "uuid", nullable: false),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Principal = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AddressEmail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AddressEmail_Address_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Address",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AddressPhone",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AddressId = table.Column<Guid>(type: "uuid", nullable: false),
                    Phone = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: true),
                    PhoneExt = table.Column<string>(type: "character varying(8)", maxLength: 8, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AddressPhone", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AddressPhone_Address_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Address",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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
                table: "Country",
                columns: new[] { "Id", "Code", "CodeMH", "ConcurrencyStamp", "CreatedAt", "CreatedBy", "LastUpdatedAt", "LastUpdatedBy", "Name" },
                values: new object[] { new Guid("e4100766-8988-47c0-97aa-34be5e88bb44"), "SV", null, "e4100766-8988-47c0-97aa-34be5e88bb44", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "El Salvador" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "Id", "Code", "ConcurrencyStamp", "CreatedAt", "CreatedBy", "ISOCode", "LastUpdatedAt", "LastUpdatedBy", "Name" },
                values: new object[] { new Guid("cf579d9d-18f9-432b-806a-4cad7311fb38"), "USD", "cf579d9d-18f9-432b-806a-4cad7311fb38", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Dólar Estadounidense" });

            migrationBuilder.InsertData(
                table: "EBilling",
                columns: new[] { "Id", "CertPathProd", "CertPathTest", "Code", "ConcurrencyStamp", "CreatedAt", "CreatedBy", "LastUpdatedAt", "LastUpdatedBy", "Name", "UrlProd", "UrlSigner", "UrlTest" },
                values: new object[] { new Guid("63be85df-6805-41c3-beb2-f6a44db746f6"), null, null, "MHSV", "63be85df-6805-41c3-beb2-f6a44db746f6", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Ministerio de Hacienda de El Salvador", "https://api.dtes.mh.gob.sv", "http://localhost:8113", "https://apitest.dtes.mh.gob.sv" });

            migrationBuilder.InsertData(
                table: "IDType",
                columns: new[] { "Id", "AltCode", "Code", "ConcurrencyStamp", "CreatedAt", "CreatedBy", "LastUpdatedAt", "LastUpdatedBy", "Name" },
                values: new object[,]
                {
                    { new Guid("132a6258-f433-4345-9da1-cc9cfd0b64ac"), "03", "PAS", "132a6258-f433-4345-9da1-cc9cfd0b64ac", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Pasaporte" },
                    { new Guid("3e4d4e92-7932-4310-8cda-39b8bdba8d07"), "36", "NIT", "3e4d4e92-7932-4310-8cda-39b8bdba8d07", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "NIT" },
                    { new Guid("429ff286-4d34-42c6-8e6b-254a8d4fa79e"), "37", "OTH", "429ff286-4d34-42c6-8e6b-254a8d4fa79e", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Otro" },
                    { new Guid("7ae985b6-e461-4a66-b028-badf1f16f9f0"), "", "NRC", "7ae985b6-e461-4a66-b028-badf1f16f9f0", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Número Registro Contribuyente" },
                    { new Guid("822ac932-46ab-4588-8f82-fbafb14e27eb"), "02", "CR", "822ac932-46ab-4588-8f82-fbafb14e27eb", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Carnet Residente" },
                    { new Guid("acae8706-50e1-4296-aacd-bd1d59b946d1"), "13", "DUI", "acae8706-50e1-4296-aacd-bd1d59b946d1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "DUI" }
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
                    { new Guid("0f8d6ce6-6177-4892-843a-11e2af8aa134"), "B", "0f8d6ce6-6177-4892-843a-11e2af8aa134", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Bienes" },
                    { new Guid("691f3725-3ef8-434b-aecf-b663791cc501"), "S", "691f3725-3ef8-434b-aecf-b663791cc501", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Servicios" }
                });

            migrationBuilder.InsertData(
                table: "MeanOfPayment",
                columns: new[] { "Id", "Code", "ConcurrencyStamp", "CreatedAt", "CreatedBy", "LastUpdatedAt", "LastUpdatedBy", "Name" },
                values: new object[,]
                {
                    { new Guid("0674f1dd-e567-4ddd-90d6-500f01aaed2e"), "01", "0674f1dd-e567-4ddd-90d6-500f01aaed2e", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Billetes y monedas" },
                    { new Guid("0ddf88a1-d603-4ec3-80e2-de7fccc4a1c4"), "14", "a4dd8f5b-cd62-4157-9912-130b8ffccf27", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Giro bancario" },
                    { new Guid("0f6abca4-6b6a-4d13-8d32-761097171581"), "08", "0f6abca4-6b6a-4d13-8d32-761097171581", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Dinero electrónico" },
                    { new Guid("199b7755-1265-4cda-a1e3-535c867bac21"), "05", "199b7755-1265-4cda-a1e3-535c867bac21", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Transferencia-Depósito Bancario" },
                    { new Guid("2084c2e1-5b24-4c79-bdab-ef75e18d559c"), "09", "2084c2e1-5b24-4c79-bdab-ef75e18d559c", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Monedero electrónico" },
                    { new Guid("76c955a3-0c97-4f18-846f-2b0765ce3a66"), "02", "76c955a3-0c97-4f18-846f-2b0765ce3a66", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Tarjeta de débito" },
                    { new Guid("8f305418-ec29-4231-a0fc-1ff523933f68"), "11", "8f305418-ec29-4231-a0fc-1ff523933f68", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Bitcoin" },
                    { new Guid("99b7591d-c073-4506-87d1-f01363444b66"), "13", "a4dd8f5b-cd62-4157-9912-130b8ffccf27", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Cuentas por pagar del receptor" },
                    { new Guid("a4dd8f5b-cd62-4157-9912-130b8ffccf27"), "12", "a4dd8f5b-cd62-4157-9912-130b8ffccf27", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Otras criptomonedas" },
                    { new Guid("b5b02924-fd31-4790-a220-3218698aac6e"), "04", "b5b02924-fd31-4790-a220-3218698aac6e", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Cheque" },
                    { new Guid("ba614885-ea45-463a-8e3f-7e894902f74f"), "03", "ba614885-ea45-463a-8e3f-7e894902f74f", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Tarjeta de crédito" }
                });

            migrationBuilder.InsertData(
                table: "PartnerFeatures",
                columns: new[] { "Id", "ConcurrencyStamp", "CreatedAt", "CreatedBy", "LastUpdatedAt", "LastUpdatedBy", "NumType", "RangeId" },
                values: new object[] { new Guid("7ca98566-a39c-41fc-bd63-237dd34eb344"), "7ca98566-a39c-41fc-bd63-237dd34eb344", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, (short)0, new Guid("00000000-0000-0000-0000-000000000000") });

            migrationBuilder.InsertData(
                table: "PartnerRole",
                columns: new[] { "Id", "Code", "ConcurrencyStamp", "CreatedAt", "CreatedBy", "LastUpdatedAt", "LastUpdatedBy", "Name" },
                values: new object[,]
                {
                    { new Guid("099a35d5-fddf-486d-b3b7-ba8011b1a7ff"), "A", "099a35d5-fddf-486d-b3b7-ba8011b1a7ff", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Proveedor (Acreedor)" },
                    { new Guid("0f8d6ce6-6177-4892-843a-11e2af8aa134"), "C", "0f8d6ce6-6177-4892-843a-11e2af8aa134", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Contacto" },
                    { new Guid("cf579d9d-18f9-432b-806a-4cad7311fb38"), "D", "cf579d9d-18f9-432b-806a-4cad7311fb38", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Cliente (Deudor)" }
                });

            migrationBuilder.InsertData(
                table: "PartnerType",
                columns: new[] { "Id", "Code", "ConcurrencyStamp", "CreatedAt", "CreatedBy", "LastUpdatedAt", "LastUpdatedBy", "Name" },
                values: new object[,]
                {
                    { new Guid("099a35d5-fddf-486d-b3b7-ba8011b1a7ff"), "O", "099a35d5-fddf-486d-b3b7-ba8011b1a7ff", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Organización" },
                    { new Guid("cf579d9d-18f9-432b-806a-4cad7311fb38"), "P", "cf579d9d-18f9-432b-806a-4cad7311fb38", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Persona" }
                });

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

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "CreatedAt", "CreatedBy", "LastUpdatedAt", "LastUpdatedBy", "Name", "NormalizedName", "RoleName" },
                values: new object[,]
                {
                    { "0da061ae-e10f-4516-a727-59363d4fbacc", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "user", "USER", "Usuario" },
                    { "1b9a01aa-4f39-4e6c-8012-9f0a894ea01b", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "admin", "ADMIN", "Administrador de Sistema" }
                });

            migrationBuilder.InsertData(
                table: "UnitMeasure",
                columns: new[] { "Id", "AltCode", "Code", "ConcurrencyStamp", "CreatedAt", "CreatedBy", "LastUpdatedAt", "LastUpdatedBy", "Name" },
                values: new object[] { new Guid("07f1cfbc-2b69-4c76-910c-e1c38eeaa9fc"), "59", "UN", "07f1cfbc-2b69-4c76-910c-e1c38eeaa9fc", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Unidad" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "Active", "ConcurrencyStamp", "CreatedAt", "CreatedBy", "Email", "EmailConfirmed", "IDCode", "IDNumber", "IDTypeId", "LastUpdatedAt", "LastUpdatedBy", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "8c5b29e5-fbb8-4cc9-871a-d61aaf739bf5", 0, true, "cf12012f-c891-4f5f-aa46-fe4271aca2cf", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "admin@avalink.com", true, null, null, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, "Administrador de Sistema", "ADMIN@AVALINK.COM", "ADMIN@AVALINK.COM", "AQAAAAIAAYagAAAAEG3VrHMqyIN4gVB/lVaj6OGcuVKSCx3EJhfna64rRTI/0qlORLppSj2xkzKyeCrNKA==", null, false, "12f8e906-1586-4fac-add7-298f566b358a", false, "admin@avalink.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "1b9a01aa-4f39-4e6c-8012-9f0a894ea01b", "8c5b29e5-fbb8-4cc9-871a-d61aaf739bf5" });

            migrationBuilder.InsertData(
                table: "EBillingDocument",
                columns: new[] { "EBillingId", "Id", "Code", "ConcurrencyStamp", "CreatedAt", "CreatedBy", "LastUpdatedAt", "LastUpdatedBy", "Name" },
                values: new object[,]
                {
                    { new Guid("63be85df-6805-41c3-beb2-f6a44db746f6"), new Guid("1dc5c31b-adb6-40b7-8523-5a91ecaea5a3"), "05", "24dd7daf-bc2c-4a25-9a8e-c056b4eb1a8a", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Nota de crédito" },
                    { new Guid("63be85df-6805-41c3-beb2-f6a44db746f6"), new Guid("1f0e23ab-5e6b-4fca-936e-fb1ea18b40af"), "11", "24dd7daf-bc2c-4a25-9a8e-c056b4eb1a8a", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Factura exportador" },
                    { new Guid("63be85df-6805-41c3-beb2-f6a44db746f6"), new Guid("6d339ff2-e58c-4eba-95e9-dd0df0d1abbe"), "01", "24dd7daf-bc2c-4a25-9a8e-c056b4eb1a8a", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Factura" },
                    { new Guid("63be85df-6805-41c3-beb2-f6a44db746f6"), new Guid("9ca389cf-65aa-4b54-bb1b-0080efaa6cb2"), "06", "24dd7daf-bc2c-4a25-9a8e-c056b4eb1a8a", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Nota de débito" },
                    { new Guid("63be85df-6805-41c3-beb2-f6a44db746f6"), new Guid("d4a32bf7-a3dd-4623-81f4-5a9994e6c9d0"), "03", "24dd7daf-bc2c-4a25-9a8e-c056b4eb1a8a", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Comprobante crédito fiscal" }
                });

            migrationBuilder.InsertData(
                table: "EBillingTax",
                columns: new[] { "Id", "ConcurrencyStamp", "CreatedAt", "CreatedBy", "EBillingId", "LastUpdatedAt", "LastUpdatedBy", "TaxCode", "TaxName" },
                values: new object[,]
                {
                    { new Guid("5714c5db-4033-4c8f-a425-13186fb02220"), "5714c5db-4033-4c8f-a425-13186fb02220", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new Guid("63be85df-6805-41c3-beb2-f6a44db746f6"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "C3", "Impuesto al valor agregado (exportaciones)" },
                    { new Guid("6b1fe653-82f8-43f5-a674-c4d5cba0b1b9"), "6b1fe653-82f8-43f5-a674-c4d5cba0b1b9", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new Guid("63be85df-6805-41c3-beb2-f6a44db746f6"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "20", "Impuesto al valor agregado" },
                    { new Guid("845ee146-38ca-4041-90f4-b8411274fa15"), "845ee146-38ca-4041-90f4-b8411274fa15", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new Guid("63be85df-6805-41c3-beb2-f6a44db746f6"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "59", "Turismo: por alojamiento" },
                    { new Guid("8507b0ca-b579-4c2a-90fc-d8ec516ba909"), "8507b0ca-b579-4c2a-90fc-d8ec516ba909", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new Guid("63be85df-6805-41c3-beb2-f6a44db746f6"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "71", "Turismo: salida del país por vía aérea" }
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_AccountCatalog_AccountTypeId",
                table: "AccountCatalog",
                column: "AccountTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountCatalog_Code",
                table: "AccountCatalog",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AccountCatalog_ParentId",
                table: "AccountCatalog",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Address_AddressTypeId",
                table: "Address",
                column: "AddressTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Address_BranchId",
                table: "Address",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Address_CityId",
                table: "Address",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Address_CompanyId",
                table: "Address",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Address_CountryId",
                table: "Address",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Address_PartnerId",
                table: "Address",
                column: "PartnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Address_RegionId",
                table: "Address",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_AddressEmail_AddressId",
                table: "AddressEmail",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_AddressPhone_AddressId",
                table: "AddressPhone",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_AddressType_Code",
                table: "AddressType",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Branch_BranchTypeId",
                table: "Branch",
                column: "BranchTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Branch_Code",
                table: "Branch",
                column: "Code");

            migrationBuilder.CreateIndex(
                name: "IX_Branch_CompanyId",
                table: "Branch",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_BranchType_Code",
                table: "BranchType",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_City_Code",
                table: "City",
                column: "Code");

            migrationBuilder.CreateIndex(
                name: "IX_City_RegionId",
                table: "City",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_Company_Code",
                table: "Company",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Company_CompanyTypeId",
                table: "Company",
                column: "CompanyTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyEconomicActivities_CompanyId",
                table: "CompanyEconomicActivities",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyEconomicActivities_EconomicActivityId",
                table: "CompanyEconomicActivities",
                column: "EconomicActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyID_CompanyId",
                table: "CompanyID",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyID_IDTypeId",
                table: "CompanyID",
                column: "IDTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyType_Code",
                table: "CompanyType",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Country_Code",
                table: "Country",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Country_CodeMH",
                table: "Country",
                column: "CodeMH");

            migrationBuilder.CreateIndex(
                name: "IX_Currency_Code",
                table: "Currency",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EBilling_Code",
                table: "EBilling",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EBillingDocument_Code",
                table: "EBillingDocument",
                column: "Code");

            migrationBuilder.CreateIndex(
                name: "IX_EBillingLog_BranchId",
                table: "EBillingLog",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_EBillingLog_CodGen",
                table: "EBillingLog",
                column: "CodGen");

            migrationBuilder.CreateIndex(
                name: "IX_EBillingLog_CompanyId",
                table: "EBillingLog",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_EBillingLog_InvoiceId",
                table: "EBillingLog",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_EBillingLog_PointSaleId",
                table: "EBillingLog",
                column: "PointSaleId");

            migrationBuilder.CreateIndex(
                name: "IX_EBillingLog_SaleOrderId",
                table: "EBillingLog",
                column: "SaleOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_EBillingTax_EBillingId",
                table: "EBillingTax",
                column: "EBillingId");

            migrationBuilder.CreateIndex(
                name: "IX_EBillingTax_TaxCode",
                table: "EBillingTax",
                column: "TaxCode");

            migrationBuilder.CreateIndex(
                name: "IX_EconomicActivities_Code",
                table: "EconomicActivities",
                column: "Code",
                unique: true);

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

            migrationBuilder.CreateIndex(
                name: "IX_IDType_Code",
                table: "IDType",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_IncoTerms_Code",
                table: "IncoTerms",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_CodGeneracion",
                table: "Invoice",
                column: "CodGeneracion",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_InvoiceTypeId",
                table: "Invoice",
                column: "InvoiceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_Number",
                table: "Invoice",
                column: "Number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_PaymentConditionId",
                table: "Invoice",
                column: "PaymentConditionId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoicePosition_InvoiceId",
                table: "InvoicePosition",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoicePositionCondition_Code",
                table: "InvoicePositionCondition",
                column: "Code");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceSporadicPartner_InvoiceId",
                table: "InvoiceSporadicPartner",
                column: "InvoiceId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceType_Code",
                table: "InvoiceType",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Journal_Code",
                table: "Journal",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Journal_JournalTypeId",
                table: "Journal",
                column: "JournalTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Material_Code",
                table: "Material",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Material_MaterialTypeId",
                table: "Material",
                column: "MaterialTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialFeatures_RangeId",
                table: "MaterialFeatures",
                column: "RangeId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MaterialType_Code",
                table: "MaterialType",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MeanOfPayment_Code",
                table: "MeanOfPayment",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Partner_Code",
                table: "Partner",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Partner_OldCode",
                table: "Partner",
                column: "OldCode");

            migrationBuilder.CreateIndex(
                name: "IX_Partner_PartnerTypeId",
                table: "Partner",
                column: "PartnerTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Partner_PaymentConditionId",
                table: "Partner",
                column: "PaymentConditionId");

            migrationBuilder.CreateIndex(
                name: "IX_PartnerCompany_CompanyId",
                table: "PartnerCompany",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_PartnerCompany_PartnerId",
                table: "PartnerCompany",
                column: "PartnerId");

            migrationBuilder.CreateIndex(
                name: "IX_PartnerEconomicActivity_EconomicActivityId",
                table: "PartnerEconomicActivity",
                column: "EconomicActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_PartnerEconomicActivity_PartnerId",
                table: "PartnerEconomicActivity",
                column: "PartnerId");

            migrationBuilder.CreateIndex(
                name: "IX_PartnerFeatures_RangeId",
                table: "PartnerFeatures",
                column: "RangeId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PartnerID_IDTypeId",
                table: "PartnerID",
                column: "IDTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PartnerID_PartnerId",
                table: "PartnerID",
                column: "PartnerId");

            migrationBuilder.CreateIndex(
                name: "IX_PartnerRole_Code",
                table: "PartnerRole",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PartnerRoles_PartnerId",
                table: "PartnerRoles",
                column: "PartnerId");

            migrationBuilder.CreateIndex(
                name: "IX_PartnerRoles_RoleId",
                table: "PartnerRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_PartnerType_Code",
                table: "PartnerType",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PaymentCondition_Code",
                table: "PaymentCondition",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PointSale_BranchId",
                table: "PointSale",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_PointSale_Code",
                table: "PointSale",
                column: "Code");

            migrationBuilder.CreateIndex(
                name: "IX_PriceCondition_Code",
                table: "PriceCondition",
                column: "Code");

            migrationBuilder.CreateIndex(
                name: "IX_PriceScheme_Code",
                table: "PriceScheme",
                column: "Code");

            migrationBuilder.CreateIndex(
                name: "IX_PriceSchemeCondition_PriceConditionId",
                table: "PriceSchemeCondition",
                column: "PriceConditionId");

            migrationBuilder.CreateIndex(
                name: "IX_PriceSchemeCondition_PriceSchemeId",
                table: "PriceSchemeCondition",
                column: "PriceSchemeId");

            migrationBuilder.CreateIndex(
                name: "IX_RecintoFiscal_Code",
                table: "RecintoFiscal",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RegimenExport_Code",
                table: "RegimenExport",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Region_Code",
                table: "Region",
                column: "Code");

            migrationBuilder.CreateIndex(
                name: "IX_Region_CountryId",
                table: "Region",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "Roles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SaleOrder_Number",
                table: "SaleOrder",
                column: "Number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SaleOrder_PaymentConditionId",
                table: "SaleOrder",
                column: "PaymentConditionId");

            migrationBuilder.CreateIndex(
                name: "IX_SaleOrder_SaleOrderTypeId",
                table: "SaleOrder",
                column: "SaleOrderTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_SaleOrderPosition_SaleOrderId",
                table: "SaleOrderPosition",
                column: "SaleOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_SaleOrderPositionCondition_Code",
                table: "SaleOrderPositionCondition",
                column: "Code");

            migrationBuilder.CreateIndex(
                name: "IX_SaleOrderSporadicPartner_SaleOrderId",
                table: "SaleOrderSporadicPartner",
                column: "SaleOrderId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SaleOrderType_Code",
                table: "SaleOrderType",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UnitMeasure_Code",
                table: "UnitMeasure",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "Users",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "Users",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WareHouse_BranchId",
                table: "WareHouse",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_WareHouse_Code",
                table: "WareHouse",
                column: "Code");

            migrationBuilder.CreateSPGetNumberNext();
            migrationBuilder.CreateEBillingDocumentNextNumber();
            migrationBuilder.CreateSPMaterialWareHouseInvoice();
            migrationBuilder.CreateSPMaterialWareHouseInvoiceCancel();
            migrationBuilder.CreateSPMaterialWareHouseSaleOrder();
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountCatalog");

            migrationBuilder.DropTable(
                name: "AddressEmail");

            migrationBuilder.DropTable(
                name: "AddressPhone");

            migrationBuilder.DropTable(
                name: "AppLog");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "CompanyEconomicActivities");

            migrationBuilder.DropTable(
                name: "CompanyID");

            migrationBuilder.DropTable(
                name: "Currency");

            migrationBuilder.DropTable(
                name: "EBillingCompanyInvoice");

            migrationBuilder.DropTable(
                name: "EBillingDocument");

            migrationBuilder.DropTable(
                name: "EBillingLog");

            migrationBuilder.DropTable(
                name: "EBillingTax");

            migrationBuilder.DropTable(
                name: "EndPointPolicy");

            migrationBuilder.DropTable(
                name: "IncoTerms");

            migrationBuilder.DropTable(
                name: "InvoicePayment");

            migrationBuilder.DropTable(
                name: "InvoicePositionCondition");

            migrationBuilder.DropTable(
                name: "InvoiceSporadicPartner");

            migrationBuilder.DropTable(
                name: "Journal");

            migrationBuilder.DropTable(
                name: "Material");

            migrationBuilder.DropTable(
                name: "MaterialBranch");

            migrationBuilder.DropTable(
                name: "MaterialFeatures");

            migrationBuilder.DropTable(
                name: "MaterialWareHouse");

            migrationBuilder.DropTable(
                name: "MeanOfPayment");

            migrationBuilder.DropTable(
                name: "NumberRange");

            migrationBuilder.DropTable(
                name: "PartnerCompany");

            migrationBuilder.DropTable(
                name: "PartnerEconomicActivity");

            migrationBuilder.DropTable(
                name: "PartnerFeatures");

            migrationBuilder.DropTable(
                name: "PartnerID");

            migrationBuilder.DropTable(
                name: "PartnerRoles");

            migrationBuilder.DropTable(
                name: "PointSale");

            migrationBuilder.DropTable(
                name: "PriceSchemeCondition");

            migrationBuilder.DropTable(
                name: "RecintoFiscal");

            migrationBuilder.DropTable(
                name: "RegimenExport");

            migrationBuilder.DropTable(
                name: "SaleOrderPayment");

            migrationBuilder.DropTable(
                name: "SaleOrderPositionCondition");

            migrationBuilder.DropTable(
                name: "SaleOrderSporadicPartner");

            migrationBuilder.DropTable(
                name: "UnitMeasure");

            migrationBuilder.DropTable(
                name: "WareHouse");

            migrationBuilder.DropTable(
                name: "AccountType");

            migrationBuilder.DropTable(
                name: "Address");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "EBillingCompany");

            migrationBuilder.DropTable(
                name: "Policy");

            migrationBuilder.DropTable(
                name: "InvoicePosition");

            migrationBuilder.DropTable(
                name: "JournalType");

            migrationBuilder.DropTable(
                name: "MaterialType");

            migrationBuilder.DropTable(
                name: "EconomicActivities");

            migrationBuilder.DropTable(
                name: "IDType");

            migrationBuilder.DropTable(
                name: "PartnerRole");

            migrationBuilder.DropTable(
                name: "PriceCondition");

            migrationBuilder.DropTable(
                name: "PriceScheme");

            migrationBuilder.DropTable(
                name: "SaleOrderPosition");

            migrationBuilder.DropTable(
                name: "AddressType");

            migrationBuilder.DropTable(
                name: "Branch");

            migrationBuilder.DropTable(
                name: "City");

            migrationBuilder.DropTable(
                name: "Partner");

            migrationBuilder.DropTable(
                name: "EBilling");

            migrationBuilder.DropTable(
                name: "Invoice");

            migrationBuilder.DropTable(
                name: "SaleOrder");

            migrationBuilder.DropTable(
                name: "BranchType");

            migrationBuilder.DropTable(
                name: "Company");

            migrationBuilder.DropTable(
                name: "Region");

            migrationBuilder.DropTable(
                name: "PartnerType");

            migrationBuilder.DropTable(
                name: "InvoiceType");

            migrationBuilder.DropTable(
                name: "PaymentCondition");

            migrationBuilder.DropTable(
                name: "SaleOrderType");

            migrationBuilder.DropTable(
                name: "CompanyType");

            migrationBuilder.DropTable(
                name: "Country");
        }
    }
}
