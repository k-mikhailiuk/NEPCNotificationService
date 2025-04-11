namespace NotificationService.DataAccess.Abstractions;

/// <summary>
/// Интерфейс единицы работы (Unit of Work) для управления транзакциями и доступом к репозиториям.
/// </summary>
public interface IUnitOfWork : IDisposable
{
    /// <summary>
    /// Репозиторий для работы с уведомлениями.
    /// </summary>
    INotificationMessagesRepository NotificationMessages { get; }
    
    /// <summary>
    /// Асинхронно сохраняет все изменения, сделанные в контексте текущей единицы работы.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Количество затронутых записей в базе данных.</returns>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
