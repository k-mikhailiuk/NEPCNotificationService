using Aggregator.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aggregator.DataAccess.Configurations;

/// <summary>
/// Конфигурация сущности <see cref="MerchantInfo"/> для Entity Framework.
/// </summary>
/// <remarks>
/// Определяет настройки таблицы, первичный ключ, свойства и связи для сущности <see cref="MerchantInfo"/>.
/// </remarks>
public class MerchantInfoConfiguration : IEntityTypeConfiguration<MerchantInfo>
{
    /// <summary>
    /// Настраивает конфигурацию для сущности <see cref="MerchantInfo"/>.
    /// </summary>
    /// <param name="builder">Построитель конфигурации для сущности <see cref="MerchantInfo"/>.</param>
    public void Configure(EntityTypeBuilder<MerchantInfo> builder)
    {
        builder.ToTable("MerchantInfos");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.MerchantId).IsRequired(false);
        builder.Property(x => x.Mcc).IsRequired().HasMaxLength(4);
        builder.Property(x => x.TerminalId).IsRequired(false).HasMaxLength(8);
        builder.Property(x => x.Aid).IsRequired(false).HasMaxLength(11);
        builder.Property(x => x.Name).IsRequired(false).HasMaxLength(25);
        builder.Property(x => x.Street).IsRequired(false).HasMaxLength(31);
        builder.Property(x => x.City).IsRequired(false).HasMaxLength(31);
        builder.Property(x => x.Country).IsRequired(false).HasMaxLength(3);
        builder.Property(x => x.ZipCode).IsRequired(false).HasMaxLength(10);
    }
}