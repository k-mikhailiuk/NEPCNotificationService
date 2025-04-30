using Aggregator.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aggregator.DataAccess.Configurations;

/// <summary>
/// Конфигурация сущности <see cref="CheckedLimit"/> для Entity Framework.
/// </summary>
/// <remarks>
/// Определяет настройки таблицы, первичный ключ, свойства и связи для сущности <see cref="CheckedLimit"/>.
/// </remarks>
public class CheckedLimitConfiguration : IEntityTypeConfiguration<CheckedLimit>
{
    /// <summary>
    /// Настраивает конфигурацию для сущности <see cref="CheckedLimit"/>.
    /// </summary>
    /// <param name="builder">Построитель конфигурации для сущности <see cref="CheckedLimit"/>.</param>
    public void Configure(EntityTypeBuilder<CheckedLimit> builder)
    {
        builder.ToTable("CheckedLimits");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).IsRequired().ValueGeneratedNever();
        builder.Property(x => x.ObjectType).IsRequired().HasConversion<byte>();
        builder.Property(x => x.Exceeded).IsRequired();
        builder.Property(x => x.IssFinAuthDetailsId).IsRequired();
    }
}