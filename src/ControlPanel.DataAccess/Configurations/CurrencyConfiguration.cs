using ControlPanel.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ControlPanel.DataAccess.Configurations;

/// <summary>
/// Конфигурация сущности Currency.
/// </summary>
public class CurrencyConfiguration : IEntityTypeConfiguration<Currency>
{
    /// <summary>
    /// Настраивает таблицу и свойства для сущности Currency.
    /// </summary>
    /// <param name="builder">Построитель конфигурации для сущности Currency.</param>
    public void Configure(EntityTypeBuilder<Currency> builder)
    {
        builder.ToTable("Currencies");
        
        builder.HasKey(x => x.CurrencyCode);
        
        builder.Property(x => x.CurrencyName).IsRequired();
        builder.Property(x => x.CurrencySymbol).IsRequired();
    }
}