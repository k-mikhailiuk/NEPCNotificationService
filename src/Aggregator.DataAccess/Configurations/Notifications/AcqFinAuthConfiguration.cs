using Aggregator.DataAccess.Entities.AcqFinAuth;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aggregator.DataAccess.Configurations.Notifications;

/// <summary>
/// Конфигурация сущности <see cref="AcqFinAuth"/> для Entity Framework.
/// </summary>
/// <remarks>
/// Определяет настройки таблицы, первичный ключ, свойства и связи для сущности <see cref="AcqFinAuth"/>.
/// </remarks>
public class AcqFinAuthConfiguration : IEntityTypeConfiguration<AcqFinAuth>
{
    /// <summary>
    /// Настраивает конфигурацию для сущности <see cref="AcqFinAuth"/>.
    /// </summary>
    /// <param name="builder">Построитель конфигурации для сущности <see cref="AcqFinAuth"/>.</param>
    public void Configure(EntityTypeBuilder<AcqFinAuth> builder)
    {
        builder.ToTable("AcqFinAuths");

        builder.Property(x => x.MerchantInfoId).IsRequired();

        builder.HasOne(x => x.Details)
            .WithOne()
            .HasForeignKey<AcqFinAuthDetails>(x => x.NotificationId)
            .HasPrincipalKey<AcqFinAuth>(x=>x.NotificationId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.MerchantInfo)
            .WithMany()
            .HasForeignKey(x => x.MerchantInfoId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}