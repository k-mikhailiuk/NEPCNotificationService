using Aggregator.DataAccess.Entities;
using Aggregator.DataAccess.Entities.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aggregator.DataAccess.Configurations;

public class NotificationExtensionConfiguration : IEntityTypeConfiguration<NotificationExtension>
{
    public void Configure(EntityTypeBuilder<NotificationExtension> builder)
    {
        builder.ToTable("NotificationExtensions");

        builder.HasKey(x => x.ExtensionId);
        
        builder.Property(x => x.ExtensionId).IsRequired().HasMaxLength(50);
        builder.Property(x => x.NotificationId).IsRequired();
        builder.Property(x => x.NotificationType).IsRequired();
        builder.Property(x => x.Critical).IsRequired();
    }
}