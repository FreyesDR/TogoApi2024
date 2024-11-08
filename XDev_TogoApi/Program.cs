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
    };

    var errorBL = context.RequestServices.GetRequiredService<IAppLogBL>();
    await errorBL.CreateAsync(error);

    await TypedResults.BadRequest(new ExceptionReturnDTO
    { Message = UtilsExtension.InternalErrorMessage, StatusCode = StatusCodes.Status500InternalServerError.ToString() }).ExecuteAsync(context);
}));

app.UseHttpsRedirection();

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
app.MapGroup("/address/type").MapAddressType().RequireAuthorization();
app.MapGroup("/address").MapAddress().RequireAuthorization();
app.MapGroup("/partner/type").MapPartnerType().RequireAuthorization();
app.MapGroup("/country").MapCountry().RequireAuthorization();
app.MapGroup("/region").MapRegion().RequireAuthorization();
app.MapGroup("/city").MapCity().RequireAuthorization();
app.MapGroup("/idtype").MapIDType().RequireAuthorization();
app.MapGroup("/eactivity").MapEconomicActivity().RequireAuthorization();

//app.MapGroup("/auth").MapIdentityApi<ApplicationUser>();
#endregion
app.Run();
