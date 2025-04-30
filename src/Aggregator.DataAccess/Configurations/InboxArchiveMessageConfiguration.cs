using Aggregator.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aggregator.DataAccess.Configurations;

/// <summary>
/// Конфигурация сущности <see cref="InboxArchiveMessage"/> для Entity Framework.
/// </summary>
/// <remarks>
/// Определяет настройки таблицы, первичный ключ, свойства и связи для сущности <see cref="InboxArchiveMessage"/>.
/// </remarks>
public class InboxArchiveMessageConfiguration : IEntityTypeConfiguration<InboxArchiveMessage>
{
    /// <summary>
    /// Настраивает конфигурацию для сущности <see cref="InboxArchiveMessage"/>.
    /// </summary>
    /// <param name="builder">Построитель конфигурации для сущности <see cref="InboxArchiveMessage"/>.</param>
    public void Configure(EntityTypeBuilder<InboxArchiveMessage> builder)
    {
        builder.ToTable("InboxArchiveMessages");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).UseIdentityColumn();
        builder.Property(x => x.Payload).IsRequired().HasColumnType("nvarchar(max)");
        builder.Property(x => x.Timestamp).IsRequired();
    }
}