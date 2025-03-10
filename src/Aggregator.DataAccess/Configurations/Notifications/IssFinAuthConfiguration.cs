using Aggregator.DataAccess.Entities.IssFinAuth;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aggregator.DataAccess.Configurations.Notifications;

public class IssFinAuthConfiguration : IEntityTypeConfiguration<IssFinAuth>
{
    public void Configure(EntityTypeBuilder<IssFinAuth> builder)
    {
        builder.ToTable("IssFinAuths");

        builder.HasKey(x=>x.NotificationId);

        builder.Property(x => x.EventId).IsRequired();
        builder.Property(x => x.Time).IsRequired();
        builder.Property(x => x.DetailsId).IsRequired();
        builder.Property(x => x.MerchantInfoId).IsRequired();
        builder.Property(x => x.CardInfoId).IsRequired(false);

        builder.HasOne(x => x.Details)
            .WithOne()
            .HasForeignKey<IssFinAuth>(x => x.DetailsId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.MerchantInfo)
            .WithOne()
            .HasForeignKey<IssFinAuth>(x => x.MerchantInfoId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.CardInfo)
            .WithOne()
            .HasForeignKey<IssFinAuth>(x => x.CardInfoId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
