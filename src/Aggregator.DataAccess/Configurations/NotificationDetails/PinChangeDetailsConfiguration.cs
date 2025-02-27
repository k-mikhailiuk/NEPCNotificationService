using Aggregator.DataAccess.Entities.PinChange;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aggregator.DataAccess.Configurations.NotificationDetails;

public class PinChangeDetailsConfiguration : IEntityTypeConfiguration<PinChangeDetails>
{
    public void Configure(EntityTypeBuilder<PinChangeDetails> builder)
    {
        builder.ToTable("PinChangeDetails");

        builder.HasKey(x => x.PinChangeDetailsId);

        builder.Property(x=>x.PinChangeDetailsId).ValueGeneratedOnAdd();
        builder.Property(x => x.ExpDate).IsRequired().HasMaxLength(4);
        builder.Property(x => x.TransactionTime).IsRequired();
        builder.Property(x => x.Status).IsRequired();
        builder.Property(x => x.ResponseCode).IsRequired(false).HasMaxLength(6);
        builder.Property(x => x.Service).IsRequired().HasMaxLength(30);

        builder.OwnsOne(x => x.CardIdentifier);
    }
}