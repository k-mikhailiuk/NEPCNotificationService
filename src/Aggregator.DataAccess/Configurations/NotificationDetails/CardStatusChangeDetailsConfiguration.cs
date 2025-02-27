using Aggregator.DataAccess.Entities.CardStatusChange;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aggregator.DataAccess.Configurations.NotificationDetails;

public class CardStatusChangeDetailsConfiguration : IEntityTypeConfiguration<CardStatusChangeDetails>
{
    public void Configure(EntityTypeBuilder<CardStatusChangeDetails> builder)
    {
        builder.ToTable("CardStatusChangeDetails");

        builder.HasKey(x => x.CardStatusChangeDetailsId);
        
        builder.Property(x=>x.CardStatusChangeDetailsId).ValueGeneratedOnAdd();
        builder.Property(x=>x.ExpDate).IsRequired().HasMaxLength(4);
        builder.Property(x=>x.OldStatus).IsRequired();
        builder.Property(x=>x.NewStatus).IsRequired();
        builder.Property(x=>x.ChangeDate).IsRequired();
        builder.Property(x=>x.Service).IsRequired(false).HasMaxLength(30);
        builder.Property(x=>x.UserName).IsRequired(false).HasMaxLength(30);
        builder.Property(x=>x.Note).IsRequired(false).HasMaxLength(400);

        builder.OwnsOne(x => x.CardIdentifier);
    }
}