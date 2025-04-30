using Aggregator.DataAccess.Entities.ABSEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aggregator.DataAccess.Configurations.ABSEntities;

public class OfficesConfiguration : IEntityTypeConfiguration<Office>
{
    public void Configure(EntityTypeBuilder<Office> builder)
    {
            builder.ToTable("Office", "Cards", t=>t.ExcludeFromMigrations());

            builder.HasKey(x => x.DeviceCode)
                   .HasName("PK_Cards_Offices");

            builder.Property(x => x.DeviceCode)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(x => x.MainOfficeID)
                .IsRequired();

            builder.Property(x => x.DeviceTypeID)
                .HasColumnName("IDDevice")
                .HasDefaultValue(1)
                .IsRequired();

            builder.Property(x => x.Adress)
                .HasMaxLength(200);

            builder.Property(x => x.NameDevice)
                .HasMaxLength(100);

            builder.Property(x => x.AccountNoTransit)
                .HasMaxLength(50);

            builder.Property(x => x.AccountNoIncome)
                .HasMaxLength(50);

            builder.Property(x => x.OfficeIDIn);

            builder.Property(x => x.CityID)
                .IsRequired();

            builder.Property(x => x.AdditionalDeviceType);

            builder.Property(x => x.SerialNumber)
                .HasMaxLength(20);

            builder.Property(x => x.Latitude);
            builder.Property(x => x.Longitude);

            builder.Property(x => x.CustomProcess);

            builder.Property(x => x.MccCode)
                .HasMaxLength(20);

            builder.Property(x => x.Comission)
                .HasColumnType("numeric(15,2)")
                .HasDefaultValue(0m)
                .IsRequired();

            builder.Property(x => x.TotalComission)
                .HasColumnType("numeric(15,2)")
                .HasDefaultValue(0m)
                .IsRequired();

            builder.Property(x => x.FriendCommission)
                .HasColumnType("numeric(15,2)")
                .HasDefaultValue(0m)
                .IsRequired();

            builder.Property(x => x.CashBack)
                .HasColumnType("numeric(15,2)")
                .IsRequired();

            builder.Property(x => x.OperationMonthPlan)
                .HasColumnType("numeric(15,2)")
                .IsRequired();

            builder.Property(x => x.ComisionType)
                .IsRequired();

            builder.Property(x => x.InsuranceСoverageTypeID);
            builder.Property(x => x.DeviceRampTypeID);
            builder.Property(x => x.DeviceInstallationTypeID);
            builder.Property(x => x.DeviceLocationTypeID);
            builder.Property(x => x.DeviceTouchTypingTypeID);
            builder.Property(x => x.DeviceAudioOutputTypeID);

            builder.Property(x => x.InstallationDate)
                .HasColumnType("date");

            builder.Property(x => x.StartTime)
                .HasColumnType("time");

            builder.Property(x => x.FinishTime)
                .HasColumnType("time");

            builder.Property(x => x.DeviceProducerID);
            builder.Property(x => x.OperationSystemID);
            builder.Property(x => x.DataProcessingMethodID);
            builder.Property(x => x.DeviceModelID);
            builder.Property(x => x.DeviceAffiliationTypeID);

            builder.Property(x => x.Location)
                .HasMaxLength(150);

            builder.Property(x => x.Owner)
                .HasMaxLength(150);

            builder.Property(x => x.CloseDate)
                .HasColumnType("date");

            builder.Property(x => x.RBODeviceTypeID);
    }
}