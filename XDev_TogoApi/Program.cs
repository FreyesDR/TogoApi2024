using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.OpenApi.Models;
using System.Globalization;
using XDev_Model;
using XDev_Model.Entities;
using Swashbuckle.AspNetCore.Filters;
using XDev_UnitWork.Custom;
using Microsoft.EntityFrameworkCore;
using PortalWeb.EndPoints;
using FluentValidation;
using XDev_UnitWork.Validators;
using Microsoft.AspNetCore.Identity.UI.Services;
using XDev_UnitWork.Services;
using XDev_UnitWork.Interfaces;
using XDev_UnitWork.Business;
using XDev_TogoApi.EndPoints;
using XDev_UnitWork.DTO;
using Microsoft.AspNetCore.Diagnostics;
using XDev_UnitWork.DTO.Company;
using XDev_UnitWork.DTO.Partner;
using XDev_UnitWork.DTO.SaleOrder;
using XDev_UnitWork.DTO.Invoice;
using XDev_UnitWork.DTO.Address;
using XDev_UnitWork.DTO.DM;
using XDev_UnitWork.DTO.Material;
using XDev_UnitWork.DTO.PriceScheme;

using Microsoft.Reporting.NETCore;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
using XDev_UnitWork.DTO.ElectronicBilling;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>();

builder.Services.AddIdentityApiEndpoints<ApplicationUser>(options =>
{
    // Password settings.
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;

    // Lockout settings.
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;

    // User settings.
    options.User.AllowedUserNameCharacters = "abcdefghijklmnñopqrstuvwxyzABCDEFGHIJKLMNÑOPQRSTUVWXYZ0123456789-._@";
    options.User.RequireUniqueEmail = true;

    // Sigin settings.
    options.SignIn.RequireConfirmedEmail = true;

}).AddRoles<ApplicationRole>()
  .AddEntityFrameworkStores<ApplicationDbContext>()
  .AddSignInManager()
  .AddRoleManager<RoleManager<ApplicationRole>>()
  .AddDefaultTokenProviders()
  .AddErrorDescriber<CustomIdentityError>();


builder.Services.AddScoped<IClaimsTransformation, UserClaimsTransformation>();
builder.Services.AddHttpClient();

builder.Services.AddCors(opc =>
{
    opc.AddDefaultPolicy(pol =>
    {
        pol.WithOrigins("*")
           .AllowAnyHeader()
           .AllowAnyMethod()
           .WithExposedHeaders(new string[] { "page", "pages", "items", "www-authenticate" });
    });

});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
    opt.OperationFilter<SecurityRequirementsOperationFilter>();
    opt.SwaggerDoc("v1", new OpenApiInfo { Title = "Api Togo", Version = "v1" });
    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Ingrese un token valido",
        Name = "Autorización",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = new CultureInfo[] { new("es-SV"), new("en-US") };
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
    options.DefaultRequestCulture = new
    RequestCulture(supportedCultures.First());

    options.RequestCultureProviders = new List<IRequestCultureProvider>()
    {
        new QueryStringRequestCultureProvider(),
        new CookieRequestCultureProvider(),
        new AcceptLanguageHeaderRequestCultureProvider(),
    };
});

builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options =>
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles
    );


builder.Services.AddHttpContextAccessor();
builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));

builder.Services.AddAuthorization();

builder.Services.AddOutputCache();

#region Servicios
builder.Services.AddTransient<IEmailSender, EmailSender>();
builder.Services.AddScoped<IValidator<RegisterDTO>, RegisterRequestValidator>();

builder.Services.AddScoped<IAccountBL, AccountBL>();

builder.Services.AddScoped<ICompanyTypeBL, CompanyTypeBL>();
builder.Services.AddScoped<IValidator<CompanyTypeDTO>, CompanyTypeValidator>();

builder.Services.AddScoped<ICompanyBL, CompanyBL>();
builder.Services.AddScoped<IValidator<CompanyDTO>, CompanyValidator>();

builder.Services.AddScoped<ICompanyEconomicActivityBL, CompanyEconomicActivityBL>();
builder.Services.AddScoped<IValidator<CompanyEconomicActivityDTO>, CompanyEconomicActivityValidator>();

