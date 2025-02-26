using Aggregator.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aggregator.DataAccess.Configurations;

public class NotificationExtensionConfiguration : IEntityTypeConfiguration<NotificationExtension>
{
    public void Configure(EntityTypeBuilder<NotificationExtension> builder)
    {
        builder.ToTable("NotificationExtensions");
        
        builder.HasKey(x => x.Id);
        
        builder.Property(x=>x.Id).ValueGeneratedOnAdd();
        builder.Property(x => x.ExtensionId).IsRequired();
        builder.Property(x => x.NotificationId).IsRequired();
        builder.Property(x => x.Critical).IsRequired();
        
        builder.HasMany(x=>x.ExtesionParameters)
            .WithOne()
            .HasForeignKey(x=>x.ExtensionId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}