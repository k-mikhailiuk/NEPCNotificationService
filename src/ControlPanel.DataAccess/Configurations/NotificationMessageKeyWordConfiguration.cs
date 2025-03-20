using ControlPanel.DataAccess.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ControlPanel.DataAccess.Configurations;

public class NotificationMessageKeyWordConfiguration : IEntityTypeConfiguration<NotificationMessageKeyWord>
{
    public void Configure(EntityTypeBuilder<NotificationMessageKeyWord> builder)
    {
        builder.ToTable("NotificationMessageKeyWords");
        
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.KeyWord).IsRequired();
        builder.Property(x => x.Description).IsRequired(false);
        builder.Property(x => x.NotificationType).IsRequired().HasConversion<byte>();
    }
}