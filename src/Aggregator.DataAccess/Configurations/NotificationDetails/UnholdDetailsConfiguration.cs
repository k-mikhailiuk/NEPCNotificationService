using Aggregator.DataAccess.Entities.Unhold;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aggregator.DataAccess.Configurations.NotificationDetails;

public class UnholdDetailsConfiguration : IEntityTypeConfiguration<UnholdDetails>
{
    public void Configure(EntityTypeBuilder<UnholdDetails> builder)
    {
        builder.ToTable("UnholdDetails");

        builder.HasKey(x => x.UnholdId);
        
        builder.Property(x => x.Reversal).IsRequired();
        builder.Property(x => x.TransType).IsRequired();
        builder.Property(x => x.CorrespondingAccount).IsRequired();
        builder.Property(x => x.AccountId).IsRequired();
        builder.Property(x => x.AccountId).IsRequired();

        builder.OwnsOne(x => x.AuthMoney);
        
        builder.Property(x => x.UnholdDirection).IsRequired();

        builder.OwnsOne(x => x.UnholdMoney);
        
        builder.Property(x => x.LocalTime).IsRequired();
        builder.Property(x => x.TransactionDate).IsRequired();
        builder.Property(x => x.ApprovalCode).IsRequired();
        builder.Property(x => x.Rrn).IsRequired();
        
        builder.OwnsOne(x => x.IssFee);
        
        builder.Property(x => x.IssFeeDirection).IsRequired(false);
        builder.Property(x => x.SvTrace).IsRequired(false);
        
        builder.OwnsOne(x => x.WalletProvider);
        
        builder.Property(x => x.Dpan).IsRequired(false);
        
        builder.OwnsOne(x => x.CardIdentifier);
    }
}