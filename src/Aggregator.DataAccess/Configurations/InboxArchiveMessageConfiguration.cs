using Aggregator.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aggregator.DataAccess.Configurations;

public class InboxArchiveMessageConfiguration : IEntityTypeConfiguration<InboxArchiveMessage>
{
    /// <summary>
    /// Конфигурация InboxMessage
    /// </summary>
    /// <param name="builder"></param>
    public void Configure(EntityTypeBuilder<InboxArchiveMessage> builder)
    {
        builder.ToTable("InboxArchiveMessages");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        builder.Property(x => x.Payload).IsRequired().HasColumnType("nvarchar(max)");
        builder.Property(x => x.Timestamp).IsRequired();
    }
}