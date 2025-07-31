using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using XDev_Model.Entities;

namespace XDev_Model.Configuration
{
    public class JournalTypeConfig : IEntityTypeConfiguration<JournalType>
    {
        public void Configure(EntityTypeBuilder<JournalType> builder)
        {
            builder.HasKey("Id");            
            builder.Property(p => p.Name).HasMaxLength(50);
            builder.Property(p => p.ConcurrencyStamp).IsConcurrencyToken();
            builder.Property(p => p.CreatedBy).HasMaxLength(256);
            builder.Property(p => p.LastUpdatedBy).HasMaxLength(256);

            builder.HasMany( m => m.Journals)
                .WithOne(o => o.JournalType)
                .HasForeignKey(f => f.JournalTypeId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