builder.Services.AddScoped<ICompanyIDBL, CompanyIDBL>();
builder.Services.AddScoped<IValidator<CompanyIDDTO>, CompanyIDValidator>();

builder.Services.AddScoped<IAddressTypeBL, AddressTypeBL>();
builder.Services.AddScoped<IValidator<AddressTypeDTO>, AddressTypeValidator>();

builder.Services.AddScoped<IBranchTypeBL, BranchTypeBL>();
builder.Services.AddScoped<IValidator<BranchTypeDTO>, BranchTypeValidator>();

builder.Services.AddScoped<IBranchBL, BranchBL>();
builder.Services.AddScoped<IValidator<BranchDTO>, BranchValidator>();

builder.Services.AddScoped<IPartnerTypeBL, PartnerTypeBL>();
builder.Services.AddScoped<IValidator<PartnerTypeDTO>, PartnerTypeValidator>();

builder.Services.AddScoped<IPartnerFeaturesBL, PartnerFeaturesBL>();

builder.Services.AddScoped<IPartnerRoleBL, PartnerRoleBL>();
builder.Services.AddScoped<IValidator<PartnerRoleDTO>, PartnerRoleValidator>();

builder.Services.AddScoped<IPartnerBL, PartnerBL>();
builder.Services.AddScoped<IValidator<PartnerDTO>, PartnerValidator>();

builder.Services.AddScoped<IPartnerIDBL, PartnerIDBL>();
builder.Services.AddScoped<IValidator<PartnerIDDTO>, PartnerIDValidator>();

builder.Services.AddScoped<IPartnerEconomicActivityBL, PartnerEconomicActivityBL>();
builder.Services.AddScoped<IValidator<PartnerEconomicActivityDTO>, PartnerEconomicActivityValidator>();

builder.Services.AddScoped<IPartnerRolesBL, PartnerRolesBL>();

builder.Services.AddScoped<IPartnerCompanyBL, PartnerCompanyBL>();

builder.Services.AddScoped<IAddressBL, AddressBL>();

builder.Services.AddScoped<ICountryBL, CountryBL>();
builder.Services.AddScoped<IValidator<CountryDTO>, CountryValidator>();

builder.Services.AddScoped<IRegionBL, RegionBL>();
builder.Services.AddScoped<IValidator<RegionDTO>, RegionValidator>();

builder.Services.AddScoped<ICityBL, CityBL>();
builder.Services.AddScoped<IValidator<CityDTO>, CityValidator>();

builder.Services.AddScoped<IIDTypeBL, IDTypeBL>();
builder.Services.AddScoped<IValidator<IDTypeDTO>, IDTypeValidator>();

builder.Services.AddScoped<IEconomicActivityBL, EconomicActivityBL>();
builder.Services.AddScoped<IValidator<EconomicActivityDTO>, EconomicActivityValidator>();

builder.Services.AddScoped<IAppLogBL, AppLogBL>();

builder.Services.AddScoped<IWareHouseBL, WareHouseBL>();
builder.Services.AddScoped<IValidator<WareHouseDTO>, WareHouseValidator>();

builder.Services.AddScoped<IPointSaleBL, PointSaleBL>();
builder.Services.AddScoped<IValidator<PointSaleDTO>, PointSaleValidator>();

builder.Services.AddScoped<INumberRangeBL, NumberRangeBL>();
builder.Services.AddScoped<IValidator<NumberRangeDTO>, NumberRangeValidator>();

builder.Services.AddScoped<ICurrencyBL, CurrencyBL>();
builder.Services.AddScoped<IValidator<CurrencyDTO>, CurrencyValidator>();

builder.Services.AddScoped<IInvoiceTypeBL, InvoiceTypeBL>();
builder.Services.AddScoped<IValidator<InvoiceTypeDTO>, InvoiceTypeValidator>();

builder.Services.AddScoped<ISaleOrderTypeBL, SaleOrderTypeBL>();
builder.Services.AddScoped<IValidator<SaleOrderTypeDTO>, SaleOrderTypeValidator>();

builder.Services.AddScoped<ISaleOrderBL, SaleOrderBL>();
builder.Services.AddScoped<IValidator<SaleOrderDTO>, SaleOrderValidator>();

