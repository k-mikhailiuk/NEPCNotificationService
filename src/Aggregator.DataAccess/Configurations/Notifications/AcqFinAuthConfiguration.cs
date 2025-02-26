using Aggregator.DataAccess.Entities.AcqFinAuth;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aggregator.DataAccess.Configurations.Notifications;

public class AcqFinAuthConfiguration : IEntityTypeConfiguration<AcqFinAuth>
{
    public void Configure(EntityTypeBuilder<AcqFinAuth> builder)
    {
        builder.ToTable("AcqFinAuths");
        
        builder.HasKey(x => x.AcqFinAuthId);
        
        builder.Property(x=>x.EventId).IsRequired();
        builder.Property(x=>x.Time).IsRequired();
        builder.Property(x=>x.DetailsId).IsRequired();
        builder.Property(x=>x.MerchantInfoId).IsRequired();
        
        builder.HasOne(x=>x.Details)
            .WithOne()
            .HasForeignKey<AcqFinAuth>(x=>x.DetailsId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne(x=>x.MerchantInfo)
            .WithOne()
            .HasForeignKey<AcqFinAuth>(x=>x.MerchantInfoId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasMany(x=>x.Extensions)
            .WithOne()
            .HasForeignKey(x=>x.NotificationId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}