using ControlPanel.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ControlPanel.DataAccess.Configurations;

public class LimitIdDescriptionDirectoryConfiguration : IEntityTypeConfiguration<LimitIdDescriptionDirectory>
{
    public void Configure(EntityTypeBuilder<LimitIdDescriptionDirectory> builder)
    {
        builder.ToTable("LimitIdDescriptionDirectories");
        
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        builder.Property(x => x.LimitCode).IsRequired();
        builder.Property(x => x.Name).IsRequired();
        builder.Property(x => x.DescriptionRu).IsUnicode().IsRequired();
        builder.Property(x => x.DescriptionKg).IsUnicode().IsRequired();
        builder.Property(x => x.DescriptionEn).IsUnicode().IsRequired();
    }
}