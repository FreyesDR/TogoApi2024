using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using XDev_Model.Entities;

namespace XDev_Model.Configuration
{
    public class BranchTypeConfig : IEntityTypeConfiguration<BranchType>
    {
        public void Configure(EntityTypeBuilder<BranchType> builder)
        {
            
            builder.HasKey("Id");
            builder.HasIndex(p => p.Code).IsUnique();
            builder.Property(p => p.Code).HasMaxLength(2);
            builder.Property(p => p.Name).HasMaxLength(30);
            builder.Property(p => p.ConcurrencyStamp).IsConcurrencyToken();
            builder.Property(p => p.CreatedBy).HasMaxLength(256);
            builder.Property(p => p.LastUpdatedBy).HasMaxLength(256);

            builder.HasMany(m => m.Branches)
                .WithOne(o => o.BranchType)
                .HasForeignKey(f => f.BranchTypeId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
