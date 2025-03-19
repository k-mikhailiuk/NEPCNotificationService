using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ControlPanel.DataAccess;

public class ControlPanelDbContext : IdentityDbContext<IdentityUser>
{
    public ControlPanelDbContext(DbContextOptions<ControlPanelDbContext> options)
        : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("nepc");
        
        base.OnModelCreating(modelBuilder);
    }
}