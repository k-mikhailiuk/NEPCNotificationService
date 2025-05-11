using Aggregator.DataAccess.Entities.AcctBalChange;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aggregator.DataAccess.Configurations.Notifications;

/// <summary>
/// Конфигурация сущности <see cref="AcctBalChange"/> для Entity Framework.
/// </summary>
/// <remarks>
/// Определяет настройки таблицы, первичный ключ, свойства и связи для сущности <see cref="AcctBalChange"/>.
/// </remarks>
public class AcctBalChangeConfiguration : IEntityTypeConfiguration<AcctBalChange>
{
    /// <summary>
    /// Настраивает конфигурацию для сущности <see cref="AcctBalChange"/>.
    /// </summary>
    /// <param name="builder">Построитель конфигурации для сущности <see cref="AcctBalChange"/>.</param>
    public void Configure(EntityTypeBuilder<AcctBalChange> builder)
    {
        builder.ToTable("AcctBalChanges");

        builder.Property(x => x.DetailsId).IsRequired();
        builder.Property(x => x.CardInfoId).IsRequired();

        builder.HasOne(x => x.Details)
            .WithOne()
            .HasForeignKey<AcctBalChange>(x => x.DetailsId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.CardInfo)
            .WithMany()
            .HasForeignKey(x => x.CardInfoId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}