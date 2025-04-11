using Aggregator.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace NotificationService.DataAccess;

/// <summary>
/// Контекст базы данных для сервиса уведомлений.
/// Содержит настройку сущности <see cref="NotificationMessage"/> и указывает схему по умолчанию.
/// </summary>
public class NotificationServiceDbContext(DbContextOptions<NotificationServiceDbContext> options) : DbContext(options)
{
    /// <summary>
    /// Набор данных, представляющий таблицу уведомлений.
    /// </summary>
    public DbSet<NotificationMessage> NotificationMessages { get; set; }

    /// <summary>
    /// Конфигурация модели сущностей при создании модели контекста.
    /// </summary>
    /// <param name="modelBuilder">Построитель модели.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("nepc");
        
        modelBuilder.Entity<NotificationMessage>()
            .ToTable("NotificationMessages", "nepc", t => t.ExcludeFromMigrations());
    }
}