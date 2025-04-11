using Aggregator.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aggregator.DataAccess.Configurations;

/// <summary>
/// Конфигурация сущности <see cref="FinTransaction"/> для Entity Framework.
/// </summary>
/// <remarks>
/// Определяет настройки таблицы, первичный ключ, свойства и связи для сущности <see cref="FinTransaction"/>.
/// </remarks>
public class FinTransactionConfiguration : IEntityTypeConfiguration<FinTransaction>
{
    /// <summary>
    /// Настраивает конфигурацию для сущности <see cref="FinTransaction"/>.
    /// </summary>
    /// <param name="builder">Построитель конфигурации для сущности <see cref="FinTransaction"/>.</param>
    public void Configure(EntityTypeBuilder<FinTransaction> builder)
    {
        builder.ToTable("FinTransactions");

        builder.HasKey(x => x.FinTransactionId);

        builder.Property(x => x.FeTrans).IsRequired(false).HasMaxLength(7);

        builder.OwnsOne(x => x.TranMoney, parameters =>
        {
            parameters.Property(x => x.Amount).IsRequired(false);
            parameters.Property(x => x.Currency).IsRequired(false).HasMaxLength(3);
        });

        builder.Property(x => x.Direction).IsRequired(false);
        builder.Property(x => x.MerchantInfoId).IsRequired(false);
        builder.Property(x => x.CorrespondingAccountType).IsRequired(false);

        builder.HasOne(x => x.MerchantInfo)
            .WithOne()
            .HasForeignKey<FinTransaction>(x => x.MerchantInfoId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}