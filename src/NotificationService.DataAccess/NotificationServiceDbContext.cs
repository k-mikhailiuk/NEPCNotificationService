using Microsoft.EntityFrameworkCore;

namespace NotificationService.DataAccess;

public class NotificationServiceDbContext : DbContext
{
    public NotificationServiceDbContext(DbContextOptions<NotificationServiceDbContext> options) : base(options)
    {

    }
}