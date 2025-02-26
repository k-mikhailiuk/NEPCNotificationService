using Aggregator.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aggregator.DataAccess.Configurations;

public class LimitConfiguration : IEntityTypeConfiguration<Limit>
{
    public void Configure(EntityTypeBuilder<Limit> builder)
    {
        builder.ToTable("Limits");
        
        builder.HasKey(x=>x.LimitId);
        
        builder.Property(x=>x.CycleType).IsRequired(false);
        builder.Property(x=>x.CycleLength).IsRequired(false);
        builder.Property(x=>x.EndTime).IsRequired(false);
        builder.Property(x=>x.Currency).IsRequired(false);
        builder.Property(x=>x.TrsValue).IsRequired();
        builder.Property(x=>x.UsedValue).IsRequired();
        builder.Property(x=>x.LimitType).IsRequired().HasConversion<byte>();
    }
}