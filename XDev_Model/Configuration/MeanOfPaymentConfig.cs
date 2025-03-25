using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using XDev_Model.Entities;

namespace XDev_Model.Configuration
{
    public class MeanOfPaymentConfig : IEntityTypeConfiguration<MeanOfPayment>
    {
        public void Configure(EntityTypeBuilder<MeanOfPayment> builder)
        {
            builder.HasKey("Id");
            builder.HasIndex(i => i.Code).IsUnique();
            builder.Property(p => p.Code).HasMaxLength(4);            
            builder.Property(p => p.Name).HasMaxLength(50);
            builder.Property(p => p.ConcurrencyStamp).IsConcurrencyToken();
            builder.Property(p => p.CreatedBy).HasMaxLength(256);
            builder.Property(p => p.LastUpdatedBy).HasMaxLength(256);
        }
    }
}
