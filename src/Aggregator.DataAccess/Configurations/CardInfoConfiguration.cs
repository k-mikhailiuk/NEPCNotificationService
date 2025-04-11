using Aggregator.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aggregator.DataAccess.Configurations;

/// <summary>
/// Конфигурация сущности <see cref="CardInfo"/> для Entity Framework.
/// </summary>
/// <remarks>
/// Определяет настройки таблицы, первичный ключ, свойства и связи для сущности <see cref="CardInfo"/>.
/// </remarks>
public class CardInfoConfiguration : IEntityTypeConfiguration<CardInfo>
{
    /// <summary>
    /// Настраивает конфигурацию для сущности <see cref="CardInfo"/>.
    /// </summary>
    /// <param name="builder">Построитель конфигурации для сущности <see cref="CardInfo"/>.</param>
    public void Configure(EntityTypeBuilder<CardInfo> builder)
    {
        builder.ToTable("CardInfos");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        builder.Property(x => x.ExpDate).IsRequired().HasMaxLength(4);
        builder.Property(x => x.RefPan).IsRequired().HasMaxLength(64);
        builder.Property(x => x.ContractId).IsRequired().HasMaxLength(6);
        builder.Property(x => x.MobilePhone).IsRequired(false).HasMaxLength(16);

        builder.OwnsOne(x => x.CardIdentifier);

        builder.HasMany(x => x.Limits)
            .WithOne(x => x.CardInfo)
            .HasForeignKey(x => x.CardInfoId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}