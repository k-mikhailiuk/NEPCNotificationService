using Aggregator.DataAccess.Entities.ABSEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aggregator.DataAccess.Configurations.ABSEntities;

/// <summary>
/// Конфигурация сущности <see cref="PushNotificationSettings"/> для Entity Framework.
/// </summary>
/// <remarks>
/// Определяет схему таблицы, ключи, ограничения и собственные типы для сущности <see cref="PushNotificationSettings"/>.
/// </remarks>
public class PushNotificationSettingsConfiguration : IEntityTypeConfiguration<PushNotificationSettings>
{
    /// <summary>
    /// Настраивает конфигурацию для сущности <see cref="PushNotificationSettings"/>.
    /// </summary>
    /// <param name="builder">Построитель конфигурации для сущности <see cref="PushNotificationSettings"/>.</param>
    public void Configure(EntityTypeBuilder<PushNotificationSettings> builder)
    {
        builder.ToTable("Settings", "PushNotification", t => t.ExcludeFromMigrations());

        builder.HasKey(x => new { x.LoginID, x.CustomerID })
            .HasName("PK_Settings");

        builder.Property(x => x.LoginID)
            .IsRequired();

        builder.Property(x => x.CustomerID)
            .IsRequired();

        builder.Property(x => x.LoginName)
            .HasMaxLength(20)
            .IsUnicode()
            .IsRequired(false);

        builder.Property(x => x.IsActive)
            .IsRequired();

        builder.Property(x => x.LastRequestDate)
            .HasColumnType("datetime")
            .HasDefaultValueSql("getdate()")
            .IsRequired();

        builder.Property(x => x.LanguageID)
            .HasDefaultValue(1)
            .IsRequired();
    }
}