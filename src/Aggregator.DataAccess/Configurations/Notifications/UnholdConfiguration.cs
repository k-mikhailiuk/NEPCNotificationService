using Aggregator.DataAccess.Entities.Unhold;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aggregator.DataAccess.Configurations.Notifications;

public class UnholdConfiguration : IEntityTypeConfiguration<Unhold>
{
    public void Configure(EntityTypeBuilder<Unhold> builder)
    {
        builder.ToTable("Unholds");

        builder.HasKey(x => x.UnholdId);
        
        builder.Property(x => x.EventId).IsRequired();
        builder.Property(x=>x.Time).IsRequired();
        builder.Property(x=>x.CardInfoId).IsRequired();
        builder.Property(x=>x.DetailsId).IsRequired();
        builder.Property(x=>x.MerchantInfoId).IsRequired();
        
        builder.HasOne(x=>x.Details)
            .WithOne()
            .HasForeignKey<Unhold>(x=>x.DetailsId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne(x=>x.MerchantInfo)
            .WithOne()
            .HasForeignKey<Unhold>(x=>x.MerchantInfoId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne(x=>x.CardInfo)
            .WithOne()
            .HasForeignKey<Unhold>(x=>x.CardInfoId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasMany(x=>x.Extensions)
            .WithOne()
            .HasForeignKey(x=>x.NotificationId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}