using Aggregator.DataAccess.Entities.Unhold;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aggregator.DataAccess.Configurations.Notifications;

/// <summary>
/// Конфигурация сущности <see cref="Unhold"/> для Entity Framework.
/// </summary>
/// <remarks>
/// Определяет настройки таблицы, первичный ключ, свойства и связи для сущности <see cref="Unhold"/>.
/// </remarks>
public class UnholdConfiguration : IEntityTypeConfiguration<Unhold>
{
    /// <summary>
    /// Настраивает конфигурацию для сущности <see cref="Unhold"/>.
    /// </summary>
    /// <param name="builder">Построитель конфигурации для сущности <see cref="Unhold"/>.</param>
    public void Configure(EntityTypeBuilder<Unhold> builder)
    {
        builder.ToTable("Unholds");

        builder.Property(x => x.CardInfoId).IsRequired();
        builder.Property(x => x.DetailsId).IsRequired();
        builder.Property(x => x.MerchantInfoId).IsRequired();

        builder.HasOne(x => x.Details)
            .WithOne()
            .HasForeignKey<Unhold>(x => x.DetailsId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.MerchantInfo)
            .WithMany()
            .HasForeignKey(x => x.MerchantInfoId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.CardInfo)
            .WithMany()
            .HasForeignKey(x => x.CardInfoId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}