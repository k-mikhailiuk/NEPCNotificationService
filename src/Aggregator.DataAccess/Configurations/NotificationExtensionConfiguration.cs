using Aggregator.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aggregator.DataAccess.Configurations;

public class NotificationExtensionConfiguration : IEntityTypeConfiguration<NotificationExtension>
{
    public void Configure(EntityTypeBuilder<NotificationExtension> builder)
    {
        builder.ToTable("NotificationExtensions");
        
        builder.HasKey(x => x.ExtensionId);
        
        builder.Property(x => x.NotificationId).IsRequired();
        builder.Property(x => x.Critical).IsRequired();
        
        builder.HasMany(x => x.ExtesionParameters)
            .WithOne(x => x.Extension)
            .HasForeignKey(x => x.ExtensionId)
            .HasPrincipalKey(x => x.ExtensionId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}