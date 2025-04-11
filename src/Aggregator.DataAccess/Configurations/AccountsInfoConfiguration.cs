using Aggregator.DataAccess.Entities.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aggregator.DataAccess.Configurations;

/// <summary>
/// Конфигурация сущности <see cref="AccountsInfo"/> для Entity Framework.
/// </summary>
/// <remarks>
/// Определяет схему таблицы, ключи, ограничения и собственные типы для сущности <see cref="AccountsInfo"/>.
/// </remarks>
public class AccountsInfoConfiguration : IEntityTypeConfiguration<AccountsInfo>
{
    /// <summary>
    /// Настраивает сущность <see cref="AccountsInfo"/>.
    /// </summary>
    /// <param name="builder">Построитель конфигурации для сущности <see cref="AccountsInfo"/>.</param>
    public void Configure(EntityTypeBuilder<AccountsInfo> builder)
    {
        builder.HasKey(x => x.Id);

        builder.ToTable("AccountsInfo");

        builder.Property(x => x.NotificationId).IsRequired();
        builder.Property(x => x.NotificationType).IsRequired();
        builder.Property(x => x.AccountsInfoId).IsRequired().HasMaxLength(32);
        builder.Property(x => x.Type).IsRequired();

        builder.OwnsOne(x => x.AviableBalance, parameters =>
        {
            parameters.Property(x => x.Amount).IsRequired();
            parameters.Property(x => x.Currency).IsRequired().HasMaxLength(3);
        });
        builder.OwnsOne(x => x.ExceedLimit, parameters =>
        {
            parameters.Property(x => x.Amount).IsRequired(false);
            parameters.Property(x => x.Currency).IsRequired(false).HasMaxLength(3);
        });
    }
}