using Aggregator.DataAccess.Entities.AcctBalChange;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aggregator.DataAccess.Configurations.Notifications;

public class AcctBalChangeConfiguration : IEntityTypeConfiguration<AcctBalChange>
{
    public void Configure(EntityTypeBuilder<AcctBalChange> builder)
    {
        builder.ToTable("AcctBalChanges");

        builder.HasKey(x => x.AcctBalChangeId);
        
        builder.Property(x=>x.EventId).IsRequired();
        builder.Property(x=>x.Time).IsRequired();
        builder.Property(x=>x.DetailsId).IsRequired();
        builder.Property(x=>x.CardInfoId).IsRequired();
        
        builder.HasOne(x=>x.Details)
            .WithOne()
            .HasForeignKey<AcctBalChange>(x=>x.DetailsId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne(x=>x.CardInfo)
            .WithOne()
            .HasForeignKey<AcctBalChange>(x=>x.CardInfoId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasMany(x=>x.AccountsInfo)
            .WithOne()
            .HasForeignKey(x=>x.AcctBalChangeId)
            .OnDelete(DeleteBehavior.NoAction);
        
        builder.HasMany(x=>x.Extensions)
            .WithOne()
            .HasForeignKey(x=>x.NotificationId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}