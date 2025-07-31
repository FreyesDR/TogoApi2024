using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using XDev_Model.Entities;

namespace XDev_Model.Configuration
{
    public class AccountCatalogConfig : IEntityTypeConfiguration<AccountCatalog>
    {
        public void Configure(EntityTypeBuilder<AccountCatalog> builder)
        {
            builder.HasKey("Id");
            builder.Property(p => p.Code).HasMaxLength(50);
            builder.HasIndex(p => p.Code).IsUnique();
            builder.Property(p => p.Name).HasMaxLength(150);
            builder.Property(p => p.ConcurrencyStamp).IsConcurrencyToken();
            builder.Property(p => p.CreatedBy).HasMaxLength(256);
            builder.Property(p => p.LastUpdatedBy).HasMaxLength(256);

            builder.HasMany(m => m.ChildrenCatalog)
                .WithOne(o => o.ParentCatalog)
                .HasForeignKey(o => o.ParentId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
