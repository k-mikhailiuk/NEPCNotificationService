using Aggregator.DataAccess.Entities.AcqFinAuth;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aggregator.DataAccess.Configurations.NotificationDetails;

public class AcqFinAuthDetailsConfiguration : IEntityTypeConfiguration<AcqFinAuthDetails>
{
    public void Configure(EntityTypeBuilder<AcqFinAuthDetails> builder)
    {
        builder.ToTable("AcqFinAuthDetails");

        builder.HasKey(x => x.AcqFinAuthDetailsId);
        
        builder.Property(x => x.Reversal).IsRequired();
        builder.Property(x => x.TransType).IsRequired();
        builder.Property(x => x.ExpDate).IsRequired(false);
        builder.Property(x => x.AccountId).IsRequired(false);
        builder.Property(x => x.CorrespondingAccount).IsRequired();
        
        builder.OwnsOne(x => x.AuthMoney);

        builder.Property(x => x.AuthDirection).IsRequired();
        builder.Property(x => x.LocalTime).IsRequired();
        builder.Property(x => x.TransactionTime).IsRequired();
        builder.Property(x => x.ResponseCode).IsRequired();
        builder.Property(x => x.ApprovalCode).IsRequired(false);
        builder.Property(x => x.Rrn).IsRequired(false);

        builder.OwnsOne(x => x.AcqFee);
        
        builder.Property(x => x.AcqFeeDirection).IsRequired(false);
        
        builder.OwnsOne(x => x.ConvMoney);
        
        builder.Property(x => x.PhysTerm).IsRequired();
        builder.Property(x => x.AuthorizationCondition).IsRequired();
        builder.Property(x => x.PosEntryMode).IsRequired();
        builder.Property(x => x.ServiceId).IsRequired();
        builder.Property(x => x.ServiceCode).IsRequired();

        builder.OwnsOne(x => x.CardIdentifier);
    }
}