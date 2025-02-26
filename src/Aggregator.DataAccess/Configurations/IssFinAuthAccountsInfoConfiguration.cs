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
        builder.Property(x => x.AccountsInfoId).IsRequired();
        builder.Property(x => x.IssFinAuthId).IsRequired();
        builder.Property(x => x.Type).IsRequired();
        
        builder.OwnsOne(x => x.AviableBalance);
        builder.OwnsOne(x => x.ExceedLimit);
        
        builder.HasMany(x=>x.Limits)
            .WithOne()
            .HasForeignKey(x=>x.Id)
            .OnDelete(DeleteBehavior.Cascade);
        
    }
}