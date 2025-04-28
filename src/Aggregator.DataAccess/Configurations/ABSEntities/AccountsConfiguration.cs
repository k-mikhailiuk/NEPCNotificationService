using Aggregator.DataAccess.Entities.ABSEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aggregator.DataAccess.Configurations.ABSEntities;

public class AccountsConfiguration : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        builder.ToTable("Accounts", "dbo", t => t.ExcludeFromMigrations());
        
        builder.HasKey(x => new { x.AccountNo, x.CurrencyID })
               .HasName("PK_Accounts");

        builder.Property(x => x.AccountNo)
            .HasColumnType("nvarchar(50)")
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(x => x.CurrencyID)
            .IsRequired()
            .HasDefaultValue(417);

        builder.Property(x => x.CustomerID)
            .IsRequired();

        builder.Property(x => x.OfficeID)
            .IsRequired();

        builder.Property(x => x.BalanceGroup)
            .HasColumnType("char(5)")
            .IsRequired();

        builder.Property(x => x.AccountName)
            .HasColumnType("nvarchar(255)")
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(x => x.OpenDate)
            .HasColumnType("date")
            .IsRequired();

        builder.Property(x => x.EndDate)
            .HasColumnType("date");

        builder.Property(x => x.CloseDate)
            .HasColumnType("date");

        builder.Property(x => x.AccountTypeID)
            .IsRequired();

        builder.Property(x => x.CurrentBalance)
            .HasColumnType("numeric(15,2)")
            .HasDefaultValue(0m)
            .IsRequired();

        builder.Property(x => x.CurrentNationalBalance)
            .HasColumnType("numeric(15,2)")
            .HasDefaultValue(0m)
            .IsRequired();

        builder.Property(x => x.Codename)
            .HasColumnType("nvarchar(100)")
            .HasMaxLength(100);

        builder.Property(x => x.AccountGroup)
            .HasColumnType("nvarchar(9)")
            .HasMaxLength(9)
            .IsRequired();

        builder.Property(x => x.DtSumV)
            .HasColumnType("numeric(15,2)")
            .HasDefaultValue(0m)
            .IsRequired();

        builder.Property(x => x.DtSumN)
            .HasColumnType("numeric(15,2)")
            .HasDefaultValue(0m)
            .IsRequired();

        builder.Property(x => x.CtSumV)
            .HasColumnType("numeric(15,2)")
            .HasDefaultValue(0m)
            .IsRequired();

        builder.Property(x => x.CtSumN)
            .HasColumnType("numeric(15,2)")
            .HasDefaultValue(0m)
            .IsRequired();
    }
}