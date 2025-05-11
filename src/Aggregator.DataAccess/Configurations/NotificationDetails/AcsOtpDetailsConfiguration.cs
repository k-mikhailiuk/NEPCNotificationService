using Aggregator.DataAccess.Entities.AcsOtp;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aggregator.DataAccess.Configurations.NotificationDetails;

public class AcsOtpDetailsConfiguration : IEntityTypeConfiguration<AcsOtpDetails>
{
    public void Configure(EntityTypeBuilder<AcsOtpDetails> builder)
    {
        builder.HasKey(x => x.DetailsId);
        
        builder.Property(d => d.TransactionTime).IsRequired();

        builder.OwnsOne(d => d.OtpInfo, otp =>
        {
            otp.Property(o => o.Otp).IsRequired();
            otp.Property(o => o.ExpirationTime).IsRequired();
        });

        builder.OwnsOne(d => d.AuthMoney, auth =>
        {
            auth.Property(a => a.Amount).IsRequired(false);
            auth.Property(a => a.Currency)
                .HasMaxLength(3)
                .IsRequired(false);
        });
    }
}