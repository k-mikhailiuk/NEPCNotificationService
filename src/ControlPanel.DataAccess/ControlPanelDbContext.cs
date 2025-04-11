using ControlPanel.DataAccess.Configurations;
using ControlPanel.DataAccess.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ControlPanel.DataAccess;

/// <summary>
/// Контекст базы данных для приложения ControlPanel.
/// </summary>
/// <remarks>
/// Наследуется от <see cref="IdentityDbContext{IdentityUser}"/> для интеграции механизмов Identity.
/// </remarks>
public class ControlPanelDbContext(DbContextOptions<ControlPanelDbContext> options)
    : IdentityDbContext<IdentityUser>(options)
{
    /// <summary>
    /// Таблица ключевых слов сообщений уведомлений.
    /// </summary>
    public DbSet<NotificationMessageKeyWord> NotificationMessageKeyWords { get; set; }
    
    /// <summary>
    /// Таблица справочника текстов сообщений уведомлений.
    /// </summary>
    public DbSet<NotificationMessageTextDirectory> NotificationMessageTextDirectories { get; set; }
    
    /// <summary>
    /// Таблица валют.
    /// </summary>
    public DbSet<Currency> Currencies { get; set; }
    
    /// <summary>
    /// Таблица справочника описаний лимитов.
    /// </summary>
    public DbSet<LimitIdDescriptionDirectory> LimitIdDescriptionDirectories { get; set; }

    /// <summary>
    /// Настраивает модель базы данных.
    /// </summary>
    /// <param name="modelBuilder">Объект для построения модели.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("nepc");
        
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.ApplyConfiguration(new NotificationMessageKeyWordConfiguration());
        modelBuilder.ApplyConfiguration(new NotificationMessageTextDirectoryConfiguration());
        modelBuilder.ApplyConfiguration(new CurrencyConfiguration());
        modelBuilder.ApplyConfiguration(new LimitIdDescriptionDirectoryConfiguration());
    }
}