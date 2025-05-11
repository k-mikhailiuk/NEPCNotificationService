using Aggregator.DataAccess.Entities.OwiUserAction;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aggregator.DataAccess.Configurations.Notifications;

/// <summary>
/// Конфигурация сущности <see cref="OwiUserAction"/> для Entity Framework.
/// </summary>
/// <remarks>
/// Определяет настройки таблицы, первичный ключ, свойства и связи для сущности <see cref="OwiUserAction"/>.
/// </remarks>
public class OwiUserActionConfiguration : IEntityTypeConfiguration<OwiUserAction>
{
    /// <summary>
    /// Настраивает конфигурацию для сущности <see cref="OwiUserAction"/>.
    /// </summary>
    /// <param name="builder">Построитель конфигурации для сущности <see cref="OwiUserAction"/>.</param>
    public void Configure(EntityTypeBuilder<OwiUserAction> builder)
    {
        builder.ToTable("OwiUserActions");

        builder.Property(x => x.CardInfoId).IsRequired(false);
        builder.Property(x => x.DetailsId).IsRequired();

        builder.HasOne(x => x.Details)
            .WithOne()
            .HasForeignKey<OwiUserAction>(x => x.DetailsId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.CardInfo)
            .WithMany()
            .HasForeignKey(x => x.CardInfoId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}