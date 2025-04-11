using DataIngrestorApi.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataIngrestorApi.DataAccess;

/// <summary>
/// Контекст базы данных для Ingress API.
/// Отвечает за конфигурацию подключения и работу с сущностями.
/// </summary>
public class IngressApiDbContext(DbContextOptions<IngressApiDbContext> options) : DbContext(options)
{
    /// <summary>
    /// Набор сущностей, представляющих входящие сообщения.
    /// </summary>
    public DbSet<InboxMessage> InboxMessages { get; set; }

    /// <summary>
    /// Настраивает модель при создании.
    /// Устанавливает схему по умолчанию и применяет все конфигурации из текущей сборки.
    /// </summary>
    /// <param name="modelBuilder">Построитель модели, используемый для настройки сущностей и связей.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("nepc");
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(IngressApiDbContext).Assembly);
    }
    
}