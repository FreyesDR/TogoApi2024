using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using XDev_Model.Entities;

namespace XDev_Model.Configuration
{
    public class PolicyConfig : IEntityTypeConfiguration<Policy>
    {
        public void Configure(EntityTypeBuilder<Policy> builder)
        {
            builder.HasKey("Id");
            builder.Property(p => p.Name).HasMaxLength(15);

            builder.HasMany(m => m.EndPointPolicies)
                .WithOne(o => o.Policy)
                .HasForeignKey(f => f.PolicyId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
