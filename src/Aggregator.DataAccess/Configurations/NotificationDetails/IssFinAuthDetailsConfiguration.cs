using Aggregator.DataAccess.Entities.IssFinAuth;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aggregator.DataAccess.Configurations.NotificationDetails;

public class IssFinAuthDetailsConfiguration : IEntityTypeConfiguration<IssFinAuthDetails>
{
    public void Configure(EntityTypeBuilder<IssFinAuthDetails> builder)
    {
        builder.ToTable("IssFinAuthDetails");
        
        builder.HasKey(x => x.IssFinAuthDetailsId);
        
        builder.Property(x=>x.Reversal).IsRequired();
        builder.Property(x=>x.TransType).IsRequired();
        builder.Property(x=>x.IssInstId).IsRequired();
        builder.Property(x=>x.CorrespondingAccount).IsRequired();
        builder.Property(x=>x.AccountId).IsRequired();
        builder.Property(x => x.AccountId).IsRequired(false);

        builder.OwnsOne(x => x.AuthMoney);
        
        builder.Property(x=>x.AuthDirection).IsRequired();
        
        builder.OwnsOne(x => x.ConvMoney);
        builder.OwnsOne(x => x.AccountBalance);
        builder.OwnsOne(x => x.BillingMoney);
        
        builder.Property(x => x.LocalTime).IsRequired();
        builder.Property(x=>x.TransType).IsRequired();
        builder.Property(x=>x.ResponseCode).IsRequired(false);
        builder.Property(x=>x.ApprovalCode).IsRequired(false);
        builder.Property(x=>x.Rrn).IsRequired(false);
        
        builder.OwnsOne(x => x.AcqFee);
        
        builder.Property(x=>x.AcqFeeDirection).IsRequired(false);
        
        builder.OwnsOne(x => x.IssFee);
        
        builder.Property(x=>x.IssFeeDirection).IsRequired(false);
        builder.Property(x=>x.SvTrace).IsRequired(false);

        builder.OwnsOne(x => x.WalletProvider);

        builder.Property(x => x.Dpan);

        builder.OwnsOne(x => x.AuthMoneyDetails, details =>
        {
            details.OwnsOne(x => x.OwnFundsMoney);
            details.OwnsOne(x => x.ExceedLimitMoney);
        });
        builder.OwnsOne(x => x.CardIdentifier);
        
        builder.HasMany(x=>x.CheckedLimits)
            .WithOne()
            .HasForeignKey(x=>x.IssFinAuthDetailsId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}