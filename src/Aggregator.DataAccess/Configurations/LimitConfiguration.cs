using Aggregator.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aggregator.DataAccess.Configurations;

public class LimitConfiguration : IEntityTypeConfiguration<Limit>
{
    public void Configure(EntityTypeBuilder<Limit> builder)
    {
        builder.ToTable("Limits");
        
        builder.HasKey(x=>x.Id);
        
        builder.Property(x=>x.Id).ValueGeneratedOnAdd();
        builder.Property(x=>x.LimitId).IsRequired();
        builder.Property(x=>x.CycleType).IsRequired(false).HasMaxLength(30);
        builder.Property(x=>x.CycleLength).IsRequired(false);
        builder.Property(x=>x.EndTime).IsRequired(false);
        builder.Property(x=>x.Currency).IsRequired(false).HasMaxLength(3);
        builder.Property(x=>x.TrsValue).IsRequired();
        builder.Property(x=>x.UsedValue).IsRequired();
        builder.Property(x=>x.LimitType).IsRequired().HasConversion<byte>();
    }
}