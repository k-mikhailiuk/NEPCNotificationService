using Aggregator.DataAccess.Entities.TokenChangeStatus;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aggregator.DataAccess.Configurations.NotificationDetails;

public class TokenStatusChangeDetailsConfiguration : IEntityTypeConfiguration<TokenStatusChangeDetails>
{
    public void Configure(EntityTypeBuilder<TokenStatusChangeDetails> builder)
    {
        builder.ToTable("TokenStatusChangeDetails");

        builder.HasKey(x => x.TokenStatusChangeDetailsId);
        
        builder.Property(x=>x.TokenStatusChangeDetailsId).ValueGeneratedOnAdd();
        builder.Property(x=>x.DpanRef).IsRequired();
        builder.Property(x=>x.PaymentSystem).IsRequired();
        builder.Property(x=>x.Status).IsRequired();
        builder.Property(x=>x.ChangeDate).IsRequired();
        builder.Property(x=>x.DpanExpDate).IsRequired();
        builder.Property(x=>x.WalletProvider).IsRequired();
        builder.Property(x=>x.DeviceName).IsRequired(false);
        builder.Property(x=>x.DeviceType).IsRequired(false);
        builder.Property(x=>x.FpanRef).IsRequired(false);

        builder.OwnsOne(x => x.CardIdentifier);
    }
}