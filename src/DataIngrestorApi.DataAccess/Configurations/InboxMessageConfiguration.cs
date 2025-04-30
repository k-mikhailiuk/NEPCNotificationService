using DataIngrestorApi.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataIngrestorApi.DataAccess.Configurations;

/// <summary>
/// Класс конфигурации InboxMessage
/// </summary>
public class InboxMessageConfiguration : IEntityTypeConfiguration<InboxMessage>
{
    /// <summary>
    /// Конфигурация InboxMessage
    /// </summary>
    /// <param name="builder"></param>
    public void Configure(EntityTypeBuilder<InboxMessage> builder)
    {
        builder.ToTable("InboxMessages");
        
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Id).UseIdentityColumn();
        builder.Property(x => x.Payload).IsRequired().HasColumnType("nvarchar(max)");
        builder.Property(x => x.Status).IsRequired().HasConversion<byte>();
        builder.Property(x => x.Timestamp).IsRequired();
        
        builder.HasIndex(x=>x.Status).HasDatabaseName("IX_InboxMessages_Status");
    }
}