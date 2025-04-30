using Aggregator.DataAccess.Entities.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aggregator.DataAccess.Configurations;

/// <summary>
/// Конфигурация сущности <see cref="AccountInfo"/> для Entity Framework.
/// </summary>
/// <remarks>
/// Определяет схему таблицы, ключи, ограничения и собственные типы для сущности <see cref="AccountInfo"/>.
/// </remarks>
public class AccountsInfoConfiguration : IEntityTypeConfiguration<AccountInfo>
{
    /// <summary>
    /// Настраивает сущность <see cref="AccountInfo"/>.
    /// </summary>
    /// <param name="builder">Построитель конфигурации для сущности <see cref="AccountInfo"/>.</param>
    public void Configure(EntityTypeBuilder<AccountInfo> builder)
    {
        builder.HasKey(x => x.Id);

        builder.ToTable("AccountInfo");

        builder.Property(x => x.Id).UseIdentityColumn();
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