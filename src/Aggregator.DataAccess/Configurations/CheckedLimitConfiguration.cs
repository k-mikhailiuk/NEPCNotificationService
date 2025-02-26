using Aggregator.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aggregator.DataAccess.Configurations;

public class CheckedLimitConfiguration : IEntityTypeConfiguration<CheckedLimit>
{
    public void Configure(EntityTypeBuilder<CheckedLimit> builder)
    {
        builder.ToTable("CheckedLimits");
        
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.ObjectType).IsRequired().HasConversion<byte>();
        builder.Property(x=>x.Exceeded).IsRequired();
        builder.Property(x=>x.IssFinAuthDetailsId).IsRequired();
    }
}