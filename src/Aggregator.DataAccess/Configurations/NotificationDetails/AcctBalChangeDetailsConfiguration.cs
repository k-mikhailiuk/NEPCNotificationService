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
        builder.Property(x => x.TransType).IsRequired().HasMaxLength(3);
        builder.Property(x => x.TransactionTime).IsRequired();

        builder.OwnsOne(x => x.Auth);
        
        builder.Property(x=>x.FinTransId).IsRequired(false);
        builder.Property(x=>x.IssInstId).IsRequired().HasMaxLength(4);
        builder.Property(x=>x.AccountId).IsRequired().HasMaxLength(32);

        builder.OwnsOne(x => x.AccountAmount, parameters =>
        {
            parameters.Property(x => x.Amount).IsRequired();
            parameters.Property(x => x.Currency).HasMaxLength(3);
        });
        
        builder.Property(x=>x.Direction).IsRequired();

        builder.OwnsOne(x => x.AccountBalance, parameters =>
        {
            parameters.Property(x => x.Amount).IsRequired();
            parameters.Property(x => x.Currency).HasMaxLength(3);
        });
        
        builder.HasOne(x=>x.FinTrans)
            .WithMany()
            .HasForeignKey(x=>x.FinTransId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}