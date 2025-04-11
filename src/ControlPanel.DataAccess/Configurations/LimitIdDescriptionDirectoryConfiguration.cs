using ControlPanel.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ControlPanel.DataAccess.Configurations;

/// <summary>
/// Конфигурация сущности LimitIdDescriptionDirectory.
/// </summary>
public class LimitIdDescriptionDirectoryConfiguration : IEntityTypeConfiguration<LimitIdDescriptionDirectory>
{
    /// <summary>
    /// Настраивает таблицу и свойства для сущности LimitIdDescriptionDirectory.
    /// </summary>
    /// <param name="builder">Построитель конфигурации для сущности LimitIdDescriptionDirectory.</param>
    public void Configure(EntityTypeBuilder<LimitIdDescriptionDirectory> builder)
    {
        builder.ToTable("LimitIdDescriptionDirectories");
        
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        builder.Property(x => x.LimitCode).IsRequired();
        builder.Property(x => x.Name).IsRequired();
        builder.Property(x => x.DescriptionRu).IsUnicode().IsRequired();
        builder.Property(x => x.DescriptionKg).IsUnicode().IsRequired();
        builder.Property(x => x.DescriptionEn).IsUnicode().IsRequired();
    }
}