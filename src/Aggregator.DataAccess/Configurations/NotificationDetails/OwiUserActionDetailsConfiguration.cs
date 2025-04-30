using Aggregator.DataAccess.Entities.OwiUserAction;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aggregator.DataAccess.Configurations.NotificationDetails;

/// <summary>
/// Конфигурация сущности <see cref="OwiUserActionDetails"/> для Entity Framework.
/// </summary>
/// <remarks>
/// Определяет настройки таблицы, первичный ключ, свойства и связи для сущности <see cref="OwiUserActionDetails"/>.
/// </remarks>
public class OwiUserActionDetailsConfiguration : IEntityTypeConfiguration<OwiUserActionDetails>
{
    /// <summary>
    /// Настраивает конфигурацию для сущности <see cref="OwiUserActionDetails"/>.
    /// </summary>
    /// <param name="builder">Построитель конфигурации для сущности <see cref="OwiUserActionDetails"/>.</param>
    public void Configure(EntityTypeBuilder<OwiUserActionDetails> builder)
    {
        builder.ToTable("OwiUserActionDetails");

        builder.HasKey(x => x.OwiUserActionDetailsId);

        builder.Property(x => x.OwiUserActionDetailsId).ValueGeneratedNever();
        builder.Property(x => x.TransactionTime).IsRequired();
        builder.Property(x => x.Login).IsRequired().HasMaxLength(30);
        builder.Property(x => x.Action).IsRequired().HasMaxLength(30);
    }
}