using Aggregator.DataAccess.Entities.OwiUserAction;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aggregator.DataAccess.Configurations.NotificationDetails;

public class OwiUserActionDetailsConfiguration : IEntityTypeConfiguration<OwiUserActionDetails>
{
    public void Configure(EntityTypeBuilder<OwiUserActionDetails> builder)
    {
        builder.ToTable("OwiUserActionDetails");

        builder.HasKey(x => x.OwiUserActionDetailsId);
        
        builder.Property(x=>x.OwiUserActionDetailsId).ValueGeneratedOnAdd();
        builder.Property(x=>x.TransactionTime).IsRequired();
        builder.Property(x=>x.Login).IsRequired();
        builder.Property(x=>x.Action).IsRequired();
    }
}