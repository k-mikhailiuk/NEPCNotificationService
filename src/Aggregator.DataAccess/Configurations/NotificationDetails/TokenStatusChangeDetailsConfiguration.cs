using Aggregator.DataAccess.Entities.TokenChangeStatus;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aggregator.DataAccess.Configurations.NotificationDetails;

/// <summary>
/// Конфигурация сущности <see cref="TokenStatusChangeDetails"/> для Entity Framework.
/// </summary>
/// <remarks>
/// Определяет настройки таблицы, первичный ключ, свойства и связи для сущности <see cref="TokenStatusChangeDetails"/>.
/// </remarks>
public class TokenStatusChangeDetailsConfiguration : IEntityTypeConfiguration<TokenStatusChangeDetails>
{
    /// <summary>
    /// Настраивает конфигурацию для сущности <see cref="TokenStatusChangeDetails"/>.
    /// </summary>
    /// <param name="builder">Построитель конфигурации для сущности <see cref="TokenStatusChangeDetails"/>.</param>
    public void Configure(EntityTypeBuilder<TokenStatusChangeDetails> builder)
    {
        builder.ToTable("TokenStatusChangeDetails");

        builder.HasKey(x => x.TokenStatusChangeDetailsId);

        builder.Property(x => x.TokenStatusChangeDetailsId).ValueGeneratedNever();
        builder.Property(x => x.DpanRef).IsRequired().HasMaxLength(48);
        builder.Property(x => x.PaymentSystem).IsRequired();
        builder.Property(x => x.Status).IsRequired();
        builder.Property(x => x.ChangeDate).IsRequired().HasMaxLength(14);
        builder.Property(x => x.DpanExpDate).IsRequired().HasMaxLength(4);
        builder.Property(x => x.WalletProvider).IsRequired().HasMaxLength(11);
        builder.Property(x => x.DeviceName).IsRequired(false).HasMaxLength(128);
        builder.Property(x => x.DeviceType).IsRequired(false).HasMaxLength(30);
        builder.Property(x => x.DeviceId).IsRequired(false).HasMaxLength(48);
        builder.Property(x => x.FpanRef).IsRequired(false).HasMaxLength(48);
        builder.Property(x => x.NotificationId).IsRequired();

        builder.OwnsOne(x => x.CardIdentifier);
    }
}