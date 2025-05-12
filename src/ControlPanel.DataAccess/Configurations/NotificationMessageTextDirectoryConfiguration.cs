using ControlPanel.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ControlPanel.DataAccess.Configurations;

/// <summary>
/// Конфигурация сущности NotificationMessageTextDirectory.
/// </summary>
public class NotificationMessageTextDirectoryConfiguration : IEntityTypeConfiguration<NotificationMessageTextDirectory>
{
    /// <summary>
    /// Настраивает таблицу и свойства для сущности NotificationMessageTextDirectory.
    /// </summary>
    /// <param name="builder">Построитель конфигурации для сущности NotificationMessageTextDirectory.</param>
    public void Configure(EntityTypeBuilder<NotificationMessageTextDirectory> builder)
    {
        builder.ToTable("NotificationMessageTextDirectories");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).UseIdentityColumn();
        builder.Property(x => x.MessageTextRu).IsUnicode().IsRequired(false);
        builder.Property(x => x.MessageTextEn).IsUnicode().IsRequired(false);
        builder.Property(x => x.MessageTextKg).IsUnicode().IsRequired(false);
        builder.Property(x => x.NotificationType).IsRequired().HasConversion<byte>();
        builder.Property(x => x.OperationType).IsRequired(false).HasConversion<int>();
        builder.Property(x => x.IsNeedSend).IsRequired();

        builder.HasIndex(n => n.NotificationType)
            .IsUnique()
            .HasFilter("OperationType IS NULL");

        builder.HasIndex(n => new { n.NotificationType, n.OperationType })
            .IsUnique()
            .HasFilter("OperationType IS NOT NULL");
    }
}