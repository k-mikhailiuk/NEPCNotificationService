using Aggregator.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace NotificationService.DataAccess;

public class NotificationServiceDbContext : DbContext
{
    public DbSet<NotificationMessage> NotificationMessages { get; set; }
    
    public NotificationServiceDbContext(DbContextOptions<NotificationServiceDbContext> options) : base(options)
    {

    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("nepc");
        
        modelBuilder.Entity<NotificationMessage>()
            .ToTable("NotificationMessages", "nepc", t => t.ExcludeFromMigrations());
    }
}