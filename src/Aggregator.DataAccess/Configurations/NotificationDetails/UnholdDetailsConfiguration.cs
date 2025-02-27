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
        builder.Property(x => x.CorrespondingAccount).IsRequired().HasMaxLength(4);
        builder.Property(x => x.AccountId).IsRequired().HasMaxLength(32);

        builder.OwnsOne(x => x.AuthMoney, parameters =>
        {
            parameters.Property(x => x.Amount).IsRequired();
            parameters.Property(x => x.Currency).IsRequired().HasMaxLength(3);
        });
        
        builder.Property(x => x.UnholdDirection).IsRequired();

        builder.OwnsOne(x => x.UnholdMoney, parameters =>
        {
            parameters.Property(x => x.Amount).IsRequired();
            parameters.Property(x => x.Currency).IsRequired().HasMaxLength(3);
        });
        
        builder.Property(x => x.LocalTime).IsRequired();
        builder.Property(x => x.TransactionDate).IsRequired();
        builder.Property(x => x.ApprovalCode).IsRequired().HasMaxLength(6);
        builder.Property(x => x.Rrn).IsRequired().HasMaxLength(12);
        
        builder.OwnsOne(x => x.IssFee, parameters =>
        {
            parameters.Property(x => x.Amount).IsRequired(false);
            parameters.Property(x => x.Currency).IsRequired(false).HasMaxLength(3);
        });
        
        builder.Property(x => x.IssFeeDirection).IsRequired(false);
        builder.Property(x => x.SvTrace).IsRequired(false);
        
        builder.OwnsOne(x => x.WalletProvider, parameters =>
        {
            parameters.Property(x=>x.PaymentSystem).IsRequired(false);
            parameters.Property(x=>x.Id).IsRequired(false).HasMaxLength(11);
        });
        
        builder.Property(x => x.Dpan).IsRequired(false).HasMaxLength(19);
        
        builder.OwnsOne(x => x.CardIdentifier);
    }
}