builder.Services.AddScoped<IMaterialTypeBL, MaterialTypeBL>();
builder.Services.AddScoped<IValidator<MaterialTypeDTO>, MaterialTypeValidator>();

builder.Services.AddScoped<IUnitMeasureBL, UnitMeasureBL>();
builder.Services.AddScoped<IValidator<UnitMeasureDTO>, UnitMeasureValidator>();

builder.Services.AddScoped<IMaterialFeatureBL, MaterialFeatureBL>();

builder.Services.AddScoped<IMaterialBL, MaterialBL>();
builder.Services.AddScoped<IValidator<MaterialDTO>, MaterialValidator>();

builder.Services.AddScoped<IPriceConditionBL, PriceConditionBL>();
builder.Services.AddScoped<IValidator<PriceConditionDTO>, PriceConditionValidator>();

builder.Services.AddScoped<IPriceSchemeBL, PriceSchemeBL>();
builder.Services.AddScoped<IValidator<PriceSchemeDTO>, PriceSchemeValidator>();

builder.Services.AddScoped<IMaterialBranchBL, MaterialBranchBL>();
builder.Services.AddScoped<IValidator<MaterialBranchDTO>, MaterialBranchValidator>();

builder.Services.AddScoped<IMaterialWareHouseBL, MaterialWareHouseBL>();
builder.Services.AddScoped<IValidator<MaterialWareHouseDTO>, MaterialWareHouseValidator>();

builder.Services.AddScoped<IEBillingBL, EBillingBL>();
builder.Services.AddScoped<IValidator<EBillingDTO>, EBillingValidator>();

builder.Services.AddScoped<IEBillingCompanyBL, EBillingCompanyBL>();
builder.Services.AddScoped<IValidator<EBillingCompanyDTO>, EBillingCompanyValidator>();

builder.Services.AddScoped<IEBillingDocumentBL, EBillingDocumentBL>();
builder.Services.AddScoped<IValidator<EBillingDocumentDTO>, EBillingDocumentValidator>();

builder.Services.AddScoped<IEBillingCompanyInvoiceBL, EBillingCompanyInvoiceBL>();
builder.Services.AddScoped<IValidator<EBillingCompanyInvoiceDTO>, EBillingCompanyInvoiceValidator>();
#endregion

var app = builder.Build();

app.UseOutputCache();

