using Aggregator.DataAccess.Entities.PinChange;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aggregator.DataAccess.Configurations.Notifications;

public class PinChangeConfiguration : IEntityTypeConfiguration<PinChange>
{
    public void Configure(EntityTypeBuilder<PinChange> builder)
    {
        builder.ToTable("PinChanges");

        builder.HasKey(x => x.PinChangeId);
        
        builder.Property(x => x.EventId).IsRequired();
        builder.Property(x => x.Time).IsRequired();
        builder.Property(x=>x.DetailsId).IsRequired();
        builder.Property(x=>x.CardInfoId).IsRequired();
        
        builder.HasOne(x=>x.Details)
            .WithOne()
            .HasForeignKey<PinChange>(x=>x.DetailsId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne(x=>x.CardInfo)
            .WithOne()
            .HasForeignKey<PinChange>(x=>x.CardInfoId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasMany(x=>x.Extensions)
            .WithOne()
            .HasForeignKey(x=>x.NotificationId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}