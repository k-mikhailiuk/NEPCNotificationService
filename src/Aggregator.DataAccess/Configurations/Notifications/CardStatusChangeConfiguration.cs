using Aggregator.DataAccess.Entities.CardStatusChange;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aggregator.DataAccess.Configurations.Notifications;

/// <summary>
/// Конфигурация сущности <see cref="CardStatusChange"/> для Entity Framework.
/// </summary>
/// <remarks>
/// Определяет настройки таблицы, первичный ключ, свойства и связи для сущности <see cref="CardStatusChange"/>.
/// </remarks>
public class CardStatusChangeConfiguration : IEntityTypeConfiguration<CardStatusChange>
{
    /// <summary>
    /// Настраивает конфигурацию для сущности <see cref="CardStatusChange"/>.
    /// </summary>
    /// <param name="builder">Построитель конфигурации для сущности <see cref="CardStatusChange"/>.</param>
    public void Configure(EntityTypeBuilder<CardStatusChange> builder)
    {
        builder.ToTable("CardStatusChanges");
        
        builder.Property(x => x.CardInfoId).IsRequired();

        builder.HasOne(x => x.Details)
            .WithOne()
            .HasForeignKey<CardStatusChangeDetails>(x => x.NotificationId)
            .HasPrincipalKey<CardStatusChange>(x=>x.NotificationId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.CardInfo)
            .WithMany()
            .HasForeignKey(x => x.CardInfoId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}