using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Reflection;
using System.Security.Claims;
using XDev_AIO;
using XDev_Model.Entities;
using XDev_Model.Interfaces;

namespace XDev_Model
{
    public class ApplicationDbContext: IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        private readonly IHttpContextAccessor contentAccessor;
        private readonly IConfiguration configuration;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IHttpContextAccessor contentAccessor, IConfiguration configuration)
            : base(options)
        {
            this.contentAccessor = contentAccessor;
            this.configuration = configuration;
        }

        private string GetUserId()
        {
            if (contentAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                var idClaim = contentAccessor.HttpContext.User.Identities.FirstOrDefault().Claims.Where(w => w.Type == ClaimTypes.NameIdentifier).FirstOrDefault();
                if (idClaim is not null)
                {
                    return idClaim.Value;
                }
                return Guid.Empty.ToString();
            }

            return string.Empty;
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            ProcessSaveChangeAsync();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void ProcessSaveChangeAsync()
        {
            foreach (var itm in ChangeTracker.Entries().Where(e => e.State == EntityState.Added && e.Entity is IAuditEntity))
            {
                var entity = itm.Entity as IAuditEntity;
                entity.CreatedBy = GetUserId();
                entity.CreatedAt = DateTime.Now;
                entity.ConcurrencyStamp = Guid.NewGuid().ToString();
            }

            foreach (var itm in ChangeTracker.Entries().Where(e => e.State == EntityState.Modified && e.Entity is IAuditEntity))
            {
                var entity = itm.Entity as IAuditEntity;
                entity.LastUpdatedBy = GetUserId();
                entity.LastUpdatedAt = DateTime.Now;
                entity.ConcurrencyStamp = Guid.NewGuid().ToString();
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var config = JsonConvert.DeserializeObject<ConfigDTO>(Utils.GetConfig(""));
            optionsBuilder.UseSqlServer(config.SQLConnectionString);
            
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            SeedingInitialData.Seed(builder);
        }

        public DbSet<Company> Company { get; set; }
        public DbSet<CompanyType> CompanyType { get; set; }
        public DbSet<CompanyEconomicActivity> CompanyEconomicActivities { get; set; }
        public DbSet<CompanyID> CompanyID { get; set; }
        public DbSet<Address> Address { get; set; }
        public DbSet<AddressType> AddressType { get; set; }
        public DbSet<AddressEmail> AddressEmail { get; set; }   
        public DbSet<AddressPhone> AddressPhone { get; set; }
        public DbSet<Country> Country { get; set; }
        public DbSet<Region> Region { get; set; }
        public DbSet<City> City { get; set; }
        public DbSet<EconomicActivity> EconomicActivities { get; set; }
        public DbSet<IDType> IDType { get; set; }
        public DbSet<AppLog> AppLog { get; set; }
        public DbSet<BranchType> BranchType { get; set; }
        public DbSet<Branch> Branch { get; set; }
    }

    internal class ConfigDTO
    {
        public string FrontEndUrl { get; set; }                
        public string SQLConnectionString { get; set; }
    }
}
