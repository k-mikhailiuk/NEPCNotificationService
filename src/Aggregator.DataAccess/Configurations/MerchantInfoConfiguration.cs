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
        builder.Property(x => x.Mcc).IsRequired();
        builder.Property(x => x.TerminalId).IsRequired(false);
        builder.Property(x => x.Aid).IsRequired(false);
        builder.Property(x => x.Name).IsRequired(false);
        builder.Property(x => x.Street).IsRequired(false);
        builder.Property(x => x.City).IsRequired(false);
        builder.Property(x => x.Country).IsRequired(false);
        builder.Property(x => x.ZipCode).IsRequired(false);
    }
}