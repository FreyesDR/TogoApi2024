using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using XDev_Model.Entities;

namespace XDev_Model.Configuration
{
    public class PaymentConditionConfig : IEntityTypeConfiguration<PaymentCondition>
    {
        public void Configure(EntityTypeBuilder<PaymentCondition> builder)
        {
            builder.HasKey("Id");
            builder.HasIndex(i => i.Code).IsUnique();
            builder.Property(p => p.Code).HasMaxLength(4);            
            builder.Property(p => p.Name).HasMaxLength(50);
            builder.Property(p => p.Tipo).HasMaxLength(1); 
            builder.Property(p => p.Plazo).HasMaxLength(2);
            builder.Property(p => p.ConcurrencyStamp).IsConcurrencyToken();
            builder.Property(p => p.CreatedBy).HasMaxLength(256);
            builder.Property(p => p.LastUpdatedBy).HasMaxLength(256);

            builder.HasMany(m => m.Partners)
                .WithOne(o => o.PaymentCondition)
                .HasForeignKey(f => f.PaymentConditionId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(m => m.SaleOrders)
                .WithOne(o => o.PaymentCondition)
                .HasForeignKey(f => f.PaymentConditionId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(m => m.Invoices)
                .WithOne(o => o.PaymentCondition)
                .HasForeignKey(f => f.PaymentConditionId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
