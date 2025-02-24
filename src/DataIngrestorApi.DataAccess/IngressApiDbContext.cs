using DataIngrestorApi.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataIngrestorApi.DataAccess;

public class IngressApiDbContext : DbContext
{
    
    public DbSet<InboxMessage> InboxMessages { get; set; }

    public IngressApiDbContext(DbContextOptions<IngressApiDbContext> options)
        : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("nepc");
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(IngressApiDbContext).Assembly);
    }
    
}