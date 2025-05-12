using Aggregator.DataAccess.Entities.PinChange;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aggregator.DataAccess.Configurations.NotificationDetails;

/// <summary>
/// Конфигурация сущности <see cref="PinChangeDetails"/> для Entity Framework.
/// </summary>
/// <remarks>
/// Определяет настройки таблицы, первичный ключ, свойства и связи для сущности <see cref="PinChangeDetails"/>.
/// </remarks>
public class PinChangeDetailsConfiguration : IEntityTypeConfiguration<PinChangeDetails>
{
    /// <summary>
    /// Настраивает конфигурацию для сущности <see cref="PinChangeDetails"/>.
    /// </summary>
    /// <param name="builder">Построитель конфигурации для сущности <see cref="PinChangeDetails"/>.</param>
    public void Configure(EntityTypeBuilder<PinChangeDetails> builder)
    {
        builder.ToTable("PinChangeDetails");

        builder.HasKey(x => x.PinChangeDetailsId);

        builder.Property(x => x.PinChangeDetailsId).ValueGeneratedNever();
        builder.Property(x => x.ExpDate).IsRequired().HasMaxLength(4);
        builder.Property(x => x.TransactionTime).IsRequired();
        builder.Property(x => x.Status).IsRequired();
        builder.Property(x => x.ResponseCode).IsRequired(false).HasMaxLength(6);
        builder.Property(x => x.Service).IsRequired().HasMaxLength(30);
        builder.Property(x => x.NotificationId).IsRequired();

        builder.OwnsOne(x => x.CardIdentifier);
    }
}