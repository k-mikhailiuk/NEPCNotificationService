using ControlPanel.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ControlPanel.DataAccess.Configurations;

/// <summary>
/// Конфигурация сущности NotificationMessageKeyWord.
/// </summary>
public class NotificationMessageKeyWordConfiguration : IEntityTypeConfiguration<NotificationMessageKeyWord>
{
    /// <summary>
    /// Настраивает таблицу и свойства для сущности NotificationMessageKeyWord.
    /// </summary>
    /// <param name="builder">Построитель конфигурации для сущности NotificationMessageKeyWord.</param>
    public void Configure(EntityTypeBuilder<NotificationMessageKeyWord> builder)
    {
        builder.ToTable("NotificationMessageKeyWords");
        
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.KeyWord).IsRequired();
        builder.Property(x => x.Description).IsUnicode().IsRequired(false);
        builder.Property(x => x.NotificationType).IsRequired().HasConversion<byte>();
    }
}