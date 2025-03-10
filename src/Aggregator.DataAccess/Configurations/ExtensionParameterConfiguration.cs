using Aggregator.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aggregator.DataAccess.Configurations;

public class ExtensionParameterConfiguration : IEntityTypeConfiguration<ExtensionParameter>
{
    public void Configure(EntityTypeBuilder<ExtensionParameter> builder)
    {
        builder.ToTable("ExtensionParameters");
        
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Name).IsRequired().HasMaxLength(256);
        builder.Property(x => x.Value).IsRequired();
        
        builder.HasOne(p => p.Extension)
            .WithMany(e => e.ExtensionParameters)
            .HasForeignKey(p => p.NotificationExtensionId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}