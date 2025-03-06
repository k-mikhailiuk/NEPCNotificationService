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
        builder.Property(x => x.ExpDate).IsRequired().HasMaxLength(4);
        builder.Property(x => x.RefPan).IsRequired().HasMaxLength(64);
        builder.Property(x => x.ContractId).IsRequired().HasMaxLength(6);
        builder.Property(x => x.MobilePhone).IsRequired(false).HasMaxLength(16);

        builder.OwnsOne(x => x.CardIdentifier);
        
        builder.HasMany(x => x.Limits)
            .WithOne(x => x.CardInfo)
            .HasForeignKey(x => x.CardInfoId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}