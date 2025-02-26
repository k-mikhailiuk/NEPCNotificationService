using Aggregator.DataAccess.Entities.OwiUserAction;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aggregator.DataAccess.Configurations.Notifications;

public class OwiUserActionConfiguration : IEntityTypeConfiguration<OwiUserAction>
{
    public void Configure(EntityTypeBuilder<OwiUserAction> builder)
    {
        builder.ToTable("OwiUserActions");

        builder.HasKey(x => x.OwiUserActionId);
        
        builder.Property(x=>x.EventId).IsRequired();
        builder.Property(x=>x.Time).IsRequired();
        builder.Property(x=>x.CardInfoId).IsRequired(false);
        builder.Property(x=>x.DetailsId).IsRequired();
        
        builder.HasOne(x=>x.Details)
            .WithOne()
            .HasForeignKey<OwiUserAction>(x=>x.DetailsId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne(x=>x.CardInfo)
            .WithOne()
            .HasForeignKey<OwiUserAction>()
            .OnDelete(DeleteBehavior.SetNull);
        
        builder.HasMany(x=>x.Extensions)
            .WithOne()
            .HasForeignKey(x=>x.ExtensionId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}