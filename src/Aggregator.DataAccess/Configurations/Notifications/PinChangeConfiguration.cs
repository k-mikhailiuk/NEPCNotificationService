using Aggregator.DataAccess.Entities.PinChange;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aggregator.DataAccess.Configurations.Notifications;

/// <summary>
/// Конфигурация сущности <see cref="PinChange"/> для Entity Framework.
/// </summary>
/// <remarks>
/// Определяет настройки таблицы, первичный ключ, свойства и связи для сущности <see cref="PinChange"/>.
/// </remarks>
public class PinChangeConfiguration : IEntityTypeConfiguration<PinChange>
{
    /// <summary>
    /// Настраивает конфигурацию для сущности <see cref="PinChange"/>.
    /// </summary>
    /// <param name="builder">Построитель конфигурации для сущности <see cref="PinChange"/>.</param>
    public void Configure(EntityTypeBuilder<PinChange> builder)
    {
        builder.ToTable("PinChanges");

        builder.Property(x => x.DetailsId).IsRequired();
        builder.Property(x => x.CardInfoId).IsRequired();

        builder.HasOne(x => x.Details)
            .WithOne()
            .HasForeignKey<PinChange>(x => x.DetailsId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.CardInfo)
            .WithMany()
            .HasForeignKey(x => x.CardInfoId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}