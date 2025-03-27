using Aggregator.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aggregator.DataAccess.Configurations;

public class NotificationMessageConfiguration : IEntityTypeConfiguration<NotificationMessage>
{
    public void Configure(EntityTypeBuilder<NotificationMessage> builder)
    {
        builder.ToTable("NotificationMessages");

        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Title).IsRequired();
        builder.Property(x => x.Message).IsRequired();
        builder.Property(x=>x.Status).IsRequired().HasConversion<byte>();
        builder.Property(x=>x.CustomerId).IsRequired();
    }
}