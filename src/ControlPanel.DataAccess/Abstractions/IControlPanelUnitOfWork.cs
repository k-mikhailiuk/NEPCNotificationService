using ControlPanel.DataAccess.Abstractions.Repositories;

namespace ControlPanel.DataAccess.Abstractions;

/// <summary>
/// Интерфейс единицы работы (Unit of Work) для управления репозиториями.
/// </summary>
/// <remarks>
/// Объединяет несколько репозиториев и предоставляет метод сохранения изменений.
/// </remarks>
public interface IControlPanelUnitOfWork : IDisposable
{
    /// <summary>
    /// Репозиторий ключевых слов сообщений уведомлений.
    /// </summary>
    INotificationMessageKeyWordsRepository NotificationMessageKeyWords { get; }
    
    /// <summary>
    /// Репозиторий справочника текстов сообщений уведомлений.
    /// </summary>
    INotificationMessageTextDirectoriesRepository NotificationMessageTextDirectories { get; }
    
    /// <summary>
    /// Репозиторий валют.
    /// </summary>
    ICurrenciesRepository Currencies { get; }
    
    /// <summary>
    /// Репозиторий справочника описаний лимитов.
    /// </summary>
    ILimitIdDescriptionDirectoriesRepository LimitIdDescriptionDirectories { get; }
    
    /// <summary>
    /// Сохранение изменений в базе данных.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Количество сохранённых объектов.</returns>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}