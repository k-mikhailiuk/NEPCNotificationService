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
        builder.Property(x => x.ExpDate).IsRequired(false).HasMaxLength(4);
        builder.Property(x => x.AccountId).IsRequired(false).HasMaxLength(32);
        builder.Property(x => x.CorrespondingAccount).IsRequired().HasMaxLength(4);
        
        builder.OwnsOne(x => x.AuthMoney, parameters =>
        {
            parameters.Property(x => x.Amount).IsRequired();
            parameters.Property(x => x.Currency).IsRequired().HasMaxLength(3);
        });

        builder.Property(x => x.AuthDirection).IsRequired();
        builder.Property(x => x.LocalTime).IsRequired();
        builder.Property(x => x.TransactionTime).IsRequired();
        builder.Property(x => x.ResponseCode).IsRequired().HasMaxLength(6);
        builder.Property(x => x.ApprovalCode).IsRequired(false).HasMaxLength(6);
        builder.Property(x => x.Rrn).IsRequired(false).HasMaxLength(12);

        builder.OwnsOne(x => x.AcqFee, parameters =>
        {
            parameters.Property(x => x.Amount).IsRequired(false);
            parameters.Property(x => x.Currency).IsRequired(false).HasMaxLength(3);
        });
        
        builder.Property(x => x.AcqFeeDirection).IsRequired(false);
        
        builder.OwnsOne(x => x.ConvMoney, parameters =>
        {
            parameters.Property(x => x.Amount).IsRequired(false);
            parameters.Property(x => x.Currency).IsRequired(false).HasMaxLength(3);
        });
        
        builder.Property(x => x.PhysTerm).IsRequired();
        builder.Property(x => x.AuthorizationCondition).IsRequired().HasMaxLength(12);
        builder.Property(x => x.PosEntryMode).IsRequired().HasMaxLength(4);
        builder.Property(x => x.ServiceId).IsRequired().HasMaxLength(12);
        builder.Property(x => x.ServiceCode).IsRequired().HasMaxLength(3);

        builder.OwnsOne(x => x.CardIdentifier);
    }
}