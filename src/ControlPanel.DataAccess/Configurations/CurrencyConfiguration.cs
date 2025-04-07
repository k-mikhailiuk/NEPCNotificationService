using ControlPanel.DataAccess.Entites;
using ControlPanel.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ControlPanel.DataAccess.Configurations;

public class CurrencyConfiguration : IEntityTypeConfiguration<Currency>
{
    public void Configure(EntityTypeBuilder<Currency> builder)
    {
        builder.ToTable("Currencies");
        
        builder.HasKey(x => x.CurrencyCode);
        
        builder.Property(x => x.CurrencyName).IsRequired();
        builder.Property(x => x.CurrencySymbol).IsRequired();
    }
}