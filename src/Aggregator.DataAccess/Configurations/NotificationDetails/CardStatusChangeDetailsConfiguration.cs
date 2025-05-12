using Aggregator.DataAccess.Entities.CardStatusChange;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aggregator.DataAccess.Configurations.NotificationDetails;

/// <summary>
/// Конфигурация сущности <see cref="CardStatusChangeDetails"/> для Entity Framework.
/// </summary>
/// <remarks>
/// Определяет настройки таблицы, первичный ключ, свойства и связи для сущности <see cref="CardStatusChangeDetails"/>.
/// </remarks>
public class CardStatusChangeDetailsConfiguration : IEntityTypeConfiguration<CardStatusChangeDetails>
{
    /// <summary>
    /// Настраивает конфигурацию для сущности <see cref="CardStatusChangeDetails"/>.
    /// </summary>
    /// <param name="builder">Построитель конфигурации для сущности <see cref="CardStatusChangeDetails"/>.</param>
    public void Configure(EntityTypeBuilder<CardStatusChangeDetails> builder)
    {
        builder.ToTable("CardStatusChangeDetails");

        builder.HasKey(x => x.CardStatusChangeDetailsId);

        builder.Property(x => x.CardStatusChangeDetailsId).ValueGeneratedNever();
        builder.Property(x => x.ExpDate).IsRequired().HasMaxLength(4);
        builder.Property(x => x.OldStatus).IsRequired();
        builder.Property(x => x.NewStatus).IsRequired();
        builder.Property(x => x.ChangeDate).IsRequired();
        builder.Property(x => x.Service).IsRequired(false).HasMaxLength(30);
        builder.Property(x => x.UserName).IsRequired(false).HasMaxLength(30);
        builder.Property(x => x.Note).IsRequired(false).HasMaxLength(400);
        builder.Property(x => x.NotificationId).IsRequired();

        builder.OwnsOne(x => x.CardIdentifier);
    }
}