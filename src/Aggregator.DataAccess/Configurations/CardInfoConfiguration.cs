using Aggregator.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aggregator.DataAccess.Configurations;

public class CardInfoConfiguration : IEntityTypeConfiguration<CardInfo>
{
    public void Configure(EntityTypeBuilder<CardInfo> builder)
    {
        builder.ToTable("CardInfos");
        
        builder.HasKey(x => x.Id);
        
        builder.Property(x=>x.Id).ValueGeneratedOnAdd();
        builder.Property(x => x.ExpDate).IsRequired();
        builder.Property(x => x.RefPan).IsRequired();
        builder.Property(x => x.ContractId).IsRequired();
        builder.Property(x => x.MobilePhone).IsRequired(false);

        builder.OwnsOne(x => x.CardIdentifier);
        
        builder.HasMany(x=>x.Limits)
            .WithOne()
            .HasForeignKey(x=>x.Id)
            .OnDelete(DeleteBehavior.Cascade);
    }
}