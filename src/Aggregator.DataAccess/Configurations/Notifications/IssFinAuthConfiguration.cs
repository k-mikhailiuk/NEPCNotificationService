using Aggregator.DataAccess.Entities.IssFinAuth;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aggregator.DataAccess.Configurations.Notifications;

/// <summary>
/// Конфигурация сущности <see cref="IssFinAuth"/> для Entity Framework.
/// </summary>
/// <remarks>
/// Определяет настройки таблицы, первичный ключ, свойства и связи для сущности <see cref="IssFinAuth"/>.
/// </remarks>
public class IssFinAuthConfiguration : IEntityTypeConfiguration<IssFinAuth>
{
    /// <summary>
    /// Настраивает конфигурацию для сущности <see cref="IssFinAuth"/>.
    /// </summary>
    /// <param name="builder">Построитель конфигурации для сущности <see cref="IssFinAuth"/>.</param>
    public void Configure(EntityTypeBuilder<IssFinAuth> builder)
    {
        builder.ToTable("IssFinAuths");

        builder.HasKey(x => x.NotificationId);

        builder.Property(x => x.EventId).IsRequired();
        builder.Property(x => x.Time).IsRequired();
        builder.Property(x => x.DetailsId).IsRequired();
        builder.Property(x => x.MerchantInfoId).IsRequired();
        builder.Property(x => x.CardInfoId).IsRequired(false);

        builder.HasOne(x => x.Details)
            .WithMany()
            .HasForeignKey(x => x.DetailsId)
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