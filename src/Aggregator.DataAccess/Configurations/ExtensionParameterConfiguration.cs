using Aggregator.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aggregator.DataAccess.Configurations;

/// <summary>
/// Конфигурация сущности <see cref="ExtensionParameter"/> для Entity Framework.
/// </summary>
/// <remarks>
/// Определяет настройки таблицы, первичный ключ, свойства и связи для сущности <see cref="ExtensionParameter"/>.
/// </remarks>
public class ExtensionParameterConfiguration : IEntityTypeConfiguration<ExtensionParameter>
{
    /// <summary>
    /// Настраивает конфигурацию для сущности <see cref="ExtensionParameter"/>.
    /// </summary>
    /// <param name="builder">Построитель конфигурации для сущности <see cref="ExtensionParameter"/>.</param>
    public void Configure(EntityTypeBuilder<ExtensionParameter> builder)
    {
        builder.ToTable("ExtensionParameters");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name).IsRequired().HasMaxLength(256);
        builder.Property(x => x.Value).IsRequired();

        builder.HasOne(p => p.Extension)
            .WithMany(e => e.ExtensionParameters)
            .HasForeignKey(p => p.NotificationExtensionId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}