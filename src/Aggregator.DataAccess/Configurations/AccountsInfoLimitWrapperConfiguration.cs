using Aggregator.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aggregator.DataAccess.Configurations;

public class AccountsInfoLimitWrapperConfiguration : IEntityTypeConfiguration<AccountsInfoLimitWrapper>
{
    public void Configure(EntityTypeBuilder<AccountsInfoLimitWrapper> builder)
    {
        builder.ToTable("AccountsInfoLimitWrappers");
        
        builder.HasKey(x => x.Id);
        
        builder.Property(x=>x.Id).UseIdentityColumn().ValueGeneratedOnAdd();
        builder.Property(x=>x.AccountsInfoNotificationId).IsRequired();
        builder.Property(x => x.LimitType).IsRequired().HasConversion<byte>();
        builder.Property(x=>x.CardInfoId).IsRequired();
        builder.Property(x=>x.LimitId).IsRequired();
        
        builder.HasOne(x=>x.Limit)
            .WithMany()
            .HasForeignKey(x=>x.LimitId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}