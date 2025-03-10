using Aggregator.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aggregator.DataAccess.Configurations;

public class IssFinAuthAccountsInfoConfiguration : IEntityTypeConfiguration<IssFinAuthAccountsInfo>
{
    public void Configure(EntityTypeBuilder<IssFinAuthAccountsInfo> builder)
    {
        builder.ToTable("IssFinAuthAccountsInfos");

        builder.HasKey(x => x.Id);
        
        builder.Property(x=>x.Id).ValueGeneratedOnAdd();
        builder.Property(x => x.AccountsInfoId).IsRequired().HasMaxLength(32);
        builder.Property(x => x.IssFinAuthId).IsRequired();
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
        
        builder.HasMany(x=>x.Limits)
            .WithOne()
            .HasForeignKey(x=>x.Id)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne(e => e.IssFinAuth)
            .WithMany(i => i.AccountsInfo)
            .HasForeignKey(e => e.IssFinAuthId)
            .HasPrincipalKey(i => i.NotificationId); 
    }
}