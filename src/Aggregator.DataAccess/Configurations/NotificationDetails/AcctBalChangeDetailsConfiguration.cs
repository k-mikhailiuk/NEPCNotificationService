using Aggregator.DataAccess.Entities.AcctBalChange;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aggregator.DataAccess.Configurations.NotificationDetails;

public class AcctBalChangeDetailsConfiguration : IEntityTypeConfiguration<AcctBalChangeDetails>
{
    public void Configure(EntityTypeBuilder<AcctBalChangeDetails> builder)
    {
        builder.ToTable("AcctBalChangeDetails");

        builder.HasKey(x => x.AcctBalChangeDetailsId);
        
        builder.Property(x => x.Reversal).IsRequired();
        builder.Property(x => x.TransType).IsRequired();
        builder.Property(x => x.TransactionTime).IsRequired();

        builder.OwnsOne(x => x.Auth);
        
        builder.Property(x=>x.FinTransId).IsRequired(false);
        builder.Property(x=>x.IssInstId).IsRequired();
        builder.Property(x=>x.AccountId).IsRequired();

        builder.OwnsOne(x => x.AccountAmount);
        
        builder.Property(x=>x.Direction).IsRequired();

        builder.OwnsOne(x => x.AccountBalance);
        
        builder.HasOne(x=>x.FinTrans)
            .WithOne()
            .HasForeignKey<AcctBalChangeDetails>(x=>x.FinTransId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}