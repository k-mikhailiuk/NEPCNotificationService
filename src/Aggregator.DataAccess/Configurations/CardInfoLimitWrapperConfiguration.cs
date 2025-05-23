using Aggregator.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aggregator.DataAccess.Configurations;

/// <summary>
/// Конфигурация сущности <see cref="CardInfoLimitWrapper"/> для Entity Framework.
/// </summary>
/// <remarks>
/// Определяет настройки таблицы, первичный ключ, свойства и связи для сущности <see cref="CardInfoLimitWrapper"/>.
/// </remarks>
public class CardInfoLimitWrapperConfiguration : IEntityTypeConfiguration<CardInfoLimitWrapper>
{
    /// <summary>
    /// Настраивает конфигурацию для сущности <see cref="CardInfoLimitWrapper"/>.
    /// </summary>
    /// <param name="builder">Построитель конфигурации для сущности <see cref="CardInfoLimitWrapper"/>.</param>
    public void Configure(EntityTypeBuilder<CardInfoLimitWrapper> builder)
    {
        builder.ToTable("CardInfoLimitWrappers");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).UseIdentityColumn();
        builder.Property(x => x.LimitType).IsRequired().HasConversion<byte>();
        builder.Property(x => x.CardInfoId).IsRequired();
        builder.Property(x => x.LimitId).IsRequired();

        builder.HasOne(x => x.CardInfo)
            .WithMany(x => x.Limits)
            .HasForeignKey(x => x.CardInfoId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Limit)
            .WithMany()
            .HasForeignKey(x => x.LimitId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}