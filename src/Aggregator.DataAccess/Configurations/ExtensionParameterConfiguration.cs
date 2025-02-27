using Aggregator.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aggregator.DataAccess.Configurations;

public class ExtensionParameterConfiguration : IEntityTypeConfiguration<ExtensionParameter>
{
    public void Configure(EntityTypeBuilder<ExtensionParameter> builder)
    {
        builder.ToTable("ExtensionParameters");
        
        builder.HasKey(x => x.ExtensionId);
        
        builder.Property(x => x.Name).IsRequired().HasMaxLength(256);
        builder.Property(x => x.Value).IsRequired();
        
        builder.HasOne(x=>x.Extension)
            .WithMany(x=>x.ExtesionParameters)
            .HasForeignKey(x=>x.ExtensionId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}