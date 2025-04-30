using Aggregator.DataAccess.Entities.IssFinAuth;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aggregator.DataAccess.Configurations.NotificationDetails;

/// <summary>
/// Конфигурация сущности <see cref="IssFinAuthDetails"/> для Entity Framework.
/// </summary>
/// <remarks>
/// Определяет настройки таблицы, первичный ключ, свойства и связи для сущности <see cref="IssFinAuthDetails"/>.
/// </remarks>
public class IssFinAuthDetailsConfiguration : IEntityTypeConfiguration<IssFinAuthDetails>
{
    /// <summary>
    /// Настраивает конфигурацию для сущности <see cref="IssFinAuthDetails"/>.
    /// </summary>
    /// <param name="builder">Построитель конфигурации для сущности <see cref="IssFinAuthDetails"/>.</param>
    public void Configure(EntityTypeBuilder<IssFinAuthDetails> builder)
    {
        builder.ToTable("IssFinAuthDetails");

        builder.HasKey(x => x.IssFinAuthDetailsId);

        builder.Property(x=>x.IssFinAuthDetailsId).IsRequired().ValueGeneratedNever();
        builder.Property(x => x.Reversal).IsRequired();
        builder.Property(x => x.TransType).IsRequired();
        builder.Property(x => x.IssInstId).IsRequired().HasMaxLength(4);
        builder.Property(x => x.CorrespondingAccount).IsRequired().HasMaxLength(4);
        builder.Property(x => x.AccountId).IsRequired(false).HasMaxLength(32);

        builder.OwnsOne(x => x.AuthMoney, parameters =>
        {
            parameters.Property(x => x.Amount).IsRequired();
            parameters.Property(x => x.Currency).IsRequired().HasMaxLength(3);
        });

        builder.Property(x => x.AuthDirection).IsRequired();

        builder.OwnsOne(x => x.ConvMoney, parameters =>
        {
            parameters.Property(x => x.Amount).IsRequired(false);
            parameters.Property(x => x.Currency).IsRequired(false).HasMaxLength(3);
        });
        builder.OwnsOne(x => x.AccountBalance, parameters =>
        {
            parameters.Property(x => x.Amount).IsRequired(false);
            parameters.Property(x => x.Currency).IsRequired(false).HasMaxLength(3);
        });
        builder.OwnsOne(x => x.BillingMoney, parameters =>
        {
            parameters.Property(x => x.Amount).IsRequired(false);
            parameters.Property(x => x.Currency).IsRequired(false).HasMaxLength(3);
        });

        builder.Property(x => x.LocalTime).IsRequired();
        builder.Property(x => x.TransType).IsRequired();
        builder.Property(x => x.ResponseCode).IsRequired().HasMaxLength(6);
        builder.Property(x => x.ApprovalCode).IsRequired(false).HasMaxLength(6);
        builder.Property(x => x.Rrn).IsRequired(false).HasMaxLength(12);

        builder.OwnsOne(x => x.AcqFee, parameters =>
        {
            parameters.Property(x => x.Amount).IsRequired(false);
            parameters.Property(x => x.Currency).IsRequired(false).HasMaxLength(3);
        });

        builder.Property(x => x.AcqFeeDirection).IsRequired(false);

        builder.OwnsOne(x => x.IssFee, parameters =>
        {
            parameters.Property(x => x.Amount).IsRequired(false);
            parameters.Property(x => x.Currency).IsRequired(false).HasMaxLength(3);
        });

        builder.Property(x => x.IssFeeDirection).IsRequired(false);
        builder.Property(x => x.SvTrace).IsRequired(false);
        builder.Property(x => x.AuthorizationCondition).IsRequired(false).HasMaxLength(12);

        builder.OwnsOne(x => x.WalletProvider, parameters =>
        {
            parameters.Property(x => x.PaymentSystem).IsRequired(false);
            parameters.Property(x => x.Id).IsRequired(false).HasMaxLength(11);
        });

        builder.Property(x => x.Dpan).IsRequired(false).HasMaxLength(19);

        builder.OwnsOne(x => x.AuthMoneyDetails, details =>
        {
            details.OwnsOne(x => x.OwnFundsMoney);
            details.OwnsOne(x => x.ExceedLimitMoney);
        });
        builder.OwnsOne(x => x.CardIdentifier);

        builder.HasMany(x => x.CheckedLimits)
            .WithOne()
            .HasForeignKey(x => x.IssFinAuthDetailsId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}