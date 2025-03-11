using Aggregator.DataAccess.Entities.TokenChangeStatus;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aggregator.DataAccess.Configurations.Notifications;

public class TokenStatusChangeConfiguration : IEntityTypeConfiguration<TokenStatusChange>
{
    public void Configure(EntityTypeBuilder<TokenStatusChange> builder)
    {
        builder.ToTable("TokenStatusChanges");
        
        builder.HasKey(x=>x.NotificationId);

        builder.Property(x => x.EventId).IsRequired();
        builder.Property(x=>x.Time).IsRequired();
        builder.Property(x=>x.DetailsId).IsRequired();
        builder.Property(x=>x.CardInfoId).IsRequired();
        
        builder.HasOne(x => x.CardInfo)
            .WithMany()
            .HasForeignKey(x=>x.CardInfoId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne(x=>x.Details)
            .WithMany()
            .HasForeignKey(x=>x.DetailsId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}