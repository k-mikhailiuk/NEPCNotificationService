using ControlPanel.DataAccess.Configurations;
using ControlPanel.DataAccess.Entites;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ControlPanel.DataAccess;

public class ControlPanelDbContext : IdentityDbContext<IdentityUser>
{
    public DbSet<NotificationMessageKeyWord> NotificationMessageKeyWords { get; set; }
    public DbSet<NotificationMessageTextDirectory> NotificationMessageTextDirectories { get; set; }
    
    public ControlPanelDbContext(DbContextOptions<ControlPanelDbContext> options)
        : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("nepc");
        
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.ApplyConfiguration(new NotificationMessageKeyWordConfiguration());
        modelBuilder.ApplyConfiguration(new NotificationMessageTextDirectoryConfiguration());
    }
}