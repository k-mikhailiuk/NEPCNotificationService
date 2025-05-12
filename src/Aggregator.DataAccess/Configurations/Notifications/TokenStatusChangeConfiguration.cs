using Aggregator.DataAccess.Entities.TokenChangeStatus;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aggregator.DataAccess.Configurations.Notifications;

/// <summary>
/// Конфигурация сущности <see cref="TokenStatusChange"/> для Entity Framework.
/// </summary>
/// <remarks>
/// Определяет настройки таблицы, первичный ключ, свойства и связи для сущности <see cref="TokenStatusChange"/>.
/// </remarks>
public class TokenStatusChangeConfiguration : IEntityTypeConfiguration<TokenStatusChange>
{
    /// <summary>
    /// Настраивает конфигурацию для сущности <see cref="TokenStatusChange"/>.
    /// </summary>
    /// <param name="builder">Построитель конфигурации для сущности <see cref="TokenStatusChange"/>.</param>
    public void Configure(EntityTypeBuilder<TokenStatusChange> builder)
    {
        builder.ToTable("TokenStatusChanges");
        
        builder.Property(x => x.CardInfoId).IsRequired();

        builder.HasOne(x => x.CardInfo)
            .WithMany()
            .HasForeignKey(x => x.CardInfoId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Details)
            .WithOne()
            .HasForeignKey<TokenStatusChangeDetails>(x => x.NotificationId)
            .HasPrincipalKey<TokenStatusChange>(x=>x.NotificationId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}