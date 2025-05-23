using Aggregator.DataAccess.Entities.AcsOtp;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aggregator.DataAccess.Configurations.Notifications;

/// <summary>
/// Конфигурация сущности <see cref="AcsOtp"/> для Entity Framework.
/// </summary>
/// <remarks>
/// Определяет настройки таблицы, первичный ключ, свойства и связи для сущности <see cref="AcsOtp"/>.
/// </remarks>
public class AcsOtpConfiguration : IEntityTypeConfiguration<AcsOtp>
{
    /// <summary>
    /// Настраивает конфигурацию для сущности <see cref="AcsOtp"/>.
    /// </summary>
    /// <param name="builder">Построитель конфигурации для сущности <see cref="AcsOtp"/>.</param>
    public void Configure(EntityTypeBuilder<AcsOtp> builder)
    {
        builder.ToTable("AcsOtps");

        builder.Property(x => x.CardInfoId).IsRequired();

        builder.OwnsOne(x => x.MerchantInfo, merchantInfo =>
        {
            merchantInfo.Property(x => x.MerchantId).IsRequired().HasMaxLength(15);
            merchantInfo.Property(x => x.Name).IsRequired().HasMaxLength(25);
            merchantInfo.Property(x => x.Country).IsRequired().HasMaxLength(3);
            merchantInfo.Property(x => x.Url).IsRequired().HasMaxLength(4000);
        });
        
        builder.HasOne(x => x.Details)
            .WithOne()
            .HasForeignKey<AcsOtpDetails>(x => x.NotificationId)
            .HasPrincipalKey<AcsOtp>(x=>x.NotificationId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.CardInfo)
            .WithMany()
            .HasForeignKey(x => x.CardInfoId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}