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
        builder.Property(x => x.AccountsInfoId).IsRequired();
        builder.Property(x => x.AcctBalChangeId).IsRequired();
        builder.Property(x => x.Type).IsRequired();
        
        builder.OwnsOne(x => x.AviableBalance);
        builder.OwnsOne(x => x.ExceedLimit);
        
        builder.HasMany(x=>x.Limits)
            .WithOne()
            .HasForeignKey(x=>x.Id)
            .OnDelete(DeleteBehavior.Cascade);
    }
}