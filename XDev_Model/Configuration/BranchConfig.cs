using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using XDev_Model.Entities;

namespace XDev_Model.Configuration
{
    public class BranchConfig : IEntityTypeConfiguration<Branch>
    {
        public void Configure(EntityTypeBuilder<Branch> builder)
        {
            
            builder.HasKey("Id");
            builder.HasIndex(p => p.Code).IsUnique(false);
            builder.Property(p => p.Code).HasMaxLength(4);
            builder.Property(p => p.Name).HasMaxLength(100);
            builder.Property(p => p.ConcurrencyStamp).IsConcurrencyToken();
            builder.Property(p => p.CreatedBy).HasMaxLength(256);
            builder.Property(p => p.LastUpdatedBy).HasMaxLength(256);

            builder.HasMany(m => m.Addresses)
                .WithOne(o => o.Branch)
                .HasForeignKey(f => f.BranchId)
                .IsRequired(false);

            builder.HasMany(m => m.WareHouses)
                .WithOne(o => o.Branch)
                .HasForeignKey(f => f.BranchId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(m => m.PointsSale)
                .WithOne(o => o.Branch)
                .HasForeignKey(f => f.BranchId)
                .OnDelete(DeleteBehavior.Cascade);            
        }
    }
}
