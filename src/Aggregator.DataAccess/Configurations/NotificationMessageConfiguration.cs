using Aggregator.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aggregator.DataAccess.Configurations;

/// <summary>
/// Конфигурация сущности <see cref="NotificationMessage"/> для Entity Framework.
/// </summary>
/// <remarks>
/// Определяет настройки таблицы, первичный ключ, свойства и связи для сущности <see cref="NotificationMessage"/>.
/// </remarks>
public class NotificationMessageConfiguration : IEntityTypeConfiguration<NotificationMessage>
{
    /// <summary>
    /// Настраивает конфигурацию для сущности <see cref="NotificationMessage"/>.
    /// </summary>
    /// <param name="builder">Построитель конфигурации для сущности <see cref="NotificationMessage"/>.</param>
    public void Configure(EntityTypeBuilder<NotificationMessage> builder)
    {
        builder.ToTable("NotificationMessages");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).UseIdentityColumn();
        builder.Property(x => x.Title).IsRequired();
        builder.Property(x => x.Message).IsRequired();
        builder.Property(x => x.Status).IsRequired().HasConversion<byte>();
        builder.Property(x => x.CustomerId).IsRequired();
    }
}