using Aggregator.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aggregator.DataAccess.Configurations;

public class MerchantInfoConfiguration : IEntityTypeConfiguration<MerchantInfo>
{
    public void Configure(EntityTypeBuilder<MerchantInfo> builder)
    {
        builder.ToTable("MerchantInfos");
        
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.MerchantId).IsRequired(false);
        builder.Property(x => x.Mcc).IsRequired().HasMaxLength(4);
        builder.Property(x => x.TerminalId).IsRequired(false).HasMaxLength(8);
        builder.Property(x => x.Aid).IsRequired(false).HasMaxLength(11);
        builder.Property(x => x.Name).IsRequired(false).HasMaxLength(25);
        builder.Property(x => x.Street).IsRequired(false).HasMaxLength(31);
        builder.Property(x => x.City).IsRequired(false).HasMaxLength(31);
        builder.Property(x => x.Country).IsRequired(false).HasMaxLength(3);
        builder.Property(x => x.ZipCode).IsRequired(false).HasMaxLength(10);
    }
}