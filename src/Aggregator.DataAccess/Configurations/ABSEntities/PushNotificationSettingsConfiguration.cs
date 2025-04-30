using Aggregator.DataAccess.Entities;
using Aggregator.DataAccess.Entities.ABSEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aggregator.DataAccess.Configurations.ABSEntities;

public class PushNotificationSettingsConfiguration : IEntityTypeConfiguration<PushNotificationSettings>
{
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