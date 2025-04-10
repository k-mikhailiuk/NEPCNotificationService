using Aggregator.DataAccess.Entities.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aggregator.DataAccess.Configurations;

public class AccountsInfoConfiguration : IEntityTypeConfiguration<AccountsInfo>
{
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