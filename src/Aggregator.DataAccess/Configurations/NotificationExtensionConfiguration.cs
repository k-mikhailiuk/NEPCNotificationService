using Aggregator.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aggregator.DataAccess.Configurations;

/// <summary>
/// Конфигурация сущности <see cref="NotificationExtension"/> для Entity Framework.
/// </summary>
/// <remarks>
/// Определяет настройки таблицы, первичный ключ, свойства и связи для сущности <see cref="NotificationExtension"/>.
/// </remarks>
public class NotificationExtensionConfiguration : IEntityTypeConfiguration<NotificationExtension>
{
    /// <summary>
    /// Настраивает конфигурацию для сущности <see cref="NotificationExtension"/>.
    /// </summary>
    /// <param name="builder">Построитель конфигурации для сущности <see cref="NotificationExtension"/>.</param>
    public void Configure(EntityTypeBuilder<NotificationExtension> builder)
    {
        builder.ToTable("NotificationExtensions");

        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Id).UseIdentityColumn();

        builder
            .HasIndex(x => new { x.ExtensionId, x.NotificationId })
            .IsUnique();

        builder.Property(x => x.ExtensionId).IsRequired().HasMaxLength(50);
        builder.Property(x => x.NotificationId).IsRequired();
        builder.Property(x => x.NotificationType).IsRequired();
        builder.Property(x => x.Critical).IsRequired();
        
        builder.HasOne(e => e.Notification)
            .WithMany(n => n.Extensions)
            .HasForeignKey(e => e.NotificationId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}