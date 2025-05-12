using Aggregator.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aggregator.DataAccess.Configurations;

/// <summary>
/// Конфигурация сущности <see cref="AccountsInfoLimitWrapper"/> для Entity Framework.
/// </summary>
/// <remarks>
/// Этот класс задаёт конфигурацию таблицы, первичный ключ, свойства и связи для сущности <see cref="AccountsInfoLimitWrapper"/>.
/// </remarks>
public class AccountsInfoLimitWrapperConfiguration : IEntityTypeConfiguration<AccountsInfoLimitWrapper>
{
    /// <summary>
    /// Настраивает конфигурацию для сущности <see cref="AccountsInfoLimitWrapper"/>.
    /// </summary>
    /// <param name="builder">Построитель конфигурации для сущности <see cref="AccountsInfoLimitWrapper"/>.</param>
    public void Configure(EntityTypeBuilder<AccountsInfoLimitWrapper> builder)
    {
        builder.ToTable("AccountsInfoLimitWrappers");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).UseIdentityColumn();
        builder.Property(x => x.AccountsInfoId).IsRequired();
        builder.Property(x => x.LimitType).IsRequired().HasConversion<byte>();
        builder.Property(x => x.CardInfoId).IsRequired();
        builder.Property(x => x.LimitId).IsRequired();

        builder.HasOne(p => p.AccountInfo)
            .WithMany(e => e.Limits)
            .HasForeignKey(p => p.AccountsInfoId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Limit)
            .WithMany()
            .HasForeignKey(x => x.LimitId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}