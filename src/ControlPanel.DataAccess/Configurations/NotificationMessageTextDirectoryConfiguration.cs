using ControlPanel.DataAccess.Entites;
using ControlPanel.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ControlPanel.DataAccess.Configurations;

public class NotificationMessageTextDirectoryConfiguration : IEntityTypeConfiguration<NotificationMessageTextDirectory>
{
    public void Configure(EntityTypeBuilder<NotificationMessageTextDirectory> builder)
    {
        builder.ToTable("NotificationMessageTextDirectories");

        builder.HasKey(x => x.Id);

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