using (var scope = app.Services.CreateScope())
{
    var appDbcontext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    appDbcontext.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler(exceptionHandlerApp => exceptionHandlerApp.Run(async context =>
{
    var exceptionHandleFeature = context.Features.Get<IExceptionHandlerFeature>();
    var exception = exceptionHandleFeature?.Error;

    var error = new AppLogDTO
    {
        Message = exception!.InnerException is null ? exception.Message : exception.InnerException.Message,
        StackTrace = exception.InnerException is null ? exception.StackTrace : exception.InnerException.StackTrace,
        Source = exception.Source
    };

    var errorBL = context.RequestServices.GetRequiredService<IAppLogBL>();
    await errorBL.CreateAsync(error);

    await TypedResults.BadRequest(new ExceptionReturnDTO
    { Message = UtilsExtension.InternalErrorMessage, StatusCode = StatusCodes.Status500InternalServerError.ToString() }).ExecuteAsync(context);
}));

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRequestLocalization();
app.UseCors();
app.UseAuthentication();
app.UseAuthorization();

#region EndPoints
app.MapGroup("/auth").MapSecurity<ApplicationUser>();
app.MapGroup("/account").MapAccount().RequireAuthorization();
app.MapGroup("/company").MapCompany().RequireAuthorization();
app.MapGroup("/company/type").MapCompanyType().RequireAuthorization();
app.MapGroup("/company/idtype").MapCompanyID().RequireAuthorization();
app.MapGroup("/company/eactivity").MapCompanyEconomicActivity().RequireAuthorization();
app.MapGroup("/branch/type").MapBranchType().RequireAuthorization();
app.MapGroup("/branch").MapBranch().RequireAuthorization();
app.MapGroup("/warehouse").MapWareHouse().RequireAuthorization();
app.MapGroup("/pointsale").MapPointSale().RequireAuthorization();
app.MapGroup("/address/type").MapAddressType().RequireAuthorization();
app.MapGroup("/address").MapAddress().RequireAuthorization();
app.MapGroup("/partner").MapPartner().RequireAuthorization();
app.MapGroup("/partner/type").MapPartnerType().RequireAuthorization();
app.MapGroup("/partner/features").MapPartnerFeatures().RequireAuthorization();
app.MapGroup("/partner/role").MapPartnerRole().RequireAuthorization();
app.MapGroup("/partner/roles").MapPartnerRoles().RequireAuthorization();
app.MapGroup("/partner/idtype").MapPartnerID().RequireAuthorization();
app.MapGroup("/partner/eactivity").MapPartnerEconomicActivity().RequireAuthorization();
app.MapGroup("/partner/company").MapPartnerCompany().RequireAuthorization();
app.MapGroup("/country").MapCountry().RequireAuthorization();
app.MapGroup("/region").MapRegion().RequireAuthorization();
app.MapGroup("/city").MapCity().RequireAuthorization();
app.MapGroup("/idtype").MapIDType().RequireAuthorization();
app.MapGroup("/eactivity").MapEconomicActivity().RequireAuthorization();
app.MapGroup("/nrange").MapNumberRange().RequireAuthorization();
app.MapGroup("/currency").MapCurrency().RequireAuthorization();
app.MapGroup("/log").MapAppLog().RequireAuthorization();

app.MapGroup("/invoice/type").MapInvoiceType().RequireAuthorization();
app.MapGroup("/saleorder/type").MapSaleOrderType().RequireAuthorization();
app.MapGroup("/saleorder").MapSaleOrder().RequireAuthorization();
app.MapGroup("/pricecond").MapPriceCondition().RequireAuthorization();
app.MapGroup("/pricescheme").MapPriceScheme().RequireAuthorization();

app.MapGroup("/unitm").MapUnitMeasure().RequireAuthorization();
app.MapGroup("/material").MapMaterial().RequireAuthorization();
app.MapGroup("/material/type").MapMaterialType().RequireAuthorization();
app.MapGroup("/material/features").MapMaterialFeature().RequireAuthorization();
app.MapGroup("/material/branch").MapMaterialBranch().RequireAuthorization();
app.MapGroup("/material/warehouse").MapMaterialWareHouse().RequireAuthorization();

app.MapGroup("/ebilling").MapEBilling().RequireAuthorization();
app.MapGroup("/ebilling/company").MapEBillingCompany().RequireAuthorization();
app.MapGroup("/ebilling/companyinvoice").MapEBillingCompanyInvoice().RequireAuthorization();
app.MapGroup("/ebilling/document").MapEBillingDocument().RequireAuthorization();

//app.MapGroup("/auth").MapIdentityApi<ApplicationUser>();

//app.MapGet("/testpdf", (IWebHostEnvironment env) =>
//{
//    var filePath = $"{env.ContentRootPath}\\Reports\\GenericSaleOrder.rdlc";

//    if (!File.Exists(filePath))
//        throw new Exception($"El formulario {filePath} no existe");

//    var fs = new FileStream(filePath, FileMode.Open);
//    LocalReport localReport = new LocalReport();
//    localReport.LoadReportDefinition(fs);

//    var dto = new List<SaleOrderPdfFormDTO>();

//    var imagePath = $"{env.WebRootPath}\\image\\LogoAvaLink.png";
//    var image = File.ReadAllBytes(imagePath);

//    dto.Add(new SaleOrderPdfFormDTO
//    {
//        SOType = "Documento de Prueba",
//        SODate = DateTime.Now,
//        SONumber = "1234567890",
//        CompanyLogo = image
//    });

//    localReport.DataSources.Add(new ReportDataSource("SaleOrderHeaderDataSet", dto));
//    localReport.DataSources.Add(new ReportDataSource("SaleOrderPositionDataSet", new List<SaleOrderPositionDTO>()));

//    fs.Close();

//    var ms = new MemoryStream(localReport.Render("PDF"));

//    return Results.File(ms, "application/pdf", "SaleOrderPDF.pdf");
//});
#endregion
app.Run();
