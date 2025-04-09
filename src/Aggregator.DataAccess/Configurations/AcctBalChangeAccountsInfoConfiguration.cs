using Aggregator.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aggregator.DataAccess.Configurations;

public class AcctBalChangeAccountsInfoConfiguration : IEntityTypeConfiguration<AcctBalChangeAccountsInfo>
{
    public void Configure(EntityTypeBuilder<AcctBalChangeAccountsInfo> builder)
    {
        builder.ToTable("AcctBalChangeAccountsInfos");

        builder.HasKey(x => x.Id);
        
        builder.Property(x=>x.Id).ValueGeneratedOnAdd();
        builder.Property(x => x.AccountsInfoId).IsRequired().HasMaxLength(32);
        builder.Property(x => x.NotificationId).IsRequired();
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
        
        builder.HasMany(x => x.Limits)
            .WithOne()
            .HasForeignKey(x => x.AccountsInfoNotificationId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}