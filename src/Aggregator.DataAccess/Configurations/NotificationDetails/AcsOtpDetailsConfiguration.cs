using Aggregator.DataAccess.Entities.AcsOtp;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aggregator.DataAccess.Configurations.NotificationDetails;

/// <summary>
/// Конфигурация сущности <see cref="AcsOtpDetails"/> для Entity Framework.
/// </summary>
/// <remarks>
/// Определяет настройки таблицы, первичный ключ, свойства и связи для сущности <see cref="AcsOtpDetails"/>.
/// </remarks>
public class AcsOtpDetailsConfiguration : IEntityTypeConfiguration<AcsOtpDetails>
{
    /// <summary>
    /// Настраивает конфигурацию для сущности <see cref="AcsOtpDetails"/>.
    /// </summary>
    /// <param name="builder">Построитель конфигурации для сущности <see cref="AcsOtpDetails"/>.</param>
    public void Configure(EntityTypeBuilder<AcsOtpDetails> builder)
    {
        builder.HasKey(x => x.DetailsId);
        
        builder.Property(d => d.TransactionTime).IsRequired();
        builder.Property(x => x.NotificationId).IsRequired();
        builder.Property(x => x.DetailsId).IsRequired().ValueGeneratedNever();

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